using CodeWalker.Forms;
using CodeWalker.GameFiles;
using CodeWalker.Properties;
using CodeWalker.Rendering;
using CodeWalker.Tools;
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
using System.Runtime.InteropServices;
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
        volatile bool tRendererReady = false;
        public bool IsRendererReady => tRendererReady;
        public bool tPauseRendering = false;

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
        public bool SaveThumbnailToDisk { get; set; } = true;

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

        private Texture2D offscreenTexture;
        private RenderTargetView offscreenRTV;

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

        ContentPropItem tCurrPropItem;

        // Events
        public event Action StatusReady;
        public event Action<Bitmap> ThumbnailReady;

        private int captureGeneration = 0;
        private int activeCaptureGeneration = 0;
        private bool awaitingThumbnailCapture = false;
        private int captureReadyFrames = 0;
        private int captureWaitFrames = 0;
        private const int CaptureRequiredReadyFrames = 8;
        private const int CaptureMaxWaitFrames = 600;

        public OffscreenRenderer()
        {
            InitializeComponent();

            var tMainForm = Application.OpenForms.OfType<MainForm>().FirstOrDefault();
            tGameFileCache = tMainForm?.tViewport?.GameFileCache;

            tRenderer = new Renderer(this, tGameFileCache);
            tCamera = tRenderer.camera;
            tInitedOk = tRenderer.Init(1);
            ConfigureForThumbnailRendering();

            tRenderer.rendercollisionmeshes = false;
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
                //InitOffscreenTarget();
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
            ConfigureForThumbnailRendering();
            LoadSettings();

            tFormOpen = true;
            tRendererReady = true;
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

            tRenderer.RenderedDrawablesListEnable = true;

            tRenderer.Update(elapsed, 0, 0);
            tRenderer.BeginRender(context);
            RenderSingleItem();
            tRenderer.RenderQueued();
            tRenderer.RenderSelectionGeometry(MapSelectionMode.Entity);
            tRenderer.RenderFinalPass();
            tRenderer.EndRender();

            TryCaptureThumbnail();

            Monitor.Exit(tRenderer.RenderSyncRoot);
        }

        private void TryCaptureThumbnail()
        {
            if (!awaitingThumbnailCapture || SaveThumbnailToDisk)
                return;

            captureWaitFrames++;

            if (Ydr != null && Ydr.Loaded && tRenderer.RenderedDrawables.Count >= 1)
                captureReadyFrames++;
            else
                captureReadyFrames = 0;

            if (captureReadyFrames >= CaptureRequiredReadyFrames)
            {
                awaitingThumbnailCapture = false;
                int generation = activeCaptureGeneration;
                Bitmap bmp = CaptureBackBufferFromDevice();
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    if (generation == activeCaptureGeneration)
                        ThumbnailReady?.Invoke(bmp);
                    else
                        bmp?.Dispose();
                });
            }
            else if (captureWaitFrames >= CaptureMaxWaitFrames)
            {
                awaitingThumbnailCapture = false;
                int generation = activeCaptureGeneration;
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    if (generation == activeCaptureGeneration)
                        ThumbnailReady?.Invoke(null);
                });
            }
        }

        private void LoadSettings()
        {
            var s = Settings.Default;
        }

        private void ConfigureForThumbnailRendering()
        {
            tRenderer.DXMan?.SetClearColour(new SharpDX.Color(0x55, 0x8B, 0xAD, 255));
            tRenderer.controllightdir = true;
            tRenderer.renderskydome = false;
            tRenderer.renderclouds = false;
            tRenderer.rendermoon = false;
            tRenderer.controltimeofday = false;
            tRenderer.timerunning = false;
            tRenderer.rendernaturalambientlight = false;
            tRenderer.renderartificialambientlight = false;
            tRenderer.lightdirx = 0.75f;
            tRenderer.lightdiry = 0.35f;

            if (tRenderer.shaders != null)
            {
                tRenderer.shaders.hdr = false;
                tRenderer.shaders.deferred = false;
                tRenderer.shaders.shadows = false;
            }
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

            if (!ydr.Loaded && tGameFileCache != null)
                tGameFileCache.LoadFile(ydr);

            if (!ydr.Loaded || ydr.Drawable == null)
            {
                if (!SaveThumbnailToDisk)
                    ThumbnailReady?.Invoke(null);
                return;
            }
            FileName = ydr.Name;
            Ydr = ydr;
            tModelArchetype = null;
            tRpfFileEntry = Ydr.RpfFileEntry;
            tModelHash = Ydr.RpfFileEntry?.ShortNameHash ?? 0;
            if (tCurrPropItem?.Archetype != null)
                tModelArchetype = tCurrPropItem.Archetype;
            else if (tModelHash != 0)
                tModelArchetype = TryGetArchetype(tModelHash);
            else if (tCurrPropItem != null)
                tModelHash = JenkHash.GenHash(tCurrPropItem.GetCleanName());

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

            if (tThumbnailThread.ThreadState == System.Threading.ThreadState.Stopped)
                tThumbnailThread = new Thread(new ThreadStart(Thread_CheckForRenderProp));

            if (SaveThumbnailToDisk)
            {
                if (tThumbnailThread.ThreadState == System.Threading.ThreadState.Unstarted)
                    tThumbnailThread.Start();
            }
            else
            {
                BeginThumbnailCapture();
            }
        }

        private void BeginThumbnailCapture()
        {
            activeCaptureGeneration = captureGeneration;
            awaitingThumbnailCapture = true;
            captureReadyFrames = 0;
            captureWaitFrames = 0;
        }

        private void MoveCameraToView(Vector3 pos, float rad)
        {
            rad = Math.Max(0.01f, rad);

            tCamera.FollowEntity.Position = pos;
            tCamera.TargetDistance = rad * 2.1f; //1.6f;
            tCamera.CurrentDistance = rad * 2.1f; //1.6f;

            tCamera.TargetRotation.X = (float)(Math.PI / 4);
            tCamera.TargetRotation.Y = (float)(Math.PI / 4);

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
                    SaveThumbnailAsJpeg(tBmp, tCurrPropItem.ThumbnailPath);
                    Thread.Sleep(500);

                    StatusReady();
                    Thread.Sleep(500);
                }
                else
                {
                    Thread.Sleep(500);
                }
            }
        }

        private Bitmap CaptureBackBufferFromDevice()
        {
            var device = tRenderer.Device;
            var backbuffer = tRenderer.DXMan?.backbuffer;
            if (device == null || backbuffer == null)
                return null;

            var context = device.ImmediateContext;
            var desc = backbuffer.Description;

            var stagingDesc = new Texture2DDescription()
            {
                Width = desc.Width,
                Height = desc.Height,
                MipLevels = 1,
                ArraySize = 1,
                Format = desc.Format,
                SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
                Usage = ResourceUsage.Staging,
                BindFlags = BindFlags.None,
                CpuAccessFlags = CpuAccessFlags.Read,
                OptionFlags = ResourceOptionFlags.None
            };

            using (var staging = new Texture2D(device, stagingDesc))
            {
                context.CopyResource(backbuffer, staging);
                var dataBox = context.MapSubresource(staging, 0, MapMode.Read, MapFlags.None);

                int rowPitch = dataBox.RowPitch;
                int width = desc.Width;
                int height = desc.Height;
                int bytesPerPixel = 4;
                byte[] pixelData = new byte[height * width * bytesPerPixel];

                IntPtr srcPtr = dataBox.DataPointer;
                int destOffset = 0;

                for (int y = 0; y < height; y++)
                {
                    Marshal.Copy(srcPtr + y * rowPitch, pixelData, destOffset, width * bytesPerPixel);
                    destOffset += width * bytesPerPixel;
                }

                context.UnmapSubresource(staging, 0);

                var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
                var bmpData = bmp.LockBits(
                    new System.Drawing.Rectangle(0, 0, width, height),
                    ImageLockMode.WriteOnly,
                    PixelFormat.Format32bppArgb);

                Marshal.Copy(pixelData, 0, bmpData.Scan0, pixelData.Length);
                bmp.UnlockBits(bmpData);
                return bmp;
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

            /*this.Invoke((MethodInvoker)(() =>
            {
                this.Close();
            }));*/
        }

        public Bitmap CaptureWindowBitmap()
        {
            Bitmap bmp = new Bitmap(this.ClientSize.Width, this.ClientSize.Height, PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                System.Drawing.Point tLocation = System.Drawing.Point.Empty;

                this.Invoke((MethodInvoker)(() =>
                {
                    var tForm = Application.OpenForms.OfType<ContentThumbnailGenerator>().FirstOrDefault();
                    tLocation = tForm != null
                        ? tForm.GetRendererLocation()
                        : this.PointToScreen(System.Drawing.Point.Empty);
                }));

                g.CopyFromScreen(tLocation, System.Drawing.Point.Empty, this.ClientSize);
            }

            return bmp;
        }

        public void ViewModel(ContentPropItem aPropItem, bool RestartTimer = false)
        {
            tCurrPropItem = aPropItem;
            captureGeneration++;
            awaitingThumbnailCapture = false;
            Ydr = null;
            tModelArchetype = null;
            tRenderer.RenderedDrawables.Clear();
            tPauseRendering = false;

            if (TryLoadModelFromProp(aPropItem))
            {
                HandlePostLoad(RestartTimer);
                return;
            }

            if (!SaveThumbnailToDisk)
                ThumbnailReady?.Invoke(null);
            else
                HandlePostLoad(RestartTimer);
        }

        private bool TryLoadModelFromProp(ContentPropItem aPropItem)
        {
            RpfFileEntry tRpfFileEntry = aPropItem.YdrFile?.RpfFileEntry;

            if (tRpfFileEntry != null)
            {
                RpfFile tRpfFile = tRpfFileEntry.File;
                if (tRpfFile == null) return false;

                byte[] data = tRpfFile.ExtractFile(tRpfFileEntry);
                if (data == null) return false;

                string extension = Path.GetExtension(tRpfFileEntry.Name ?? string.Empty);
                if (string.IsNullOrEmpty(extension) && !string.IsNullOrEmpty(aPropItem.FilePath))
                    extension = Path.GetExtension(aPropItem.FilePath);

                if (extension == ".ydr")
                {
                    var tYdr = RpfFile.GetFile<YdrFile>(tRpfFileEntry, data);
                    LoadModel(tYdr);
                    return true;
                }
            }

            if (tGameFileCache != null && tGameFileCache.IsInited)
            {
                var nameHash = JenkHash.GenHash(aPropItem.GetCleanName());
                YdrFile ydr = null;

                if (aPropItem.Archetype != null)
                    ydr = tGameFileCache.GetYdr(aPropItem.Archetype.Hash.Hash);

                if (ydr == null)
                    ydr = aPropItem.YdrFile;

                if (ydr == null)
                    ydr = tGameFileCache.GetYdr(nameHash);

                if (ydr != null)
                {
                    LoadModel(ydr);
                    return true;
                }
            }

            return false;
        }

        private void HandlePostLoad(bool restartTimer)
        {
            if (SaveThumbnailToDisk)
            {
                if (restartTimer)
                {
                    if (tThumbnailThread.ThreadState == System.Threading.ThreadState.Stopped)
                        tThumbnailThread = new Thread(new ThreadStart(Thread_CheckForRenderProp));

                    if (tThumbnailThread.ThreadState == System.Threading.ThreadState.Unstarted)
                        tThumbnailThread.Start();
                }
            }
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

        private void InitOffscreenTarget()
        {
            var device = tRenderer.Device;
            var desc = new Texture2DDescription()
            {
                Width = this.ClientSize.Width,
                Height = this.ClientSize.Height,
                MipLevels = 1,
                ArraySize = 1,
                Format = SharpDX.DXGI.Format.R8G8B8A8_UNorm,
                SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
                Usage = ResourceUsage.Default,
                BindFlags = BindFlags.RenderTarget | BindFlags.ShaderResource,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None
            };

            offscreenTexture = new Texture2D(device, desc);
            offscreenRTV = new RenderTargetView(device, offscreenTexture);
        }

        public Bitmap CaptureOffscreenTexture()
        {
            if (offscreenTexture == null || tRenderer?.Device == null)
                return null;

            var device = tRenderer.Device;
            var context = device.ImmediateContext;
            var desc = offscreenTexture.Description;

            var stagingDesc = new Texture2DDescription()
            {
                Width = desc.Width,
                Height = desc.Height,
                MipLevels = 1,
                ArraySize = 1,
                Format = desc.Format,
                SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
                Usage = ResourceUsage.Staging,
                BindFlags = BindFlags.None,
                CpuAccessFlags = CpuAccessFlags.Read,
                OptionFlags = ResourceOptionFlags.None
            };

            using (var staging = new Texture2D(device, stagingDesc))
            {
                context.CopyResource(offscreenTexture, staging);
                var dataBox = context.MapSubresource(staging, 0, MapMode.Read, MapFlags.None);

                int rowPitch = dataBox.RowPitch;
                int width = desc.Width;
                int height = desc.Height;
                int bytesPerPixel = 4;
                byte[] pixelData = new byte[height * width * bytesPerPixel];

                IntPtr srcPtr = dataBox.DataPointer;
                int destOffset = 0;

                for (int y = 0; y < height; y++)
                {
                    Marshal.Copy(srcPtr + y * rowPitch, pixelData, destOffset, width * bytesPerPixel);
                    destOffset += width * bytesPerPixel;
                }

                context.UnmapSubresource(staging, 0);
                var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);

                var bmpData = bmp.LockBits(
                    new System.Drawing.Rectangle(0, 0, width, height),
                    ImageLockMode.WriteOnly,
                    PixelFormat.Format32bppArgb);

                Marshal.Copy(pixelData, 0, bmpData.Scan0, pixelData.Length);
                bmp.UnlockBits(bmpData);
                bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);

                return bmp;
            }
        }

    }
}
