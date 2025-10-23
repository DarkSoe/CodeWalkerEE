using CodeWalker.GameFiles;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeWalker.Utils
{
    public partial class ContentBrowserItem : UserControl
    {
        public ContentPropItem tPropItem { get; set; }
        public OffscreenRenderer tOffscreenRenderer { get; set; }

        public bool bIsDynamic = false;
        public bool bIsDoor = false;
        public bool bIsGlass = false;
        public bool bIsLadder = false;
        public bool bIsTree = false;
        public bool bIsMlo = false;

        WorldForm tViewport = null;

        public ToolTip Add_toolTip;
        public ToolTip Clipboard_toolTip;
        public ToolTip Browser_toolTip;

        public ContentBrowserItem()
        {
            InitializeComponent();
            tViewport = Application.OpenForms.OfType<WorldForm>().FirstOrDefault();

            Add_toolTip = new ToolTip();
            Clipboard_toolTip = new ToolTip();
            Browser_toolTip = new ToolTip();

            Add_toolTip.SetToolTip(label_addEntry, "Add this as new Entity to the YMap");
            Clipboard_toolTip.SetToolTip(img_save_clipboard, "Save Plebmasters Link to Clipboard");
            Browser_toolTip.SetToolTip(img_plep_link, "Open Plebmaster-page of this Prop in Browser");

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
            tViewport = Application.OpenForms.OfType<WorldForm>().FirstOrDefault();

            tPropItem = aPropItem;
            label_Name.Text = tPropItem.GetCleanName();
            img_thumbnail.ImageLocation = tPropItem.ThumbnailPath;
            ApplyArchetypeFlags(tPropItem.Archetype);

            Add_toolTip = new ToolTip();
            Clipboard_toolTip = new ToolTip();
            Browser_toolTip = new ToolTip();

            Add_toolTip.SetToolTip(label_addEntry, "Add this as new Entity to the YMap");
            Clipboard_toolTip.SetToolTip(img_save_clipboard, "Save Plebmasters Link to Clipboard");
            Browser_toolTip.SetToolTip(img_plep_link, "Open Plebmaster-page of this Prop in Browser");

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
            ApplyArchetypeFlags(tPropItem.Archetype);

            Add_toolTip = new ToolTip();
            Clipboard_toolTip = new ToolTip();
            Browser_toolTip = new ToolTip();

            Add_toolTip.SetToolTip(label_addEntry, "Add this as new Entity to the YMap");
            Clipboard_toolTip.SetToolTip(img_save_clipboard, "Save Plebmasters Link to Clipboard");
            Browser_toolTip.SetToolTip(img_plep_link, "Open Plebmaster-page of this Prop in Browser");


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

        public void ApplyArchetypeFlags(Archetype archetype)
        {
            if (archetype?.BaseArchetypeDef == null) return;

            uint flags = archetype.BaseArchetypeDef.flags;

            bIsDoor = (flags & 0x04000000) != 0;
            bIsGlass = (flags & 0x00040000) != 0;
            bIsLadder = (flags & 0x00008000) != 0;
            bIsDynamic = (flags & 0x20000000) != 0;
            bIsTree = (flags & 0x00000010) != 0;
            bIsMlo = (flags & 0x00000002) != 0;

            string tFlagString = "";

            if (bIsDynamic)
                tFlagString = AddFlagText(tFlagString, "Dynamic");

            if (bIsGlass)
                tFlagString = AddFlagText(tFlagString, "Glass");

            if (bIsLadder)
                tFlagString = AddFlagText(tFlagString, "Ladder");

            if (bIsDoor)
                tFlagString = AddFlagText(tFlagString, "Door");

            if (bIsTree)
                tFlagString = AddFlagText(tFlagString, "Tree");

            if (bIsMlo)
                tFlagString = AddFlagText(tFlagString, "MLO");

            label_itemType.Text = tFlagString;
        }

        public string AddFlagText(string tCurrFlagText, string tAddText)
        {
            string tCurr = tCurrFlagText;

            if (tCurr != "")
                tCurr += ", ";

            tCurr += tAddText;

            return tCurr;
        }

        private void label_addEntry_Click(object sender, EventArgs e)
        {
            tViewport.AddItem(tPropItem);
        }

        private void img_plep_link_Click(object sender, EventArgs e)
        {
            string url = "https://forge.plebmasters.de/objects/" + tPropItem.GetCleanName();
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }

        private void img_save_clipboard_Click(object sender, EventArgs e)
        {
            string url = "https://forge.plebmasters.de/objects/" + tPropItem.GetCleanName();
            Clipboard.SetText(url);
        }
    }
}
