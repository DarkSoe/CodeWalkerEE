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

namespace CodeWalker
{
    public partial class MainForm : Form
    {
        // Fenster bewegen, wenn man z. B. auf ein Panel klickt:
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        WorldForm tViewport;

        public MainForm()
        {
            InitializeComponent();
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
            Application.Exit();
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
    }
}