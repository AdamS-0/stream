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
using System.Reflection;
using System.Text;
using System.Threading;

using System.Linq;

using CommandLineParser;

namespace stream {
    
    public enum InterenalMessageType {
        Info    = 0,
        Warning = 1,
        Error   = 2
    }
    
    class Program {
        
        static string CL_RESET  = "\x1B[0m";
        static string CL_RED    = "\x1B[31m";
        static string CL_YEL    = "\x1B[93m";
        static string CL_GREEN  = "\x1B[32m";
        
        static IPAddress localAddress = IPAddress.Any;
        static Stream strm = Console.OpenStandardOutput();
        static Connection conn;
        static bool tryToReconnect = false;
        static int reconnectTimeout = 1000;
        static bool redirectStdInputToStdOutput = false;
        static Aliases aliases = new Aliases();
        static bool useColors = true;
        
        //! 0 - normal mode, 1 - silent (info/warnings), 2 - no errors!
        static int silentMode = 0;
        
        static string aliasFile = "streamAliases.cfg";
        
        /* Commands input:
         * stream<<{command}:[params]*/
        static string commandPrompter = "stream<<";
        
        public static void Main(string[] args) {
            byte[] dataOut = new byte[1024];
            int len2copy = 0;
            bool status = true;
            
            aliasFile = AppDomain.CurrentDomain.BaseDirectory + "" + aliasFile;
            aliases = Aliases.ReadFromFile(aliasFile);
            
            Options opt = CommandLineParser.CommandLineParser.ParseArguments<Options>(args);
            
            if ( opt.silent != null ) {
                if ( opt.silent == "" ) { silentMode = 1; }
                else { int.TryParse( opt.silent, out silentMode ); }
            }
            
            if( opt.noColors != null ) { useColors = false; }
            
            if ( silentMode == 0 ) {
                int ret = reportedInternalMessage(InterenalMessageType.Info, "Main", "Initializing", false);
                
                if ( opt.noColors == null ) {
                    if ( ret != 0 ) {
                        useColors = true;
                    } else {
                        useColors = false;
                    }
                }
                
                Console.Write("\r");
                Console.Write(new string(' ', Console.BufferWidth));
                
                reportedInternalMessage(InterenalMessageType.Info, "Main", "Initializing");
            }
            
            if ( opt.help    != null ) { CommandLineParser.CommandLineParser.PrintHelp<Options>(); }
            if ( opt.version != null ) { CommandLineParser.CommandLineParser.PrintVersion<Options>(); }
            
            if( (opt.alias != null) && (opt.list != null) ) {
                /* Show alias list */
                aliases.ShowAliases();
                return;
            }
            
            if( opt.alias != null && opt.clear == null ) {
                /* Serve aliases. If returned false, just exit the programm */
                if( !GetFromAlias(ref opt) ) { return; }
            }
            
            if( opt.alias != null && opt.save != null ) {
                /* Seve aliases. If returned false, just exit the programm */
                if( !SaveAlias(ref opt) ) { return; }
            }
            
            if      (opt.com    != null) { conn = new ConnSerialPort(); }
            else if (opt.mudp   != null) { conn = new ConnMulticastUDP(); }
            else if (opt.udp    != null) { conn = new ConnUDP(); }
            else if (opt.tcp    != null) { conn = new ConnTCP(); }
            else { return; }
            
            conn.ReportedInternalMessage += reportedInternalMessage;
            
            status = conn.Prepare(ref opt, useColors);
            
            if (!status) { return; }
            
            if( opt.echo != null) { redirectStdInputToStdOutput = true; }
            
            if( opt.reconnect != null ) {
                try {
                    if ( opt.reconnect.Length > 0 ) {
                        reconnectTimeout = Convert.ToInt32(opt.reconnect);
                    }
                } catch { }
                tryToReconnect = true;
            }
            
            /* Correct parameters. Openning connection... */
            conn.DataReceived += DataReceived;
            bool cmdExecuted = false;
            Thread thReconnect = new Thread(TaskReconnect);
            thReconnect.Start();
            try {                
                Stream inStream = Console.OpenStandardInput();
//                inStream.BeginRead
                
                while (status) {
                    Thread.Sleep(10);
                    while ((len2copy = inStream.Read(dataOut, 0, 1024)) > 0) {
                        cmdExecuted = false;
                        if ( len2copy > commandPrompter.Length ) {
                            cmdExecuted = tryCheckCommand(ref dataOut, ref len2copy, ref opt);
                        }
                        
                        if ( !cmdExecuted ) { conn.Send(ref dataOut, len2copy);}
                        
                        if ( redirectStdInputToStdOutput ) {
                            strm.Write(dataOut, 0, len2copy);
                        }
                    }
                }
            } catch (Exception ex) {
                reportedInternalMessage(InterenalMessageType.Error, "strIn", ex.Message, true);
            }
        }
        
        
        
