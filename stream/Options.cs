/*
 * Created by SharpDevelop.
 * User: AdamS-0
 * Date: 21.05.2025
 * Time: 18:36
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using CommandLineParser;

namespace stream {
    public class Options {
        
        [Parameter(ParameterAlias = "c", HelpText = "Serial port connection",
                   ArgumentName = "COM port", ArgumentRequired = false)]
        public string com { get; set; }
        
        
        [Parameter(HelpText = "Serial port name", 
                   ArgumentName = "name", ArgumentRequired = true)]
        public string name { get; set; }

        [Parameter(ParameterAlias = "b", HelpText = "Serial port baudrate", 
                   ArgumentName = "baudrate", ArgumentRequired = true)]
        public string baud { get; set; }

        [Parameter(HelpText = "Serial port parity", 
                   ArgumentName = "parity", ArgumentRequired = true)]
        public string parity { get; set; }
        
        [Parameter(ParameterAlias = "d", HelpText = "Serial port data bits", 
                   ArgumentName = "dataBits", ArgumentRequired = true)]
        public string dataBits { get; set; }
        
        [Parameter(HelpText = "Serial port stop bits", 
                   ArgumentName = "stopBits", ArgumentRequired = true)]
        public string stopBits { get; set; }
        
        [Parameter(ParameterAlias = "l", HelpText = "List al serial ports", 
                   ArgumentName = "list", ArgumentRequired = false)]
        public string list { get; set; }
        
        
//        [Parameter(ParameterAlias = "t", HelpText = "TCP client/server",
//            ArgumentName = "address:port", ArgumentRequired = false)]
//        public string tcp { get; set; }
        
        [Parameter(ParameterAlias = "mudp", HelpText = "Multicast UDP client",
                   ArgumentName = "address:port", ArgumentRequired = false)]
        public string mudp { get; set; }
        
        [Parameter(ParameterAlias = "a", HelpText = "IPv4 address",
                   ArgumentRequired = false)]
        public string address { get; set; }
        
        [Parameter(ParameterAlias = "p", HelpText = "IPv4 port",
                   ArgumentRequired = false)]
        public string port { get; set; }
        
    }
}
