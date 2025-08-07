/*
 * Created by SharpDevelop.
 * User: AdamS-0
 * Date: 01.06.2025
 * Time: 12:58
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net;
using System.Net.Sockets;

namespace stream {
    /// <summary>
    /// Description of ConnTCP.
    /// </summary>
    public class ConnTCP : Connection {
        public class State {
            public byte[] buffer = new byte[bufSize];
        }
        
        const int bufSize = 8 * 1024;
        State state = new State();
        TcpClient tcp = new TcpClient();
        EndPoint epFrom = new IPEndPoint(IPAddress.Any, 0);
        TcpListener server;
        
        string _IPAddr = "";
        int _IPPort = 0;
        bool asServer = false;
        
        public ConnTCP() {
        }
        
        
        public override bool IsConnected() {
            try {
                return tcp.Connected;
            } catch { return false; }
        }
        
        public override bool Prepare(ref Options opt) {
            string ipPort = "";
            
            if (opt.tcp != null) {
                if (opt.tcp.Length > 0) {
                    if (opt.tcp.Contains(":")) {
                        try {
                            string[] parts = opt.tcp.Split(':');
                            opt.tcp = "";
                            _IPAddr = parts [ 0 ];
                            ipPort = parts [ 1 ];
                            _IPPort = Convert.ToInt32(parts [ 1 ]);
                        } catch {
                            Console.WriteLine("Error durign IPAddress parsing!");
                            return false;
                        }
                    }
                }
            }
            
            if (opt.address != null) {
                if (opt.address.Length > 0) {
                    _IPAddr = opt.address;
                } else {
                    Console.WriteLine("Invalid IPv4 address!");
                    return false;
                }
            } else { opt.address = _IPAddr; }
            
            if (opt.port != null) {
                try {
                    _IPPort = Convert.ToInt32(opt.port);
                } catch {
                    Console.WriteLine("Incorrect value for '--port': {" + opt.port + "}");
                    return false;
                }
            } else { opt.port = ipPort; }
            
            if ( opt.server != null ) { asServer = true; }
            
            return true;
        }
        
        
        public override void Send(ref byte[] dataOut, int dataLen) {
            try {
                if( tcp != null ) {
                    if( tcp.Client.Connected ) {
                        tcp.Client.Send(dataOut, 0, dataLen, SocketFlags.None);
                    }
                }
            } catch (Exception ex) {
                if( tcp != null ) { tcp.Close(); }
                Console.WriteLine("<stream.error>[TCP send] " + ex.Message);
                _isConnected = false;
            }
        }
        
        
        
        public override bool Open() {
            try {
                if(asServer) {

                    if( server != null ) {
                        if(server.Server != null) {
                            server.Server.Dispose();
                        }
                    }
                    
                    server = new TcpListener(IPAddress.Parse(_IPAddr), _IPPort);
                    
                    server.Start();
                    Console.WriteLine("Waiting for client...");
                    tcp = server.AcceptTcpClient();
                } else {
                    Console.WriteLine("Connecting to the TCP server: {0}:{1} ...", _IPAddr, _IPPort);
                    tcp = new TcpClient(_IPAddr, _IPPort);
                }
                _isConnected = true;
                tcp.Client.BeginReceive(state.buffer, 0, bufSize, SocketFlags.None, new AsyncCallback(ReceivedCallback), state);
            } catch (Exception ex) {
                tcp.Close();
                Console.WriteLine(ex.Message);
                _isConnected = false;
            }
            
            return _isConnected;
        }
        
        private void ReceivedCallback(IAsyncResult ar) {
            try {
                State so = (State)ar.AsyncState;
                int bytes = tcp.Client.EndReceiveFrom(ar, ref epFrom);
                tcp.Client.BeginReceive(state.buffer, 0, bufSize, SocketFlags.None, new AsyncCallback(ReceivedCallback), state);
                if( this.DataReceived != null ) {
                    this.DataReceived(ref state.buffer, bytes);
                }
                _isConnected = true;
            } catch (Exception ex) {
                tcp.Close();
                Console.WriteLine("<stream.error>[TCP rxClbck] " + ex.Message);
                _isConnected = false;
            }
        }
    }
}