        static string getColorFromMsgType(InterenalMessageType msgType) {
            switch (msgType) {
                case InterenalMessageType.Error     : return CL_RED;
                case InterenalMessageType.Warning   : return CL_YEL;
                case InterenalMessageType.Info      : return CL_GREEN;
            }
            return "";
        }
        /* "<stream.{msgType}>[{moduleSender}] {message}" */
        static int reportedInternalMessage(InterenalMessageType msgType, string moduleSender, string message, bool newLine = true) {           
            if          ( silentMode == 1 ) {
                if ( msgType < InterenalMessageType.Error ) { return 0; }
            } else if   ( silentMode == 2 ) { return 0; }
            
            string msg = "";
            
            if ( useColors ) { msg += getColorFromMsgType(msgType); }
            msg += "<stream." + msgType.ToString() + ">";
            
            if ( useColors ) { msg += CL_RESET; }
            
            msg += "[" + moduleSender + "] " + message + (newLine ? "\r\n" : "");
            
            Console.Write( msg );
            return msg.Length - Console.CursorLeft;
        }
        
        
        static void TaskReconnect() {
            int cLeft = 0;
            int cTop = 0;
            char[] connSeq = new char[]{ '|', '/', '-', '\\' };
            int idx = 0;
            DateTime dtStart = DateTime.Now;
            bool fisrIterAfterDisconnect = false;
            TimeSpan duration;
            string strDuration = "";
            
            do {
                try {
                    if( !conn.IsConnected() ) {
                        if( fisrIterAfterDisconnect ) {
                            fisrIterAfterDisconnect = false;
                            dtStart = DateTime.Now;
                        }
                        
                        cLeft = Console.CursorLeft;
                        cTop = Console.CursorTop;
                        idx = ( idx + 1 ) % connSeq.Length;
                        
                        duration = DateTime.Now.Subtract(dtStart);
                        strDuration = duration.ToString("hh\\:mm\\:ss\\.fff");
                        
                        reportedInternalMessage(InterenalMessageType.Info, "Reconnect", "[" + connSeq[idx] + "]", false);
                        if ( silentMode == 0 ) Console.Write(" Connecting... ({0}) ", strDuration);
                        
                        if( conn.Open() ) {
                            if ( silentMode == 0 ) {
                                Console.Write('\r');
                                Console.Write(new string(' ', Console.BufferWidth));
                                Console.Write('\r');
                                reportedInternalMessage(InterenalMessageType.Info, "Reconnect", 
                                   "Connected successfully after " + strDuration);
                            }
                            fisrIterAfterDisconnect = true;
                            
                            reportedInternalMessage(InterenalMessageType.Info, "Reconnect", "Configuration: " + conn.ToString(), true);
                        } else {
                            if ( silentMode == 0 ) { Console.Write('\r'); }
                        }
                    }
                } catch (Exception ex) {
                    reportedInternalMessage(InterenalMessageType.Error, "Reconnector", ex.Message, true);
                }
                
                Thread.Sleep(reconnectTimeout);
            } while(tryToReconnect);
            
        }
        
        
        
