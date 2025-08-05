/*
 * Created by SharpDevelop.
 * User: AdamS-0
 * Date: 05.08.2025
 * Time: 07:28
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace streamNode {
    /// <summary>
    /// Description of Conn.
    /// </summary>
    public partial class ConnPanel : UserControl {
        public delegate void DataReceivedEvent(ref byte[] dataOut, int dataLen);
        public DataReceivedEvent DataReceived;
        
        public bool CanWrite() { return chkBxWrite.Checked; }
        public bool CanRead () { return chkBxRead .Checked; }
        
        stream.Options  opt  = new stream.Options();
        
        System.Windows.Forms.Timer timerStatus = new System.Windows.Forms.Timer();
        Process pr = new Process();
        
        Terminal trm = null;
        
        public ConnPanel() {
            InitializeComponent();
            timerStatus.Tick += timerStatus_tick;
            timerStatus.Interval = 500;
            
            pr.Exited += processExited;
            
            CheckVisibilityOfPanelParams();
        }

        
        void CheckVisibilityOfPanelParams() {
            pConnSerial .Visible = false;
            pConnTcp    .Visible = false;
            pConnUdp    .Visible = false;
            pConnMult   .Visible = false;
            
            if      ( rdBtnSerial   .Checked ) { pConnSerial .Visible = true; }
            else if ( rdBtnTCP      .Checked ) { pConnTcp    .Visible = true; }
            else if ( rdBtnUDP      .Checked ) { pConnUdp    .Visible = true; }
            else if ( rdBtnMult     .Checked ) { pConnMult   .Visible = true; }
        }
        
        void RdBtnConnType_CheckedChanged(object sender, EventArgs e) {
            CheckVisibilityOfPanelParams();
        }
        
        void BtnOpenClick(object sender, EventArgs e) {
            try {
                if( pr != null ) {
                    if( !pr.HasExited ) {
                        Close();
                    } else {
                        open();
                    }
                } else {
                    open();
                }
            } catch { open(); }
            
        }
        
        bool procWorking = false;
        void open() {
            opt  = new stream.Options();
            getOptionsFromGUI( ref opt );
            string args = getStrArgsFromOptions(opt);
            
            txtBxCfg.Text = args;
            pr.StartInfo.FileName = "stream.exe";
            pr.StartInfo.Arguments = args;
            pr.StartInfo.UseShellExecute = false;
            pr.StartInfo.RedirectStandardInput = true;
            pr.StartInfo.RedirectStandardOutput = true;
            pr.StartInfo.CreateNoWindow = true;
            pr.EnableRaisingEvents = true;
            
            
                
            if( pr.Start() ) {
                btnOpen.Text = "Close";
                procWorking = true;
                timerStatus.Start();
                
                Thread thReconnect = new Thread(threadWaitForData);
                thReconnect.Start();
            } else {
                procWorking = false;
                btnOpen.Text = "Open";
            }
        }
        
        public void Close() {
            try {
                pr.Kill();
                procWorking = false;
                btnOpen.Text = "Open";
            } catch {}
        }
        
        void processExited(object sender, EventArgs e) {
            procWorking = false;
        }
        
        void timerStatus_tick(object sender, EventArgs e) {
            if( procWorking ) {
                pCfg.Enabled = false;
                btnOpen.Text = "Close";
            } else {
                pCfg.Enabled = true;
                btnOpen.Text = "Open";
                timerStatus.Stop();
            }
        }
        
        
        public void Send(ref byte[] dataOut, int dataLen) {
            try {
                pr.StandardInput.Write(System.Text.Encoding.GetEncoding("windows-1256").GetString(dataOut, 0, dataLen));
            } catch { }
        }
        
        
        void DataReceivedFromProcess(object sender, DataReceivedEventArgs e) {

        }
        
        
        void threadWaitForData() {
            int len2copy = 0;
            byte[] dataIn = new byte[1024];
            
            using(StreamReader sr = pr.StandardOutput) {
                while( procWorking ) {
                    Thread.Sleep(10);
                    if( !CanRead() ) { continue; }
                    while ((len2copy = sr.BaseStream.Read(dataIn, 0, 1024)) > 0) {
                        if( this.DataReceived == null ) { return; }
                        this.DataReceived( ref dataIn, len2copy );
                        if ( trm != null ) {
                            if ( trm.IsHandleCreated ) {
                                trm.AppendText(System.Text.Encoding.GetEncoding("windows-1256").GetString(dataIn, 0, len2copy));
                            }
                        }
                        len2copy = 0;
                    }
                }
            }
        }
        
        
        
        
        
        
        
        string getStrArgsFromOptions(stream.Options opts) {
            string ret = "";
            object propValue = null;
            
            PropertyInfo[] properties = opts.GetType().GetProperties();
            foreach( PropertyInfo pi in properties ) {
                propValue = pi.GetValue(opts);
                if( propValue == null ) { continue; }
                ret += " --" + pi.Name + " " + propValue.ToString();
            }
            return ret;
        }
        
        
        
        void getConfigurationToTxtBx() {
            getOptionsFromGUI( ref opt );
            txtBxCfg.Text = getStrArgsFromOptions( opt );
        }
        
        
        void getOptionsFromGUI(ref stream.Options opts) {
            opts = new stream.Options();
            
            if      ( rdBtnSerial.Checked   ) { getOptionsFromGUI_Ser(ref opts); }
            else if ( rdBtnTCP.Checked      ) { getOptionsFromGUI_TCP(ref opts); }
            else if ( rdBtnUDP.Checked      ) { getOptionsFromGUI_UDP(ref opts); }
            else if ( rdBtnMult.Checked     ) { getOptionsFromGUI_Mult(ref opts); }
        }
        
        
        void getOptionsFromGUI_Ser(ref stream.Options opts) {
            opts.com = "";
            opts.name = cmbBxPorts.Text;
            opts.baud = cmbBxBaud.Text;
            opts.parity = getFromGUI_Parity();
            opts.dataBits= getFromGUI_DataBits();
            opts.stopBits = getFromGUI_StopBits();
            
            /* Hardware flow control */
        }
        
        string getFromGUI_Parity() {
            if      ( rdBtnParity_Odd.Checked   ) { return "odd";   }
            else if ( rdBtnParity_Even.Checked  ) { return "even";  }
            else if ( rdBtnParity_Mark.Checked  ) { return "mark";  }
            else if ( rdBtnParity_Space.Checked ) { return "space"; }
            return "none";
        }
        
        string getFromGUI_DataBits() {
            if      ( rdBtnDataBits_5.Checked ) { return "5"; }
            else if ( rdBtnDataBits_6.Checked ) { return "6"; }
            else if ( rdBtnDataBits_7.Checked ) { return "7"; }
            return "8";
        }
        
        string getFromGUI_StopBits() {
            if      ( rdBtnStopBits_1bit.Checked    ) { return "one";           }
            else if ( rdBtnStopBits_1_5.Checked     ) { return "onepointfive";  }
            else if ( rdBtnStopBits_2bits.Checked   ) { return "2";             }
            return "none";
        }
        
        
        
        
        void getOptionsFromGUI_TCP(ref stream.Options opts) {
            opts.tcp = "";
            opts.address = txtBxTcpAdr.Text;
            opts.port = txtBxTcpPort.Text;
            if ( rdBtnTCPType_server.Checked )  { opts.server = "";  }
            else                                { opts.server = null; }
        }
        
        void getOptionsFromGUI_UDP(ref stream.Options opts) {
            opts.udp = "";
            opts.address = txtBxUdpAdr.Text;
            opts.port = txtBxUdpPort.Text;
            if ( rdBtnUdpType_server.Checked )  { opts.server = "";  }
            else                                { opts.server = null; }
        }
        
        void getOptionsFromGUI_Mult(ref stream.Options opts) {
            opts.mudp = "";
            opts.address = txtBxMultAdr.Text;
            opts.port = txtBxMultPort.Text;
        }
        
        
        
        /* Serialport GUI changed */
        void CmbBxPortsSelectedIndexChanged(object sender, EventArgs e) {
          
        }
        void CmbBxBaudSelectedIndexChanged(object sender, EventArgs e) {
          
        }
        void RdBtnSerFlowCtrl_CheckedChanged(object sender, EventArgs e) {
          
        }
        void RdBtnSerParity_CheckedChanged(object sender, EventArgs e) {
          
        }
        void RdBtnSerDataBits_CheckedChanged(object sender, EventArgs e) {
          
        }
        void RdBtnSerStopBits_CheckedChanged(object sender, EventArgs e) {
          
        }
        
        
        /* TCP GUI changed */
        void TxtBxTcpAdrTextChanged(object sender, EventArgs e) {
          
        }
        void TxtBxTcpPortTextChanged(object sender, EventArgs e) {
          
        }
        void RdBtnTcpType_CheckedChanged(object sender, EventArgs e) {
          
        }
        
        /* UDP GUI changed */
        void TxtBxUdpAdrTextChanged(object sender, EventArgs e) {
          
        }
        void TxtBxUdpPortTextChanged(object sender, EventArgs e) {
          
        }
        void RdBtnUdpType_CheckedChanged(object sender, EventArgs e) {
          
        }
        
        /* Multicast GUI changed */
        void TxtBxMultAdrTextChanged(object sender, EventArgs e) {
          
        }
        void TxtBxMultPortTextChanged(object sender, EventArgs e) {
          
        }
        
        
        void BtnCfgGetClick(object sender, EventArgs e) {
            getConfigurationToTxtBx();
        }
        
        void BtnCfgSetClick(object sender, EventArgs e) {
          
        }
        
        
        
        void BtnTerminalClick(object sender, EventArgs e) {
            if( trm == null || !trm.IsHandleCreated ) {
                trm = new Terminal();
                trm.SendDataFromTerminal += sendDataFromTerminal;
                trm.Text = txtBxName.Text + " - " + txtBxCfg.Text;
            }
            
            trm.Show();
            trm.BringToFront();
        }
        
        void sendDataFromTerminal(string msg) {
            try {
                if( CanWrite() ) {
                    pr.StandardInput.Write(msg);
                }
            } catch { }
        }

        
    }
}
