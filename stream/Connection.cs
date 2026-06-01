/*
 * Created by SharpDevelop.
 * User: AdamS-0
 * Date: 30.05.2025
 * Time: 20:03
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;

namespace stream {
    
    public delegate void DataReceivedEvent(ref byte[] dataOut, int dataLen);
    public delegate int ReportedInternalMessageEvent(InterenalMessageType msgType, string moduleSender, string message, bool newLine = true);
    
    /// <summary>
    /// Description of Connection.
    /// </summary>
    public class Connection {
        public DataReceivedEvent DataReceived;
        public ReportedInternalMessageEvent ReportedInternalMessage;
        
        protected bool _isConnected = false;
        
        protected Stopwatch _stopWatch = new Stopwatch();
        private int _timeoutRestart = 1000; //! [ms]
        public void SetRxTimeout(int timeout_ms) { _timeoutRestart = timeout_ms; }
        
        public bool Timeouted() {
            bool timeouted = _stopWatch.ElapsedMilliseconds > _timeoutRestart;
            if ( timeouted ) { _stopWatch.Restart(); }
            return timeouted;
        }
        
        protected void RestartTimeoutRx() { _stopWatch.Restart(); }
        
        public virtual bool IsConnected() { return _isConnected; }
        
        public Connection() { }
        
        public virtual bool Prepare(ref Options opt, bool useColors = true) { return false; }
        
        public virtual void Send(ref byte[] dataOut, int dataLen) { }
        
        public virtual bool Open() { return false; }
        
        public override string ToString() { return ""; }
        
        protected void ReportInternalMessage(InterenalMessageType msgType, string moduleSender, string message, bool newLine = true) {
            if ( this.ReportedInternalMessage == null ) { return; }
            
            this.ReportedInternalMessage(msgType, moduleSender, message, newLine);
        }
    }
}
