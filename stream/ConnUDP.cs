/*
 * Created by SharpDevelop.
 * User: AdamS-0
 * Date: 30.05.2025
 * Time: 20:54
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace stream {
    /// <summary>
    /// Description of ConnUDP.
    /// </summary>
    public class ConnUDP : Connection {
        public class State {
            public byte[] buffer = new byte[bufSize];
        }
        
        Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        const int bufSize = 8 * 1024;
        State state = new State();
        EndPoint epFrom = new IPEndPoint(IPAddress.Any, 0);
        AsyncCallback recv = null;
        
        string _IPAddr = "";
        int _IPPort = 0;
        
        bool asServer = false;
        
        short ttl = 1;
        
        public ConnUDP() {
        }
        
        public override bool Prepare(ref Options opt, bool useColors = true) {
            string ipPort = "";
            
            if (opt.udp != null) {
                if (opt.udp.Length > 0) {
                    if (opt.udp.Contains(":")) {
                        try {
                            string[] parts = opt.udp.Split(':');
                            opt.udp = "";
                            _IPAddr = parts [ 0 ];
                            ipPort = parts [ 1 ];
                            _IPPort = Convert.ToInt32(parts [ 1 ]);
                        } catch {
                            ReportInternalMessage(InterenalMessageType.Error, "UDP.Initializer",
                                "Error durign IPAddress parsing!");
                            return false;
                        }
                    }
                }
            }
            
            if (opt.address != null) {
                if (opt.address.Length > 0) {
                    _IPAddr = opt.address;
                } else {
                    ReportInternalMessage(InterenalMessageType.Error, "UDP.Initializer",
                        "Invalid IPv4 address!");
                    return false;
                }
            } else { opt.address = _IPAddr; }
            
            if (opt.port != null) {
                try {
                    _IPPort = Convert.ToInt32(opt.port);
                } catch {
                    ReportInternalMessage(InterenalMessageType.Error, "UDP.Initializer",
                        "Incorrect value for '--port': {" + opt.port + "}");
                    return false;
                }
            }  else { opt.port = ipPort; }
            
            if ( opt.server != null ) { asServer = true; }
            
            if ( opt.ttl != null ) {
                try {
                    ttl = (short)Convert.ToInt16(opt.ttl);
                } catch {
                    ReportInternalMessage(InterenalMessageType.Error, "UDP.Initializer",
                        "Incorrect value for '-ttl': {" + opt.ttl + "}");
                    return false;
                }
            }
            
            return true;
        }
        
        public override void Send(ref byte[] dataOut, int dataLen) {
            try {
                if(asServer) {
                    _socket.SendTo(dataOut, 0, dataLen, SocketFlags.None, epFrom );
                } else {
                    _socket.BeginSend(dataOut, 0, dataLen, SocketFlags.None, (ar) =>
                    {
                        State so = (State)ar.AsyncState;
                        int bytes = _socket.EndSend(ar);
                    }, state);
                }
            } catch (Exception ex) { 
                ReportInternalMessage(InterenalMessageType.Error, "UDP.Send", ex.Message);
            }
        }
                
        
        public override bool Open() {
            try {
                UdpClient cl = new UdpClient();
                
                if( asServer ) {
                    _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
                    _socket.Ttl = ttl;
                    _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.IpTimeToLive, ttl);
                    _socket.Bind(new IPEndPoint(IPAddress.Parse(_IPAddr), _IPPort));
                } else {
                    _socket.Connect(IPAddress.Parse(_IPAddr), _IPPort);
                }
                
                _isConnected = true;
                _stopWatch.Restart();
                _socket.BeginReceiveFrom(state.buffer, 0, bufSize, SocketFlags.None,
                             ref epFrom, new AsyncCallback(ReceivedCallback), state);
            } catch (Exception ex) {
                ReportInternalMessage(InterenalMessageType.Error, "UDP.Open", ex.Message);
            }
            
            return _isConnected;
        }
        
        public override string ToString() {
            return "UDP " + (asServer ? "Server at" : "Client to") + " " + _IPAddr + ":" + _IPPort + ", ttl(" + ttl + ")";
        }
        
        private void ReceivedCallback(IAsyncResult ar) {
            _stopWatch.Restart();
            try {
                State so = (State)ar.AsyncState;
                int bytes = _socket.EndReceiveFrom(ar, ref epFrom);
                if( this.DataReceived != null ) {
                    this.DataReceived(ref state.buffer, bytes);
                }
                _socket.BeginReceive(state.buffer, 0, bufSize, SocketFlags.None, new AsyncCallback(ReceivedCallback), state);
                _isConnected = true;
            } catch (Exception ex) {
                ReportInternalMessage(InterenalMessageType.Error, "UDP.DataRxCallback", ex.Message);
                _isConnected = false;
            }
        }
        
        
    }
}
