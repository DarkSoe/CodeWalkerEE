using CodeWalker.Forms;
using CodeWalker.GameFiles;
using CodeWalker.Rendering;
using CodeWalker.Utils;
using CodeWalker.World;
using SharpDX.Direct2D1;
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
using System.Xml;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

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
        List<string> FavoriteList = new List<string>();

        Archetype tCurrentArchetype = null;
        bool tUpdateArchetypeStatus = true;

        private int CurrentPage = 0;
        private const int PageSize = 65;

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
            FavoriteList.Clear();

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
            LoadFavoriteList();

            var props = await Task.Run(() =>
            {
                var list = new List<ContentPropItem>();
                foreach (var kvp in GameFileCache.YdrDict)
                {
                    var hash = kvp.Key;
                    var entry = kvp.Value;

                    YdrFile ydr = GameFileCache.GetYdr(hash);
                    if (ydr != null)
                    {
                        RpfFileEntry tRpfFileEntry = ydr.RpfFileEntry;
                        var tModelHash = tRpfFileEntry?.ShortNameHash ?? 0;

                        string tGtaPath = GTAFolder.GetCurrentGTAFolderWithTrailingSlash();
                        string tFullFilePath = tGtaPath + tRpfFileEntry.Path;

                        int tFileVersion = 0;
                        if (tRpfFileEntry is RpfResourceFileEntry)
                        {
                            var resf = tRpfFileEntry as RpfResourceFileEntry;
                            tFileVersion = resf.Version;
                        }

                        if (tModelHash != 0 /*&& tFileVersion > 164*/)
                        {
                            var tModelArchetype = TryGetArchetype(tModelHash);
                            if (tModelArchetype != null)
                            {
                                ContentPropItem tProp = new ContentPropItem(entry.Name, ydr);
                                tProp.Archetype = tModelArchetype;
                                tProp.FilePath = entry.Path;

                                if (FavoriteList.Contains(entry.Name))
                                    tProp.IsFavorite = true;

                                list.Add(tProp);
                            }
                        }                            
                    }
                    
                }
                return list;
            });

            PropList.AddRange(props);

            FilteredPropList = new List<ContentPropItem>(PropList);
            PropList.Reverse();
            PopulatePage(0);
        }

        private void PopulatePage(int pageIndex)
        {
            if (FilteredPropList.Count == 0) return;

            int totalItems = FilteredPropList.Count;
            int totalPages = (int)Math.Ceiling((double)totalItems / PageSize);
            int currentpage = pageIndex + 1;

            if (currentpage > totalPages)
                return;

            label_currentPage.Text = (currentpage + " | " + totalPages).ToString();

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
            int totalItems = FilteredPropList.Count;
            int totalPages = (int)Math.Ceiling((double)totalItems / PageSize);

            if (CurrentPage < (totalPages - 1))
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
                if (check_Favorites.Checked)
                {
                    FilteredPropList = PropList.Where(p =>
                        p.IsFavorite
                    )
                    .ToList();
                }
                else
                {
                    FilteredPropList = new List<ContentPropItem>(PropList);
                }
            }
            else
            {
                string lower = search.ToLowerInvariant();

                FilteredPropList = PropList.Where(p => 
                    p.Name.ToLowerInvariant().Contains(lower) &&
                    (!check_Favorites.Checked || p.IsFavorite)
                )
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

        private void check_Favorites_CheckStateChanged(object sender, EventArgs e)
        {
            btn_search.PerformClick();
        }

        private void LoadFavoriteList()
        {
            FavoriteList.Clear();

            try
            {
                var codeWalkerDir = AppDomain.CurrentDomain.BaseDirectory;
                var favpath = Path.Combine(codeWalkerDir, "favorites.json");

                var favlist = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(favpath));
                FavoriteList = new List<string>(favlist);
            }
            catch (Exception e) { }
        }

        private void SaveFavoriteList()
        {
            var codeWalkerDir = AppDomain.CurrentDomain.BaseDirectory;
            var favpath = Path.Combine(codeWalkerDir, "favorites.json");

            File.WriteAllText(favpath, JsonConvert.SerializeObject(FavoriteList, Newtonsoft.Json.Formatting.Indented));
        }

        public void AddToFavorites(string aName)
        {
            if (!FavoriteList.Contains(aName))
            {
                FavoriteList.Add(aName);
            }    

            SaveFavoriteList();
        }

        public void RemoveFromFavorites(string aName)
        {
            if (FavoriteList.Contains(aName))
                FavoriteList.Remove(aName);

            SaveFavoriteList();
        }

        public bool IsFavFilterChecked()
        {
            return check_Favorites.Checked;
        }

        public void PerformSearchClick()
        {
            btn_search.PerformClick();
        }
    }
}
