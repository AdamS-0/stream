/*
 * Created by SharpDevelop.
 * User: AdamS-0
 * Date: 05.08.2025
 * Time: 16:45
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace streamNode {
    /// <summary>
    /// Description of Terminal.
    /// </summary>
    public partial class Terminal : Form {
        public delegate void SendDataFromTerminalEvent(string msg);
        public SendDataFromTerminalEvent SendDataFromTerminal;
        
        public Terminal() {
            InitializeComponent();
            
        }
        
        
        
        public void AppendText(string message) {
            try {
                rtbTerm.Invoke((MethodInvoker)(() => {
                                                rtbTerm.AppendText(message);
                                                if ( tlStrip_autoscroll.Checked ) {
                                                    rtbTerm.ScrollToCaret();
                                                }
                }));
            } catch {}
        }
        
        void TxtBxData2SendTextChanged(object sender, EventArgs e) {
            
        }
        
        void TxtBxData2SendKeyDown(object sender, KeyEventArgs e) {
            if( (e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return) ) {
                if( this.SendDataFromTerminal != null ) {
                    this.SendDataFromTerminal( txtBxData2Send.Text + (tlStrip_newline.Text == "CR+LF" ? "\r" : "" ) + "\n" );
                }
            }
        }
        void TlStrip_clearClick(object sender, EventArgs e) {
            rtbTerm.Text = "";
        }
        
        
    }
}
