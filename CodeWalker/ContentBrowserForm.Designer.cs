namespace CodeWalker
{
    partial class ContentBrowserForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContentBrowserForm));
            this.timer_justUndocked = new System.Windows.Forms.Timer(this.components);
            this.panel_ContentBrowser = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label_currentPage = new System.Windows.Forms.Label();
            this.btn_NextPage = new System.Windows.Forms.Button();
            this.btn_PrevPage = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox_search = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_search = new System.Windows.Forms.Button();
            this.panel_ContentBrowser.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer_justUndocked
            // 
            this.timer_justUndocked.Interval = 1000;
            this.timer_justUndocked.Tick += new System.EventHandler(this.timer_justUndocked_Tick);
            // 
            // panel_ContentBrowser
            // 
            this.panel_ContentBrowser.AutoScroll = true;
            this.panel_ContentBrowser.Controls.Add(this.listBox1);
            this.panel_ContentBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_ContentBrowser.Location = new System.Drawing.Point(3, 83);
            this.panel_ContentBrowser.Name = "panel_ContentBrowser";
            this.panel_ContentBrowser.Size = new System.Drawing.Size(972, 478);
            this.panel_ContentBrowser.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel_ContentBrowser, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(978, 644);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Location = new System.Drawing.Point(3, 3);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(722, 0);
            this.listBox1.TabIndex = 0;
            // 
            // label_currentPage
            // 
            this.label_currentPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label_currentPage.AutoSize = true;
            this.label_currentPage.ForeColor = System.Drawing.SystemColors.Control;
            this.label_currentPage.Location = new System.Drawing.Point(430, 30);
            this.label_currentPage.MaximumSize = new System.Drawing.Size(140, 20);
            this.label_currentPage.MinimumSize = new System.Drawing.Size(140, 20);
            this.label_currentPage.Name = "label_currentPage";
            this.label_currentPage.Size = new System.Drawing.Size(140, 20);
            this.label_currentPage.TabIndex = 0;
            this.label_currentPage.Text = "1";
            this.label_currentPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_NextPage
            // 
            this.btn_NextPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_NextPage.BackColor = System.Drawing.Color.DimGray;
            this.btn_NextPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_NextPage.ForeColor = System.Drawing.SystemColors.Control;
            this.btn_NextPage.Location = new System.Drawing.Point(576, 15);
            this.btn_NextPage.MaximumSize = new System.Drawing.Size(50, 50);
            this.btn_NextPage.MinimumSize = new System.Drawing.Size(50, 50);
            this.btn_NextPage.Name = "btn_NextPage";
            this.btn_NextPage.Size = new System.Drawing.Size(50, 50);
            this.btn_NextPage.TabIndex = 1;
            this.btn_NextPage.Text = ">";
            this.btn_NextPage.UseVisualStyleBackColor = false;
            this.btn_NextPage.Click += new System.EventHandler(this.btn_NextPage_Click);
            // 
            // btn_PrevPage
            // 
            this.btn_PrevPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_PrevPage.BackColor = System.Drawing.Color.DimGray;
            this.btn_PrevPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PrevPage.ForeColor = System.Drawing.SystemColors.Control;
            this.btn_PrevPage.Location = new System.Drawing.Point(374, 15);
            this.btn_PrevPage.MaximumSize = new System.Drawing.Size(50, 50);
            this.btn_PrevPage.MinimumSize = new System.Drawing.Size(50, 50);
            this.btn_PrevPage.Name = "btn_PrevPage";
            this.btn_PrevPage.Size = new System.Drawing.Size(50, 50);
            this.btn_PrevPage.TabIndex = 2;
            this.btn_PrevPage.Text = "<";
            this.btn_PrevPage.UseVisualStyleBackColor = false;
            this.btn_PrevPage.Click += new System.EventHandler(this.btn_PrevPage_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_PrevPage);
            this.panel1.Controls.Add(this.btn_NextPage);
            this.panel1.Controls.Add(this.label_currentPage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 567);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(972, 74);
            this.panel1.TabIndex = 1;
            // 
            // textBox_search
            // 
            this.textBox_search.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBox_search.BackColor = System.Drawing.Color.DimGray;
            this.textBox_search.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_search.ForeColor = System.Drawing.SystemColors.Control;
            this.textBox_search.Location = new System.Drawing.Point(291, 19);
            this.textBox_search.Name = "textBox_search";
            this.textBox_search.Size = new System.Drawing.Size(300, 35);
            this.textBox_search.TabIndex = 2;
            this.textBox_search.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_search_KeyDown);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_search);
            this.panel2.Controls.Add(this.textBox_search);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(972, 74);
            this.panel2.TabIndex = 2;
            // 
            // btn_search
            // 
            this.btn_search.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_search.BackColor = System.Drawing.Color.DimGray;
            this.btn_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_search.ForeColor = System.Drawing.SystemColors.Control;
            this.btn_search.Location = new System.Drawing.Point(604, 19);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(75, 35);
            this.btn_search.TabIndex = 3;
            this.btn_search.Text = "search";
            this.btn_search.UseVisualStyleBackColor = false;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // ContentBrowserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 644);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ContentBrowserForm";
            this.Text = "ContentBrowserForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ContentBrowserForm_FormClosing);
            this.Move += new System.EventHandler(this.ContentBrowserForm_Move);
            this.panel_ContentBrowser.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer_justUndocked;
        private System.Windows.Forms.FlowLayoutPanel panel_ContentBrowser;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_PrevPage;
        private System.Windows.Forms.Button btn_NextPage;
        private System.Windows.Forms.Label label_currentPage;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.TextBox textBox_search;
    }
}