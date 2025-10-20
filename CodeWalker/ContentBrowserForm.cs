using CodeWalker.Forms;
using CodeWalker.GameFiles;
using CodeWalker.Rendering;
using CodeWalker.Utils;
using CodeWalker.World;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace CodeWalker
{
    public partial class ContentBrowserForm : Form
    {
        public event Action RequestDockBack;
        public bool bJustUndocked = false;

        private WorldForm WorldForm;
        private GameFileCache GameFileCache;

        List<ContentPropItem> PropList = new List<ContentPropItem>();
        List<ContentPropItem> FilteredPropList = new List<ContentPropItem>();

        private int CurrentPage = 0;
        private const int PageSize = 1;

        //Events
        public event Action<string> PropDragged;

        public ContentBrowserForm()
        {
            InitializeComponent();
        }

        private void timer_justUndocked_Tick(object sender, EventArgs e)
        {
            bJustUndocked = false;
            timer_justUndocked.Stop();
        }

        public void Undock()
        {
            bJustUndocked = true;
            timer_justUndocked.Start();
        }

        private void ContentBrowserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        public void InitializeContentBrowser(WorldForm aWorldForm)
        {
            WorldForm = aWorldForm;

            if (WorldForm != null && WorldForm.GameFileCache != null)
                GameFileCache = WorldForm.GameFileCache;

            _ = LoadPropsFromCacheAsync();
        }

        private void ContentBrowserForm_Move(object sender, EventArgs e)
        {
            if (!bJustUndocked)
                CheckForDockBack();
        }

        private void CheckForDockBack()
        {
            Point mousePos = Cursor.Position;

            var tMainForm = Application.OpenForms["MainForm"] as MainForm;
            if (tMainForm == null) return;

            Rectangle tDockRect = tMainForm.GetDockAreaScreenRect();

            if (tDockRect.Contains(mousePos))
            {
                RequestDockBack?.Invoke();
            }
        }

        public async Task LoadPropsFromCacheAsync()
        {
            panel_ContentBrowser.Controls.Clear();
            PropList.Clear();

            var props = await Task.Run(() =>
            {
                var list = new List<ContentPropItem>();
                foreach (var kvp in GameFileCache.YdrDict)
                {
                    var hash = kvp.Key;
                    var entry = kvp.Value;

                    var ydr = GameFileCache.GetYdr(hash);
                    if (ydr == null) continue;

                    list.Add(new ContentPropItem(entry.Name, ydr));
                }
                return list;
            });

            PropList.AddRange(props);

            FilteredPropList = new List<ContentPropItem>(PropList);
            PopulatePage(0);
        }

        private void PopulatePage(int pageIndex)
        {
            if (FilteredPropList.Count == 0) return;

            panel_ContentBrowser.Controls.Clear();
            label_currentPage.Text = (pageIndex + 1).ToString();

            var pageItems = FilteredPropList
                .Skip(pageIndex * PageSize)
                .Take(PageSize);

            foreach (var prop in pageItems)
            {
                if (!prop.HasThumbnail())
                {
                    RenderPropToBitmap(prop);
                }

                var itemControl = new ContentBrowserItem(prop);
                panel_ContentBrowser.Controls.Add(itemControl);
            }
        }

        private void btn_NextPage_Click(object sender, EventArgs e)
        {
            int maxPage = (PropList.Count - 1) / PageSize;
            if (CurrentPage < maxPage)
            {
                CurrentPage++;
                PopulatePage(CurrentPage);
            }
        }

        private void btn_PrevPage_Click(object sender, EventArgs e)
        {
            if (CurrentPage > 0)
            {
                CurrentPage--;
                PopulatePage(CurrentPage);
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            ApplyFilter(textBox_search.Text);
        }

        private void ApplyFilter(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                FilteredPropList = new List<ContentPropItem>(PropList);
            }
            else
            {
                string lower = search.ToLowerInvariant();
                FilteredPropList = PropList
                    .Where(p => p.Name.ToLowerInvariant().Contains(lower))
                    .ToList();
            }

            CurrentPage = 0;
            PopulatePage(CurrentPage);
        }

        private void textBox_search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btn_search.PerformClick();
            }
        }

        private void RenderPropToBitmap(ContentPropItem aItem)
        {
            int width = 350;
            int height = 350;

            var tOffscreenRenderer = new OffscreenRenderer();
            tOffscreenRenderer.TopLevel = true;
            tOffscreenRenderer.FormBorderStyle = FormBorderStyle.None;
            tOffscreenRenderer.ClientSize = new Size(width, height);
            tOffscreenRenderer.Visible = true;
            tOffscreenRenderer.FilePath = aItem.YdrFile.FilePath;
            tOffscreenRenderer.SaveFilePath = aItem.ThumbnailPath;
            tOffscreenRenderer.Location = new Point(0, 0);
            tOffscreenRenderer.Show();
            tOffscreenRenderer.LoadModel(aItem.YdrFile);
        }
    }
}
