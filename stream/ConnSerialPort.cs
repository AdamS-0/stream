/*
 * Created by SharpDevelop.
 * User: AdamS-0
 * Date: 30.05.2025
 * Time: 20:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO.Ports;

namespace stream {
    /// <summary>
    /// Description of ConnSerialPort.
    /// </summary>
    public class ConnSerialPort : Connection {
        SerialPort _sp = new SerialPort();
        
        public ConnSerialPort() { }
        
        public override bool IsConnected() {
            try {
                return _sp.IsOpen;
            } catch { return false; }
        }
        
        public override bool Prepare(ref Options opt, bool useColors = true) {
            string paramValue = "";
            int stopBits = 0;
            string portName = "";
            
            if (opt.list != null) {
                SerialPortUtils.ShowAllPorts(useColors);
                return false;
            }
            
            if (_sp == null) { _sp = new SerialPort(); }
            if (_sp.IsOpen) { _sp.Close(); }
            _sp.Dispose();
            _sp = new SerialPort();
            
            if (opt.com != null) { portName = opt.com; }
            if (opt.name != null) { portName = opt.name; }
            portName = SerialPortUtils.getNameFromString(portName);
            
            try { _sp.PortName = portName; } 
            catch (Exception ex) {
                Console.Write(ex.Message);
                return false;
            }
            
            if (opt.baud != null) {
                try {
                    _sp.BaudRate = Convert.ToInt32(opt.baud);
                } catch {
                    ReportInternalMessage(InterenalMessageType.Error, "SerialPort.Initializer",
                      "Incorrect value for '--baud': {" + opt.baud + "}");
                    return false;
                }
            }
            
            if (opt.parity != null) {
                paramValue = opt.parity.ToLower();
                if      ( paramValue == "none"  ) { _sp.Parity = Parity.None;  }
                else if ( paramValue == "odd"   ) { _sp.Parity = Parity.Odd;   }
                else if ( paramValue == "even"  ) { _sp.Parity = Parity.Even;  }
                else if ( paramValue == "mark"  ) { _sp.Parity = Parity.Mark;  }
                else if ( paramValue == "space" ) { _sp.Parity = Parity.Space; }
                else {
                    ReportInternalMessage(InterenalMessageType.Error, "SerialPort.Initializer",
                      "Incorrect value for '--parity': {" + opt.parity + "}");
                    return false;
                }
            }
            
            if (opt.dataBits != null) {
                try {
                    _sp.DataBits = Convert.ToInt32(opt.dataBits);
                } catch {
                    ReportInternalMessage(InterenalMessageType.Error, "SerialPort.Initializer",
                      "Incorrect value for '--dataBits': {" + opt.dataBits + "}");
                    return false;
                }
            }
            
            if (opt.stopBits != null) {
                paramValue = opt.stopBits.ToLower();
                if      ( paramValue == "none"          ) { _sp.StopBits = StopBits.None;           }
                else if ( paramValue == "one"           ) { _sp.StopBits = StopBits.One;            }
                else if ( paramValue == "two"           ) { _sp.StopBits = StopBits.Two;            }
                else if ( paramValue == "onepointfive"  ) { _sp.StopBits = StopBits.OnePointFive;   }
                else if ( paramValue == "1.5"           ) { _sp.StopBits = StopBits.OnePointFive;   }
                else {
                    try {
                        stopBits = Convert.ToInt32(opt.stopBits);
                        if      ( stopBits == 0         ) { _sp.StopBits = StopBits.None;           }
                        else if ( stopBits == 1         ) { _sp.StopBits = StopBits.One;            }
                        else if ( stopBits == 2         ) { _sp.StopBits = StopBits.Two;            }
                    } catch {
                        ReportInternalMessage(InterenalMessageType.Error, "SerialPort.Initializer",
                          "Incorrect value for '--stopBits': {" + opt.stopBits + "}");
                        return false;
                    }
                }
            }
            
            if ( opt.dtrDisabled != null )  { _sp.DtrEnable = false; }
            else                            { _sp.DtrEnable = true; }
            
            if ( opt.rtsDisabled != null )  { _sp.RtsEnable = false; }
            else                            { _sp.RtsEnable = true; }
            
            return true;
        }
        
        public override void Send(ref byte[] dataOut, int dataLen) {
            if( !_sp.IsOpen ) { return; }
            try {
                _sp.Write(dataOut, 0, dataLen);
            } catch (Exception ex) {
                ReportInternalMessage(InterenalMessageType.Error, "SerialPort.Send",
                      ex.Message );
            }
        }
        
        
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e) {
            try {
                SerialPort sp = (SerialPort)sender;
                int bytesToRead = sp.BytesToRead;
                byte[] dataIn = new byte[bytesToRead];
                sp.Read(dataIn, 0, bytesToRead);
                if( this.DataReceived != null ) {
                    this.DataReceived(ref dataIn, dataIn.Length);
                }
            } catch (Exception ex) {
                ReportInternalMessage(InterenalMessageType.Error, "SerialPort.DataRxCallback",
                      ex.Message );
                _isConnected = false;
            }
        }
        
        
        public override bool Open() {
            try {
                _sp.DataReceived += DataReceivedHandler;
                _sp.Open();
                _isConnected = true;
            } catch (Exception ex) {
                Console.Write(ex.Message);
                _isConnected = false;
            }
            return _isConnected;
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