        public static void DataReceived(ref byte[] dataOut, int dataLen) {
            try {
                strm.Write(dataOut, 0, dataLen);
            } catch { }
        }
        
        
        
        
        static bool GetFromAlias(ref Options opt) {
            bool allPropsNulls = true;
            Alias als;
            string aliasName = "";
            
            foreach (PropertyInfo propInfo in typeof(Options).GetProperties()) {
                if( propInfo.Name.Equals("alias") ) { continue; }
                if( propInfo.Name.Equals("save") ) { continue; }
                if( propInfo.Name.Equals("clear") ) { continue; }
                if( propInfo.GetValue(opt) != null ) { allPropsNulls = false; }
            }
            
            /* Get alias name */
            aliasName = opt.alias;
            
            /* Find alias with given name */
            als = aliases.items.Find(o => o.Name.Equals(aliasName));
            
            if( als == null && opt.save == null ) {
                reportedInternalMessage(InterenalMessageType.Error, "AliasGetter", "Alias '" + opt.alias + "' was not found!", true);
                return false;
            }
            
            if ( allPropsNulls ) {
                /* All properties are null so it is read from alias sequence! */                
                opt = als.options;
                opt.save = null;
                opt.clear = null;
            } else {
                if ( als != null ) {
                    foreach (PropertyInfo propInfo in typeof(Options).GetProperties()) {
                        if( propInfo.Name.Equals("alias") ) { continue; }
                        if( propInfo.GetValue(opt) == null ) {
                            /* Replace only undefined parameters with avlues from alias */
                            propInfo.SetValue( opt, propInfo.GetValue(als.options) );
                        }
                    }
                }
            }
            
            return true;
        }
        
        
        
        static bool SaveAlias(ref Options opt) {
            bool allPropsNulls = true;
            Alias als;
            string aliasName = "";
            
            foreach (PropertyInfo propInfo in typeof(Options).GetProperties()) {
                if( propInfo.Name.Equals("alias") ) { continue; }
                if( propInfo.Name.Equals("save") ) { continue; }
                if( propInfo.Name.Equals("clear") ) { continue; }
                if( propInfo.GetValue(opt) != null ) { allPropsNulls = false; }
            }
            
            /* Get alias name */
            aliasName = opt.alias;
            
            /* Find alias with given name */
            als = aliases.items.Find(o => o.Name.Equals(aliasName));
            
            
            if ( allPropsNulls && als != null ) {
                /* All properties were set to null, so this is clearing this alias */
                aliases.items.Remove(als);
            } else {
                /* Some properties were applied, so this is create/update sequence */
                if( als == null ) {
                    als = new Alias(opt.alias, ref opt);
                    aliases.items.Add( als );
                } else { als.options = opt; }
                als.options.save = null;
                als.options.clear = null;
            }
            Aliases.SaveToFile( aliasFile, ref aliases );
            return true;
            
        }
        
        
        /// <summary>
        /// Parse commands from console input
        /// </summary>
        /// <param name="dataOut">Array of bytes</param>
        /// <param name="dataLen">Number of received bytes. When command parsed successfully, this value will be changed</param>
        /// <returns></returns>
        static bool tryCheckCommand(ref byte[] dataOut, ref int dataLen, ref Options opt) {
            int     startIdx    = 0;
            string  line        = Encoding.ASCII.GetString(dataOut);
            
            if ( !line.Substring(0, commandPrompter.Length).Equals(commandPrompter) ) { return false; }
            
            /* Line starts with commandPrompter */
            startIdx = commandPrompter.Length;
            
            /* Trim line to command (after "stream<<") */
            line = line.Substring(startIdx);
            
            return parseCommand(line, ref dataOut, ref dataLen, ref opt);
        }
        
