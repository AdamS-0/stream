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
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripLabel1;
        private System.Windows.Forms.Panel pConns;
        private System.Windows.Forms.ToolStripMenuItem tlStrip_cfgLoad;
        private System.Windows.Forms.ToolStripLabel tlStrip_AddNewConn;
        private System.Windows.Forms.ToolStripMenuItem tlStrip_cfgSave;
        private System.Windows.Forms.ToolStripDropDownButton tlStrip_connections;
        private System.Windows.Forms.ToolStripMenuItem deleteAllToolStripMenuItem;
        
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.tlStrip_cfgLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.tlStrip_cfgSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tlStrip_AddNewConn = new System.Windows.Forms.ToolStripLabel();
            this.pConns = new System.Windows.Forms.Panel();
            this.tlStrip_connections = new System.Windows.Forms.ToolStripDropDownButton();
            this.deleteAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.richTextBox1.Location = new System.Drawing.Point(0, 432);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(564, 59);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tlStrip_AddNewConn,
            this.tlStrip_connections});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(564, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlStrip_cfgLoad,
            this.tlStrip_cfgSave});
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(96, 22);
            this.toolStripLabel1.Text = "Configuration";
            // 
            // tlStrip_cfgLoad
            // 
            this.tlStrip_cfgLoad.Name = "tlStrip_cfgLoad";
            this.tlStrip_cfgLoad.Size = new System.Drawing.Size(104, 22);
            this.tlStrip_cfgLoad.Text = "Load";
            this.tlStrip_cfgLoad.Click += new System.EventHandler(this.TlStrip_cfgLoadClick);
            // 
            // tlStrip_cfgSave
            // 
            this.tlStrip_cfgSave.Name = "tlStrip_cfgSave";
            this.tlStrip_cfgSave.Size = new System.Drawing.Size(104, 22);
            this.tlStrip_cfgSave.Text = "Save";
            this.tlStrip_cfgSave.Click += new System.EventHandler(this.TlStrip_cfgSaveClick);
            // 
            // tlStrip_AddNewConn
            // 
            this.tlStrip_AddNewConn.Name = "tlStrip_AddNewConn";
            this.tlStrip_AddNewConn.Size = new System.Drawing.Size(31, 22);
            this.tlStrip_AddNewConn.Text = "Add";
            this.tlStrip_AddNewConn.Click += new System.EventHandler(this.TlStrip_AddNewConnClick);
            // 
            // pConns
            // 
            this.pConns.AutoScroll = true;
            this.pConns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pConns.Location = new System.Drawing.Point(0, 25);
            this.pConns.Name = "pConns";
            this.pConns.Size = new System.Drawing.Size(564, 407);
            this.pConns.TabIndex = 2;
            // 
            // tlStrip_connections
            // 
            this.tlStrip_connections.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlStrip_connections.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteAllToolStripMenuItem});
            this.tlStrip_connections.Image = ((System.Drawing.Image)(resources.GetObject("tlStrip_connections.Image")));
            this.tlStrip_connections.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlStrip_connections.Name = "tlStrip_connections";
            this.tlStrip_connections.Size = new System.Drawing.Size(93, 22);
            this.tlStrip_connections.Text = "Connections";
            // 
            // deleteAllToolStripMenuItem
            // 
            this.deleteAllToolStripMenuItem.Name = "deleteAllToolStripMenuItem";
            this.deleteAllToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.deleteAllToolStripMenuItem.Text = "Delete all";
            this.deleteAllToolStripMenuItem.Click += new System.EventHandler(this.DeleteAllToolStripMenuItemClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 491);
            this.Controls.Add(this.pConns);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.richTextBox1);
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
