/*
 * Created by SharpDevelop.
 * User: AdamS-0
 * Date: 23.03.2025
 * Time: 13:14
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace stream {
    class Program {
        
        enum OpenedConnection {
            None,
            SerialPort,
            TCP,
            UDP,
        }
        
        static SerialPort _sp = new SerialPort();
        static OpenedConnection _connectedWith = OpenedConnection.None;
        
        static string _IPAddr = "";
        static int _IPPort = 0;
        static bool _UDPMulticats = true;
        
        static UdpClient udpClient;
        static IPAddress multicastAddr;
        static IPAddress localAddress = IPAddress.Any;
        static IPEndPoint localEP;
        static IPEndPoint remoteEP;
        static Stream strm = Console.OpenStandardOutput();
        
        
        public static void Main(string[] args) {
            byte[] dataOut = new byte[1024];
            int len2copy = 0;
            int idx = 0;
            
            if(args.Length == 0) { printHelp(); return; }
            
            bool status = parseArgs(args);
            Stream inStream = Console.OpenStandardInput();
            
            while(status) {
                Thread.Sleep(10);
                while( (len2copy = inStream.Read(dataOut, 0, 1024)) > 0 ) {
                    send( ref dataOut, len2copy );
                }
            }
        }
        
        static void printHelp() {
            Console.WriteLine(
                "stream\r\n" +
                "\t-cp\r\n"+
                "\t\t--name COM1\r\n"+
                "\t\t--baud 115200 (default: 9600)\r\n"+
                "\t\t--parity [None/Odd/Even/Mark/Space] (default: None)\r\n"+
                "\t\t--dataBits ## (default: 8)\r\n"+
                "\t\t--stopBits [None/One/Two/OnePointFive] (default: One)\r\n"+
                "\r\n"+
                "\t-udp [IPAddress:Port]\r\n"+
                "\t\t--address #.#.#.#\r\n"+
                "\t\t--port #\r\n"
                );
                
//                stream -udp 127.0.0.1:16987
//                stream -tcp 127.0.0.1:16987
        }
        
        
        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e) {
            try {
                SerialPort sp = (SerialPort)sender;
                int bytesToRead = sp.BytesToRead;
                byte[] dataIn = new byte[bytesToRead];
                sp.Read(dataIn, 0, bytesToRead);
    //            Console.Write(dataIn);
    //            Console.OpenStandardOutput().Write(dataIn, 0, bytesToRead);
                strm.Write(dataIn, 0, dataIn.Length);
    //            Console.Write(Encoding.Default.GetString(dataIn));
            } catch {}
        }
        
        
        // obsługa odbioru datagramów - UDP multicast
        private static void ReceivedCallback(IAsyncResult ar) {
            try {
                IPEndPoint sender = new IPEndPoint(0, 0);
                byte[] dataIn = udpClient.EndReceive(ar, ref sender);
    
                // jeżeli zdarzenie Received może zostać obsłużone to je wywołaj
//                if (Received != null) Received(dataIn);
//                Console.Write(Encoding.Default.GetString(dataIn));
                strm.Write(dataIn, 0, dataIn.Length);
                    
                // wskazanie metody obsługującej odbiór datagramu (jednorazowe),
                // czyli tutaj akurat rekurencja do samej siebie
                udpClient.BeginReceive(new AsyncCallback(ReceivedCallback), null);
            } catch (Exception ex) {  }
        }
        
        
        private static void send(ref byte[] dataOut, int dataLen) {
            switch(_connectedWith) {
                case OpenedConnection.SerialPort:
                    if( !_sp.IsOpen ) { break; }
                    _sp.Write(dataOut, 0, dataLen);
                    break;
                case OpenedConnection.TCP: break;
                case OpenedConnection.UDP:
                        udpClient.Send(dataOut, dataLen, remoteEP);
                    break;
//                default: ;
            }
        }
        
        
        static bool parseArgs(string[] args) {
            bool status = false;
            if(args.Length == 0) { return false; }
            
            if      ( args[0] == "--help" ) { printHelp(); return false; }
            else if ( args[0] == "-cp"  ) {
                status = parseComPort(args);
                if( !status ) { return status; }
                /* Correct parameters of COM port configuration */
                
                try {
                    _sp.DataReceived += DataReceivedHandler;
                    _sp.Open();
                    _connectedWith = OpenedConnection.SerialPort;
//                    _sp.BaseStream;
                } catch (Exception ex){
                    Console.WriteLine( "Could not open COM port" );
                    Console.WriteLine(ex.Message);
                    return false;
                }
                status = true;
            }
            else if ( args[0] == "-tcp" ) { status = parseTCP(args); }
            else if ( args[0] == "-udp" ) {
                status = parseUDP(args);
                
                multicastAddr = IPAddress.Parse(_IPAddr);
                localAddress = IPAddress.Any;
                localEP = new IPEndPoint(localAddress, _IPPort);
                remoteEP = new IPEndPoint(multicastAddr, _IPPort);
                
                udpClient = new UdpClient(AddressFamily.InterNetwork);
                if( _UDPMulticats ) {
                    // umozliwienie wielu aplikacjom dostępu do jednego portu
                    udpClient.Client.SetSocketOption( SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true );
                    udpClient.ExclusiveAddressUse = false;
                    
                    // podpięcie klienta do nasłuchiwania
                    udpClient.Client.Bind(localEP);
            
                    // dołączenie do grupy multicastowej
                    udpClient.JoinMulticastGroup(multicastAddr, localAddress);
            
                    // wskazanie metody obsługującej odbiór datagramu (jednorazowe)
                    udpClient.BeginReceive( new AsyncCallback(ReceivedCallback), null );
                    _connectedWith = OpenedConnection.UDP;
                }
                
            }
            
            return status;
            
        }
        
        static bool parseComPort(string[] args) {
            string paramName = "";
            string paramValue = "";
            
            if( _sp == null ) { _sp = new SerialPort(); }
            if( _sp.IsOpen ) { _sp.Close(); }
            _sp.Dispose();
            _sp = new SerialPort();
            
            for( int i = 1; i < args.Length; i += 2 ) {
                if( args.Length <= (i + 1) ) {
                    Console.WriteLine("Missing value for parameter '" + args[i] + "'");
                    return false;
                }
                
                paramName = args[i].ToLower();
                paramValue = args[i+1];
                
                
                if      ( paramName == "--name" ) { _sp.PortName = paramValue; }
                else if ( paramName == "--baud" ) {
                    try {
                        _sp.BaudRate = Convert.ToInt32(paramValue);
                    } catch {
                        Console.WriteLine("Incorrect value for '--baud': {" + paramValue + "}");
                        return false;
                    }
                }
                else if ( paramName == "--parity" ) {
                    paramValue = paramValue.ToLower();
                    if      ( paramValue == "none"  ) { _sp.Parity = Parity.None;  }
                    else if ( paramValue == "odd"   ) { _sp.Parity = Parity.Odd;   }
                    else if ( paramValue == "even"  ) { _sp.Parity = Parity.Even;  }
                    else if ( paramValue == "mark"  ) { _sp.Parity = Parity.Mark;  }
                    else if ( paramValue == "space" ) { _sp.Parity = Parity.Space; }
                    else { Console.WriteLine("Incorrect value for '--parity': {" + args[i+1] + "}"); return false; }
                }
                else if ( paramName == "--databits" ) {
                    try {
                        _sp.DataBits = Convert.ToInt32(paramValue);
                    } catch {
                        Console.WriteLine("Incorrect value for '--dataBits': {" + paramValue + "}");
                        return false;
                    }
                }
                else if ( paramName == "--stopbits" ) {
                    paramValue = paramValue.ToLower();
                    if      ( paramValue == "none"          ) { _sp.StopBits = StopBits.None;           }
                    else if ( paramValue == "one"           ) { _sp.StopBits = StopBits.One;            }
                    else if ( paramValue == "two"           ) { _sp.StopBits = StopBits.Two;            }
                    else if ( paramValue == "onepointfive"  ) { _sp.StopBits = StopBits.OnePointFive;   }
                    else { Console.WriteLine("Incorrect value for '--stopBits': {" + args[i+1] + "}"); return false;}
                } else { Console.WriteLine( "Incorrect parameter: '" + args[i] + "'" ); return false; }
            }
            
            return true;
        }
        
        static bool parseTCP(string[] args) {
            return false;
        }
        
        static bool parseUDP(string[] args) {
            string paramName = "";
            string paramValue = "";
            
            if( _sp == null ) { _sp = new SerialPort(); }
            if( _sp.IsOpen ) { _sp.Close(); }
            _sp.Dispose();
            _sp = new SerialPort();
            _sp.DataReceived += DataReceivedHandler;
            
            for( int i = 1; i < args.Length; i += 2 ) {
                
                if( args[i].Contains(":") ) {
                    try {
                        string[] parts = args[i].Split(':');
                        _IPAddr = parts[0];
                        _IPPort = Convert.ToInt32(parts[1]);
                        i++;
                        continue;
                    } catch {
                        Console.WriteLine("Error durign IPAddress parsing!");
                        return false;
                    }
                }
                
                if( args.Length <= (i + 1) ) {
                    Console.WriteLine("Missing value for parameter '" + args[i] + "'");
                    return false;
                }
                
                paramName = args[i].ToLower();
                paramValue = args[i+1];
                
                
                if      ( paramName == "--address" ) { _IPAddr = paramValue; }
                else if ( paramName == "--port" ) {
                    try {
                        _IPPort = Convert.ToInt32(paramValue);
                    } catch {
                        Console.WriteLine("Incorrect value for '--port': {" + paramValue + "}");
                        return false;
                    }
                }
                else { Console.WriteLine( "Incorrect parameter: '" + args[i] + "'" ); return false; }
            }
            
            return true;
        }

        
    }
}