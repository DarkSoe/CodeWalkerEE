using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeWalker.Utils
{
    public partial class ContentBrowserItem : UserControl
    {
        public ContentPropItem tPropItem { get; set; }
        public OffscreenRenderer tOffscreenRenderer { get; set; }

        public ContentBrowserItem()
        {
            InitializeComponent();

            /*if (tOffscreenRenderer == null)
            {
                tOffscreenRenderer = new OffscreenRenderer();
                tOffscreenRenderer.TopLevel = false;
                tOffscreenRenderer.FormBorderStyle = FormBorderStyle.None;
                tOffscreenRenderer.ClientSize = new Size(350, 350);
                tOffscreenRenderer.Location = new Point(0, 0);
                tOffscreenRenderer.Visible = false;
                tOffscreenRenderer.Show();

                panel_RenderImage.Controls.Add(tOffscreenRenderer);
            }*/
        }

        public ContentBrowserItem(ContentPropItem aPropItem)
        {
            InitializeComponent();

            tPropItem = aPropItem;
            label_Name.Text = tPropItem.GetCleanName();
            img_thumbnail.ImageLocation = tPropItem.ThumbnailPath;

            /*if (tOffscreenRenderer == null)
            {
                tOffscreenRenderer = new OffscreenRenderer();
                tOffscreenRenderer.TopLevel = false;
                tOffscreenRenderer.FormBorderStyle = FormBorderStyle.None;
                tOffscreenRenderer.ClientSize = new Size(350, 350);
                tOffscreenRenderer.Location = new Point(0, 0);
                tOffscreenRenderer.Visible = true;
                tOffscreenRenderer.Show();

                panel_RenderImage.Controls.Add(tOffscreenRenderer);
            }

            tOffscreenRenderer.FilePath = tPropItem.YdrFile.FilePath;
            tOffscreenRenderer.SaveFilePath = tPropItem.ThumbnailPath;
            tOffscreenRenderer.ViewModel(tPropItem);*/
        }

        public void SetProp(ContentPropItem aPropItem)
        {
            tPropItem = aPropItem;
            label_Name.Text = tPropItem.GetCleanName();
            img_thumbnail.ImageLocation = tPropItem.ThumbnailPath;

           /* if (tOffscreenRenderer == null)
            {
                tOffscreenRenderer = new OffscreenRenderer();
                tOffscreenRenderer.TopLevel = false;
                tOffscreenRenderer.FormBorderStyle = FormBorderStyle.None;
                tOffscreenRenderer.ClientSize = new Size(350, 350);
                tOffscreenRenderer.Location = new Point(0, 0);
                tOffscreenRenderer.Visible = true;
                tOffscreenRenderer.Show();

                panel_RenderImage.Controls.Add(tOffscreenRenderer);
            }
            
            tOffscreenRenderer.FilePath = tPropItem.YdrFile.FilePath;
            tOffscreenRenderer.SaveFilePath = tPropItem.ThumbnailPath;
            tOffscreenRenderer.tPauseRendering = false;
            tOffscreenRenderer.ViewModel(tPropItem);*/
        }
    }
}
