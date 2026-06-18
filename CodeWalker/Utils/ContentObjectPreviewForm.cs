using System;
using System.Drawing;
using System.Windows.Forms;

namespace CodeWalker.Utils
{
    public partial class ContentObjectPreviewForm : Form
    {
        private static ContentObjectPreviewForm instance;

        private OffscreenRenderer previewRenderer;
        private ContentPropItem pendingProp;
        private System.Windows.Forms.Timer readyTimer;

        public ContentObjectPreviewForm()
        {
            InitializeComponent();
            InitPreviewRenderer();
        }

        public static void ShowFor(ContentPropItem prop)
        {
            if (prop == null)
                return;

            if (instance == null || instance.IsDisposed)
                instance = new ContentObjectPreviewForm();

            instance.LoadObject(prop);
            if (!instance.Visible)
                instance.Show();

            instance.BringToFront();
            instance.Activate();
        }

        private void InitPreviewRenderer()
        {
            previewRenderer = new OffscreenRenderer
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                PreviewMode = true,
                SaveThumbnailToDisk = false,
                Dock = DockStyle.Fill
            };

            panelRenderHost.Controls.Add(previewRenderer);
            previewRenderer.Show();

            readyTimer = new System.Windows.Forms.Timer { Interval = 100 };
            readyTimer.Tick += ReadyTimer_Tick;
        }

        private void ReadyTimer_Tick(object sender, EventArgs e)
        {
            if (pendingProp == null || !previewRenderer.IsRendererReady)
                return;

            readyTimer.Stop();
            ApplyPendingModel();
        }

        private void LoadObject(ContentPropItem prop)
        {
            pendingProp = prop;
            Text = prop.GetCleanName() + " – Preview";

            labelNameValue.Text = prop.GetCleanName();
            labelFlagsValue.Text = prop.GetFlagsText();
            labelTagsValue.Text = "—";

            if (previewRenderer.IsRendererReady)
                ApplyPendingModel();
            else
                readyTimer.Start();
        }

        private void ApplyPendingModel()
        {
            if (pendingProp == null || previewRenderer == null || previewRenderer.IsDisposed)
                return;

            previewRenderer.PreviewMode = true;
            previewRenderer.SaveThumbnailToDisk = false;

            void startRender()
            {
                previewRenderer.ViewModel(pendingProp);
            }

            if (previewRenderer.InvokeRequired)
                previewRenderer.BeginInvoke((Action)startRender);
            else
                startRender();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            readyTimer?.Stop();
            readyTimer?.Dispose();
            readyTimer = null;

            if (previewRenderer != null && !previewRenderer.IsDisposed)
            {
                previewRenderer.PreviewMode = false;
                previewRenderer.CleanupScene();
                previewRenderer.Dispose();
                previewRenderer = null;
            }

            instance = null;
            base.OnFormClosed(e);
        }
    }
}

