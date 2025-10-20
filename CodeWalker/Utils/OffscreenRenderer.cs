using CodeWalker.Forms;
using CodeWalker.GameFiles;
using CodeWalker.Properties;
using CodeWalker.Rendering;
using CodeWalker.World;
using SharpDX;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using static CodeWalker.Project.Panels.GenerateLODLightsPanel;

namespace CodeWalker.Utils
{
    public partial class OffscreenRenderer : Form, DXForm
    {
        Thread tThumbnailThread;

        volatile bool tFormOpen = false;
        volatile bool tPauseRendering = false;

        private Renderer tRenderer = null;
        Vector3 tRrevWorldPos = new Vector3(0, 0, 0);
        bool tInitedOk = false;
        World.Camera tCamera;
        Entity tCamEntity = new Entity();
        GameFileCache tGameFileCache;
        Archetype tCurrentArchetype = null;
        bool tUpdateArchetypeStatus = true;
        Stopwatch tFrameTimer = new Stopwatch();

        public string FilePath { get; set; }
        public string SaveFilePath { get; set; }

        YdrFile Ydr = null;
        YddFile Ydd = null;
        YftFile Yft = null;
        YbnFile Ybn = null;
        YptFile Ypt = null;
        YnvFile Ynv = null;

        MetaHash tModelHash;
        Archetype tModelArchetype = null;
        ClipMapEntry tAnimClip = null;
        RpfFileEntry tRpfFileEntry = null;

        public Skeleton tSkeleton = null;

        private string fileName;
        public string FileName
        {
            get { return fileName; }
            set
            {
                fileName = value;
            }
        }

        public Form Form { get { return this; } }

        public OffscreenRenderer()
        {
            InitializeComponent();

            MainForm tMainForm = Application.OpenForms.OfType<MainForm>().FirstOrDefault();
            tGameFileCache = tMainForm.tViewport.GameFileCache;

            tRenderer = new Renderer(this, tGameFileCache);
            tCamera = tRenderer.camera;
            tInitedOk = tRenderer.Init();

            tRenderer.controllightdir = !Settings.Default.Skydome;
            tRenderer.rendercollisionmeshes = false;
            tRenderer.renderclouds = false;
            tRenderer.rendermoon = false;
            tRenderer.renderskeletons = false;
            tRenderer.renderfragwindows = false;
            tRenderer.SelectionFlagsTestAll = true;

            tThumbnailThread = new Thread(new ThreadStart(Thread_CheckForRenderProp));
        }

        public void BuffersResized(int w, int h)
        {
            tRenderer.BuffersResized(w, h);
        }

        public void CleanupScene()
        {
            tRenderer.DeviceDestroyed();
        }

        public bool ConfirmQuit()
        {
            return true;
        }

        public void InitScene(Device device)
        {
            int width = ClientSize.Width;
            int height = ClientSize.Height;

            try
            {
                tRenderer.DeviceCreated(device, width, height);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading shaders!\n" + ex.ToString());
                return;
            }

            tCamera.FollowEntity = tCamEntity;
            tCamera.FollowEntity.Position = tRrevWorldPos;
            tCamera.FollowEntity.Orientation = SharpDX.Quaternion.LookAtLH(Vector3.Zero, Vector3.Up, Vector3.ForwardLH);
            tCamera.TargetDistance = 2.0f;
            tCamera.CurrentDistance = 2.0f;
            tCamera.TargetRotation.Y = 0.2f;
            tCamera.CurrentRotation.Y = 0.2f;
            tCamera.TargetRotation.X = 0.5f * (float)Math.PI;
            tCamera.CurrentRotation.X = 0.5f * (float)Math.PI;

            tRenderer.shaders.deferred = false;

            LoadSettings();

            tFormOpen = true;
            new Thread(new ThreadStart(ContentThread)).Start();
            tFrameTimer.Start();
        }

        public void RenderScene(DeviceContext context)
        {
            float elapsed = (float)tFrameTimer.Elapsed.TotalSeconds;
            tFrameTimer.Restart();

            if (tPauseRendering) return;

            if (!Monitor.TryEnter(tRenderer.RenderSyncRoot, 50))
            { return; }

            tRenderer.Update(elapsed, 0, 0);
            tRenderer.BeginRender(context);
            RenderSingleItem();
            tRenderer.RenderQueued();
            tRenderer.RenderSelectionGeometry(MapSelectionMode.Entity);
            tRenderer.RenderFinalPass();
            tRenderer.EndRender();
            tRenderer.RenderedDrawablesListEnable = true;

            Monitor.Exit(tRenderer.RenderSyncRoot);
        }

        private void LoadSettings()
        {
            var s = Settings.Default;
        }

        private void ContentThread()
        {
            UpdateStatus("Ready");

            while (tFormOpen && !IsDisposed)
            {
                bool rcItemsPending = tRenderer.ContentThreadProc();

                if (!(rcItemsPending))
                {
                    Thread.Sleep(1);
                }
            }
        }

        private void UpdateStatus(string text) { }

        private void RenderSingleItem()
        {
            if (Ydr != null)
            {
                if (Ydr.Loaded)
                {
                    if (tModelArchetype == null) tModelArchetype = TryGetArchetype(tModelHash);

                    tRenderer.RenderDrawable(Ydr.Drawable, tModelArchetype, null, tModelHash, null, null, tAnimClip);
                }
            }
        }

