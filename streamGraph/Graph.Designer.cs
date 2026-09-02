/*
 * Created by SharpDevelop.
 * User: AdamS-0
 * Date: 18.05.2025
 * Time: 15:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace streamGraph
{
    partial class Graph
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private VisBox.Plot plot1;
        private System.Windows.Forms.ContextMenuStrip cntxMn1;
        private System.Windows.Forms.ToolStripMenuItem alwaysOnTopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem borderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propertyExplorerToolStripMenuItem;
        
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
            this.components = new System.ComponentModel.Container();
            this.plot1 = new VisBox.Plot();
            this.cntxMn1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.alwaysOnTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.borderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertyExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cntxMn1.SuspendLayout();
            this.SuspendLayout();
            // 
            // plot1
            // 
            this.plot1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.plot1.ContextMenuStrip = this.cntxMn1;
            this.plot1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plot1.FlatStyle = VisBox.VisElement.VisFlatStyle.Collapsed;
            this.plot1.Location = new System.Drawing.Point(0, 0);
            this.plot1.Name = "plot1";
            this.plot1.Size = new System.Drawing.Size(284, 261);
            this.plot1.TabIndex = 0;
            this.plot1.Text = "plot1";
            // 
            // cntxMn1
            // 
            this.cntxMn1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alwaysOnTopToolStripMenuItem,
            this.borderToolStripMenuItem,
            this.propertyExplorerToolStripMenuItem});
            this.cntxMn1.Name = "cntxMn1";
            this.cntxMn1.Size = new System.Drawing.Size(167, 92);
            // 
            // alwaysOnTopToolStripMenuItem
            // 
            this.alwaysOnTopToolStripMenuItem.Checked = true;
            this.alwaysOnTopToolStripMenuItem.CheckOnClick = true;
            this.alwaysOnTopToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.alwaysOnTopToolStripMenuItem.Name = "alwaysOnTopToolStripMenuItem";
            this.alwaysOnTopToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.alwaysOnTopToolStripMenuItem.Text = "Always on top";
            this.alwaysOnTopToolStripMenuItem.CheckedChanged += new System.EventHandler(this.AlwaysOnTopToolStripMenuItemCheckedChanged);
            // 
            // borderToolStripMenuItem
            // 
            this.borderToolStripMenuItem.Checked = true;
            this.borderToolStripMenuItem.CheckOnClick = true;
            this.borderToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.borderToolStripMenuItem.Name = "borderToolStripMenuItem";
            this.borderToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.borderToolStripMenuItem.Text = "Border";
            this.borderToolStripMenuItem.CheckedChanged += new System.EventHandler(this.BorderToolStripMenuItemCheckedChanged);
            // 
            // propertyExplorerToolStripMenuItem
            // 
            this.propertyExplorerToolStripMenuItem.CheckOnClick = true;
            this.propertyExplorerToolStripMenuItem.Name = "propertyExplorerToolStripMenuItem";
            this.propertyExplorerToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.propertyExplorerToolStripMenuItem.Text = "Property explorer";
            this.propertyExplorerToolStripMenuItem.CheckedChanged += new System.EventHandler(this.PropertyExplorerToolStripMenuItemCheckedChanged);
            // 
            // Graph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.plot1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Graph";
            this.Text = "Graph";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.GraphLoad);
            this.cntxMn1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
