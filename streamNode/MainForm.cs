/*
 * Created by SharpDevelop.
 * User: AdamS-0
 * Date: 04.08.2025
 * Time: 16:41
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using stream;

namespace streamNode {
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class MainForm : Form {
        
        
        List<ConnPanel> conns = new List<ConnPanel>();
        
        void addNewConn() {
            ConnPanel cp = new ConnPanel();
            
            cp.Dock = DockStyle.Top;
            pConns.Controls.Add(cp);
            
            cp.DataReceived += delegate (ref byte[] dataOut, int dataLen) {
                DataReceived(cp, ref dataOut, dataLen);
            };
            
            conns.Add(cp);
        }
        
        public MainForm() {
            InitializeComponent();

        }
        
        void DataReceived(object sender, ref byte[] dataOut, int dataLen) {
            /* Data just received from this conn */
            
            /* Get object as connection panel */
            ConnPanel conPanel = sender as ConnPanel;
            
            /* If this connection is only out - skip this data */
            if( !conPanel.CanRead() ) { return; }
            
            if ( tlStrpBtnDebugMode.Checked ) {
                Console.WriteLine(
                    "[" + DateTime.Now.ToString("yyyy.MM.dd-HH:mm:ss.ffffff") +
                    "] PID#" + conPanel.GetProcId().ToString() + "\t, Rx: " + dataLen );
            }
            
            /* Send data to other connections in this node */
            foreach( ConnPanel cp in conns ) {
                /* If this is sender, skip it */
                if( cp == conPanel ) { continue; }
                
                /* If writing was disabled, skip loop */
                if( !cp.CanWrite() ) { continue; }
                
                try {
                    cp.Send(ref dataOut, dataLen);
                } catch { }
            }
        }
        
        void MainFormFormClosing(object sender, FormClosingEventArgs e) {
            foreach( ConnPanel cp in conns ) {
                cp.Close();
            }
        }
        
        
        void MainFormLoad(object sender, EventArgs e) {
            if( pConns.Controls.Count <= 0 ) {
                addNewConn();
            }
        }
        
        
        
        void closeAllConnections() {
            foreach( Control con in pConns.Controls ) {
                try {
                    ((ConnPanel)con).Close();
                } catch {}
            }
        }
        
        void deleteAllConnections() {
            closeAllConnections();
            while( 0 < pConns.Controls.Count ) {
                pConns.Controls.RemoveAt(0);
            }      
        }
        
        
        
        void WriteCfgToFile(string filePath) {
            NodeCfg cfg = new NodeCfg();
            
            foreach( Control con in pConns.Controls ) {
                if ( con.GetType() != typeof(ConnPanel) ) { continue; }
                
                cfg.conns.Add( ((ConnPanel)con).ToCfgNodeCon() );
            }
            
            NodeCfg.SaveToFile(filePath, ref cfg);
        }
        
        void loadCfgFromFile(string filePath) {
            this.SuspendLayout();
            
            deleteAllConnections();
            
            NodeCfg cfg = NodeCfg.ReadFromFile(filePath);
            foreach( NodeCfgConn nodeCfgCon in cfg.conns) {
                ConnPanel connPanelI = ConnPanel.FromCfgNodeConn(nodeCfgCon);
                
                connPanelI.Dock = DockStyle.Top;
                pConns.Controls.Add(connPanelI);
                
                connPanelI.DataReceived += delegate (ref byte[] dataOut, int dataLen) {
                    DataReceived(connPanelI, ref dataOut, dataLen);
                };
                
                conns.Add(connPanelI);
            }
            this.ResumeLayout();
        }
        
        void TlStrip_cfgLoadClick(object sender, EventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            if( ofd.ShowDialog() == DialogResult.OK ) {
                loadCfgFromFile( ofd.FileName );
            }
        }
        
        
        void TlStrip_cfgSaveClick(object sender, EventArgs e) {
            SaveFileDialog sfd = new SaveFileDialog();
            if( sfd.ShowDialog() == DialogResult.OK ) {
                WriteCfgToFile( sfd.FileName );
            }
        }
        
        void TlStrip_AddNewConnClick(object sender, EventArgs e) {
            addNewConn();
        }
        
       
        
        void DeleteAllToolStripMenuItemClick(object sender, EventArgs e) {
            deleteAllConnections();
        }
        

    }

}
