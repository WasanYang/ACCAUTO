namespace AccAutoKey.Page
{
    partial class JobPage
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.controlPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();

            this.controlPanel.AutoScroll = true;
            this.controlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlPanel.Location = new System.Drawing.Point(0, 0);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Size = new System.Drawing.Size(150, 150);
            this.controlPanel.TabIndex = 0;

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.controlPanel);
            this.Load += new System.EventHandler(this.JobPage_Load);
            this.Name = "JobPage";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel controlPanel;
    }
}
