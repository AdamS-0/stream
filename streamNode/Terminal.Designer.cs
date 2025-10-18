/*
 * Created by SharpDevelop.
 * User: AdamS-0
 * Date: 05.08.2025
 * Time: 16:45
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace streamNode
{
    partial class Terminal
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.RichTextBox rtbTerm;
        private System.Windows.Forms.TextBox txtBxData2Send;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tlStrip_clear;
        private System.Windows.Forms.ToolStripButton tlStrip_autoscroll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox tlStrip_newline;
        
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Terminal));
            this.rtbTerm = new System.Windows.Forms.RichTextBox();
            this.txtBxData2Send = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tlStrip_clear = new System.Windows.Forms.ToolStripButton();
            this.tlStrip_autoscroll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tlStrip_newline = new System.Windows.Forms.ToolStripComboBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbTerm
            // 
            this.rtbTerm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbTerm.Location = new System.Drawing.Point(0, 25);
            this.rtbTerm.Name = "rtbTerm";
            this.rtbTerm.ReadOnly = true;
            this.rtbTerm.Size = new System.Drawing.Size(464, 276);
            this.rtbTerm.TabIndex = 1;
            this.rtbTerm.Text = "";
            // 
            // txtBxData2Send
            // 
            this.txtBxData2Send.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtBxData2Send.Location = new System.Drawing.Point(0, 301);
            this.txtBxData2Send.Name = "txtBxData2Send";
            this.txtBxData2Send.Size = new System.Drawing.Size(464, 20);
            this.txtBxData2Send.TabIndex = 2;
            this.txtBxData2Send.TextChanged += new System.EventHandler(this.TxtBxData2SendTextChanged);
            this.txtBxData2Send.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBxData2SendKeyDown);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlStrip_clear,
            this.tlStrip_autoscroll,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.tlStrip_newline});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(464, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tlStrip_clear
            // 
            this.tlStrip_clear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlStrip_clear.Image = ((System.Drawing.Image)(resources.GetObject("tlStrip_clear.Image")));
            this.tlStrip_clear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlStrip_clear.Name = "tlStrip_clear";
            this.tlStrip_clear.Size = new System.Drawing.Size(42, 22);
            this.tlStrip_clear.Text = "Clear";
            this.tlStrip_clear.Click += new System.EventHandler(this.TlStrip_clearClick);
            // 
            // tlStrip_autoscroll
            // 
            this.tlStrip_autoscroll.Checked = true;
            this.tlStrip_autoscroll.CheckOnClick = true;
            this.tlStrip_autoscroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tlStrip_autoscroll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlStrip_autoscroll.Image = ((System.Drawing.Image)(resources.GetObject("tlStrip_autoscroll.Image")));
            this.tlStrip_autoscroll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlStrip_autoscroll.Name = "tlStrip_autoscroll";
            this.tlStrip_autoscroll.Size = new System.Drawing.Size(70, 22);
            this.tlStrip_autoscroll.Text = "Autoscroll";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(57, 22);
            this.toolStripLabel1.Text = "New line";
            // 
            // tlStrip_newline
            // 
            this.tlStrip_newline.AutoSize = false;
            this.tlStrip_newline.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tlStrip_newline.Items.AddRange(new object[] {
            "CR+LF",
            "LF"});
            this.tlStrip_newline.Name = "tlStrip_newline";
            this.tlStrip_newline.Size = new System.Drawing.Size(65, 23);
            this.tlStrip_newline.Text = "CR+LF";
            // 
            // Terminal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 321);
            this.Controls.Add(this.rtbTerm);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.txtBxData2Send);
            this.Name = "Terminal";
            this.Text = "Terminal";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TerminalFormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
