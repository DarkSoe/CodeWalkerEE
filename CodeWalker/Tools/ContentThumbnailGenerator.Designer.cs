namespace CodeWalker.Tools
{
    partial class ContentThumbnailGenerator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContentThumbnailGenerator));
            this.label1 = new System.Windows.Forms.Label();
            this.panel_Renderer = new System.Windows.Forms.Panel();
            this.btn_generateButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(342, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "To generate the Thumbnails this window needs to be visible on screen.";
            // 
            // panel_Renderer
            // 
            this.panel_Renderer.Location = new System.Drawing.Point(15, 30);
            this.panel_Renderer.Name = "panel_Renderer";
            this.panel_Renderer.Size = new System.Drawing.Size(350, 350);
            this.panel_Renderer.TabIndex = 1;
            // 
            // btn_generateButton
            // 
            this.btn_generateButton.Location = new System.Drawing.Point(290, 386);
            this.btn_generateButton.Name = "btn_generateButton";
            this.btn_generateButton.Size = new System.Drawing.Size(75, 23);
            this.btn_generateButton.TabIndex = 2;
            this.btn_generateButton.Text = "Generate";
            this.btn_generateButton.UseVisualStyleBackColor = true;
            this.btn_generateButton.Click += new System.EventHandler(this.btn_generateButton_Click);
            // 
            // ContentThumbnailGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 420);
            this.Controls.Add(this.btn_generateButton);
            this.Controls.Add(this.panel_Renderer);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(399, 459);
            this.MinimumSize = new System.Drawing.Size(399, 459);
            this.Name = "ContentThumbnailGenerator";
            this.Text = "ContentThumbnailGenerator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel_Renderer;
        private System.Windows.Forms.Button btn_generateButton;
    }
}