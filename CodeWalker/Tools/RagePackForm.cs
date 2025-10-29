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

            await RunGtaUtilAsync(gtaUtilPath, inputPath, outputPath);

            btn_packdlc.Enabled = true;
        }

        private async Task RunGtaUtilAsync(string gtaUtilPath, string inputPath, string outputPath)
        {
            var tcs = new TaskCompletionSource<bool>();
            string gtavPath = CodeWalker.GTAFolder.CurrentGTAFolder;

            var psi = new ProcessStartInfo
            {
                FileName = gtaUtilPath,
                Arguments = $"createarchive --input \"{inputPath}\" --output \"{outputPath}\" --name dlc",
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

            process.Exited += (s, e) =>
            {
                AppendLog($"Process exited with code {process.ExitCode}");
                tcs.TrySetResult(true);
            };

            process.Start();
            process.StandardInput.WriteLine(CodeWalker.GTAFolder.CurrentGTAFolder);
            process.StandardInput.BaseStream.Flush();
            process.StandardInput.BaseStream.Close();
            process.CloseMainWindow();

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            await tcs.Task;
            AppendLog("Finished");
        }

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
    }
}
