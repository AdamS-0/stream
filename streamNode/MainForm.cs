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
            /* Get object as connection panel */
            ConnPanel conPanel = sender as ConnPanel;
            
            /* If this connection is only out - skip this data */
            if( !conPanel.CanRead() ) { return; }
            
            /* Send data to the local terminal */
            Console.Write(System.Text.Encoding.GetEncoding("windows-1256").GetString(dataOut, 0, dataLen));
//            richTextBox1.AppendText(
//               System.Text.Encoding.GetEncoding("windows-1256").GetString(dataOut)
//            );
            
            /* Send data to other connections in this node */
            foreach( ConnPanel cp in conns ) {
                /* Iff this is sender, skip it */
                if( cp == conPanel ) { continue; }
                
                /* If writing was disabled, skip loop */
                if( !cp.CanWrite() ) { continue; }
                
                cp.Send(ref dataOut, dataLen);
            }
        }
        
        
        
        void PrintDataToTerminal() {
            
        }
        
        void MainFormFormClosing(object sender, FormClosingEventArgs e) {
            foreach( ConnPanel cp in conns ) {
                cp.Close();
            }
        }
        
        void ToolStripLabel2Click(object sender, EventArgs e) {
            addNewConn();
        }
        

    }
}
