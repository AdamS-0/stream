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
using System.Xml.Serialization;

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
        bool processStarted = false;
        
        Terminal trm = null;
        
        public ConnPanel() {
            InitializeComponent();
            
            cmbBxBaud.Items.AddRange( new string[]{
                                         "4800", "9600", "19200", "38400", "57600", "115200", "230400", "460800", "921600" } );
            
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
            
            processStarted = pr.Start();
                
            if( processStarted ) {
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
            processStarted = false;
        }
        
        void timerStatus_tick(object sender, EventArgs e) {
            if( procWorking ) {
                btnDelete.Enabled = grpBxCfgArgsLine.Enabled = pCfg.Enabled = false;
                
                btnOpen.Text = "Close";
            } else {
                btnDelete.Enabled = grpBxCfgArgsLine.Enabled = pCfg.Enabled = true;
                btnOpen.Text = "Open";
                timerStatus.Stop();
            }
        }
        
        
        public void Send(ref byte[] dataOut, int dataLen) {
            try {
                if( processStarted ) {
                    pr.StandardInput.Write(System.Text.Encoding.GetEncoding("windows-1256").GetString(dataOut, 0, dataLen));
                }
            } catch { Console.WriteLine( "Error during connPanel send!" ); }
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
            string strPropVal = "";
            object propValue = null;
            
            PropertyInfo[] properties = opts.GetType().GetProperties();
            foreach( PropertyInfo pi in properties ) {
                propValue = pi.GetValue(opts);
                if( propValue == null ) { continue; }
                strPropVal = propValue.ToString();
                ret += " --" + pi.Name + (strPropVal.Length > 0 ? " " + propValue.ToString() : "" );
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
            
            if( chkBxReconnect.Checked ) { opts.reconnect = ""; }
            else                         { opts.reconnect = null; }
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
            if      ( rdBtnStopBits_1bit.Checked    ) { return "1"  ; }
            else if ( rdBtnStopBits_1_5.Checked     ) { return "1.5"; }
            else if ( rdBtnStopBits_2bits.Checked   ) { return "2"  ; }
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
            loadStreamCfgFromString(txtBxCfg.Text);
        }
        
        
        
        void loadStreamCfgFromString(string strCfg) {
            if( strCfg.Length <= 0 ) { return; }
            
            string[] args = strCfg.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            
            opt = CommandLineParser.CommandLineParser.ParseArguments<stream.Options>(args);
            
            if      ( opt.com  != null ) { setStreamCfgGUI_Ser(opt); }
            else if ( opt.tcp  != null ) { setStreamCfgGUI_Tcp(opt); }
            else if ( opt.udp  != null ) { setStreamCfgGUI_Udp(opt); }
            else if ( opt.mudp != null ) { setStreamCfgGUI_Mult(opt); }
            
            if( opt.reconnect != null ) { chkBxReconnect.Checked = true; }
            else                        { chkBxReconnect.Checked = false; }
            
        }
        
        
        void setStreamCfgGUI_Ser(stream.Options opts) {
            string portName = "";
            string baud = "";
            
            if( opts.com.Length > 0 ) { portName = opts.com; }
            if( opts.name != null ) { portName = opts.name; }
            if( opts.baud != null ) { baud = opts.baud; }
            
            cmbBxPorts.Text = portName;
            cmbBxBaud .Text = baud;
            
            rdBtnParity_None .Checked = true;
            rdBtnParity_Odd  .Checked = false;
            rdBtnParity_Even .Checked = false;
            rdBtnParity_Mark .Checked = false;
            rdBtnParity_Space.Checked = false;
            
            if( opts.parity != null ) {
                string parity = opts.parity.ToLower();
                if      ( parity == "odd"   ) { rdBtnParity_Odd  .Checked = true; }
                else if ( parity == "even"  ) { rdBtnParity_Even .Checked = true; }
                else if ( parity == "mark"  ) { rdBtnParity_Mark .Checked = true; }
                else if ( parity == "space" ) { rdBtnParity_Space.Checked = true; }
            }
            
            
            rdBtnDataBits_5.Checked = false;
            rdBtnDataBits_6.Checked = false;
            rdBtnDataBits_7.Checked = false;
            rdBtnDataBits_8.Checked = true;
            
            if( opts.dataBits != null ) {
                string dataBits = opts.dataBits.ToLower();
                if      ( dataBits == "5" ) { rdBtnDataBits_5.Checked = true; }
                else if ( dataBits == "6" ) { rdBtnDataBits_6.Checked = true; }
                else if ( dataBits == "7" ) { rdBtnDataBits_7.Checked = true; }
            }
            
            
            rdBtnStopBits_None .Checked = false;
            rdBtnStopBits_1bit .Checked = true;
            rdBtnStopBits_1_5  .Checked = false;
            rdBtnStopBits_2bits.Checked = false;
            
            if (opts.stopBits != null) {
                string stopBits = opts.stopBits.ToLower();
                if      ( stopBits == "none"         || stopBits == "0"   ) { rdBtnStopBits_None.Checked = true; }
                else if ( stopBits == "two"          || stopBits == "2"   ) { rdBtnStopBits_2bits.Checked = true; }
                else if ( stopBits == "onepointfive" || stopBits == "1.5" ) { rdBtnStopBits_1_5.Checked = true; }
            }
            
            rdBtnSerial.Checked = true;
            rdBtnTCP   .Checked = false;
            rdBtnUDP   .Checked = false;
            rdBtnMult  .Checked = false;
        }
        
        void setStreamCfgGUI_Tcp(stream.Options opts) {
            string adr = "";
            string port = "";
            
            if( opts.tcp != null ) {
                if( opts.tcp.Length > 0 ) {
                    string[] adrPort = opts.tcp.Split(':');
                    adr = adrPort[0];
                    
                    if(adrPort.Length > 1) {
                        port = adrPort[1];
                    }
                }
            }
            
            if( opts.address != null ) { adr  = opt.address; }
            if( opts.port    != null ) { port = opt.port; }
            
            rdBtnTCPType_server.Checked = false;
            rdBtnTCPType_client.Checked = false;
            if ( opts.server != null )  { rdBtnTCPType_server.Checked = true; }
            else                        { rdBtnTCPType_client.Checked = true; }
            
            txtBxTcpAdr.Text = adr;
            txtBxTcpPort.Text = port;
            
            rdBtnSerial.Checked = false;
            rdBtnTCP   .Checked = true;
            rdBtnUDP   .Checked = false;
            rdBtnMult  .Checked = false;
        }
        
        void setStreamCfgGUI_Udp(stream.Options opts) {
            string adr = "";
            string port = "";
            
            if( opts.udp != null ) {
                if( opts.udp.Length > 0 ) {
                    string[] adrPort = opts.udp.Split(':');
                    adr = adrPort[0];
                    
                    if(adrPort.Length > 1) {
                        port = adrPort[1];
                    }
                }
            }
            
            if( opts.address != null ) { adr  = opt.address; }
            if( opts.port    != null ) { port = opt.port; }
            
            rdBtnUdpType_server.Checked = false;
            rdBtnUdpType_client.Checked = false;
            if ( opts.server != null )  { rdBtnUdpType_server.Checked = true; }
            else                        { rdBtnUdpType_client.Checked = true; }
            
            txtBxUdpAdr.Text = adr;
            txtBxUdpPort.Text = port;
            
            rdBtnSerial.Checked = false;
            rdBtnTCP   .Checked = false;
            rdBtnUDP   .Checked = true;
            rdBtnMult  .Checked = false;
        }
        
        void setStreamCfgGUI_Mult(stream.Options opts) {
            string adr = "";
            string port = "";
            
            if( opts.mudp != null ) {
                if( opts.mudp.Length > 0 ) {
                    string[] adrPort = opts.mudp.Split(':');
                    adr = adrPort[0];
                    
                    if(adrPort.Length > 1) {
                        port = adrPort[1];
                    }
                }
            }
            
            if( opts.address != null ) { adr  = opt.address; }
            if( opts.port    != null ) { port = opt.port; }
            
            txtBxMultAdr.Text = adr;
            txtBxMultPort.Text = port;
            
            rdBtnSerial.Checked = false;
            rdBtnTCP   .Checked = false;
            rdBtnUDP   .Checked = false;
            rdBtnMult  .Checked = true;
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

        
        
        
        
        public NodeCfgConn ToCfgNodeCon() {
            NodeCfgConn cfgNodeCon = new NodeCfgConn();
            
            cfgNodeCon.name = txtBxName.Text;
            cfgNodeCon.allowIn = chkBxRead.Checked;
            cfgNodeCon.allowOut = chkBxWrite.Checked;
            
            getOptionsFromGUI( ref opt );
            cfgNodeCon.streamCfg = getStrArgsFromOptions(opt);
            
            return cfgNodeCon;
        }
        
        public static ConnPanel FromCfgNodeConn(NodeCfgConn cfgNodeCon) {
            ConnPanel connPanel = new ConnPanel();
            
            connPanel.txtBxName.Text = cfgNodeCon.name;
            connPanel.chkBxRead.Checked = cfgNodeCon.allowIn;
            connPanel.chkBxWrite.Checked = cfgNodeCon.allowOut;
            connPanel.loadStreamCfgFromString(cfgNodeCon.streamCfg);
            connPanel.txtBxCfg.Text = cfgNodeCon.streamCfg;
            
            return connPanel;
        }
        
        
        void BtnDeleteClick(object sender, EventArgs e) {
            this.Close();
            this.Parent.Controls.Remove(this);
        }
        
    }
}
