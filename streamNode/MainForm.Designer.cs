/*
 * Created by SharpDevelop.
 * User: AdamS-0
 * Date: 04.08.2025
 * Time: 16:41
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace streamNode
{
    partial class MainForm
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripLabel1;
        private System.Windows.Forms.Panel pConns;
        private System.Windows.Forms.ToolStripMenuItem tlStrip_cfgLoad;
        private System.Windows.Forms.ToolStripButton tlStrip_AddNewConn;
        private System.Windows.Forms.ToolStripMenuItem tlStrip_cfgSave;
        private System.Windows.Forms.ToolStripDropDownButton tlStrip_connections;
        private System.Windows.Forms.ToolStripMenuItem deleteAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tlStrpBtnDebugMode;
        
        /// <summary>
        /// Disposes resources used by the form.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                if (components != null) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        
        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.tlStrip_cfgLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.tlStrip_cfgSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tlStrip_AddNewConn = new System.Windows.Forms.ToolStripButton();
            this.tlStrip_connections = new System.Windows.Forms.ToolStripDropDownButton();
            this.deleteAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tlStrpBtnDebugMode = new System.Windows.Forms.ToolStripButton();
            this.pConns = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tlStrip_AddNewConn,
            this.tlStrip_connections,
            this.tlStrpBtnDebugMode});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(376, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlStrip_cfgLoad,
            this.tlStrip_cfgSave});
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(94, 22);
            this.toolStripLabel1.Text = "Configuration";
            // 
            // tlStrip_cfgLoad
            // 
            this.tlStrip_cfgLoad.Name = "tlStrip_cfgLoad";
            this.tlStrip_cfgLoad.Size = new System.Drawing.Size(102, 22);
            this.tlStrip_cfgLoad.Text = "Load";
            this.tlStrip_cfgLoad.Click += new System.EventHandler(this.TlStrip_cfgLoadClick);
            // 
            // tlStrip_cfgSave
            // 
            this.tlStrip_cfgSave.Name = "tlStrip_cfgSave";
            this.tlStrip_cfgSave.Size = new System.Drawing.Size(102, 22);
            this.tlStrip_cfgSave.Text = "Save";
            this.tlStrip_cfgSave.Click += new System.EventHandler(this.TlStrip_cfgSaveClick);
            // 
            // tlStrip_AddNewConn
            // 
            this.tlStrip_AddNewConn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlStrip_AddNewConn.Image = ((System.Drawing.Image)(resources.GetObject("tlStrip_AddNewConn.Image")));
            this.tlStrip_AddNewConn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlStrip_AddNewConn.Name = "tlStrip_AddNewConn";
            this.tlStrip_AddNewConn.Size = new System.Drawing.Size(99, 22);
            this.tlStrip_AddNewConn.Text = "New connection";
            this.tlStrip_AddNewConn.Click += new System.EventHandler(this.TlStrip_AddNewConnClick);
            // 
            // tlStrip_connections
            // 
            this.tlStrip_connections.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlStrip_connections.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteAllToolStripMenuItem});
            this.tlStrip_connections.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlStrip_connections.Name = "tlStrip_connections";
            this.tlStrip_connections.Size = new System.Drawing.Size(90, 22);
            this.tlStrip_connections.Text = "Connections";
            // 
            // deleteAllToolStripMenuItem
            // 
            this.deleteAllToolStripMenuItem.Name = "deleteAllToolStripMenuItem";
            this.deleteAllToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.deleteAllToolStripMenuItem.Text = "Delete all";
            this.deleteAllToolStripMenuItem.Click += new System.EventHandler(this.DeleteAllToolStripMenuItemClick);
            // 
            // tlStrpBtnDebugMode
            // 
            this.tlStrpBtnDebugMode.CheckOnClick = true;
            this.tlStrpBtnDebugMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlStrpBtnDebugMode.Image = ((System.Drawing.Image)(resources.GetObject("tlStrpBtnDebugMode.Image")));
            this.tlStrpBtnDebugMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlStrpBtnDebugMode.Name = "tlStrpBtnDebugMode";
            this.tlStrpBtnDebugMode.Size = new System.Drawing.Size(83, 22);
            this.tlStrpBtnDebugMode.Text = "Debug mode";
            // 
            // pConns
            // 
            this.pConns.AutoScroll = true;
            this.pConns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pConns.Location = new System.Drawing.Point(0, 25);
            this.pConns.Name = "pConns";
            this.pConns.Size = new System.Drawing.Size(376, 256);
            this.pConns.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 281);
            this.Controls.Add(this.pConns);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "streamNode";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
