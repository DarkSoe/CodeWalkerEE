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
        List<ContentBrowserItem> ItemContainerPool = new List<ContentBrowserItem>();

        Archetype tCurrentArchetype = null;
        bool tUpdateArchetypeStatus = true;

        private int CurrentPage = 0;
        private const int PageSize = 10;

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

            panel_ContentBrowser.SuspendLayout();
            panel_ContentBrowser.Controls.Clear();
            ItemContainerPool.Clear();

            for (int i = 0; i < PageSize; i++)
            {
                var item = new ContentBrowserItem();
                item.Visible = false;
                panel_ContentBrowser.Controls.Add(item);
                ItemContainerPool.Add(item);
            }

            panel_ContentBrowser.ResumeLayout();

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
            PropList.Clear();

            var props = await Task.Run(() =>
            {
                var list = new List<ContentPropItem>();
                foreach (var kvp in GameFileCache.YdrDict)
                {
                    var hash = kvp.Key;
                    var entry = kvp.Value;

                    var ydr = GameFileCache.GetYdr(hash);
                    if (ydr != null)
                    {
                        var tRpfFileEntry = ydr.RpfFileEntry;
                        var tModelHash = tRpfFileEntry?.ShortNameHash ?? 0;
                        if (tModelHash != 0)
                        {
                            var tModelArchetype = TryGetArchetype(tModelHash);
                            if (tModelArchetype != null)
                            {
                                ContentPropItem tProp = new ContentPropItem(entry.Name, ydr);
                                tProp.Archetype = tModelArchetype;
                                tProp.FilePath = entry.Path;

                                list.Add(tProp);
                            }
                        }                            
                    }
                    
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

            label_currentPage.Text = (pageIndex + 1).ToString();

            var pageItems = FilteredPropList
                .Skip(pageIndex * PageSize)
                .Take(PageSize)
                .ToList();

            for (int i = 0; i < ItemContainerPool.Count; i++)
            {
                if (i < pageItems.Count())
                {
                    var tCurrContainer = ItemContainerPool[i];
                    tCurrContainer.SetProp(pageItems[i]);
                    tCurrContainer.Visible = true;
                }
                else
                {
                    ItemContainerPool[i].Visible = false;
                }
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

        private Archetype TryGetArchetype(uint hash)
        {
            if ((GameFileCache == null) || (!GameFileCache.IsInited)) return null;

            var arch = GameFileCache.GetArchetype(hash);

            if ((arch != null) && (arch != tCurrentArchetype) && (tUpdateArchetypeStatus))
            {
                tCurrentArchetype = arch;
                tUpdateArchetypeStatus = false;
            }

            return arch;
        }
    }
}
