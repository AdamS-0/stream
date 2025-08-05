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
        private System.Windows.Forms.GroupBox grpBxConnType;
        private System.Windows.Forms.RadioButton rdBtnMult;
        private System.Windows.Forms.RadioButton rdBtnUDP;
        private System.Windows.Forms.RadioButton rdBtnTCP;
        private System.Windows.Forms.RadioButton rdBtnSerial;
        private System.Windows.Forms.Panel pConnInfo;
        private System.Windows.Forms.CheckBox chkBxWrite;
        private System.Windows.Forms.CheckBox chkBxRead;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtBxName;
        private System.Windows.Forms.Panel pConnSerial;
        private System.Windows.Forms.GroupBox grpBxParity;
        private System.Windows.Forms.RadioButton rdBtnParity_Space;
        private System.Windows.Forms.RadioButton rdBtnParity_Mark;
        private System.Windows.Forms.RadioButton rdBtnParity_Even;
        private System.Windows.Forms.RadioButton rdBtnParity_Odd;
        private System.Windows.Forms.RadioButton rdBtnParity_None;
        private System.Windows.Forms.Panel pSerBaud;
        private System.Windows.Forms.ComboBox cmbBxBaud;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grpBxHdwFlowCntrl;
        private System.Windows.Forms.RadioButton rdBtnHdwFlwCtrl_None;
        private System.Windows.Forms.RadioButton rdBtnHdwFlwCtrl_RTS_CTS;
        private System.Windows.Forms.RadioButton rdBtnHdwFlwCtrl_DTR_DSR;
        private System.Windows.Forms.GroupBox grpBxDataBits;
        private System.Windows.Forms.RadioButton rdBtnDataBits_5;
        private System.Windows.Forms.RadioButton rdBtnDataBits_6;
        private System.Windows.Forms.RadioButton rdBtnDataBits_7;
        private System.Windows.Forms.RadioButton rdBtnDataBits_8;
        private System.Windows.Forms.Panel pSerPort;
        private System.Windows.Forms.ComboBox cmbBxPorts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpBxStopBits;
        private System.Windows.Forms.RadioButton rdBtnStopBits_2bits;
        private System.Windows.Forms.RadioButton rdBtnStopBits_1_5;
        private System.Windows.Forms.RadioButton rdBtnStopBits_1bit;
        private System.Windows.Forms.RadioButton rdBtnStopBits_None;
        private System.Windows.Forms.Panel pConnTcp;
        private System.Windows.Forms.Panel pConnUdp;
        private System.Windows.Forms.Panel pConnMult;
        private System.Windows.Forms.GroupBox grpBxTypeTcp;
        private System.Windows.Forms.RadioButton rdBtnTCPType_server;
        private System.Windows.Forms.RadioButton rdBtnTCPType_client;
        private System.Windows.Forms.Panel pTcpAdr;
        private System.Windows.Forms.TextBox txtBxTcpAdr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pTcpPort;
        private System.Windows.Forms.TextBox txtBxTcpPort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pUdpAdr;
        private System.Windows.Forms.TextBox txtBxUdpAdr;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pUdpPort;
        private System.Windows.Forms.TextBox txtBxUdpPort;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox grpBxTypeUdp;
        private System.Windows.Forms.RadioButton rdBtnUdpType_server;
        private System.Windows.Forms.RadioButton rdBtnUdpType_client;
        private System.Windows.Forms.Panel pMultAdr;
        private System.Windows.Forms.TextBox txtBxMultAdr;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel pMultPort;
        private System.Windows.Forms.TextBox txtBxMultPort;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel pCfg;
        private System.Windows.Forms.GroupBox grpBxCfgArgsLine;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtBxCfg;
        private System.Windows.Forms.Button btnCfgGet;
        private System.Windows.Forms.Button btnCfgSet;
        private System.Windows.Forms.Button btnTerminal;
        
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
            this.grpBxConnType = new System.Windows.Forms.GroupBox();
            this.rdBtnMult = new System.Windows.Forms.RadioButton();
            this.rdBtnUDP = new System.Windows.Forms.RadioButton();
            this.rdBtnTCP = new System.Windows.Forms.RadioButton();
            this.rdBtnSerial = new System.Windows.Forms.RadioButton();
            this.pConnInfo = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.chkBxWrite = new System.Windows.Forms.CheckBox();
            this.chkBxRead = new System.Windows.Forms.CheckBox();
            this.btnTerminal = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtBxName = new System.Windows.Forms.TextBox();
            this.pConnSerial = new System.Windows.Forms.Panel();
            this.grpBxParity = new System.Windows.Forms.GroupBox();
            this.rdBtnParity_Space = new System.Windows.Forms.RadioButton();
            this.rdBtnParity_Mark = new System.Windows.Forms.RadioButton();
            this.rdBtnParity_Even = new System.Windows.Forms.RadioButton();
            this.rdBtnParity_Odd = new System.Windows.Forms.RadioButton();
            this.rdBtnParity_None = new System.Windows.Forms.RadioButton();
            this.pSerBaud = new System.Windows.Forms.Panel();
            this.cmbBxBaud = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grpBxHdwFlowCntrl = new System.Windows.Forms.GroupBox();
            this.rdBtnHdwFlwCtrl_None = new System.Windows.Forms.RadioButton();
            this.rdBtnHdwFlwCtrl_RTS_CTS = new System.Windows.Forms.RadioButton();
            this.rdBtnHdwFlwCtrl_DTR_DSR = new System.Windows.Forms.RadioButton();
            this.grpBxDataBits = new System.Windows.Forms.GroupBox();
            this.rdBtnDataBits_5 = new System.Windows.Forms.RadioButton();
            this.rdBtnDataBits_6 = new System.Windows.Forms.RadioButton();
            this.rdBtnDataBits_7 = new System.Windows.Forms.RadioButton();
            this.rdBtnDataBits_8 = new System.Windows.Forms.RadioButton();
            this.pSerPort = new System.Windows.Forms.Panel();
            this.cmbBxPorts = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpBxStopBits = new System.Windows.Forms.GroupBox();
            this.rdBtnStopBits_2bits = new System.Windows.Forms.RadioButton();
            this.rdBtnStopBits_1_5 = new System.Windows.Forms.RadioButton();
            this.rdBtnStopBits_1bit = new System.Windows.Forms.RadioButton();
            this.rdBtnStopBits_None = new System.Windows.Forms.RadioButton();
            this.pConnTcp = new System.Windows.Forms.Panel();
            this.grpBxTypeTcp = new System.Windows.Forms.GroupBox();
            this.rdBtnTCPType_server = new System.Windows.Forms.RadioButton();
            this.rdBtnTCPType_client = new System.Windows.Forms.RadioButton();
            this.pTcpPort = new System.Windows.Forms.Panel();
            this.txtBxTcpPort = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pTcpAdr = new System.Windows.Forms.Panel();
            this.txtBxTcpAdr = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pConnUdp = new System.Windows.Forms.Panel();
            this.grpBxTypeUdp = new System.Windows.Forms.GroupBox();
            this.rdBtnUdpType_server = new System.Windows.Forms.RadioButton();
            this.rdBtnUdpType_client = new System.Windows.Forms.RadioButton();
            this.pUdpPort = new System.Windows.Forms.Panel();
            this.txtBxUdpPort = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pUdpAdr = new System.Windows.Forms.Panel();
            this.txtBxUdpAdr = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pConnMult = new System.Windows.Forms.Panel();
            this.pMultPort = new System.Windows.Forms.Panel();
            this.txtBxMultPort = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.pMultAdr = new System.Windows.Forms.Panel();
            this.txtBxMultAdr = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.pCfg = new System.Windows.Forms.Panel();
            this.grpBxCfgArgsLine = new System.Windows.Forms.GroupBox();
            this.txtBxCfg = new System.Windows.Forms.TextBox();
            this.btnCfgSet = new System.Windows.Forms.Button();
            this.btnCfgGet = new System.Windows.Forms.Button();
            this.grpBxConnType.SuspendLayout();
            this.pConnInfo.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pConnSerial.SuspendLayout();
            this.grpBxParity.SuspendLayout();
            this.pSerBaud.SuspendLayout();
            this.grpBxHdwFlowCntrl.SuspendLayout();
            this.grpBxDataBits.SuspendLayout();
            this.pSerPort.SuspendLayout();
            this.grpBxStopBits.SuspendLayout();
            this.pConnTcp.SuspendLayout();
            this.grpBxTypeTcp.SuspendLayout();
            this.pTcpPort.SuspendLayout();
            this.pTcpAdr.SuspendLayout();
            this.pConnUdp.SuspendLayout();
            this.grpBxTypeUdp.SuspendLayout();
            this.pUdpPort.SuspendLayout();
            this.pUdpAdr.SuspendLayout();
            this.pConnMult.SuspendLayout();
            this.pMultPort.SuspendLayout();
            this.pMultAdr.SuspendLayout();
            this.pCfg.SuspendLayout();
            this.grpBxCfgArgsLine.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBxConnType
            // 
            this.grpBxConnType.Controls.Add(this.rdBtnMult);
            this.grpBxConnType.Controls.Add(this.rdBtnUDP);
            this.grpBxConnType.Controls.Add(this.rdBtnTCP);
            this.grpBxConnType.Controls.Add(this.rdBtnSerial);
            this.grpBxConnType.Dock = System.Windows.Forms.DockStyle.Left;
            this.grpBxConnType.Location = new System.Drawing.Point(0, 0);
            this.grpBxConnType.Name = "grpBxConnType";
            this.grpBxConnType.Padding = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.grpBxConnType.Size = new System.Drawing.Size(83, 117);
            this.grpBxConnType.TabIndex = 2;
            this.grpBxConnType.TabStop = false;
            this.grpBxConnType.Text = "Type";
            // 
            // rdBtnMult
            // 
            this.rdBtnMult.Dock = System.Windows.Forms.DockStyle.Top;
            this.rdBtnMult.Location = new System.Drawing.Point(6, 85);
            this.rdBtnMult.Name = "rdBtnMult";
            this.rdBtnMult.Size = new System.Drawing.Size(74, 23);
            this.rdBtnMult.TabIndex = 3;
            this.rdBtnMult.TabStop = true;
            this.rdBtnMult.Text = "Multicast";
            this.rdBtnMult.UseVisualStyleBackColor = true;
            this.rdBtnMult.CheckedChanged += new System.EventHandler(this.RdBtnConnType_CheckedChanged);
            // 
            // rdBtnUDP
            // 
            this.rdBtnUDP.Dock = System.Windows.Forms.DockStyle.Top;
            this.rdBtnUDP.Location = new System.Drawing.Point(6, 62);
            this.rdBtnUDP.Name = "rdBtnUDP";
            this.rdBtnUDP.Size = new System.Drawing.Size(74, 23);
            this.rdBtnUDP.TabIndex = 2;
            this.rdBtnUDP.TabStop = true;
            this.rdBtnUDP.Text = "UDP";
            this.rdBtnUDP.UseVisualStyleBackColor = true;
            this.rdBtnUDP.CheckedChanged += new System.EventHandler(this.RdBtnConnType_CheckedChanged);
            // 
            // rdBtnTCP
            // 
            this.rdBtnTCP.Dock = System.Windows.Forms.DockStyle.Top;
            this.rdBtnTCP.Location = new System.Drawing.Point(6, 39);
            this.rdBtnTCP.Name = "rdBtnTCP";
            this.rdBtnTCP.Size = new System.Drawing.Size(74, 23);
            this.rdBtnTCP.TabIndex = 1;
            this.rdBtnTCP.TabStop = true;
            this.rdBtnTCP.Text = "TCP";
            this.rdBtnTCP.UseVisualStyleBackColor = true;
            this.rdBtnTCP.CheckedChanged += new System.EventHandler(this.RdBtnConnType_CheckedChanged);
            // 
            // rdBtnSerial
            // 
            this.rdBtnSerial.Checked = true;
            this.rdBtnSerial.Dock = System.Windows.Forms.DockStyle.Top;
            this.rdBtnSerial.Location = new System.Drawing.Point(6, 16);
            this.rdBtnSerial.Name = "rdBtnSerial";
            this.rdBtnSerial.Size = new System.Drawing.Size(74, 23);
            this.rdBtnSerial.TabIndex = 0;
            this.rdBtnSerial.TabStop = true;
            this.rdBtnSerial.Text = "Serial";
            this.rdBtnSerial.UseVisualStyleBackColor = true;
            this.rdBtnSerial.CheckedChanged += new System.EventHandler(this.RdBtnConnType_CheckedChanged);
            // 
            // pConnInfo
            // 
            this.pConnInfo.Controls.Add(this.btnDelete);
            this.pConnInfo.Controls.Add(this.chkBxWrite);
            this.pConnInfo.Controls.Add(this.chkBxRead);
            this.pConnInfo.Controls.Add(this.btnTerminal);
            this.pConnInfo.Controls.Add(this.btnOpen);
            this.pConnInfo.Controls.Add(this.groupBox1);
            this.pConnInfo.Dock = System.Windows.Forms.DockStyle.Left;
            this.pConnInfo.Location = new System.Drawing.Point(3, 0);
            this.pConnInfo.Name = "pConnInfo";
            this.pConnInfo.Padding = new System.Windows.Forms.Padding(6, 0, 3, 3);
            this.pConnInfo.Size = new System.Drawing.Size(94, 158);
            this.pConnInfo.TabIndex = 3;
            // 
            // btnDelete
            // 
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDelete.Location = new System.Drawing.Point(6, 131);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(85, 23);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // chkBxWrite
            // 
            this.chkBxWrite.Checked = true;
            this.chkBxWrite.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBxWrite.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkBxWrite.Location = new System.Drawing.Point(6, 109);
            this.chkBxWrite.Name = "chkBxWrite";
            this.chkBxWrite.Size = new System.Drawing.Size(85, 22);
            this.chkBxWrite.TabIndex = 2;
            this.chkBxWrite.Text = "Allow OUT";
            this.chkBxWrite.UseVisualStyleBackColor = true;
            // 
            // chkBxRead
            // 
            this.chkBxRead.Checked = true;
            this.chkBxRead.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBxRead.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkBxRead.Location = new System.Drawing.Point(6, 87);
            this.chkBxRead.Name = "chkBxRead";
            this.chkBxRead.Size = new System.Drawing.Size(85, 22);
            this.chkBxRead.TabIndex = 1;
            this.chkBxRead.Text = "Allow IN";
            this.chkBxRead.UseVisualStyleBackColor = true;
            // 
            // btnTerminal
            // 
            this.btnTerminal.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTerminal.Location = new System.Drawing.Point(6, 64);
            this.btnTerminal.Name = "btnTerminal";
            this.btnTerminal.Size = new System.Drawing.Size(85, 23);
            this.btnTerminal.TabIndex = 6;
            this.btnTerminal.Text = "Terminal";
            this.btnTerminal.UseVisualStyleBackColor = true;
            this.btnTerminal.Click += new System.EventHandler(this.BtnTerminalClick);
            // 
            // btnOpen
            // 
            this.btnOpen.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnOpen.Location = new System.Drawing.Point(6, 41);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(85, 23);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.BtnOpenClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtBxName);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(6, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(85, 41);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Name:";
            // 
            // txtBxName
            // 
            this.txtBxName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBxName.Location = new System.Drawing.Point(3, 16);
            this.txtBxName.Name = "txtBxName";
            this.txtBxName.Size = new System.Drawing.Size(79, 20);
            this.txtBxName.TabIndex = 0;
            // 
            // pConnSerial
            // 
            this.pConnSerial.Controls.Add(this.grpBxParity);
            this.pConnSerial.Controls.Add(this.pSerBaud);
            this.pConnSerial.Controls.Add(this.grpBxHdwFlowCntrl);
            this.pConnSerial.Controls.Add(this.grpBxDataBits);
            this.pConnSerial.Controls.Add(this.pSerPort);
            this.pConnSerial.Controls.Add(this.grpBxStopBits);
            this.pConnSerial.Dock = System.Windows.Forms.DockStyle.Left;
            this.pConnSerial.Location = new System.Drawing.Point(83, 0);
            this.pConnSerial.Name = "pConnSerial";
            this.pConnSerial.Size = new System.Drawing.Size(382, 117);
            this.pConnSerial.TabIndex = 4;
            this.pConnSerial.Visible = false;
            // 
            // grpBxParity
            // 
            this.grpBxParity.Controls.Add(this.rdBtnParity_Space);
            this.grpBxParity.Controls.Add(this.rdBtnParity_Mark);
            this.grpBxParity.Controls.Add(this.rdBtnParity_Even);
            this.grpBxParity.Controls.Add(this.rdBtnParity_Odd);
            this.grpBxParity.Controls.Add(this.rdBtnParity_None);
            this.grpBxParity.Location = new System.Drawing.Point(151, 2);
            this.grpBxParity.Name = "grpBxParity";
            this.grpBxParity.Padding = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.grpBxParity.Size = new System.Drawing.Size(69, 114);
            this.grpBxParity.TabIndex = 18;
            this.grpBxParity.TabStop = false;
            this.grpBxParity.Text = "Parity";
            // 
            // rdBtnParity_Space
            // 
            this.rdBtnParity_Space.Dock = System.Windows.Forms.DockStyle.Top;
            this.rdBtnParity_Space.Location = new System.Drawing.Point(6, 92);
            this.rdBtnParity_Space.Name = "rdBtnParity_Space";
            this.rdBtnParity_Space.Size = new System.Drawing.Size(60, 19);
            this.rdBtnParity_Space.TabIndex = 5;
            this.rdBtnParity_Space.Text = "Space";
            this.rdBtnParity_Space.UseVisualStyleBackColor = true;
            // 
            // rdBtnParity_Mark
            // 
            this.rdBtnParity_Mark.Dock = System.Windows.Forms.DockStyle.Top;
            this.rdBtnParity_Mark.Location = new System.Drawing.Point(6, 73);
            this.rdBtnParity_Mark.Name = "rdBtnParity_Mark";
            this.rdBtnParity_Mark.Size = new System.Drawing.Size(60, 19);
            this.rdBtnParity_Mark.TabIndex = 4;
            this.rdBtnParity_Mark.Text = "Mark";
            this.rdBtnParity_Mark.UseVisualStyleBackColor = true;
            // 
            // rdBtnParity_Even
            // 
            this.rdBtnParity_Even.Dock = System.Windows.Forms.DockStyle.Top;
            this.rdBtnParity_Even.Location = new System.Drawing.Point(6, 54);
            this.rdBtnParity_Even.Name = "rdBtnParity_Even";
            this.rdBtnParity_Even.Size = new System.Drawing.Size(60, 19);
            this.rdBtnParity_Even.TabIndex = 3;
            this.rdBtnParity_Even.Text = "Even";
            this.rdBtnParity_Even.UseVisualStyleBackColor = true;
            // 
            // rdBtnParity_Odd
            // 
            this.rdBtnParity_Odd.Dock = System.Windows.Forms.DockStyle.Top;
            this.rdBtnParity_Odd.Location = new System.Drawing.Point(6, 35);
            this.rdBtnParity_Odd.Name = "rdBtnParity_Odd";
            this.rdBtnParity_Odd.Size = new System.Drawing.Size(60, 19);
            this.rdBtnParity_Odd.TabIndex = 2;
            this.rdBtnParity_Odd.Text = "Odd";
            this.rdBtnParity_Odd.UseVisualStyleBackColor = true;
            // 
            // rdBtnParity_None
            // 
            this.rdBtnParity_None.Checked = true;
            this.rdBtnParity_None.Dock = System.Windows.Forms.DockStyle.Top;
            this.rdBtnParity_None.Location = new System.Drawing.Point(6, 16);
            this.rdBtnParity_None.Name = "rdBtnParity_None";
            this.rdBtnParity_None.Size = new System.Drawing.Size(60, 19);
            this.rdBtnParity_None.TabIndex = 1;
            this.rdBtnParity_None.TabStop = true;
            this.rdBtnParity_None.Text = "None";
            this.rdBtnParity_None.UseVisualStyleBackColor = true;
            // 
            // pSerBaud
            // 
            this.pSerBaud.Controls.Add(this.cmbBxBaud);
            this.pSerBaud.Controls.Add(this.label2);
            this.pSerBaud.Location = new System.Drawing.Point(3, 34);
            this.pSerBaud.Name = "pSerBaud";
            this.pSerBaud.Size = new System.Drawing.Size(142, 23);
            this.pSerBaud.TabIndex = 17;
            // 
            // cmbBxBaud
            // 
            this.cmbBxBaud.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbBxBaud.FormattingEnabled = true;
            this.cmbBxBaud.Location = new System.Drawing.Point(44, 0);
            this.cmbBxBaud.Name = "cmbBxBaud";
            this.cmbBxBaud.Size = new System.Drawing.Size(98, 21);
            this.cmbBxBaud.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "Baud:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // grpBxHdwFlowCntrl
            // 
            this.grpBxHdwFlowCntrl.Controls.Add(this.rdBtnHdwFlwCtrl_None);
            this.grpBxHdwFlowCntrl.Controls.Add(this.rdBtnHdwFlwCtrl_RTS_CTS);
            this.grpBxHdwFlowCntrl.Controls.Add(this.rdBtnHdwFlwCtrl_DTR_DSR);
            this.grpBxHdwFlowCntrl.Location = new System.Drawing.Point(3, 57);
            this.grpBxHdwFlowCntrl.Name = "grpBxHdwFlowCntrl";
            this.grpBxHdwFlowCntrl.Size = new System.Drawing.Size(142, 59);
            this.grpBxHdwFlowCntrl.TabIndex = 21;
            this.grpBxHdwFlowCntrl.TabStop = false;
            this.grpBxHdwFlowCntrl.Text = "Flow Control";
            // 
            // rdBtnHdwFlwCtrl_None
            // 
            this.rdBtnHdwFlwCtrl_None.AutoSize = true;
            this.rdBtnHdwFlwCtrl_None.Checked = true;
            this.rdBtnHdwFlwCtrl_None.Location = new System.Drawing.Point(6, 16);
            this.rdBtnHdwFlwCtrl_None.Margin = new System.Windows.Forms.Padding(0);
            this.rdBtnHdwFlwCtrl_None.Name = "rdBtnHdwFlwCtrl_None";
            this.rdBtnHdwFlwCtrl_None.Size = new System.Drawing.Size(55, 19);
            this.rdBtnHdwFlwCtrl_None.TabIndex = 16;
            this.rdBtnHdwFlwCtrl_None.TabStop = true;
            this.rdBtnHdwFlwCtrl_None.Text = "None";
            this.rdBtnHdwFlwCtrl_None.UseVisualStyleBackColor = true;
            // 
            // rdBtnHdwFlwCtrl_RTS_CTS
            // 
            this.rdBtnHdwFlwCtrl_RTS_CTS.AutoSize = true;
            this.rdBtnHdwFlwCtrl_RTS_CTS.Location = new System.Drawing.Point(61, 16);
            this.rdBtnHdwFlwCtrl_RTS_CTS.Margin = new System.Windows.Forms.Padding(0);
            this.rdBtnHdwFlwCtrl_RTS_CTS.Name = "rdBtnHdwFlwCtrl_RTS_CTS";
            this.rdBtnHdwFlwCtrl_RTS_CTS.Size = new System.Drawing.Size(75, 19);
            this.rdBtnHdwFlwCtrl_RTS_CTS.TabIndex = 17;
            this.rdBtnHdwFlwCtrl_RTS_CTS.TabStop = true;
            this.rdBtnHdwFlwCtrl_RTS_CTS.Text = "RTS/CTS";
            this.rdBtnHdwFlwCtrl_RTS_CTS.UseVisualStyleBackColor = true;
            // 
            // rdBtnHdwFlwCtrl_DTR_DSR
            // 
            this.rdBtnHdwFlwCtrl_DTR_DSR.AutoSize = true;
            this.rdBtnHdwFlwCtrl_DTR_DSR.Location = new System.Drawing.Point(6, 35);
            this.rdBtnHdwFlwCtrl_DTR_DSR.Margin = new System.Windows.Forms.Padding(0);
            this.rdBtnHdwFlwCtrl_DTR_DSR.Name = "rdBtnHdwFlwCtrl_DTR_DSR";
            this.rdBtnHdwFlwCtrl_DTR_DSR.Size = new System.Drawing.Size(79, 19);
            this.rdBtnHdwFlwCtrl_DTR_DSR.TabIndex = 18;
            this.rdBtnHdwFlwCtrl_DTR_DSR.TabStop = true;
            this.rdBtnHdwFlwCtrl_DTR_DSR.Text = "DTR/DSR";
            this.rdBtnHdwFlwCtrl_DTR_DSR.UseVisualStyleBackColor = true;
            // 
            // grpBxDataBits
            // 
            this.grpBxDataBits.Controls.Add(this.rdBtnDataBits_5);
            this.grpBxDataBits.Controls.Add(this.rdBtnDataBits_6);
            this.grpBxDataBits.Controls.Add(this.rdBtnDataBits_7);
            this.grpBxDataBits.Controls.Add(this.rdBtnDataBits_8);
            this.grpBxDataBits.Location = new System.Drawing.Point(226, 2);
            this.grpBxDataBits.Name = "grpBxDataBits";
            this.grpBxDataBits.Padding = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.grpBxDataBits.Size = new System.Drawing.Size(69, 114);
            this.grpBxDataBits.TabIndex = 19;
            this.grpBxDataBits.TabStop = false;
            this.grpBxDataBits.Text = "Data bits";
            // 
            // rdBtnDataBits_5
            // 
            this.rdBtnDataBits_5.Dock = System.Windows.Forms.DockStyle.Top;
            this.rdBtnDataBits_5.Location = new System.Drawing.Point(6, 73);
            this.rdBtnDataBits_5.Name = "rdBtnDataBits_5";
            this.rdBtnDataBits_5.Size = new System.Drawing.Size(60, 19);
            this.rdBtnDataBits_5.TabIndex = 3;
            this.rdBtnDataBits_5.Text = "5 bits";
            this.rdBtnDataBits_5.UseVisualStyleBackColor = true;
            // 
            // rdBtnDataBits_6
            // 
            this.rdBtnDataBits_6.Dock = System.Windows.Forms.DockStyle.Top;
            this.rdBtnDataBits_6.Location = new System.Drawing.Point(6, 54);
            this.rdBtnDataBits_6.Name = "rdBtnDataBits_6";
            this.rdBtnDataBits_6.Size = new System.Drawing.Size(60, 19);
            this.rdBtnDataBits_6.TabIndex = 2;
            this.rdBtnDataBits_6.Text = "6 bits";
            this.rdBtnDataBits_6.UseVisualStyleBackColor = true;
            // 
            // rdBtnDataBits_7
            // 
            this.rdBtnDataBits_7.Dock = System.Windows.Forms.DockStyle.Top;
            this.rdBtnDataBits_7.Location = new System.Drawing.Point(6, 35);
            this.rdBtnDataBits_7.Name = "rdBtnDataBits_7";
            this.rdBtnDataBits_7.Size = new System.Drawing.Size(60, 19);
            this.rdBtnDataBits_7.TabIndex = 1;
            this.rdBtnDataBits_7.Text = "7 bits";
            this.rdBtnDataBits_7.UseVisualStyleBackColor = true;
            // 
            // rdBtnDataBits_8
            // 
            this.rdBtnDataBits_8.Checked = true;
            this.rdBtnDataBits_8.Dock = System.Windows.Forms.DockStyle.Top;
            this.rdBtnDataBits_8.Location = new System.Drawing.Point(6, 16);
            this.rdBtnDataBits_8.Name = "rdBtnDataBits_8";
            this.rdBtnDataBits_8.Size = new System.Drawing.Size(60, 19);
            this.rdBtnDataBits_8.TabIndex = 0;
            this.rdBtnDataBits_8.TabStop = true;
            this.rdBtnDataBits_8.Text = "8 bits";
            this.rdBtnDataBits_8.UseVisualStyleBackColor = true;
            // 
            // pSerPort
            // 
            this.pSerPort.Controls.Add(this.cmbBxPorts);
            this.pSerPort.Controls.Add(this.label1);
            this.pSerPort.Location = new System.Drawing.Point(3, 8);
            this.pSerPort.Name = "pSerPort";
            this.pSerPort.Size = new System.Drawing.Size(142, 23);
            this.pSerPort.TabIndex = 16;
            // 
            // cmbBxPorts
            // 
            this.cmbBxPorts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbBxPorts.FormattingEnabled = true;
            this.cmbBxPorts.Location = new System.Drawing.Point(44, 0);
            this.cmbBxPorts.Name = "cmbBxPorts";
            this.cmbBxPorts.Size = new System.Drawing.Size(98, 21);
            this.cmbBxPorts.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Port:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // grpBxStopBits
            // 
            this.grpBxStopBits.Controls.Add(this.rdBtnStopBits_2bits);
            this.grpBxStopBits.Controls.Add(this.rdBtnStopBits_1_5);
            this.grpBxStopBits.Controls.Add(this.rdBtnStopBits_1bit);
            this.grpBxStopBits.Controls.Add(this.rdBtnStopBits_None);
            this.grpBxStopBits.Location = new System.Drawing.Point(301, 2);
            this.grpBxStopBits.Name = "grpBxStopBits";
            this.grpBxStopBits.Padding = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.grpBxStopBits.Size = new System.Drawing.Size(77, 114);
            this.grpBxStopBits.TabIndex = 20;
            this.grpBxStopBits.TabStop = false;
            this.grpBxStopBits.Text = "Stop bits";
            // 
            // rdBtnStopBits_2bits
            // 
            this.rdBtnStopBits_2bits.Dock = System.Windows.Forms.DockStyle.Top;
            this.rdBtnStopBits_2bits.Location = new System.Drawing.Point(6, 73);
            this.rdBtnStopBits_2bits.Margin = new System.Windows.Forms.Padding(0);
            this.rdBtnStopBits_2bits.Name = "rdBtnStopBits_2bits";
            this.rdBtnStopBits_2bits.Size = new System.Drawing.Size(68, 19);
            this.rdBtnStopBits_2bits.TabIndex = 1;
            this.rdBtnStopBits_2bits.Text = "2 bits";
            this.rdBtnStopBits_2bits.UseVisualStyleBackColor = true;
            // 
            // rdBtnStopBits_1_5
            // 
            this.rdBtnStopBits_1_5.Dock = System.Windows.Forms.DockStyle.Top;
            this.rdBtnStopBits_1_5.Location = new System.Drawing.Point(6, 54);
            this.rdBtnStopBits_1_5.Margin = new System.Windows.Forms.Padding(0);
            this.rdBtnStopBits_1_5.Name = "rdBtnStopBits_1_5";
            this.rdBtnStopBits_1_5.Size = new System.Drawing.Size(68, 19);
            this.rdBtnStopBits_1_5.TabIndex = 3;
            this.rdBtnStopBits_1_5.Text = "1.5 bits";
            this.rdBtnStopBits_1_5.UseVisualStyleBackColor = true;
            // 
            // rdBtnStopBits_1bit
            // 
            this.rdBtnStopBits_1bit.Checked = true;
            this.rdBtnStopBits_1bit.Dock = System.Windows.Forms.DockStyle.Top;
            this.rdBtnStopBits_1bit.Location = new System.Drawing.Point(6, 35);
            this.rdBtnStopBits_1bit.Margin = new System.Windows.Forms.Padding(0);
            this.rdBtnStopBits_1bit.Name = "rdBtnStopBits_1bit";
            this.rdBtnStopBits_1bit.Size = new System.Drawing.Size(68, 19);
            this.rdBtnStopBits_1bit.TabIndex = 0;
            this.rdBtnStopBits_1bit.TabStop = true;
            this.rdBtnStopBits_1bit.Text = "1 bit";
            this.rdBtnStopBits_1bit.UseVisualStyleBackColor = true;
            // 
            // rdBtnStopBits_None
            // 
            this.rdBtnStopBits_None.Dock = System.Windows.Forms.DockStyle.Top;
            this.rdBtnStopBits_None.Location = new System.Drawing.Point(6, 16);
            this.rdBtnStopBits_None.Margin = new System.Windows.Forms.Padding(0);
            this.rdBtnStopBits_None.Name = "rdBtnStopBits_None";
            this.rdBtnStopBits_None.Size = new System.Drawing.Size(68, 19);
            this.rdBtnStopBits_None.TabIndex = 2;
            this.rdBtnStopBits_None.Text = "None";
            this.rdBtnStopBits_None.UseVisualStyleBackColor = true;
            // 
            // pConnTcp
            // 
            this.pConnTcp.Controls.Add(this.grpBxTypeTcp);
            this.pConnTcp.Controls.Add(this.pTcpPort);
            this.pConnTcp.Controls.Add(this.pTcpAdr);
            this.pConnTcp.Dock = System.Windows.Forms.DockStyle.Left;
            this.pConnTcp.Location = new System.Drawing.Point(465, 0);
            this.pConnTcp.Name = "pConnTcp";
            this.pConnTcp.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.pConnTcp.Size = new System.Drawing.Size(164, 117);
            this.pConnTcp.TabIndex = 5;
            this.pConnTcp.Visible = false;
            // 
            // grpBxTypeTcp
            // 
            this.grpBxTypeTcp.Controls.Add(this.rdBtnTCPType_server);
            this.grpBxTypeTcp.Controls.Add(this.rdBtnTCPType_client);
            this.grpBxTypeTcp.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpBxTypeTcp.Location = new System.Drawing.Point(3, 44);
            this.grpBxTypeTcp.Name = "grpBxTypeTcp";
            this.grpBxTypeTcp.Padding = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.grpBxTypeTcp.Size = new System.Drawing.Size(161, 38);
            this.grpBxTypeTcp.TabIndex = 5;
            this.grpBxTypeTcp.TabStop = false;
            this.grpBxTypeTcp.Text = "Socket as";
            // 
            // rdBtnTCPType_server
            // 
            this.rdBtnTCPType_server.Dock = System.Windows.Forms.DockStyle.Left;
            this.rdBtnTCPType_server.Location = new System.Drawing.Point(71, 16);
            this.rdBtnTCPType_server.Name = "rdBtnTCPType_server";
            this.rdBtnTCPType_server.Size = new System.Drawing.Size(61, 19);
            this.rdBtnTCPType_server.TabIndex = 1;
            this.rdBtnTCPType_server.TabStop = true;
            this.rdBtnTCPType_server.Text = "Server";
            this.rdBtnTCPType_server.UseVisualStyleBackColor = true;
            // 
            // rdBtnTCPType_client
            // 
            this.rdBtnTCPType_client.Checked = true;
            this.rdBtnTCPType_client.Dock = System.Windows.Forms.DockStyle.Left;
            this.rdBtnTCPType_client.Location = new System.Drawing.Point(6, 16);
            this.rdBtnTCPType_client.Name = "rdBtnTCPType_client";
            this.rdBtnTCPType_client.Size = new System.Drawing.Size(65, 19);
            this.rdBtnTCPType_client.TabIndex = 0;
            this.rdBtnTCPType_client.TabStop = true;
            this.rdBtnTCPType_client.Text = "Client";
            this.rdBtnTCPType_client.UseVisualStyleBackColor = true;
            // 
            // pTcpPort
            // 
            this.pTcpPort.Controls.Add(this.txtBxTcpPort);
            this.pTcpPort.Controls.Add(this.label4);
            this.pTcpPort.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTcpPort.Location = new System.Drawing.Point(3, 22);
            this.pTcpPort.Name = "pTcpPort";
            this.pTcpPort.Size = new System.Drawing.Size(161, 22);
            this.pTcpPort.TabIndex = 4;
            // 
            // txtBxTcpPort
            // 
            this.txtBxTcpPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBxTcpPort.Location = new System.Drawing.Point(59, 0);
            this.txtBxTcpPort.Name = "txtBxTcpPort";
            this.txtBxTcpPort.Size = new System.Drawing.Size(102, 20);
            this.txtBxTcpPort.TabIndex = 1;
            this.txtBxTcpPort.Text = "54321";
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 22);
            this.label4.TabIndex = 0;
            this.label4.Text = "Port:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pTcpAdr
            // 
            this.pTcpAdr.Controls.Add(this.txtBxTcpAdr);
            this.pTcpAdr.Controls.Add(this.label3);
            this.pTcpAdr.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTcpAdr.Location = new System.Drawing.Point(3, 0);
            this.pTcpAdr.Name = "pTcpAdr";
            this.pTcpAdr.Size = new System.Drawing.Size(161, 22);
            this.pTcpAdr.TabIndex = 3;
            // 
            // txtBxTcpAdr
            // 
            this.txtBxTcpAdr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBxTcpAdr.Location = new System.Drawing.Point(59, 0);
            this.txtBxTcpAdr.Name = "txtBxTcpAdr";
            this.txtBxTcpAdr.Size = new System.Drawing.Size(102, 20);
            this.txtBxTcpAdr.TabIndex = 1;
            this.txtBxTcpAdr.Text = "127.0.0.1";
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 22);
            this.label3.TabIndex = 0;
            this.label3.Text = "Address:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pConnUdp
            // 
            this.pConnUdp.Controls.Add(this.grpBxTypeUdp);
            this.pConnUdp.Controls.Add(this.pUdpPort);
            this.pConnUdp.Controls.Add(this.pUdpAdr);
            this.pConnUdp.Dock = System.Windows.Forms.DockStyle.Left;
            this.pConnUdp.Location = new System.Drawing.Point(629, 0);
            this.pConnUdp.Name = "pConnUdp";
            this.pConnUdp.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.pConnUdp.Size = new System.Drawing.Size(164, 117);
            this.pConnUdp.TabIndex = 6;
            this.pConnUdp.Visible = false;
            // 
            // grpBxTypeUdp
            // 
            this.grpBxTypeUdp.Controls.Add(this.rdBtnUdpType_server);
            this.grpBxTypeUdp.Controls.Add(this.rdBtnUdpType_client);
            this.grpBxTypeUdp.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpBxTypeUdp.Location = new System.Drawing.Point(3, 44);
            this.grpBxTypeUdp.Name = "grpBxTypeUdp";
            this.grpBxTypeUdp.Padding = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.grpBxTypeUdp.Size = new System.Drawing.Size(161, 38);
            this.grpBxTypeUdp.TabIndex = 8;
            this.grpBxTypeUdp.TabStop = false;
            this.grpBxTypeUdp.Text = "Socket as";
            // 
            // rdBtnUdpType_server
            // 
            this.rdBtnUdpType_server.AutoSize = true;
            this.rdBtnUdpType_server.Dock = System.Windows.Forms.DockStyle.Left;
            this.rdBtnUdpType_server.Location = new System.Drawing.Point(71, 16);
            this.rdBtnUdpType_server.Name = "rdBtnUdpType_server";
            this.rdBtnUdpType_server.Size = new System.Drawing.Size(73, 19);
            this.rdBtnUdpType_server.TabIndex = 1;
            this.rdBtnUdpType_server.Text = "Receiver";
            this.rdBtnUdpType_server.UseVisualStyleBackColor = true;
            // 
            // rdBtnUdpType_client
            // 
            this.rdBtnUdpType_client.Checked = true;
            this.rdBtnUdpType_client.Dock = System.Windows.Forms.DockStyle.Left;
            this.rdBtnUdpType_client.Location = new System.Drawing.Point(6, 16);
            this.rdBtnUdpType_client.Name = "rdBtnUdpType_client";
            this.rdBtnUdpType_client.Size = new System.Drawing.Size(65, 19);
            this.rdBtnUdpType_client.TabIndex = 0;
            this.rdBtnUdpType_client.TabStop = true;
            this.rdBtnUdpType_client.Text = "Sender";
            this.rdBtnUdpType_client.UseVisualStyleBackColor = true;
            // 
            // pUdpPort
            // 
            this.pUdpPort.Controls.Add(this.txtBxUdpPort);
            this.pUdpPort.Controls.Add(this.label6);
            this.pUdpPort.Dock = System.Windows.Forms.DockStyle.Top;
            this.pUdpPort.Location = new System.Drawing.Point(3, 22);
            this.pUdpPort.Name = "pUdpPort";
            this.pUdpPort.Size = new System.Drawing.Size(161, 22);
            this.pUdpPort.TabIndex = 7;
            // 
            // txtBxUdpPort
            // 
            this.txtBxUdpPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBxUdpPort.Location = new System.Drawing.Point(59, 0);
            this.txtBxUdpPort.Name = "txtBxUdpPort";
            this.txtBxUdpPort.Size = new System.Drawing.Size(102, 20);
            this.txtBxUdpPort.TabIndex = 1;
            this.txtBxUdpPort.Text = "54321";
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 22);
            this.label6.TabIndex = 0;
            this.label6.Text = "Port:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pUdpAdr
            // 
            this.pUdpAdr.Controls.Add(this.txtBxUdpAdr);
            this.pUdpAdr.Controls.Add(this.label5);
            this.pUdpAdr.Dock = System.Windows.Forms.DockStyle.Top;
            this.pUdpAdr.Location = new System.Drawing.Point(3, 0);
            this.pUdpAdr.Name = "pUdpAdr";
            this.pUdpAdr.Size = new System.Drawing.Size(161, 22);
            this.pUdpAdr.TabIndex = 6;
            // 
            // txtBxUdpAdr
            // 
            this.txtBxUdpAdr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBxUdpAdr.Location = new System.Drawing.Point(59, 0);
            this.txtBxUdpAdr.Name = "txtBxUdpAdr";
            this.txtBxUdpAdr.Size = new System.Drawing.Size(102, 20);
            this.txtBxUdpAdr.TabIndex = 1;
            this.txtBxUdpAdr.Text = "127.0.0.1";
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 22);
            this.label5.TabIndex = 0;
            this.label5.Text = "Address:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pConnMult
            // 
            this.pConnMult.Controls.Add(this.pMultPort);
            this.pConnMult.Controls.Add(this.pMultAdr);
            this.pConnMult.Dock = System.Windows.Forms.DockStyle.Left;
            this.pConnMult.Location = new System.Drawing.Point(793, 0);
            this.pConnMult.Name = "pConnMult";
            this.pConnMult.Size = new System.Drawing.Size(164, 117);
            this.pConnMult.TabIndex = 7;
            this.pConnMult.Visible = false;
            // 
            // pMultPort
            // 
            this.pMultPort.Controls.Add(this.txtBxMultPort);
            this.pMultPort.Controls.Add(this.label8);
            this.pMultPort.Dock = System.Windows.Forms.DockStyle.Top;
            this.pMultPort.Location = new System.Drawing.Point(0, 22);
            this.pMultPort.Name = "pMultPort";
            this.pMultPort.Size = new System.Drawing.Size(164, 22);
            this.pMultPort.TabIndex = 6;
            // 
            // txtBxMultPort
            // 
            this.txtBxMultPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBxMultPort.Location = new System.Drawing.Point(59, 0);
            this.txtBxMultPort.Name = "txtBxMultPort";
            this.txtBxMultPort.Size = new System.Drawing.Size(105, 20);
            this.txtBxMultPort.TabIndex = 1;
            this.txtBxMultPort.Text = "54321";
            // 
            // label8
            // 
            this.label8.Dock = System.Windows.Forms.DockStyle.Left;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 22);
            this.label8.TabIndex = 0;
            this.label8.Text = "Port:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pMultAdr
            // 
            this.pMultAdr.Controls.Add(this.txtBxMultAdr);
            this.pMultAdr.Controls.Add(this.label7);
            this.pMultAdr.Dock = System.Windows.Forms.DockStyle.Top;
            this.pMultAdr.Location = new System.Drawing.Point(0, 0);
            this.pMultAdr.Name = "pMultAdr";
            this.pMultAdr.Size = new System.Drawing.Size(164, 22);
            this.pMultAdr.TabIndex = 5;
            // 
            // txtBxMultAdr
            // 
            this.txtBxMultAdr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBxMultAdr.Location = new System.Drawing.Point(59, 0);
            this.txtBxMultAdr.Name = "txtBxMultAdr";
            this.txtBxMultAdr.Size = new System.Drawing.Size(105, 20);
            this.txtBxMultAdr.TabIndex = 1;
            this.txtBxMultAdr.Text = "127.0.0.1";
            // 
            // label7
            // 
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 22);
            this.label7.TabIndex = 0;
            this.label7.Text = "Address:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pCfg
            // 
            this.pCfg.Controls.Add(this.pConnMult);
            this.pCfg.Controls.Add(this.pConnUdp);
            this.pCfg.Controls.Add(this.pConnTcp);
            this.pCfg.Controls.Add(this.pConnSerial);
            this.pCfg.Controls.Add(this.grpBxConnType);
            this.pCfg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pCfg.Location = new System.Drawing.Point(97, 41);
            this.pCfg.Name = "pCfg";
            this.pCfg.Size = new System.Drawing.Size(1013, 117);
            this.pCfg.TabIndex = 8;
            // 
            // grpBxCfgArgsLine
            // 
            this.grpBxCfgArgsLine.Controls.Add(this.txtBxCfg);
            this.grpBxCfgArgsLine.Controls.Add(this.btnCfgSet);
            this.grpBxCfgArgsLine.Controls.Add(this.btnCfgGet);
            this.grpBxCfgArgsLine.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpBxCfgArgsLine.Location = new System.Drawing.Point(97, 0);
            this.grpBxCfgArgsLine.Name = "grpBxCfgArgsLine";
            this.grpBxCfgArgsLine.Size = new System.Drawing.Size(1013, 41);
            this.grpBxCfgArgsLine.TabIndex = 9;
            this.grpBxCfgArgsLine.TabStop = false;
            this.grpBxCfgArgsLine.Text = "Stream configuration command";
            // 
            // txtBxCfg
            // 
            this.txtBxCfg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBxCfg.Location = new System.Drawing.Point(97, 16);
            this.txtBxCfg.Name = "txtBxCfg";
            this.txtBxCfg.Size = new System.Drawing.Size(913, 20);
            this.txtBxCfg.TabIndex = 0;
            // 
            // btnCfgSet
            // 
            this.btnCfgSet.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnCfgSet.Location = new System.Drawing.Point(50, 16);
            this.btnCfgSet.Margin = new System.Windows.Forms.Padding(1);
            this.btnCfgSet.Name = "btnCfgSet";
            this.btnCfgSet.Size = new System.Drawing.Size(47, 22);
            this.btnCfgSet.TabIndex = 2;
            this.btnCfgSet.Text = "Set";
            this.btnCfgSet.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCfgSet.UseVisualStyleBackColor = true;
            this.btnCfgSet.Click += new System.EventHandler(this.BtnCfgSetClick);
            // 
            // btnCfgGet
            // 
            this.btnCfgGet.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnCfgGet.Location = new System.Drawing.Point(3, 16);
            this.btnCfgGet.Margin = new System.Windows.Forms.Padding(1);
            this.btnCfgGet.Name = "btnCfgGet";
            this.btnCfgGet.Size = new System.Drawing.Size(47, 22);
            this.btnCfgGet.TabIndex = 1;
            this.btnCfgGet.Text = "Get";
            this.btnCfgGet.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCfgGet.UseVisualStyleBackColor = true;
            this.btnCfgGet.Click += new System.EventHandler(this.BtnCfgGetClick);
            // 
            // ConnPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pCfg);
            this.Controls.Add(this.grpBxCfgArgsLine);
            this.Controls.Add(this.pConnInfo);
            this.Name = "ConnPanel";
            this.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.Size = new System.Drawing.Size(1113, 161);
            this.grpBxConnType.ResumeLayout(false);
            this.pConnInfo.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pConnSerial.ResumeLayout(false);
            this.grpBxParity.ResumeLayout(false);
            this.pSerBaud.ResumeLayout(false);
            this.grpBxHdwFlowCntrl.ResumeLayout(false);
            this.grpBxHdwFlowCntrl.PerformLayout();
            this.grpBxDataBits.ResumeLayout(false);
            this.pSerPort.ResumeLayout(false);
            this.grpBxStopBits.ResumeLayout(false);
            this.pConnTcp.ResumeLayout(false);
            this.grpBxTypeTcp.ResumeLayout(false);
            this.pTcpPort.ResumeLayout(false);
            this.pTcpPort.PerformLayout();
            this.pTcpAdr.ResumeLayout(false);
            this.pTcpAdr.PerformLayout();
            this.pConnUdp.ResumeLayout(false);
            this.grpBxTypeUdp.ResumeLayout(false);
            this.grpBxTypeUdp.PerformLayout();
            this.pUdpPort.ResumeLayout(false);
            this.pUdpPort.PerformLayout();
            this.pUdpAdr.ResumeLayout(false);
            this.pUdpAdr.PerformLayout();
            this.pConnMult.ResumeLayout(false);
            this.pMultPort.ResumeLayout(false);
            this.pMultPort.PerformLayout();
            this.pMultAdr.ResumeLayout(false);
            this.pMultAdr.PerformLayout();
            this.pCfg.ResumeLayout(false);
            this.grpBxCfgArgsLine.ResumeLayout(false);
            this.grpBxCfgArgsLine.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
