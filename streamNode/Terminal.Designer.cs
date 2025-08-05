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
            this.rtbTerm = new System.Windows.Forms.RichTextBox();
            this.txtBxData2Send = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
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
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(464, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
