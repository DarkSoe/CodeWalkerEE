using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeWalker.Tools
{
    public partial class RagePackForm : Form
    {
        private readonly HashSet<string> MappingExtensions = new HashSet<string> { ".ymap", ".ymf", ".ytyp", ".ydr", ".ytd", ".ybn" };

        public RagePackForm()
        {
            InitializeComponent();
        }

        private void btn_inSelect_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Please select the folder containing the Content.";
                fbd.ShowNewFolderButton = true;

                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string folderPath = fbd.SelectedPath;
                    text_contentInFolderPath.Text = folderPath;
                }
            }
        }

        private void btn_outSelect_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Please select the folder to output the dlc.rpf";
                fbd.ShowNewFolderButton = true;

                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string folderPath = fbd.SelectedPath;
                    text_ContentOutFolderPath.Text = folderPath;
                }
            }
        }

        private async void btn_packdlc_Click(object sender, EventArgs e)
        {
            string inputPath = text_contentInFolderPath.Text;
            string outputPath = text_ContentOutFolderPath.Text;
            string gtaUtilPath = AppDomain.CurrentDomain.BaseDirectory + @"\thirdparty\GTAUtil.exe";

            if (string.IsNullOrWhiteSpace(inputPath) || string.IsNullOrWhiteSpace(outputPath))
            {
                MessageBox.Show("Please select in and output folders.");
                return;
            }

            btn_packdlc.Enabled = false;
            text_output.Clear();

            await BuildAllMappingDlcsAsync(inputPath, outputPath, gtaUtilPath);

            btn_packdlc.Enabled = true;
        }

        private async Task BuildAllMappingDlcsAsync(string inputRoot, string outputRoot, string gtaUtilPath)
        {
            if (!Directory.Exists(inputRoot))
            {
                AppendLog($"Input root does not exist: {inputRoot}");
                return;
            }

            foreach (var folder in Directory.GetDirectories(inputRoot))
            {
                string folderName = Path.GetFileName(folder);
                string mappingBaseLower = SanitizeIdentifier(folderName);
                string mappingBaseUpper = ToUpperSnakeCase(folderName);
                string dlcLower = $"dlc_{mappingBaseLower}";
                string dlcUpper = $"dlc_{mappingBaseUpper}";
                string fixedLevelHash = "MO_JIM_L11";

                AppendLog($"--- Processing Mapping: {folderName} ---");

                // 1️. DLC Output Folder
                string dlcOutput = Path.Combine(outputRoot, dlcLower);
                Directory.CreateDirectory(dlcOutput);

                // 2️. Temp Input für einzelne Mapping-RPF
                string tempRpfInput = Path.Combine(dlcOutput, $"temp_{mappingBaseLower}_rpf_input");
                Directory.CreateDirectory(tempRpfInput);

                // 3️. Alle Mapping-Dateien kopieren
                foreach (var file in Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories)
                             .Where(f => MappingExtensions.Contains(Path.GetExtension(f).ToLower())))
                {
                    string dest = Path.Combine(tempRpfInput, Path.GetFileName(file));
                    File.Copy(file, dest, true);
                }

                AppendLog($"Copied mapping files to {tempRpfInput}");

                // 4️. Einzelne Mapping-RPF bauen
                string mappingRpfPath = Path.Combine(dlcOutput, $"{mappingBaseLower}.rpf");
                await RunGtaUtilWithTempInputAsync(gtaUtilPath, tempRpfInput, dlcOutput, mappingBaseLower);

                // 5️. Temp Input löschen
                try { Directory.Delete(tempRpfInput, true); } catch { }

                // 6️. Final DLC Struktur
                string finalTempInput = Path.Combine(dlcOutput, "temp_final_dlc_input");
                string platformDir = Path.Combine(finalTempInput, "x64");
                Directory.CreateDirectory(platformDir);

                // Verschiebe Mapping-RPF ins x64-Verzeichnis
                string finalMappingRpf = Path.Combine(platformDir, $"{mappingBaseLower}.rpf");
                File.Move(mappingRpfPath, finalMappingRpf);

                // 7️. XML Dateien generieren
                File.WriteAllText(Path.Combine(finalTempInput, "content.xml"),
                    GenerateMappingContentXml(dlcLower, dlcUpper, mappingBaseLower, mappingBaseUpper, fixedLevelHash));
                File.WriteAllText(Path.Combine(finalTempInput, "setup2.xml"),
                    GenerateMappingSetup2Xml(dlcUpper, mappingBaseLower, mappingBaseUpper));

                AppendLog("Generated content.xml and setup2.xml");

                // 8️. Final DLC-RPF bauen
                await RunGtaUtilWithTempInputAsync(gtaUtilPath, finalTempInput, dlcOutput, "dlc");

                // 9️. Temp Input löschen
                try { Directory.Delete(finalTempInput, true); } catch { }

                AppendLog($"Mapping DLC for {folderName} created at {dlcOutput}");
            }

            AppendLog("All mapping DLCs processed.");
        }

        private async Task RunGtaUtilWithTempInputAsync(string gtaUtilPath, string inputPath, string outputPath, string outputName)
        {
            var tcs = new TaskCompletionSource<bool>();
            var psi = new ProcessStartInfo
            {
                FileName = gtaUtilPath,
                Arguments = $"createarchive --input \"{inputPath}\" --output \"{outputPath}\" --gtapath \"{CodeWalker.GTAFolder.CurrentGTAFolder}\" --name \"{outputName}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = Path.GetDirectoryName(gtaUtilPath)
            };

            using (var process = new Process { StartInfo = psi, EnableRaisingEvents = true })
            {
                process.OutputDataReceived += (s, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                        AppendLog(e.Data);
                };

                process.ErrorDataReceived += (s, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                        AppendLog("[ERROR] " + e.Data);
                };

                process.Exited += (s, e) => tcs.TrySetResult(true);

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                await tcs.Task;
            }
        }

        /*private async Task RunGtaUtilAsync(string gtaUtilPath, string inputPath, string outputPath)
        {
            string tempInput = Path.Combine(outputPath, "gtautil_temp_input");
            if (Directory.Exists(tempInput)) Directory.Delete(tempInput, true);
            Directory.CreateDirectory(tempInput);

            foreach (var file in Directory.GetFiles(inputPath))
            {
                string dest = Path.Combine(tempInput, Path.GetFileName(file));
                File.Copy(file, dest);
            }

            var tcs = new TaskCompletionSource<bool>();
            string gtavPath = CodeWalker.GTAFolder.CurrentGTAFolder;

            var psi = new ProcessStartInfo
            {
                FileName = gtaUtilPath,
                Arguments = $"createarchive --input \"{tempInput}\" --output \"{outputPath}\" --gtapath \"{CodeWalker.GTAFolder.CurrentGTAFolder}\" --name dlc",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = Path.GetDirectoryName(gtaUtilPath)
            };

            var process = new Process { StartInfo = psi, EnableRaisingEvents = true };

            process.OutputDataReceived += (s, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                    AppendLog(e.Data);
            };

            process.ErrorDataReceived += (s, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                    AppendLog("[ERROR] " + e.Data);
            };

            process.Exited += (s, e) => { tcs.TrySetResult(true); };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            await tcs.Task;
        }*/

        private void AppendLog(string text)
        {
            if (text_output.InvokeRequired)
            {
                text_output.Invoke(new Action(() =>
                {
                    text_output.AppendText(text + Environment.NewLine);
                    text_output.SelectionStart = text_output.Text.Length;
                    text_output.ScrollToCaret();
                }));
            }
            else
            {
                text_output.AppendText(text + Environment.NewLine);
                text_output.SelectionStart = text_output.Text.Length;
                text_output.ScrollToCaret();
            }
        }

        private string SanitizeIdentifier(string name) => new string(name.ToLower().Where(char.IsLetterOrDigit).ToArray());
        private string ToUpperSnakeCase(string name) => new string(name.ToUpper().Where(char.IsLetterOrDigit).ToArray());

        private string GenerateMappingContentXml(
    string dlcNameLower,
    string dlcNameUpper,
    string mappingBaseNameLower,
    string mappingBaseNameUpper,
    string levelNameHash)
        {
            const string template = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<CDataFileMgr__ContentsOfDataFileXml>
    <disabledFiles />
    <includedXmlFiles />
    <includedDataFiles />
    <dataFiles>
        <Item>
            <filename>DLC_NAME_LOWER:/%PLATFORM%/MAPPING_BASE_NAME_LOWER.rpf</filename>
            <fileType>RPF_FILE</fileType>
            <locked value=""true""/>
            <disabled value=""true""/>
            <persistent value=""true""/>
            <overlay value=""true""/>
        </Item>
    </dataFiles>
    <contentChangeSets>
        <Item>
            <changeSetName>MAPPING_BASE_NAME_UPPER_STARTUP</changeSetName>
            <filesToEnable>
                <!-- NULL -->
            </filesToEnable>
        </Item>
        <Item>
            <changeSetName>MAPPING_BASE_NAME_UPPER_STREAMING</changeSetName>
            <filesToEnable>
                <Item>DLC_NAME_UPPER:/%PLATFORM%/MAPPING_BASE_NAME_LOWER.rpf</Item>
            </filesToEnable>
            <executionConditions>
                <activeChangesetConditions/>
                <genericConditions>$level=LEVEL_NAME_HASH</genericConditions>
            </executionConditions>
        </Item>
        <Item>
            <changeSetName>MAPPING_BASE_NAME_UPPER_MAP</changeSetName>
            <mapChangeSetData>
                <Item>
                    <associatedMap>LEVEL_NAME_HASH</associatedMap>
                    <filesToInvalidate/>
                    <filesToEnable>
                        <Item>DLC_NAME_UPPER:/%PLATFORM%/MAPPING_BASE_NAME_LOWER.rpf</Item>
                    </filesToEnable>
                </Item>
            </mapChangeSetData>
            <requiresLoadingScreen value=""false""/>
            <loadingScreenContext>LOADINGSCREEN_CONTEXT_LAST_FRAME</loadingScreenContext>
            <useCacheLoader value=""false""/>
        </Item>
    </contentChangeSets>
    <patchFiles/>
</CDataFileMgr__ContentsOfDataFileXml>";

            // Platzhalter ersetzen
            string content = template
                .Replace("DLC_NAME_LOWER", dlcNameLower)
                .Replace("DLC_NAME_UPPER", dlcNameUpper)
                .Replace("MAPPING_BASE_NAME_LOWER", mappingBaseNameLower)
                .Replace("MAPPING_BASE_NAME_UPPER", mappingBaseNameUpper)
                .Replace("LEVEL_NAME_HASH", levelNameHash);

            return content;
        }

        private string GenerateMappingSetup2Xml(
    string dlcNameUpper,
    string mappingBaseNameLower,
    string mappingBaseNameUpper)
        {
            const string template = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<SSetupData>
    <deviceName>DLC_NAME_UPPER</deviceName>
    <datFile>content.xml</datFile>
    <timeStamp>TIMESTAMP_PLACEHOLDER</timeStamp>
    <nameHash>MAPPING_BASE_NAME_LOWER</nameHash>
    <contentChangeSets/>
    <contentChangeSetGroups>
        <Item>
            <NameHash>GROUP_STARTUP</NameHash>
            <ContentChangeSets>
                <Item>MAPPING_BASE_NAME_UPPER_STARTUP</Item>
            </ContentChangeSets>
        </Item>
        <Item>
            <NameHash>GROUP_MAP</NameHash>
            <ContentChangeSets>
                <Item>MAPPING_BASE_NAME_UPPER_MAP</Item>
            </ContentChangeSets>
        </Item>
        <Item>
            <NameHash>GROUP_UPDATE_STREAMING</NameHash>
            <ContentChangeSets>
                <Item>MAPPING_BASE_NAME_UPPER_STREAMING</Item>
            </ContentChangeSets>
        </Item>
    </contentChangeSetGroups>
    <startupScript/>
    <scriptCallstackSize value=""0""/>
    <type>EXTRACONTENT_COMPAT_PACK</type>
    <order value=""25""/>
    <minorOrder value=""0""/>
    <isLevelPack value=""false""/>
    <dependencyPackHash/>
    <requiredVersion/>
    <subPackCount value=""0""/>
</SSetupData>";

            // Aktuelles Datum als Timestamp einfügen
            string timestamp = DateTime.Now.ToString("M/d/yyyy h:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);

            // Platzhalter ersetzen
            string content = template
                .Replace("DLC_NAME_UPPER", dlcNameUpper)
                .Replace("MAPPING_BASE_NAME_LOWER", mappingBaseNameLower)
                .Replace("MAPPING_BASE_NAME_UPPER", mappingBaseNameUpper)
                .Replace("TIMESTAMP_PLACEHOLDER", timestamp);

            return content;
        }

    }
}
