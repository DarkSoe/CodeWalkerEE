namespace CodeWalker.Utils
{
    partial class ContentObjectPreviewForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.panelRenderHost = new System.Windows.Forms.Panel();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.labelTagsValue = new System.Windows.Forms.Label();
            this.labelTagsCaption = new System.Windows.Forms.Label();
            this.labelFlagsValue = new System.Windows.Forms.Label();
            this.labelFlagsCaption = new System.Windows.Forms.Label();
            this.labelNameValue = new System.Windows.Forms.Label();
            this.labelNameCaption = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.panelInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.panelRenderHost);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.panelInfo);
            this.splitContainerMain.Size = new System.Drawing.Size(884, 561);
            this.splitContainerMain.SplitterDistance = 640;
            this.splitContainerMain.TabIndex = 0;
            // 
            // panelRenderHost
            // 
            this.panelRenderHost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(139)))), ((int)(((byte)(173)))));
            this.panelRenderHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRenderHost.Location = new System.Drawing.Point(0, 0);
            this.panelRenderHost.Name = "panelRenderHost";
            this.panelRenderHost.Size = new System.Drawing.Size(640, 561);
            this.panelRenderHost.TabIndex = 0;
            // 
            // panelInfo
            // 
            this.panelInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.panelInfo.Controls.Add(this.labelTagsValue);
            this.panelInfo.Controls.Add(this.labelTagsCaption);
            this.panelInfo.Controls.Add(this.labelFlagsValue);
            this.panelInfo.Controls.Add(this.labelFlagsCaption);
            this.panelInfo.Controls.Add(this.labelNameValue);
            this.panelInfo.Controls.Add(this.labelNameCaption);
            this.panelInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelInfo.Location = new System.Drawing.Point(0, 0);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Padding = new System.Windows.Forms.Padding(12);
            this.panelInfo.Size = new System.Drawing.Size(240, 561);
            this.panelInfo.TabIndex = 0;
            // 
            // labelTagsValue
            // 
            this.labelTagsValue.AutoSize = true;
            this.labelTagsValue.ForeColor = System.Drawing.SystemColors.Control;
            this.labelTagsValue.Location = new System.Drawing.Point(15, 152);
            this.labelTagsValue.MaximumSize = new System.Drawing.Size(210, 0);
            this.labelTagsValue.Name = "labelTagsValue";
            this.labelTagsValue.Size = new System.Drawing.Size(13, 20);
            this.labelTagsValue.TabIndex = 5;
            this.labelTagsValue.Text = "—";
            // 
            // labelTagsCaption
            // 
            this.labelTagsCaption.AutoSize = true;
            this.labelTagsCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.labelTagsCaption.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.labelTagsCaption.Location = new System.Drawing.Point(15, 124);
            this.labelTagsCaption.Name = "labelTagsCaption";
            this.labelTagsCaption.Size = new System.Drawing.Size(52, 22);
            this.labelTagsCaption.TabIndex = 4;
            this.labelTagsCaption.Text = "Tags";
            // 
            // labelFlagsValue
            // 
            this.labelFlagsValue.AutoSize = true;
            this.labelFlagsValue.ForeColor = System.Drawing.SystemColors.Control;
            this.labelFlagsValue.Location = new System.Drawing.Point(15, 92);
            this.labelFlagsValue.MaximumSize = new System.Drawing.Size(210, 0);
            this.labelFlagsValue.Name = "labelFlagsValue";
            this.labelFlagsValue.Size = new System.Drawing.Size(13, 20);
            this.labelFlagsValue.TabIndex = 3;
            this.labelFlagsValue.Text = "—";
            // 
            // labelFlagsCaption
            // 
            this.labelFlagsCaption.AutoSize = true;
            this.labelFlagsCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.labelFlagsCaption.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.labelFlagsCaption.Location = new System.Drawing.Point(15, 64);
            this.labelFlagsCaption.Name = "labelFlagsCaption";
            this.labelFlagsCaption.Size = new System.Drawing.Size(58, 22);
            this.labelFlagsCaption.TabIndex = 2;
            this.labelFlagsCaption.Text = "Flags";
            // 
            // labelNameValue
            // 
            this.labelNameValue.AutoSize = true;
            this.labelNameValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.labelNameValue.ForeColor = System.Drawing.SystemColors.Control;
            this.labelNameValue.Location = new System.Drawing.Point(15, 36);
            this.labelNameValue.MaximumSize = new System.Drawing.Size(210, 0);
            this.labelNameValue.Name = "labelNameValue";
            this.labelNameValue.Size = new System.Drawing.Size(17, 25);
            this.labelNameValue.TabIndex = 1;
            this.labelNameValue.Text = "—";
            // 
            // labelNameCaption
            // 
            this.labelNameCaption.AutoSize = true;
            this.labelNameCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.labelNameCaption.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.labelNameCaption.Location = new System.Drawing.Point(15, 12);
            this.labelNameCaption.Name = "labelNameCaption";
            this.labelNameCaption.Size = new System.Drawing.Size(62, 22);
            this.labelNameCaption.TabIndex = 0;
            this.labelNameCaption.Text = "Name";
            // 
            // ContentObjectPreviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.splitContainerMain);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "ContentObjectPreviewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Object Preview";
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.panelInfo.ResumeLayout(false);
            this.panelInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.Panel panelRenderHost;
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.Label labelNameCaption;
        private System.Windows.Forms.Label labelNameValue;
        private System.Windows.Forms.Label labelFlagsCaption;
        private System.Windows.Forms.Label labelFlagsValue;
        private System.Windows.Forms.Label labelTagsCaption;
        private System.Windows.Forms.Label labelTagsValue;
    }
}
