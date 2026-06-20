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
using System.Threading;
using System.Collections.Generic;

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
        
        short ttl = 1;
        
        List<ClientContext> clients = new List<ClientContext>();
        
        public ConnTCP() {
        }
        
        
        public override bool IsConnected() {
            try {
                return tcp.Connected;
            } catch { return false; }
        }
        
        public override bool Prepare(ref Options opt, bool useColors = true) {
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
                            ReportInternalMessage(InterenalMessageType.Error, "TCP.Initializer",
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
                    ReportInternalMessage(InterenalMessageType.Error, "TCP.Initializer",
                        "Invalid IPv4 address!");
                    return false;
                }
            } else { opt.address = _IPAddr; }
            
            if (opt.port != null) {
                try {
                    _IPPort = Convert.ToInt32(opt.port);
                } catch {
                    ReportInternalMessage(InterenalMessageType.Error, "TCP.Initializer",
                        "Incorrect value for '--port': {" + opt.port + "}");
                    return false;
                }
            } else { opt.port = ipPort; }
            
            if ( opt.server != null ) { asServer = true; }
            
            if ( opt.ttl != null ) {
                try {
                    ttl = (short)Convert.ToInt16(opt.ttl);
                } catch {
                    ReportInternalMessage(InterenalMessageType.Error, "TCP.Initializer",
                        "Incorrect value for '-ttl': {" + opt.ttl + "}");
                    return false;
                }
            }
            
            return true;
        }
        
        
        public override void Send(ref byte[] dataOut, int dataLen) {
            try {
                if ( asServer ) {
                    asServerSendToClients(ref dataOut, dataLen);
                } else if ( tcp != null ) {
                    if( tcp.Client.Connected ) {
                        tcp.Client.Send(dataOut, 0, dataLen, SocketFlags.None);
                    }
                }
            } catch (Exception ex) {
                if( tcp != null ) { tcp.Close(); }
                ReportInternalMessage(InterenalMessageType.Error, "TCP.Send",
                    ex.Message);
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
                    server.Server.Ttl = ttl;
                    server.Server.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.IpTimeToLive, ttl);
                    server.Start();
                    Console.WriteLine();
                    ReportInternalMessage(InterenalMessageType.Info, "TCP.Srv.Open",
                        "Waiting for clients...");
                    
                    server.BeginAcceptTcpClient(OnClientAccepted, server);
                } else {
                    Console.WriteLine();
                    ReportInternalMessage(InterenalMessageType.Info, "TCP.Cl.Open",
                        "Connecting to the TCP server: " + _IPAddr + ":" + _IPPort + " ...");
                    tcp = new TcpClient(_IPAddr, _IPPort);
                    tcp.Client.Ttl = ttl;
                    tcp.Client.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.IpTimeToLive, ttl);
                    
                    _isConnected = true;
                    _stopWatch.Restart();
                    tcp.Client.BeginReceive(state.buffer, 0, bufSize, SocketFlags.None, new AsyncCallback(ReceivedCallback), state);
                }
            } catch (Exception ex) {
                tcp.Close();
                ReportInternalMessage(InterenalMessageType.Error, "TCP.Open",
                        ex.Message);
                Console.CursorTop -= 3;
                _isConnected = false;
            }
            
            return _isConnected;
        }
        
        
        
        public override string ToString() {
            return "TCP " + (asServer ? "Server at" : "Client to") + " " + _IPAddr + ":" + _IPPort + ", ttl(" + ttl + ")";
        }
        
        private void ReceivedCallback(IAsyncResult ar) {
            _stopWatch.Restart();
            try {
                State so = (State)ar.AsyncState;
                int bytes = tcp.Client.EndReceiveFrom(ar, ref epFrom);
                if( this.DataReceived != null ) {
                    this.DataReceived(ref state.buffer, bytes);
                }
                tcp.Client.BeginReceive(state.buffer, 0, bufSize, SocketFlags.None, new AsyncCallback(ReceivedCallback), state);
                _isConnected = true;
            } catch (Exception ex) {
                tcp.Close();
                ReportInternalMessage(InterenalMessageType.Error, "TCP.Cl.DataRxCallback",
                        ex.Message);
                _isConnected = false;
            }
        }
        
        
        private void ReceivedCallbackAsServer(IAsyncResult ar) {
            try {
                ClientContext context = (ClientContext)ar.AsyncState;
                int bytes = context.Client.Client.EndReceiveFrom(ar, ref epFrom);
                context.Client.Client.BeginReceive(context.buffer, 0, ClientContext.bufSizeAsServerPerClient,
                                                   SocketFlags.None, new AsyncCallback(ReceivedCallbackAsServer), context);
                if( this.DataReceived != null ) {
                    this.DataReceived(ref context.buffer, bytes);
                }
                
                
                asServerSendToClients( ref context.buffer, bytes, context );
                    
                _isConnected = true;
            } catch (Exception ex) {
                tcp.Close();
                ReportInternalMessage(InterenalMessageType.Error, "TCP.Srv.DataRxCallback",
                    ex.Message);
                _isConnected = false;
            }
        }
        
        
        
        void OnClientAccepted(IAsyncResult ar) {
            TcpListener listener = ar.AsyncState as TcpListener;
            if (listener == null) return;
            
            try {
                ClientContext context = new ClientContext();
                context.Client = server.EndAcceptTcpClient(ar);
                context.Client.Client.Ttl = ttl;
                context.Client.Client.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.IpTimeToLive, ttl);
                context.Client.Client.BeginReceive(context.buffer, 0, ClientContext.bufSizeAsServerPerClient,
                                                   SocketFlags.None, new AsyncCallback(ReceivedCallbackAsServer), context);
                clients.Add(context);
                ReportInternalMessage(InterenalMessageType.Info, "TCP.Srv.OnClientAccepted",
                        "New connection from " + context.Client.Client.RemoteEndPoint.ToString() );
            }
            catch (Exception ex) {
                ReportInternalMessage(InterenalMessageType.Error, "TCP.Srv.OnClientAccepted",
                    ex.Message );
            } 
            finally {
                server.BeginAcceptTcpClient(OnClientAccepted, server);
            }
        }
        
        void asServerSendToClients(ref byte[] dataOut, int dataLen, ClientContext sender = null) {
            foreach ( ClientContext cli in clients ) {
                if ( sender != null ) {
                    if ( cli == sender ) { continue; }
                }
                
                try {
                    if( cli.Client.Connected ) {
                        cli.Client.Client.Send(dataOut, 0, dataLen, SocketFlags.None);
                    }
                } catch (Exception ex) {
                    ReportInternalMessage(InterenalMessageType.Error, "TCP.Srv.SendToClients",
                        ex.Message );
                }
            }
        }
        
    }
    
    class ClientContext {
        public TcpClient Client = new TcpClient();
        public const int bufSizeAsServerPerClient = 8 * 1024;
        public byte[] buffer = new byte[bufSizeAsServerPerClient];
    }
}
