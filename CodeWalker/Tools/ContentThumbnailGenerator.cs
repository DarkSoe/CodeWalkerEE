using CodeWalker.GameFiles;
using CodeWalker.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;

namespace CodeWalker.Tools
{
    public partial class ContentThumbnailGenerator : Form
    {
        Thread tGenerateThread;
        GameFileCache tGameFileCache;
        Archetype tCurrentArchetype = null;
        bool tUpdateArchetypeStatus = true;
        OffscreenRenderer tOffscreenRenderer = null;
        bool tStop = false;

        int tCurrentIndex = 0;
        List<ContentPropItem> tList = new List<ContentPropItem>();

        public ContentThumbnailGenerator()
        {
            InitializeComponent();
            btn_generateButton.Text = "Generate";

            tList.Clear();

            MainForm tMainForm = Application.OpenForms.OfType<MainForm>().FirstOrDefault();
            tGameFileCache = tMainForm.tViewport.GameFileCache;

            tOffscreenRenderer = new OffscreenRenderer();
            tOffscreenRenderer.TopLevel = false;
            tOffscreenRenderer.FormBorderStyle = FormBorderStyle.None;
            tOffscreenRenderer.ClientSize = new Size(350, 350);
            tOffscreenRenderer.Location = new Point(0, 0);
            tOffscreenRenderer.Visible = true;
            tOffscreenRenderer.Show();
            tOffscreenRenderer.StatusReady += ReadyForNext;

            panel_Renderer.Controls.Add(tOffscreenRenderer);
        }

        public Point GetRendererLocation()
        {
            return panel_Renderer.PointToScreen(Point.Empty);
        }

        private void btn_generateButton_Click(object sender, EventArgs e)
        {
            if (tGenerateThread == null)
            {
                btn_generateButton.Text = "Stop";
                tStop = false;
                tGenerateThread = new Thread(new ThreadStart(Thread_GenerateThumbnails));
                tGenerateThread.Start();
            }
            else
            {
                btn_generateButton.Text = "Generate";

                if (tGenerateThread.ThreadState != ThreadState.Running)
                    tGenerateThread.Abort();

                tStop = true;
            }
        }

        public void Thread_GenerateThumbnails()
        {
            byte[] data = null;
            string name = "";
            string path = "";
            string extension = "";

            foreach (var kvp in tGameFileCache.YdrDict)
            {
                var hash = kvp.Key;
                var entry = kvp.Value;

                var ydr = tGameFileCache.GetYdr(hash);
                if (ydr == null) continue;

                string FileName = ydr.Name;
                RpfFileEntry tRpfFileEntry = ydr.RpfFileEntry;
                MetaHash tModelHash = ydr.RpfFileEntry?.ShortNameHash ?? 0;
                Archetype tModelArchetype = null;

                if (tModelHash != 0)
                {
                    tModelArchetype = TryGetArchetype(tModelHash);
                }

                string tGtaPath = GTAFolder.GetCurrentGTAFolderWithTrailingSlash();
                string tFullFilePath = tGtaPath + tRpfFileEntry.Path;

                if (tModelArchetype != null)
                {
                    ContentPropItem tProp = new ContentPropItem(entry.Name, ydr);
                    tProp.Archetype = tModelArchetype;
                    tProp.FilePath = tRpfFileEntry.Path;

                    tList.Add(tProp);
                }
            }

            tCurrentIndex = 0;
            var tCurrProp = tList[tCurrentIndex];

            tOffscreenRenderer.FilePath = tCurrProp.YdrFile.FilePath;
            tOffscreenRenderer.SaveFilePath = tCurrProp.ThumbnailPath;
            tOffscreenRenderer.tPauseRendering = false;
            tOffscreenRenderer.ViewModel(tCurrProp);
        }

        private Archetype TryGetArchetype(uint hash)
        {
            if ((tGameFileCache == null) || (!tGameFileCache.IsInited)) return null;

            var arch = tGameFileCache.GetArchetype(hash);

            if ((arch != null) && (arch != tCurrentArchetype) && (tUpdateArchetypeStatus))
            {
                tCurrentArchetype = arch;
                tUpdateArchetypeStatus = false;
            }

            return arch;
        }

        private RpfFileEntry CreateFileEntry(string name, string path, ref byte[] data)
        {
            RpfFileEntry e = null;
            uint rsc7 = (data?.Length > 4) ? BitConverter.ToUInt32(data, 0) : 0;
            if (rsc7 == 0x37435352) //RSC7 header present! create RpfResourceFileEntry and decompress data...
            {
                e = RpfFile.CreateResourceFileEntry(ref data, 0);
                data = ResourceBuilder.Decompress(data);
            }
            else
            {
                var be = new RpfBinaryFileEntry();
                be.FileSize = (uint)data?.Length;
                be.FileUncompressedSize = be.FileSize;
                e = be;
            }
            e.Name = name;
            e.NameLower = name?.ToLowerInvariant();
            e.NameHash = JenkHash.GenHash(e.NameLower);
            e.ShortNameHash = JenkHash.GenHash(Path.GetFileNameWithoutExtension(e.NameLower));
            e.Path = path;
            return e;
        }

        public void ReadyForNext()
        {
            if (tStop)
                return;

            tCurrentIndex = tCurrentIndex + 1;

            if (tList.Count < tCurrentIndex - 1)
                return;

            var tCurrProp = tList[tCurrentIndex];

            if (tCurrProp != null && !File.Exists(tCurrProp.ThumbnailPath))
            {
                tOffscreenRenderer.FilePath = tCurrProp.YdrFile.FilePath;
                tOffscreenRenderer.SaveFilePath = tCurrProp.ThumbnailPath;
                tOffscreenRenderer.tPauseRendering = false;
                tOffscreenRenderer.ViewModel(tCurrProp, true);
            }
            else
            {
                ReadyForNext();
            }
        }
    }
}
