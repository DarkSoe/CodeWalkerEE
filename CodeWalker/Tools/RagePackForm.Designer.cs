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
            this.SuspendLayout();
            // 
            // text_contentInFolderPath
            // 
            this.text_contentInFolderPath.Location = new System.Drawing.Point(12, 46);
            this.text_contentInFolderPath.Name = "text_contentInFolderPath";
            this.text_contentInFolderPath.Size = new System.Drawing.Size(581, 20);
            this.text_contentInFolderPath.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Content In Folder:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "RPF Out Folder:";
            // 
            // text_ContentOutFolderPath
            // 
            this.text_ContentOutFolderPath.Location = new System.Drawing.Point(12, 134);
            this.text_ContentOutFolderPath.Name = "text_ContentOutFolderPath";
            this.text_ContentOutFolderPath.Size = new System.Drawing.Size(581, 20);
            this.text_ContentOutFolderPath.TabIndex = 3;
            // 
            // btn_inSelect
            // 
            this.btn_inSelect.Location = new System.Drawing.Point(599, 46);
            this.btn_inSelect.Name = "btn_inSelect";
            this.btn_inSelect.Size = new System.Drawing.Size(75, 23);
            this.btn_inSelect.TabIndex = 4;
            this.btn_inSelect.Text = "Select";
            this.btn_inSelect.UseVisualStyleBackColor = true;
            this.btn_inSelect.Click += new System.EventHandler(this.btn_inSelect_Click);
            // 
            // btn_outSelect
            // 
            this.btn_outSelect.Location = new System.Drawing.Point(599, 134);
            this.btn_outSelect.Name = "btn_outSelect";
            this.btn_outSelect.Size = new System.Drawing.Size(75, 23);
            this.btn_outSelect.TabIndex = 5;
            this.btn_outSelect.Text = "Select";
            this.btn_outSelect.UseVisualStyleBackColor = true;
            this.btn_outSelect.Click += new System.EventHandler(this.btn_outSelect_Click);
            // 
            // btn_packdlc
            // 
            this.btn_packdlc.Location = new System.Drawing.Point(599, 462);
            this.btn_packdlc.Name = "btn_packdlc";
            this.btn_packdlc.Size = new System.Drawing.Size(75, 23);
            this.btn_packdlc.TabIndex = 6;
            this.btn_packdlc.Text = "Pack DLC";
            this.btn_packdlc.UseVisualStyleBackColor = true;
            this.btn_packdlc.Click += new System.EventHandler(this.btn_packdlc_Click);
            // 
            // text_output
            // 
            this.text_output.Location = new System.Drawing.Point(15, 233);
            this.text_output.Multiline = true;
            this.text_output.Name = "text_output";
            this.text_output.ReadOnly = true;
            this.text_output.Size = new System.Drawing.Size(659, 223);
            this.text_output.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 217);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Output:";
            // 
            // RagePackForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 497);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.text_output);
            this.Controls.Add(this.btn_packdlc);
            this.Controls.Add(this.btn_outSelect);
            this.Controls.Add(this.btn_inSelect);
            this.Controls.Add(this.text_ContentOutFolderPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.text_contentInFolderPath);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RagePackForm";
            this.Text = "RagePackForm";
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}