        private Archetype TryGetArchetype(uint hash)
        {
            if ((tGameFileCache == null) || (!tGameFileCache.IsInited)) return null;

            var arch = tGameFileCache.GetArchetype(hash);

            if ((arch != null) && (arch != tCurrentArchetype) && (tUpdateArchetypeStatus))
            {
                UpdateStatus("Archetype: " + arch.Name.ToString());
                tCurrentArchetype = arch;
                tUpdateArchetypeStatus = false;
            }

            return arch;
        }

        public void LoadModel(YdrFile ydr)
        {
            if (ydr == null) return;

            FileName = ydr.Name;
            Ydr = ydr;
            tRpfFileEntry = Ydr.RpfFileEntry;
            tModelHash = Ydr.RpfFileEntry?.ShortNameHash ?? 0;
            if (tModelHash != 0)
            {
                tModelArchetype = TryGetArchetype(tModelHash);
            }

            if (ydr.Drawable != null)
            {
                var cen = ydr.Drawable.BoundingCenter;
                var rad = ydr.Drawable.BoundingSphereRadius;
                if (tModelArchetype != null)
                {
                    cen = tModelArchetype.BSCenter;
                    rad = tModelArchetype.BSRadius;
                }

                MoveCameraToView(cen, rad);

                tSkeleton = ydr.Drawable.Skeleton;
            }

            //tThumbnailThread.Start();
        }

        private void MoveCameraToView(Vector3 pos, float rad)
        {
            rad = Math.Max(0.01f, rad);

            tCamera.FollowEntity.Position = pos;
            tCamera.TargetDistance = rad * 1.6f;
            tCamera.CurrentDistance = rad * 1.6f;

            tCamera.UpdateProj = true;
        }

        public System.Drawing.Bitmap GetFormAsImage(int width, int height)
        {
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(width, height);

            this.Invoke((MethodInvoker)(() =>
            {
                this.DrawToBitmap(bmp, new System.Drawing.Rectangle(System.Drawing.Point.Empty, this.Size));
            }));

            return bmp;
        }

        private void OffscreenRenderer_Load(object sender, EventArgs e)
        {
            tRenderer.Start();
        }

        public void Thread_CheckForRenderProp()
        {
            while (true)
            {
                if (tRenderer.RenderedDrawables.Count >= 1)
                {
                    Bitmap tBmp = CaptureWindowBitmap();
                    SaveThumbnailAsJpeg(tBmp, SaveFilePath);

                    break;
                }
                else
                {
                    Thread.Sleep(500);
                }
            }
        }

        private void SaveThumbnailAsJpeg(Bitmap bmp, string path, long quality = 85L)
        {
            var codec = ImageCodecInfo.GetImageEncoders()
                .FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);

            if (codec != null)
            {
                var encoder = new EncoderParameters(1);
                encoder.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);

                bmp.Save(path, codec, encoder);
            }
            else
            {
                bmp.Save(path, ImageFormat.Jpeg);
            }

            this.Invoke((MethodInvoker)(() =>
            {
                this.Close();
            }));
        }

        public Bitmap CaptureWindowBitmap()
        {
            Bitmap bmp = new Bitmap(this.Size.Width, this.Size.Height, PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(this.Location, System.Drawing.Point.Empty, this.Size);
            }

            return bmp;
        }

        public void ViewModel(ContentPropItem aPropItem)
        {
            byte[] data = null;
            string name = "";
            string path = "";
            string extension = "";

            string tGtaPath = GTAFolder.GetCurrentGTAFolderWithTrailingSlash();
            string tFullFilePath = tGtaPath + aPropItem.FilePath;

            RpfFileEntry tRpfFileEntry = aPropItem.YdrFile.RpfFileEntry;
            if (tRpfFileEntry != null)
            {
                RpfFile tRpfFile = tRpfFileEntry.File;
                if (tRpfFile == null) return;

                data = tRpfFile.ExtractFile(tRpfFileEntry);
                name = new FileInfo(tFullFilePath).Name;
                path = tFullFilePath;
                extension = new FileInfo(tFullFilePath).Extension;
            }

            if (data == null) return;

            if (tRpfFileEntry == null)
            {
                //this should only happen when opening a file from filesystem...
                tRpfFileEntry = CreateFileEntry(name, path, ref data);
                extension = new FileInfo(tFullFilePath).Extension;
            }

            switch (extension)
            {
                case ".ydr":
                    var tYdr = RpfFile.GetFile<YdrFile>(tRpfFileEntry, data);
                    LoadModel(tYdr);
                    break;
            }
        }

        private RpfFileEntry CreateFileEntry(string name, string path, ref byte[] data)
        {
            //this should only really be used when loading a file from the filesystem.
            RpfFileEntry e = null;
            uint rsc7 = (data?.Length > 4) ? BitConverter.ToUInt32(data, 0) : 0;
            if (rsc7 == 0x37435352) //RSC7 header present! create RpfResourceFileEntry and decompress data...
            {
                e = RpfFile.CreateResourceFileEntry(ref data, 0);//"version" should be loadable from the header in the data..
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
    }
}
