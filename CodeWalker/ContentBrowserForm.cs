using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeWalker
{
    public partial class ContentBrowserForm : Form
    {
        public event Action RequestDockBack;
        public bool bJustUndocked = false;

        public ContentBrowserForm()
        {
            InitializeComponent();
        }

        private void ContentBrowserForm_Move(object sender, EventArgs e)
        {
            if (!bJustUndocked)
                CheckForDockBack();
        }

        private void CheckForDockBack()
        {
            // Die aktuelle Mausposition im Bildschirm-Koordinatensystem
            Point mousePos = Cursor.Position;

            // Zugriff auf MainForm
            var tMainForm = Application.OpenForms["MainForm"] as MainForm;
            if (tMainForm == null) return;

            // Rechteck des Dockpanels im Bildschirm-Koordinatensystem
            Rectangle tDockRect = tMainForm.GetDockAreaScreenRect();

            if (tDockRect.Contains(mousePos))
            {
                RequestDockBack?.Invoke();
            }
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
    }
}
