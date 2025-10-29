namespace CodeWalker.Tools
{
    partial class RagePackForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RagePackForm));
            this.text_contentInFolderPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.text_ContentOutFolderPath = new System.Windows.Forms.TextBox();
            this.btn_inSelect = new System.Windows.Forms.Button();
            this.btn_outSelect = new System.Windows.Forms.Button();
            this.btn_packdlc = new System.Windows.Forms.Button();
            this.text_output = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // text_contentInFolderPath
            // 
            this.text_contentInFolderPath.Location = new System.Drawing.Point(4, 5);
            this.text_contentInFolderPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.text_contentInFolderPath.Name = "text_contentInFolderPath";
            this.text_contentInFolderPath.Size = new System.Drawing.Size(792, 26);
            this.text_contentInFolderPath.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 40);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Content In Folder:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 150);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "RPF Out Folder:";
            // 
            // text_ContentOutFolderPath
            // 
            this.text_ContentOutFolderPath.Location = new System.Drawing.Point(4, 5);
            this.text_ContentOutFolderPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.text_ContentOutFolderPath.Name = "text_ContentOutFolderPath";
            this.text_ContentOutFolderPath.Size = new System.Drawing.Size(792, 26);
            this.text_ContentOutFolderPath.TabIndex = 3;
            // 
            // btn_inSelect
            // 
            this.btn_inSelect.Location = new System.Drawing.Point(800, 0);
            this.btn_inSelect.Margin = new System.Windows.Forms.Padding(0);
            this.btn_inSelect.Name = "btn_inSelect";
            this.btn_inSelect.Size = new System.Drawing.Size(112, 35);
            this.btn_inSelect.TabIndex = 4;
            this.btn_inSelect.Text = "Select";
            this.btn_inSelect.UseVisualStyleBackColor = true;
            this.btn_inSelect.Click += new System.EventHandler(this.btn_inSelect_Click);
            // 
            // btn_outSelect
            // 
            this.btn_outSelect.Location = new System.Drawing.Point(800, 0);
            this.btn_outSelect.Margin = new System.Windows.Forms.Padding(0);
            this.btn_outSelect.Name = "btn_outSelect";
            this.btn_outSelect.Size = new System.Drawing.Size(112, 35);
            this.btn_outSelect.TabIndex = 5;
            this.btn_outSelect.Text = "Select";
            this.btn_outSelect.UseVisualStyleBackColor = true;
            this.btn_outSelect.Click += new System.EventHandler(this.btn_outSelect_Click);
            // 
            // btn_packdlc
            // 
            this.btn_packdlc.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_packdlc.Location = new System.Drawing.Point(963, 695);
            this.btn_packdlc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_packdlc.Name = "btn_packdlc";
            this.btn_packdlc.Size = new System.Drawing.Size(112, 65);
            this.btn_packdlc.TabIndex = 6;
            this.btn_packdlc.Text = "Pack DLC";
            this.btn_packdlc.UseVisualStyleBackColor = true;
            this.btn_packdlc.Click += new System.EventHandler(this.btn_packdlc_Click);
            // 
            // text_output
            // 
            this.text_output.Dock = System.Windows.Forms.DockStyle.Fill;
            this.text_output.Location = new System.Drawing.Point(4, 295);
            this.text_output.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.text_output.Multiline = true;
            this.text_output.Name = "text_output";
            this.text_output.ReadOnly = true;
            this.text_output.Size = new System.Drawing.Size(1071, 390);
            this.text_output.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 260);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Output:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_packdlc, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.text_output, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 10;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1079, 765);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 800F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.Controls.Add(this.text_contentInFolderPath, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btn_inSelect, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 70);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1079, 40);
            this.tableLayoutPanel2.TabIndex = 10;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 800F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.Controls.Add(this.text_ContentOutFolderPath, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btn_outSelect, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 180);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1079, 40);
            this.tableLayoutPanel3.TabIndex = 10;
            // 
            // RagePackForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1079, 765);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "RagePackForm";
            this.Text = "RagePackForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox text_contentInFolderPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox text_ContentOutFolderPath;
        private System.Windows.Forms.Button btn_inSelect;
        private System.Windows.Forms.Button btn_outSelect;
        private System.Windows.Forms.Button btn_packdlc;
        private System.Windows.Forms.TextBox text_output;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}