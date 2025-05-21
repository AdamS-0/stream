/*
 * Created by SharpDevelop.
 * User: AdamS-0
 * Date: 23.03.2025
 * Time: 13:14
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using System.Management;
using Microsoft.Win32;

using CommandLineParser;

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
            bool status = true;
            
            Options opt = CommandLineParser.CommandLineParser.ParseArguments<Options>(args);
            
            if (opt.com != null) {
                if (opt.list != null) {
//                    _Seerial_showAllPorts();
                    _Seerial_showAllPorts2();
                    return;
                }
                
                status = parseComPort(opt);
                if (!status) { return; }
                /* Correct parameters of COM port configuration */
                
                try {
                    _sp.DataReceived += DataReceivedHandler;
                    _sp.Open();
                    _connectedWith = OpenedConnection.SerialPort;
//                    _sp.BaseStream;
                } catch (Exception ex) {
                    Console.WriteLine("Could not open COM port");
                    Console.WriteLine(ex.Message);
                    return;
                }
                status = true;
                
            } else if (opt.mudp != null) {
                status = parseMulticastUDP(opt);
                
                multicastAddr = IPAddress.Parse(_IPAddr);
                localAddress = IPAddress.Any;
                localEP = new IPEndPoint(localAddress, _IPPort);
                remoteEP = new IPEndPoint(multicastAddr, _IPPort);
                
                udpClient = new UdpClient(AddressFamily.InterNetwork);
                if (_UDPMulticats) {
                    // umozliwienie wielu aplikacjom dostępu do jednego portu
                    udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                    udpClient.ExclusiveAddressUse = false;
                    
                    // podpięcie klienta do nasłuchiwania
                    udpClient.Client.Bind(localEP);
            
                    // dołączenie do grupy multicastowej
                    udpClient.JoinMulticastGroup(multicastAddr, localAddress);
            
                    // wskazanie metody obsługującej odbiór datagramu (jednorazowe)
                    udpClient.BeginReceive(new AsyncCallback(ReceivedCallback), null);
                    _connectedWith = OpenedConnection.UDP;
                }
                
            } else {
                return;
            }
            
            
            Stream inStream = Console.OpenStandardInput();
            
            while (status) {
                Thread.Sleep(10);
                while ((len2copy = inStream.Read(dataOut, 0, 1024)) > 0) {
                    send(ref dataOut, len2copy);
                }
            }
        }
        
        static void _Seerial_showAllPorts() {
            string[] ports = SerialPort.GetPortNames();
            foreach (string portName in ports) {
                Console.WriteLine(">" + portName);
            }
        }
        
        static void _Seerial_showAllPorts2() {
            List<SerialPortConnInfo> conns = new List<SerialPortConnInfo>();
            
            using (ManagementClass i_Entity = new ManagementClass("Win32_PnPEntity")) {
                const String CUR_CTRL = "HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\";
            
                foreach (ManagementObject i_Inst in i_Entity.GetInstances()) {
                    Object o_Guid = i_Inst.GetPropertyValue("ClassGuid");
                    if (o_Guid == null || o_Guid.ToString().ToUpper() != "{4D36E978-E325-11CE-BFC1-08002BE10318}") continue; // Skip all devices except device class "PORTS"
            
                    String s_Caption = i_Inst.GetPropertyValue("Caption").ToString();
                    String s_Manufact = i_Inst.GetPropertyValue("Manufacturer").ToString();
                    String s_DeviceID = i_Inst.GetPropertyValue("PnpDeviceID").ToString();
                    String s_RegEnum = CUR_CTRL + "Enum\\" + s_DeviceID + "\\Device Parameters";
                    String s_RegServ = CUR_CTRL + "Services\\BTHPORT\\Parameters\\Devices\\";
                    String s_PortName = Registry.GetValue(s_RegEnum, "PortName", "").ToString();
                    String s_BT_Dir = null; // Bluetooth port direction
                    String s_BT_Dev = "";   // Bluetooth paired device name
                    String s_BT_MAC = "";   // Bluetooth paired device MAC address
            
                    int s32_Pos = s_Caption.IndexOf(" (COM");
                    if (s32_Pos > 0) // remove COM port from description
                        s_Caption = s_Caption.Substring(0, s32_Pos);
            
                    conns.Add( new SerialPortConnInfo(s_PortName, s_Caption, s_Manufact, s_DeviceID) );
                    
                    
//                    if (s_DeviceID.StartsWith("BTHENUM\\")) { // Bluetooth
//                        s_BT_Dir = "Incoming";
//            
//                        // "{00001101-0000-1000-8000-00805f9b34fb}#7445CEA614BC_C00000000"
//                        String s_UniqueID = Registry.GetValue(s_RegEnum, "Bluetooth_UniqueID", "").ToString();
//            
//                        String[] s_Split = s_UniqueID.Split('#');
//                        if (s_Split.Length == 2) {
//                            s_Split = s_Split [ 1 ].Split('_');
//            
//                            // Ignore MAC = "000000000000"
//                            if (s_Split.Length == 2 && s_Split [ 0 ].Trim('0').Length > 0) {
//                                s_BT_MAC = s_Split [ 0 ]; // 12 digits: "7445CEA614BC"
//                                s_BT_Dir = "Outgoing";
//            
//                                // Read the Bluetooth device that is paired with the COM port.
//                                Object o_BtDevice = Registry.GetValue(s_RegServ + s_BT_MAC, "Name", null);
//                                if (o_BtDevice is Byte[]) s_BT_Dev = Encoding.UTF8.GetString((Byte[])o_BtDevice);
//                            }
//                        }
//            
//                        Console.WriteLine("Port Direction: " + s_BT_Dir);
//                        Console.WriteLine("Paired Device:  " + s_BT_Dev);
//                        Console.WriteLine("Device MAC Adr: " + s_BT_MAC);
//                    }
                }
                
                
                Console.WriteLine("{0,-" + SerialPortConnInfo.maxSizeName + "}  " + 
                                  "{1,-" + SerialPortConnInfo.maxSizeDesc + "}  " + 
                                  "{2,-" + SerialPortConnInfo.maxSizeManuf + "}  " + 
                                  "{3,-" + SerialPortConnInfo.maxSizeDevID + "}",
                                  "Name", "Description", "Manufacturer", "Device ID");
                foreach( SerialPortConnInfo ci in conns ) {
                        Console.WriteLine(
                                    "{0," + SerialPortConnInfo.maxSizeName + "}  " +
                                    "{1," + SerialPortConnInfo.maxSizeDesc + "}  " + 
                                    "{2," + SerialPortConnInfo.maxSizeManuf + "}  " + 
                                    "{3," + SerialPortConnInfo.maxSizeDevID + "}",
                                    ci.Name, ci.Description, ci.Manufacturer, ci.DevID);
                }
            }
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
            } catch {
            }
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
            } catch (Exception ex) {
                Console.Write(ex.Message);
            }
        }
        
        
        private static void send(ref byte[] dataOut, int dataLen) {
            switch (_connectedWith) {
                case OpenedConnection.SerialPort:
                    if (!_sp.IsOpen) { break; }
                    _sp.Write(dataOut, 0, dataLen);
                    break;
                case OpenedConnection.TCP:
                    break;
                case OpenedConnection.UDP:
                    udpClient.Send(dataOut, dataLen, remoteEP);
                    break;
//                default: ;
            }
        }
        
        
        
        static string getSerialNameFromStrinng(string cpName) {
            if (cpName.Length > 0) {
                if (cpName.ToUpper().Contains("COM")) {
                    return cpName;
                }
                try {
                    int comNumber = 0;
                    comNumber = Convert.ToInt32(cpName);
                    return "COM" + comNumber;
                } catch (Exception ex) {
                    Console.Write(ex.Message);
                }
            }
            return "";
        }
        
        static bool parseComPort(Options opt) {
            string paramValue = "";
            int stopBits = 0;
            string portName = "";
            
            if (_sp == null) { _sp = new SerialPort(); }
            if (_sp.IsOpen) { _sp.Close(); }
            _sp.Dispose();
            _sp = new SerialPort();
            
            
            if (opt.com != null) {
                portName = getSerialNameFromStrinng(opt.com);
            }
            
            if (opt.name != null) {
                portName = getSerialNameFromStrinng(opt.name);
            }
            
            try {
                _sp.PortName = portName;
            } catch (Exception ex) {
                Console.Write(ex.Message);
                return false;
            }
            
            if (opt.baud != null) {
                try {
                    _sp.BaudRate = Convert.ToInt32(opt.baud);
                } catch {
                    Console.WriteLine("Incorrect value for '--baud': {" + opt.baud + "}");
                    return false;
                }
            }
            
            if (opt.parity != null) {
                paramValue = opt.parity.ToLower();
                if (paramValue == "none") { _sp.Parity = Parity.None;  } else if (paramValue == "odd") { _sp.Parity = Parity.Odd;   } else if (paramValue == "even") { _sp.Parity = Parity.Even;  } else if (paramValue == "mark") { _sp.Parity = Parity.Mark;  } else if (paramValue == "space") { _sp.Parity = Parity.Space; } else {
                    Console.WriteLine("Incorrect value for '--parity': {" + opt.parity + "}");
                    return false;
                }
            }
            
            if (opt.dataBits != null) {
                try {
                    _sp.DataBits = Convert.ToInt32(opt.dataBits);
                } catch {
                    Console.WriteLine("Incorrect value for '--dataBits': {" + opt.dataBits + "}");
                    return false;
                }
            }
            
            if (opt.stopBits != null) {
                paramValue = opt.stopBits.ToLower();
                if (paramValue == "none") { _sp.StopBits = StopBits.None;           } else if (paramValue == "one") { _sp.StopBits = StopBits.One;            } else if (paramValue == "two") { _sp.StopBits = StopBits.Two;            } else if (paramValue == "onepointfive") { _sp.StopBits = StopBits.OnePointFive;   } else {
                    try {
                        stopBits = Convert.ToInt32(opt.stopBits);
                        if (stopBits == 0) { _sp.StopBits = StopBits.None;           } else if (stopBits == 1) { _sp.StopBits = StopBits.One;            } else if (stopBits == 2) { _sp.StopBits = StopBits.Two;            } else if (opt.stopBits == "1.5") { _sp.StopBits = StopBits.OnePointFive;   }
                    } catch {
                        Console.WriteLine("Incorrect value for '--stopBits': {" + opt.stopBits + "}");
                        return false;
                    }
                }
            }            

            return true;
        }
        
        
        static bool parseMulticastUDP(Options opt) {
            
            if (_sp == null) { _sp = new SerialPort(); }
            if (_sp.IsOpen) { _sp.Close(); }
            _sp.Dispose();
            _sp = new SerialPort();
            _sp.DataReceived += DataReceivedHandler;
            
            
            if (opt.mudp != null) {
                if (opt.mudp.Length > 0) {
                    if (opt.mudp.Contains(":")) {
                        try {
                            string[] parts = opt.mudp.Split(':');
                            _IPAddr = parts [ 0 ];
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
            }
            
            if (opt.port != null) {
                try {
                    _IPPort = Convert.ToInt32(opt.port);
                } catch {
                    Console.WriteLine("Incorrect value for '--port': {" + opt.port + "}");
                    return false;
                }
            }
            
            return true;
        }

        
    }
    
    
    public class SerialPortConnInfo {
        public static int maxSizeName  = "Name".Length;
        public static int maxSizeDesc  = "Description".Length;
        public static int maxSizeManuf = "Manufacturer".Length;
        public static int maxSizeDevID = "Device ID".Length;
        
        public string Name = "";
        public string Description = "";
        public string Manufacturer = "";
        public string DevID = "";
        
        public SerialPortConnInfo(string name, string description, string manufacturer, string devID) {
            
            if( maxSizeName < name.Length )          { maxSizeName = name.Length; }
            if( maxSizeDesc < description.Length )   { maxSizeDesc = description.Length; }
            if( maxSizeManuf < manufacturer.Length ) { maxSizeManuf = manufacturer.Length; }
            if( maxSizeDevID < devID.Length )        { maxSizeDevID = devID.Length; }
            
            this.Name = name;
            this.Description = description;
            this.Manufacturer = manufacturer;
            this.DevID = devID;
            
        }
    }
}