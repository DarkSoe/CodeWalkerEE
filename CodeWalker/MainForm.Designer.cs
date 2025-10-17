namespace CodeWalker
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel_MainWindow = new System.Windows.Forms.Panel();
            this.BTN_Exit = new System.Windows.Forms.PictureBox();
            this.BTN_Minimize = new System.Windows.Forms.PictureBox();
            this.BTN_Fullscreen = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel_ContentPanel = new System.Windows.Forms.Panel();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.MousedLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatsLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel_MainWindow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Exit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Minimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Fullscreen)).BeginInit();
            this.panel2.SuspendLayout();
            this.StatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.panelHeader, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1594, 1047);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.panelHeader.Controls.Add(this.tableLayoutPanel2);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1594, 90);
            this.panelHeader.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1594, 90);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(84, 84);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CodeWalker.Properties.Resources.CodeWalker_Icon;
            this.pictureBox1.Location = new System.Drawing.Point(11, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(61, 62);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.panel_MainWindow, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel3, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(90, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1504, 90);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // panel_MainWindow
            // 
            this.panel_MainWindow.Controls.Add(this.BTN_Exit);
            this.panel_MainWindow.Controls.Add(this.BTN_Minimize);
            this.panel_MainWindow.Controls.Add(this.BTN_Fullscreen);
            this.panel_MainWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_MainWindow.Location = new System.Drawing.Point(0, 0);
            this.panel_MainWindow.Margin = new System.Windows.Forms.Padding(0);
            this.panel_MainWindow.Name = "panel_MainWindow";
            this.panel_MainWindow.Size = new System.Drawing.Size(1504, 45);
            this.panel_MainWindow.TabIndex = 2;
            this.panel_MainWindow.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_MainWindowPanel_MouseDown);
            // 
            // BTN_Exit
            // 
            this.BTN_Exit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN_Exit.Image = global::CodeWalker.Properties.Resources.BTN_Exit;
            this.BTN_Exit.Location = new System.Drawing.Point(1454, 0);
            this.BTN_Exit.Name = "BTN_Exit";
            this.BTN_Exit.Size = new System.Drawing.Size(50, 32);
            this.BTN_Exit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.BTN_Exit.TabIndex = 1;
            this.BTN_Exit.TabStop = false;
            this.BTN_Exit.Click += new System.EventHandler(this.BTN_Exit_Click);
            this.BTN_Exit.MouseEnter += new System.EventHandler(this.BTN_Exit_MouseEnter);
            this.BTN_Exit.MouseLeave += new System.EventHandler(this.BTN_Exit_MouseLeave);
            // 
            // BTN_Minimize
            // 
            this.BTN_Minimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN_Minimize.Image = global::CodeWalker.Properties.Resources.BTN_Minimize;
            this.BTN_Minimize.Location = new System.Drawing.Point(1356, 0);
            this.BTN_Minimize.Name = "BTN_Minimize";
            this.BTN_Minimize.Size = new System.Drawing.Size(50, 32);
            this.BTN_Minimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.BTN_Minimize.TabIndex = 1;
            this.BTN_Minimize.TabStop = false;
            this.BTN_Minimize.Click += new System.EventHandler(this.BTN_Minimize_Click);
            this.BTN_Minimize.MouseEnter += new System.EventHandler(this.BTN_Minimize_MouseEnter);
            this.BTN_Minimize.MouseLeave += new System.EventHandler(this.BTN_Minimize_MouseLeave);
            // 
            // BTN_Fullscreen
            // 
            this.BTN_Fullscreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN_Fullscreen.Image = global::CodeWalker.Properties.Resources.BTN_Fullscreen;
            this.BTN_Fullscreen.Location = new System.Drawing.Point(1405, 0);
            this.BTN_Fullscreen.Name = "BTN_Fullscreen";
            this.BTN_Fullscreen.Size = new System.Drawing.Size(50, 32);
            this.BTN_Fullscreen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.BTN_Fullscreen.TabIndex = 1;
            this.BTN_Fullscreen.TabStop = false;
            this.BTN_Fullscreen.Click += new System.EventHandler(this.BTN_Fullscreen_Click);
            this.BTN_Fullscreen.MouseEnter += new System.EventHandler(this.BTN_Fullscreen_MouseEnter);
            this.BTN_Fullscreen.MouseLeave += new System.EventHandler(this.BTN_Fullscreen_MouseLeave);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 45);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1504, 45);
            this.panel3.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel_ContentPanel);
            this.panel2.Controls.Add(this.StatusStrip);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 90);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1594, 957);
            this.panel2.TabIndex = 1;
            // 
            // panel_ContentPanel
            // 
            this.panel_ContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_ContentPanel.Location = new System.Drawing.Point(0, 0);
            this.panel_ContentPanel.Margin = new System.Windows.Forms.Padding(0);
            this.panel_ContentPanel.Name = "panel_ContentPanel";
            this.panel_ContentPanel.Size = new System.Drawing.Size(1594, 925);
            this.panel_ContentPanel.TabIndex = 2;
            // 
            // StatusStrip
            // 
            this.StatusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel,
            this.MousedLabel,
            this.StatsLabel});
            this.StatusStrip.Location = new System.Drawing.Point(0, 925);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.StatusStrip.Size = new System.Drawing.Size(1594, 32);
            this.StatusStrip.TabIndex = 1;
            this.StatusStrip.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            this.StatusLabel.BackColor = System.Drawing.SystemColors.Control;
            this.StatusLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(1428, 25);
            this.StatusLabel.Spring = true;
            this.StatusLabel.Text = "Initialising";
            this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MousedLabel
            // 
            this.MousedLabel.BackColor = System.Drawing.SystemColors.Control;
            this.MousedLabel.Name = "MousedLabel";
            this.MousedLabel.Size = new System.Drawing.Size(27, 25);
            this.MousedLabel.Text = "   ";
            // 
            // StatsLabel
            // 
            this.StatsLabel.BackColor = System.Drawing.SystemColors.Control;
            this.StatsLabel.DoubleClickEnabled = true;
            this.StatsLabel.Name = "StatsLabel";
            this.StatsLabel.Size = new System.Drawing.Size(116, 25);
            this.StatsLabel.Text = "0 geometries";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.ClientSize = new System.Drawing.Size(1594, 1047);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel_MainWindow.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Exit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Minimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Fullscreen)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox BTN_Exit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel_MainWindow;
        private System.Windows.Forms.PictureBox BTN_Fullscreen;
        private System.Windows.Forms.PictureBox BTN_Minimize;
        private System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel MousedLabel;
        private System.Windows.Forms.ToolStripStatusLabel StatsLabel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel_ContentPanel;
    }
}