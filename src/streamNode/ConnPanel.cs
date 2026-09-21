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
using System.Threading.Tasks;
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
        public int GetProcId() { return pr.Id; }
        
        stream.Options  opt  = new stream.Options();
        
        System.Windows.Forms.Timer timerStatus = new System.Windows.Forms.Timer();
        Process pr = new Process();
        bool processStarted = false;
        
        public ConnPanel() {
            InitializeComponent();
            
            timerStatus.Tick += timerStatus_tick;
            timerStatus.Interval = 500;
            
            pr.Exited += processExited;
            
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

            string[] argsAll = txtBxCmd.Text.Split(' ');            
            pr.StartInfo.FileName = argsAll[0];
            pr.StartInfo.Arguments = string.Join(" ", argsAll, 1, argsAll.Length - 1);
            pr.StartInfo.UseShellExecute = false;
            pr.StartInfo.RedirectStandardInput = true;
            pr.StartInfo.RedirectStandardOutput = true;
            pr.StartInfo.CreateNoWindow = true;
            pr.EnableRaisingEvents = true;
            
            processStarted = pr.Start();
                
            if( processStarted ) {
                btnStartStop.Text = "Close";
                procWorking = true;
                timerStatus.Start();
                txtBxPID.Text = pr.Id.ToString();
                
                Thread thReconnect = new Thread(threadWaitForData);
                thReconnect.Start();
            } else {
                procWorking = false;
                txtBxPID.Text = "---";
                btnStartStop.Text = "Open";
            }
        }
        
        public void Close() {
            try {
                pr.Kill();
                procWorking = false;
                btnStartStop.Text = "Open";
            } catch {}
        }
        
        void processExited(object sender, EventArgs e) {
            procWorking = false;
            processStarted = false;
        }
        
        void timerStatus_tick(object sender, EventArgs e) {
            if( procWorking ) {
                btnStartStop.Text = "Close";
                txtBxPID.Text = pr.Id.ToString();
            } else {
                btnStartStop.Text = "Open";
                timerStatus.Stop();
                txtBxPID.Text = "---";
            }
        }
        
        
        public void Send(ref byte[] dataOut, int dataLen) {
            try {
                if( processStarted ) {
                    pr.StandardInput.BaseStream.Write(dataOut, 0, dataLen);
                    pr.StandardInput.BaseStream.Flush();
                    
//                    pr.StandardInput.Write(System.Text.Encoding.GetEncoding("windows-1256").GetString(dataOut, 0, dataLen));
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
        
        public NodeCfgConn ToCfgNodeCon() {
            NodeCfgConn cfgNodeCon = new NodeCfgConn();
            
            cfgNodeCon.allowIn  = chkBxRead.Checked;
            cfgNodeCon.allowOut = chkBxWrite.Checked;
            cfgNodeCon.command  = txtBxCmd.Text;
            
            return cfgNodeCon;
        }
        
        public static ConnPanel FromCfgNodeConn(NodeCfgConn cfgNodeCon) {
            ConnPanel connPanel = new ConnPanel();
            
            connPanel.chkBxRead .Checked = cfgNodeCon.allowIn;
            connPanel.chkBxWrite.Checked = cfgNodeCon.allowOut;
            connPanel.txtBxCmd  .Text    = cfgNodeCon.command;
            
            return connPanel;
        }
        
        
        int btnDeleteCntr = 0;
        void BtnDeleteClick(object sender, EventArgs e) {
            btnDeleteCntr++;
            if ( btnDeleteCntr == 1 ) {
                btnDelete.Width = 64;
                btnDelete.Text = "You sure?";
            } else {
                this.Close();
                this.Parent.Controls.Remove(this);
            }
        }
        
    }
}
