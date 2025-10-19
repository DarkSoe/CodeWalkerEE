using CodeWalker.GameFiles;
using CodeWalker.Rendering;
using CodeWalker.Rendering.UI;
using CodeWalker.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace CodeWalker
{
    public partial class MainForm : Form
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        public WorldForm tViewport;
        ContentBrowserForm tContentBrowser;

        // Resizing Window
        private bool resizing = false;
        private Point resizeStartMouse;
        private Size resizeStartSize;
        private const int RESIZE_SIZE = 20;

        // Dragout ContentTabs
        private bool isDraggingContentTab = false;
        private Point dragContentTabStartPoint;

        public MainForm()
        {
            InitializeComponent();

            MenuStrip_MainMenu.Renderer = new DarkGreyMenuRenderer();
            MenuStrip_MainMenu.ForeColor = Color.FromArgb(230, 230, 230);
            MenuStrip_MainMenu.BackColor = Color.FromArgb(21, 21, 21);

            fileToolStripMenuItem.Enabled = false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            tViewport = new WorldForm();
            tViewport.TopLevel = false;
            tViewport.Dock = DockStyle.Fill;
            tViewport.FormBorderStyle = FormBorderStyle.None;
            panel_ContentPanel.Controls.Add(tViewport);
            tViewport.Show();

            this.Resize += (s, args) =>
            {
                if (tViewport != null)
                {
                    tViewport.Bounds = new Rectangle(
                        0,
                        0,
                        panel_ContentPanel.ClientSize.Width,
                        panel_ContentPanel.ClientSize.Height
                    );

                    tViewport.WorldForm_Resize();
                }
            };

            CreateContentBrowser();

            tViewport.StatusChanged += UpdateStatus;
            tViewport.StatChanged += UpdateStats;
            tViewport.MouseLabelChanged += UpdateMouseLabel;
            tViewport.EnableCacheDependetButtons += EnableCacheDependentThings;
            tViewport.UpdateUndoButton += UpdateUndoButton;
            tViewport.UpdateRedoButton += UpdateRedoButton;
            tViewport.RendererInitialized += RendererInitialized;
        }

        private void CreateContentBrowser()
        {
            if (tContentBrowser == null)
            {
                tContentBrowser = new ContentBrowserForm();
                tContentBrowser.TopLevel = false;
                tContentBrowser.Dock = DockStyle.Fill;
                tContentBrowser.FormBorderStyle = FormBorderStyle.None;
                panel_ContentPanel.Controls.Add(tContentBrowser);
                tContentBrowser.Show();
                tContentBrowser.Visible = false;

                tContentBrowser.RequestDockBack += DockContentBrowserBack;

                btn_tab_contentbrowser.Visible = true;
            }
            else if (tContentBrowser.TopLevel == true && tContentBrowser.Visible == false)
            {
                tContentBrowser.TopLevel = false;
                tContentBrowser.Dock = DockStyle.Fill;
                tContentBrowser.FormBorderStyle = FormBorderStyle.None;
                panel_ContentPanel.Controls.Add(tContentBrowser);
                tContentBrowser.Show();
                tContentBrowser.Visible = true;
                btn_tab_contentbrowser.Visible = true;
            }
        }

        private void ExitApplication()
        {
            if (tViewport != null)
            {
                tViewport.ExicApplication();
            }

            System.Windows.Forms.Application.Exit();
        }

        private void BTN_Exit_MouseEnter(object sender, EventArgs e)
        {
            BTN_Exit.BackColor = ColorTranslator.FromHtml("#cd3c3c");
        }

        private void BTN_Exit_MouseLeave(object sender, EventArgs e)
        {
            BTN_Exit.BackColor = System.Drawing.Color.Transparent;
        }

        private void BTN_Exit_Click(object sender, EventArgs e)
        {
            ExitApplication();
        }

        private void BTN_Fullscreen_MouseEnter(object sender, EventArgs e)
        {
            BTN_Fullscreen.BackColor = System.Drawing.Color.Gray;
        }

        private void BTN_Fullscreen_MouseLeave(object sender, EventArgs e)
        {
            BTN_Fullscreen.BackColor = System.Drawing.Color.Transparent;
        }

        private void BTN_Fullscreen_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void BTN_Minimize_MouseEnter(object sender, EventArgs e)
        {
            BTN_Minimize.BackColor = System.Drawing.Color.Gray;
        }

        private void BTN_Minimize_MouseLeave(object sender, EventArgs e)
        {
            BTN_Minimize.BackColor = System.Drawing.Color.Transparent;
        }

        private void BTN_Minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Panel_MainWindowPanel_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0xA1, 0x2, 0);
        }

        private void UpdateStatus(string text)
        {
            try
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() => { UpdateStatus(text); }));
                }
                else
                {
                    StatusLabel.Text = text;
                }
            }
            catch { }
        }

        private void UpdateStats(string stattext)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => UpdateStats(stattext)));
                return;
            }

            StatsLabel.Text = stattext;

            /*if (Renderer.timerunning)
            {
                float fv = Renderer.timeofday * 60.0f;
                TimeOfDayTrackBar.Value = (int)fv;
                UpdateTimeOfDayLabel();
            }

            CameraPositionTextBox.Text = FloatUtil.GetVector3StringFormat(camera.Position, "0.##");*/
        }

        private void UpdateMouseLabel(string text)
        {
            try
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() => { UpdateMouseLabel(text); }));
                }
                else
                {
                    MousedLabel.Text = text;
                }
            }
            catch { }
        }

        private void ResizePanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left &&
                e.X >= ResizingBox.Width - RESIZE_SIZE && e.Y >= ResizingBox.Height - RESIZE_SIZE)
            {
                resizing = true;
                resizeStartMouse = Cursor.Position;
                resizeStartSize = this.Size;
                ResizingBox.Capture = true;
            }
        }

        private void ResizePanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (!resizing)
            {
                if (e.X >= ResizingBox.Width - RESIZE_SIZE && e.Y >= ResizingBox.Height - RESIZE_SIZE)
                    ResizingBox.Cursor = Cursors.SizeNWSE;
                else
                    ResizingBox.Cursor = Cursors.Default;
            }
            else
            {
                Point diff = new Point(Cursor.Position.X - resizeStartMouse.X, Cursor.Position.Y - resizeStartMouse.Y);
                int newW = Math.Max(this.MinimumSize.Width, resizeStartSize.Width + diff.X);
                int newH = Math.Max(this.MinimumSize.Height, resizeStartSize.Height + diff.Y);
                this.Size = new Size(newW, newH);
            }
        }

        private void ResizePanel_MouseUp(object sender, MouseEventArgs e)
        {
            resizing = false;
            ResizingBox.Capture = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitApplication();
        }

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tViewport.NewProject();
        }

        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tViewport.New();
        }

        private void newYmapFileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tViewport.NewYmap();
        }

        private void newYtypFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tViewport.NewYtyp();
        }

        private void newYbnFileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tViewport.NewYbn();
        }

        private void newYndFileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tViewport.NewYnd();
        }

        private void newTrainsFileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tViewport.NewTrainTrack();
        }

        private void newScenarioFileToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            tViewport.NewScenario();
        }

        private void EnableCacheDependentThings()
        {
            // Files Buttons
            fileToolStripMenuItem.Enabled = true;

            // Tools
            audioExplorerToolStripMenuItem.Enabled = true;
            cutsceneViewerToolStripMenuItem.Enabled = true;
            jenkIndexToolStripMenuItem.Enabled = true;
            binarySearchToolStripMenuItem.Enabled = true;

            // Window
            projectWindowToolStripMenuItem.Enabled = true;

            // ContentBrowser
            tContentBrowser.InitializeContentBrowser(tViewport);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tViewport.Open();
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tViewport.OpenProject();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tViewport.OpenFiles();
        }

        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tViewport.OpenFolder();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tViewport.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tViewport.Redo();
        }

        private void UpdateUndoButton(bool enabled)
        {
            undoToolStripMenuItem.Enabled=enabled;
        }

        private void UpdateRedoButton(bool enabled)
        {
            redoToolStripMenuItem.Enabled=enabled;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tViewport.Save();
        }

        private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tViewport.SaveAll();
        }

        private void RendererInitialized()
        {
            try
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() => { RendererInitialized(); }));
                }
                else
                {
                    if (tViewport != null)
                    {
                        tViewport.Bounds = new Rectangle(
                            0,
                            0,
                            panel_ContentPanel.ClientSize.Width,
                            panel_ContentPanel.ClientSize.Height
                        );

                        tViewport.WorldForm_Resize();
                    }
                }
            }
            catch { }
        }

        private void btn_tab_contentbrowser_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                dragContentTabStartPoint = e.Location;
        }

        private void btn_tab_contentbrowser_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!isDraggingContentTab && (Math.Abs(e.X - dragContentTabStartPoint.X) > 5 || Math.Abs(e.Y - dragContentTabStartPoint.Y) > 5))
                {
                    isDraggingContentTab = true;
                    ContentBrowserStartUndockDrag();
                }
            }
        }

        private void ContentBrowserStartUndockDrag()
        {
            if (tContentBrowser == null) return;

            panel_ContentPanel.Controls.Remove(tContentBrowser);

            tContentBrowser.Undock();

            tContentBrowser.TopLevel = true;
            tContentBrowser.FormBorderStyle = FormBorderStyle.Sizable;
            tContentBrowser.Dock = DockStyle.None;
            tContentBrowser.StartPosition = FormStartPosition.Manual;

            Point screenPos = Cursor.Position;
            tContentBrowser.Location = new Point(screenPos.X - (tContentBrowser.Width / 2), screenPos.Y - 10);

            tContentBrowser.Visible = true;
            tContentBrowser.Show();

            ReleaseCapture();
            SendMessage(tContentBrowser.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);

            isDraggingContentTab = false;

            btn_tab_contentbrowser.Visible = false;
            tViewport.Visible = true;
            tContentBrowser.Focus();
        }

        private void DockContentBrowserBack()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(DockContentBrowserBack));
                return;
            }

            if (tContentBrowser == null) return;

            tContentBrowser.Hide();
            tContentBrowser.TopLevel = false;
            tContentBrowser.FormBorderStyle = FormBorderStyle.None;
            tContentBrowser.Dock = DockStyle.Fill;

            panel_ContentPanel.Controls.Add(tContentBrowser);
            tContentBrowser.Show();

            btn_tab_contentbrowser.Visible = true;

            tViewport.Visible = false;
            tContentBrowser.Visible = true;
        }

        private void btn_tab_contentbrowser_Click(object sender, EventArgs e)
        {
            tViewport.Visible = false;
            tContentBrowser.Visible = true;
        }

        private void btn_tab_viewport_Click(object sender, EventArgs e)
        {
            tViewport.Visible = true;
            tContentBrowser.Visible = false;
        }

        public Rectangle GetDockAreaScreenRect()
        {
            return panel_WindowTabs.RectangleToScreen(panel_WindowTabs.ClientRectangle);
        }

        private void contentBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateContentBrowser();
        }

        private void rPFExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExploreForm tRPF_Explorer = new ExploreForm();
            tRPF_Explorer.Show(this);
        }

        private void audioExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AudioExplorerForm tAudioExplorer = new AudioExplorerForm(tViewport.GameFileCache);
            tAudioExplorer.Show(this);
        }

        private void cutsceneViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tViewport.ShowCutsceneForm();
        }

        private void jenkGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JenkGenForm tJenkGeneratorForm = new JenkGenForm();
            tJenkGeneratorForm.Show(this);
        }

        private void jenkIndexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JenkIndForm tJenkIndexForm = new JenkIndForm(tViewport.GameFileCache);
            tJenkIndexForm.Show(this);
        }

        private void worldSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tViewport.ShowSearchForm();
        }

        private void binarySearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BinarySearchForm tBinarySearchForm = new BinarySearchForm(tViewport.GameFileCache);
            tBinarySearchForm.Show(this);
        }

        private void extractScriptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExtractScriptsForm tExtractScriptsForm = new ExtractScriptsForm();
            tExtractScriptsForm.Show(this);
        }

        private void extractTexturesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExtractTexForm tExtractTexturesForm = new ExtractTexForm();
            tExtractTexturesForm.Show(this);
        }

        private void extractRawFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExtractRawForm tExtractRawForm = new ExtractRawForm();
            tExtractRawForm.Show(this);
        }

        private void extractShadersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExtractShadersForm tExtractShadersForm = new ExtractShadersForm();
            tExtractShadersForm.Show(this);
        }

        private void configurateGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = GTAFolder.UpdateGTAFolder(false, false);
            if (result)
            {
                MessageBox.Show("Please restart CodeWalker to use the new folder.");
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tViewport.ShowSettingsForm();
        }

        private void rPFBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BrowseForm tRPFBrowserForm = new BrowseForm();
            tRPFBrowserForm.Show(this);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm tAboutForm = new AboutForm();
            tAboutForm.Show(this);
        }

        private void selectionInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tViewport.ShowInfoForm();
        }

        private void projectWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tViewport.ShowProjectForm();
        }
    }
}