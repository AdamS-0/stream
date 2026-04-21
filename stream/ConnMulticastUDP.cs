/*
 * Created by SharpDevelop.
 * User: AdamS-0
 * Date: 30.05.2025
 * Time: 20:34
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net;
using System.Net.Sockets;

namespace stream {
    /// <summary>
    /// Description of ConnMulticastUDP.
    /// </summary>
    public class ConnMulticastUDP : Connection {
        
        static string _IPAddr = "";
        static int _IPPort = 0;
        
        static UdpClient udpClient;
        static IPAddress multicastAddr;
        static IPAddress localAddress = IPAddress.Any;
        static IPEndPoint localEP;
        static IPEndPoint remoteEP;
        
        short ttl = 1;
        
        public ConnMulticastUDP() {
            
        }
        
        public override bool Prepare(ref Options opt, bool useColors = true) {
            string ipPort = "";
            
            if (opt.mudp != null) {
                if (opt.mudp.Length > 0) {
                    if (opt.mudp.Contains(":")) {
                        try {
                            string[] parts = opt.mudp.Split(':');
                            opt.mudp = "";
                            _IPAddr = parts [ 0 ];
                            ipPort = parts [ 1 ];
                            _IPPort = Convert.ToInt32(parts [ 1 ]);
                        } catch {
                            ReportInternalMessage(InterenalMessageType.Error, "MUDP.Initializer",
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
                    ReportInternalMessage(InterenalMessageType.Error, "MUDP.Initializer",
                        "Invalid IPv4 address!");
                    return false;
                }
            } else { opt.address = _IPAddr; }
            
            if (opt.port != null) {
                try {
                    _IPPort = Convert.ToInt32(opt.port);
                } catch {
                    ReportInternalMessage(InterenalMessageType.Error, "MUDP.Initializer",
                        "Incorrect value for '--port': {" + opt.port + "}");
                    return false;
                }
            } else { opt.port = ipPort; }
            
            if ( opt.ttl != null ) {
                try {
                    ttl = (short)Convert.ToInt16(opt.ttl);
                } catch {
                    ReportInternalMessage(InterenalMessageType.Error, "MUDP.Initializer",
                        "Incorrect value for '-ttl': {" + opt.ttl + "}");
                    return false;
                }
            }
            
            return true;
        }
        
        public override void Send(ref byte[] dataOut, int dataLen) {
            try {
                udpClient.Send(dataOut, dataLen, remoteEP);
            } catch (Exception ex) {
                ReportInternalMessage(InterenalMessageType.Error, "MUDP.Send",
                  ex.Message );
            }
        }
        
        
        // obsługa odbioru datagramów - UDP multicast
        private void ReceivedCallback(IAsyncResult ar) {
            try {
                IPEndPoint sender = new IPEndPoint(0, 0);
                byte[] dataIn = udpClient.EndReceive(ar, ref sender);
    
                // jeżeli zdarzenie Received może zostać obsłużone to je wywołaj
                if( this.DataReceived != null ) {
                    this.DataReceived(ref dataIn, dataIn.Length);
                }
                // wskazanie metody obsługującej odbiór datagramu (jednorazowe),
                // czyli tutaj akurat rekurencja do samej siebie
                udpClient.BeginReceive(new AsyncCallback(ReceivedCallback), null);
            } catch (Exception ex) {
                ReportInternalMessage(InterenalMessageType.Error, "MUDP.DataRxCallback",
                    ex.Message);
            }
        }
        
        
        public override bool Open() {
            try {
                multicastAddr = IPAddress.Parse(_IPAddr);
                localAddress = IPAddress.Any;
                localEP = new IPEndPoint(localAddress, _IPPort);
                remoteEP = new IPEndPoint(multicastAddr, _IPPort);
                
                udpClient = new UdpClient(AddressFamily.InterNetwork);
                    
                // umozliwienie wielu aplikacjom dostępu do jednego portu
                udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                udpClient.ExclusiveAddressUse = false;
                
                udpClient.Client.Ttl = ttl;
                udpClient.Client.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.IpTimeToLive, ttl);
                
                // podpięcie klienta do nasłuchiwania
                udpClient.Client.Bind(localEP);
        
                // dołączenie do grupy multicastowej
                udpClient.JoinMulticastGroup(multicastAddr, localAddress);
        
                // wskazanie metody obsługującej odbiór datagramu (jednorazowe)
                udpClient.BeginReceive(new AsyncCallback(ReceivedCallback), null);
                
                _isConnected = true;
            } catch (Exception ex) {
                ReportInternalMessage(InterenalMessageType.Error, "MUDP.DataRxCallback",
                  ex.Message );
            }
            return _isConnected;
        }
        
        
        public override string ToString() {
            return "Multicast at " + _IPAddr + ":" + _IPPort + ", ttl(" + ttl + ")";
        }
    }
}