        static bool parseCommand(string line, ref byte[] dataOut, ref int dataLen, ref Options opt) {
            int     startIdx    = line.IndexOf(':');
            string  cmd         = "";
            int     eolIdx      = 0;
            
            int strEnd_idx = line.IndexOf('\0');
            if ( ( strEnd_idx > 0 ) && ( strEnd_idx < line.Length ) ) {
                line = line.Substring( 0, strEnd_idx );
            }
            
            if ( ( startIdx > 0 ) && ( startIdx < line.Length ) ) {
                /* Found ':' in line */
                cmd = line.Substring(0, startIdx);
            } else {
                eolIdx = line.IndexOf('\r');
                if ( eolIdx > 0 && eolIdx < line.Length ) {
                    cmd = line.Substring(0, eolIdx);
                } else {
                    cmd = line;
                }
            }
            
            
            /* Extract data after ':' */
            string cmdData = line.Substring(startIdx + 1);
            eolIdx = cmdData.IndexOf('\r');
            if ( eolIdx > 0 && eolIdx < cmdData.Length ) {
                /* Trim data to <CR> */
                cmdData = cmdData.Substring(0, eolIdx);
            }
            
            if ( cmd.Equals("send_file") ) {
                string filePath2Send = new string(cmdData.Where(c => !char.IsControl(c)).ToArray());
                
                if ( !File.Exists( filePath2Send ) ) {
                    reportedInternalMessage(InterenalMessageType.Warning, "FileSender", "File '" + filePath2Send + "' not found!", true);
                    return false;
                }
                /* File exists */
                dataLen = 0;
                
                byte[] fileBytes = File.ReadAllBytes( filePath2Send );
                
                if ( opt.baud != null ) {
                    int baudrate = Convert.ToInt32( opt.baud );
                    sendFileWithBaudrate(ref fileBytes, fileBytes.Length, baudrate);
                } else {
                    conn.Send(ref fileBytes, fileBytes.Length);
                }
                
                reportedInternalMessage(InterenalMessageType.Info, "FileSender", "Send OK", true);
                return true;
            } else if ( cmd.Equals("send_hex") ) {
                return sendHex( cmdData );
            } else if ( cmd.Equals("getCfg") ) {
                reportedInternalMessage(InterenalMessageType.Info, "getCfg", "Configuration: " + conn.ToString(), true);
                return true;
            } else {
                reportedInternalMessage(InterenalMessageType.Warning, "stream", "Unknown command '" + cmd + "'", true);
                return true;
            }
            return false;
        }
        
        
        static void sendFileWithBaudrate( ref byte[] dataOut, int dataLen, int baudrate ) {
            int packetTime = 50; //! [ms]
            int packetSize = ( baudrate * packetTime ) / ( 11 * 1000 );
            int bytesLeft = dataLen;
            int dataIdx = 0;
            int lenToSend = packetSize;
            
            reportedInternalMessage(InterenalMessageType.Info, "FileSender", "Sending with baudrate [b/s]: " + baudrate + ", packetSize[B]: " + packetSize, true);
            
            byte[] packetData = new byte[packetSize];
            while ( bytesLeft > 0 ) {
                lenToSend = packetSize;
                if ( lenToSend > bytesLeft ) { lenToSend = bytesLeft; }
                
                Array.Copy( dataOut, dataIdx, packetData, 0, lenToSend );
                conn.Send(ref packetData, lenToSend);
                reportedInternalMessage(InterenalMessageType.Info, "FileSender", dataIdx + "/" + dataLen + ", sent " + lenToSend, true);
                dataIdx += lenToSend;
                bytesLeft -= lenToSend;
                Thread.Sleep( packetTime );
            }
        }
        
        static bool sendHex( string strHexIn ) {
            byte[] dataOut  = new byte[1024];
            int bytesIn     = strHexIn.Length;
            int idxIn       = 0;
            int idxOut      = 0;
            byte cC         = 0;
            byte cNipMSB    = 0;
            byte cNip       = 0;
            bool MSBNow     = true;
            
            
            for (idxIn = 0; idxIn < bytesIn; idxIn++) {
                cC = (byte)strHexIn[idxIn];
                if (cC == ' ') {
                    MSBNow = true;
                    continue;
                }
                
                /* Ignore "0x" start */
                if (cC == 'x') {
                    cNipMSB = 0;
                    MSBNow = true;
                    continue;
                }
                
                if (cC >= '0' && cC <= '9') {
                    cNip = (byte)(cC - (byte)'0');
                } else if (cC >= 'a' && cC <= 'f') {
                    cNip = (byte)(cC - (byte)'a' + 10);
                } else if (cC >= 'A' && cC <= 'F') {
                    cNip = (byte)(cC - (byte)'A' + 10);
                } else {
                    continue;
                }
                
                if ( MSBNow ) {
                    cNipMSB = (byte)(cNip << 4);
                } else {
                    cNipMSB |= cNip;
                    
                    if (idxOut >= 1024) {
                        Console.Write(dataOut);
                        idxOut = 0;
                    }
                    
                    dataOut[idxOut++] = cNipMSB;
                }
                MSBNow = !MSBNow;
            }
            
            if (idxOut > 0) {
                conn.Send(ref dataOut, idxOut);
                idxOut = 0;
            }
            return true;
        }
        
    }
    
}