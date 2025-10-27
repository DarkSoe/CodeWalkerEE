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
            this.label_Name = new System.Windows.Forms.Label();
            this.label_itemType = new System.Windows.Forms.Label();
            this.panel_RenderImage = new System.Windows.Forms.Panel();
            this.img_fav = new System.Windows.Forms.PictureBox();
            this.img_thumbnail = new System.Windows.Forms.PictureBox();
            this.label_addEntry = new System.Windows.Forms.Label();
            this.img_save_clipboard = new System.Windows.Forms.PictureBox();
            this.img_plep_link = new System.Windows.Forms.PictureBox();
            this.img_save_name = new System.Windows.Forms.PictureBox();
            this.panel_RenderImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.img_fav)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img_thumbnail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img_save_clipboard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img_plep_link)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img_save_name)).BeginInit();
            this.SuspendLayout();
            // 
            // label_Name
            // 
            this.label_Name.AutoSize = true;
            this.label_Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Name.ForeColor = System.Drawing.SystemColors.Control;
            this.label_Name.Location = new System.Drawing.Point(40, 357);
            this.label_Name.Name = "label_Name";
            this.label_Name.Size = new System.Drawing.Size(159, 29);
            this.label_Name.TabIndex = 1;
            this.label_Name.Text = "DummyName";
            // 
            // label_itemType
            // 
            this.label_itemType.AutoSize = true;
            this.label_itemType.ForeColor = System.Drawing.SystemColors.Control;
            this.label_itemType.Location = new System.Drawing.Point(44, 471);
            this.label_itemType.MinimumSize = new System.Drawing.Size(300, 20);
            this.label_itemType.Name = "label_itemType";
            this.label_itemType.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label_itemType.Size = new System.Drawing.Size(300, 20);
            this.label_itemType.TabIndex = 2;
            this.label_itemType.Text = "DummyType";
            // 
            // panel_RenderImage
            // 
            this.panel_RenderImage.Controls.Add(this.img_fav);
            this.panel_RenderImage.Controls.Add(this.img_thumbnail);
            this.panel_RenderImage.Location = new System.Drawing.Point(0, 0);
            this.panel_RenderImage.Name = "panel_RenderImage";
            this.panel_RenderImage.Size = new System.Drawing.Size(350, 350);
            this.panel_RenderImage.TabIndex = 3;
            // 
            // img_fav
            // 
            this.img_fav.Image = global::CodeWalker.Properties.Resources.NoFavButton;
            this.img_fav.Location = new System.Drawing.Point(308, 7);
            this.img_fav.Name = "img_fav";
            this.img_fav.Size = new System.Drawing.Size(32, 32);
            this.img_fav.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.img_fav.TabIndex = 7;
            this.img_fav.TabStop = false;
            this.img_fav.Click += new System.EventHandler(this.img_fav_Click);
            // 
            // img_thumbnail
            // 
            this.img_thumbnail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.img_thumbnail.Location = new System.Drawing.Point(0, 0);
            this.img_thumbnail.Name = "img_thumbnail";
            this.img_thumbnail.Size = new System.Drawing.Size(350, 350);
            this.img_thumbnail.TabIndex = 0;
            this.img_thumbnail.TabStop = false;
            // 
            // label_addEntry
            // 
            this.label_addEntry.AutoSize = true;
            this.label_addEntry.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label_addEntry.ForeColor = System.Drawing.SystemColors.Control;
            this.label_addEntry.Location = new System.Drawing.Point(7, 357);
            this.label_addEntry.Name = "label_addEntry";
            this.label_addEntry.Size = new System.Drawing.Size(27, 29);
            this.label_addEntry.TabIndex = 4;
            this.label_addEntry.Text = "+";
            this.label_addEntry.Click += new System.EventHandler(this.label_addEntry_Click);
            // 
            // img_save_clipboard
            // 
            this.img_save_clipboard.Image = global::CodeWalker.Properties.Resources.clipboard;
            this.img_save_clipboard.Location = new System.Drawing.Point(270, 431);
            this.img_save_clipboard.Name = "img_save_clipboard";
            this.img_save_clipboard.Size = new System.Drawing.Size(32, 32);
            this.img_save_clipboard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.img_save_clipboard.TabIndex = 6;
            this.img_save_clipboard.TabStop = false;
            this.img_save_clipboard.Click += new System.EventHandler(this.img_save_clipboard_Click);
            // 
            // img_plep_link
            // 
            this.img_plep_link.Image = global::CodeWalker.Properties.Resources.plepicon;
            this.img_plep_link.Location = new System.Drawing.Point(308, 431);
            this.img_plep_link.Name = "img_plep_link";
            this.img_plep_link.Size = new System.Drawing.Size(32, 32);
            this.img_plep_link.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.img_plep_link.TabIndex = 5;
            this.img_plep_link.TabStop = false;
            this.img_plep_link.Click += new System.EventHandler(this.img_plep_link_Click);
            // 
            // img_save_name
            // 
            this.img_save_name.Image = global::CodeWalker.Properties.Resources.clipboard;
            this.img_save_name.Location = new System.Drawing.Point(4, 389);
            this.img_save_name.Name = "img_save_name";
            this.img_save_name.Size = new System.Drawing.Size(32, 32);
            this.img_save_name.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.img_save_name.TabIndex = 7;
            this.img_save_name.TabStop = false;
            this.img_save_name.Click += new System.EventHandler(this.img_save_name_Click);
            // 
            // ContentBrowserItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.Controls.Add(this.img_save_name);
            this.Controls.Add(this.img_save_clipboard);
            this.Controls.Add(this.img_plep_link);
            this.Controls.Add(this.label_addEntry);
            this.Controls.Add(this.panel_RenderImage);
            this.Controls.Add(this.label_itemType);
            this.Controls.Add(this.label_Name);
            this.Name = "ContentBrowserItem";
            this.Size = new System.Drawing.Size(350, 500);
            this.panel_RenderImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.img_fav)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img_thumbnail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img_save_clipboard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img_plep_link)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img_save_name)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label_Name;
        private System.Windows.Forms.Label label_itemType;
        private System.Windows.Forms.Panel panel_RenderImage;
        private System.Windows.Forms.PictureBox img_thumbnail;
        private System.Windows.Forms.Label label_addEntry;
        private System.Windows.Forms.PictureBox img_plep_link;
        private System.Windows.Forms.PictureBox img_save_clipboard;
        private System.Windows.Forms.PictureBox img_fav;
        private System.Windows.Forms.PictureBox img_save_name;
    }
}
