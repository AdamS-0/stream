/*
 * Created by SharpDevelop.
 * User: AdamS-0
 * Date: 30.05.2025
 * Time: 20:03
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace stream {
    
    public delegate void DataReceivedEvent(ref byte[] dataOut, int dataLen);
    
    /// <summary>
    /// Description of Connection.
    /// </summary>
    public class Connection {
        public DataReceivedEvent DataReceived;
        
        protected bool _isConnected = false;
        
        public virtual bool IsConnected() { return _isConnected; }
        
        public Connection() { }
        
        public virtual bool Prepare(ref Options opt) { return false; }
        
        public virtual void Send(ref byte[] dataOut, int dataLen) { }
        
        public virtual bool Open() { return false; }
    }
}
