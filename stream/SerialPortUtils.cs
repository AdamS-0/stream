/*
 * Created by SharpDevelop.
 * User: AdamS-0
 * Date: 30.05.2025
 * Time: 19:57
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO.Ports;
using System.Collections.Generic;
using System.Management;
using Microsoft.Win32;
using System.Threading;

namespace stream {
    /// <summary>
    /// Description of SerialPortUtils.
    /// </summary>
    public class SerialPortUtils {
        public SerialPortUtils() {
        }
        
        
        
        public static string getNameFromString(string cpName) {
            if (cpName.Length > 0) {
                if (cpName.ToUpper().Contains("COM")) { return cpName; }
                
                try {
                    int comNumber = 0;
                    comNumber = Convert.ToInt32(cpName);
                    return "COM" + comNumber;
                } catch (Exception ex) { }
            }
            return "";
        }
        
        
        
        
        static void Serial_GetAllVirtualPorts(ref List<SerialPortConnInfo> portsList) {
            string spName           = "";
            string spDescription    = "";
            string spManufacturer   = "";
            string spDeviceID       = "";
            
            
            // Get all serial (COM)-ports you can see in the devicemanager
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\cimv2", "select * from Win32_SerialPort");
        
        
            // Add all available (COM)-ports to the combobox
            foreach (ManagementObject queryObj in searcher.Get()) {
                try{ spName         = (queryObj["DeviceID"      ]).ToString(); } catch { spName = ""; }
                try{ spDescription  = (queryObj["Description"   ]).ToString(); } catch { spDescription = ""; }
                try{ spManufacturer = (queryObj["Manufacturer"  ]).ToString(); } catch { spManufacturer = ""; }
                try{ spDeviceID     = (queryObj["PNPDeviceID"   ]).ToString(); } catch { spDeviceID = ""; }
                
                portsList.Add( new SerialPortConnInfo(spName, spDescription, spManufacturer, spDeviceID) );
            }
        }
        
        
        static void Serial_GetAllPhysicalPorts(ref List<SerialPortConnInfo> portsList) {
            string spName           = "";
            string spDescription    = "";
            string spManufacturer   = "";
            string spDeviceID       = "";
            
            using (ManagementClass i_Entity = new ManagementClass("Win32_PnPEntity")) {
                const String CUR_CTRL = "HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\";
            
                foreach (ManagementObject queryObj in i_Entity.GetInstances()) {
                    Object o_Guid = queryObj.GetPropertyValue("ClassGuid");
                    if (o_Guid == null || o_Guid.ToString().ToUpper() != "{4D36E978-E325-11CE-BFC1-08002BE10318}") continue; // Skip all devices except device class "PORTS"
                    
                    try{ spDescription  = (queryObj["Caption"       ]).ToString(); } catch { spDescription = ""; }
                    try{ spDeviceID     = (queryObj["PNPDeviceID"   ]).ToString(); } catch { spDeviceID = ""; }
                    try{ spManufacturer = (queryObj["Manufacturer"  ]).ToString(); } catch { spManufacturer = ""; }
                    
                    String s_Manufact = queryObj.GetPropertyValue("Manufacturer").ToString();
                    String s_RegEnum = CUR_CTRL + "Enum\\" + spDeviceID + "\\Device Parameters";
                    spName = Registry.GetValue(s_RegEnum, "PortName", "").ToString();
                    
                    int s32_Pos = spDescription.IndexOf(" (COM");
                    if (s32_Pos > 0) {
                        // remove COM port from description
                        spDescription = spDescription.Substring(0, s32_Pos);
                    }
                    
                    portsList.Add( new SerialPortConnInfo(spName, spDescription, spManufacturer, spDeviceID) );
                    
                }
            }
        }
        
        static void Serial_GetAllPorts(ref List<SerialPortConnInfo> portsList) {
            Serial_GetAllVirtualPorts(ref portsList);
            Serial_GetAllPhysicalPorts(ref portsList);
        }
        
        public static void ShowAllPorts(bool useColors = true) {
            string CL_RESET = "\x1B[0m";
            string CL_RED   = "\x1B[31m";
            string CL_GREEN = "\x1B[32m";
            
            List<SerialPortConnInfo> conns = new List<SerialPortConnInfo>();
            List<SerialPortConnInfo> connsOrg = new List<SerialPortConnInfo>();
            
            try {
                Serial_GetAllPorts(ref connsOrg);
                string strFormat =  "{0,-" + SerialPortConnInfo.maxSizeName + "}  " +
                    "{1,-" + SerialPortConnInfo.maxSizeDesc + "}  " +
                    "{2,-" + SerialPortConnInfo.maxSizeManuf + "}  " +
                    "{3,-" + SerialPortConnInfo.maxSizeDevID + "}\r\n";
                
                Console.Write(strFormat, "Name", "Description", "Manufacturer", "Device ID");
                
                connsOrg.Sort((x, y) => x.Name.CompareTo(y.Name));
                
                //
                for ( int idx = 0; idx < connsOrg.Count; idx++ ) {
                    if ( conns.Exists( e => e.Name == connsOrg[idx].Name ) ) {
                        continue;
                    }
                    
                    /* Add new unique connection info */
                    conns.Add( connsOrg[idx] );
                }
                //
                
                foreach( SerialPortConnInfo ci in conns ) {
                    bool portInUse = true;
                    
                    if ( useColors ) {
                        try {
                            using( SerialPort spi = new SerialPort() ) {
                                spi.ReadTimeout = 1;
                                spi.WriteTimeout = 1;
                                spi.PortName = ci.Name;
                                spi.Handshake = Handshake.None;
                                
                                spi.Open();
                                portInUse = false;
                                new Thread( delegate() { Thread.Sleep(1); spi.Close(); } ).Start();
                            }
                        } catch {}
                    }
                    
                    string strName = "";
                    
                    if ( useColors ) { strName += (portInUse ? CL_RED : CL_GREEN); }
                    strName += ci.Name;
                    if ( useColors ) { strName += CL_RESET; }
                    
                    Console.Write(strFormat,
                        strName, ci.Description, ci.Manufacturer, ci.DevID);
                }
                
            } catch {}
        }
        
    }
}
