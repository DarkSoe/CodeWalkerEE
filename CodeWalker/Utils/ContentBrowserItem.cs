using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public ContentPropItem PropItem { get; set; }

        public ContentBrowserItem()
        {
            InitializeComponent();
        }

        public ContentBrowserItem(ContentPropItem aPropItem)
        {
            InitializeComponent();

            PropItem = aPropItem;

            label_Name.Text = aPropItem.GetCleanName();
            img_PropImage.ImageLocation = Path.ChangeExtension(aPropItem.ThumbnailPath, ".jpg");
        }
    }
}
