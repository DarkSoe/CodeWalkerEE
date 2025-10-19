namespace CodeWalker.Utils
{
    partial class ContentBrowserItem
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.img_PropImage = new System.Windows.Forms.PictureBox();
            this.label_Name = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.img_PropImage)).BeginInit();
            this.SuspendLayout();
            // 
            // img_PropImage
            // 
            this.img_PropImage.Location = new System.Drawing.Point(0, 0);
            this.img_PropImage.Name = "img_PropImage";
            this.img_PropImage.Size = new System.Drawing.Size(350, 350);
            this.img_PropImage.TabIndex = 0;
            this.img_PropImage.TabStop = false;
            // 
            // label_Name
            // 
            this.label_Name.AutoSize = true;
            this.label_Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Name.ForeColor = System.Drawing.SystemColors.Control;
            this.label_Name.Location = new System.Drawing.Point(3, 357);
            this.label_Name.Name = "label_Name";
            this.label_Name.Size = new System.Drawing.Size(159, 29);
            this.label_Name.TabIndex = 1;
            this.label_Name.Text = "DummyName";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(44, 471);
            this.label1.MinimumSize = new System.Drawing.Size(300, 20);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(300, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "DummyType";
            this.label1.Visible = false;
            // 
            // ContentBrowserItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_Name);
            this.Controls.Add(this.img_PropImage);
            this.Name = "ContentBrowserItem";
            this.Size = new System.Drawing.Size(350, 500);
            ((System.ComponentModel.ISupportInitialize)(this.img_PropImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox img_PropImage;
        private System.Windows.Forms.Label label_Name;
        private System.Windows.Forms.Label label1;
    }
}
