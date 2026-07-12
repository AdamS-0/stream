/*
 * Created by SharpDevelop.
 * User: AdamS-0
 * Date: 05.08.2025
 * Time: 07:28
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace streamNode
{
    partial class ConnPanel
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.CheckBox chkBxWrite;
        private System.Windows.Forms.CheckBox chkBxRead;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.TextBox txtBxPID;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtBxCmd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        
        /// <summary>
        /// Disposes resources used by the control.
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
            this.btnDelete = new System.Windows.Forms.Button();
            this.chkBxWrite = new System.Windows.Forms.CheckBox();
            this.chkBxRead = new System.Windows.Forms.CheckBox();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.txtBxPID = new System.Windows.Forms.TextBox();
            this.txtBxCmd = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDelete
            // 
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDelete.Location = new System.Drawing.Point(567, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(46, 37);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.BtnDeleteClick);
            // 
            // chkBxWrite
            // 
            this.chkBxWrite.AutoSize = true;
            this.chkBxWrite.Checked = true;
            this.chkBxWrite.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBxWrite.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkBxWrite.Location = new System.Drawing.Point(3, 17);
            this.chkBxWrite.Name = "chkBxWrite";
            this.chkBxWrite.Size = new System.Drawing.Size(77, 17);
            this.chkBxWrite.TabIndex = 2;
            this.chkBxWrite.Text = "Allow OUT";
            this.chkBxWrite.UseVisualStyleBackColor = true;
            // 
            // chkBxRead
            // 
            this.chkBxRead.AutoSize = true;
            this.chkBxRead.Checked = true;
            this.chkBxRead.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBxRead.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkBxRead.Location = new System.Drawing.Point(3, 0);
            this.chkBxRead.Name = "chkBxRead";
            this.chkBxRead.Size = new System.Drawing.Size(77, 17);
            this.chkBxRead.TabIndex = 1;
            this.chkBxRead.Text = "Allow IN";
            this.chkBxRead.UseVisualStyleBackColor = true;
            // 
            // btnStartStop
            // 
            this.btnStartStop.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnStartStop.Location = new System.Drawing.Point(42, 0);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(46, 37);
            this.btnStartStop.TabIndex = 0;
            this.btnStartStop.Text = "Open";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.BtnOpenClick);
            // 
            // txtBxPID
            // 
            this.txtBxPID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBxPID.Location = new System.Drawing.Point(0, 8);
            this.txtBxPID.Name = "txtBxPID";
            this.txtBxPID.ReadOnly = true;
            this.txtBxPID.Size = new System.Drawing.Size(42, 20);
            this.txtBxPID.TabIndex = 0;
            this.txtBxPID.Text = "---";
            // 
            // txtBxCmd
            // 
            this.txtBxCmd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBxCmd.Location = new System.Drawing.Point(3, 8);
            this.txtBxCmd.Name = "txtBxCmd";
            this.txtBxCmd.Size = new System.Drawing.Size(393, 20);
            this.txtBxCmd.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtBxPID);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panel1.Size = new System.Drawing.Size(42, 37);
            this.panel1.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtBxCmd);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(168, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.panel2.Size = new System.Drawing.Size(399, 37);
            this.panel2.TabIndex = 8;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.chkBxWrite);
            this.panel3.Controls.Add(this.chkBxRead);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(88, 0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.panel3.Size = new System.Drawing.Size(80, 37);
            this.panel3.TabIndex = 9;
            // 
            // ConnPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnStartStop);
            this.Controls.Add(this.panel1);
            this.Name = "ConnPanel";
            this.Size = new System.Drawing.Size(613, 37);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
