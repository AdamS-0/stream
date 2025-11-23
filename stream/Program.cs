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
        Error,
        Warning,
        Info
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
            
            if( opt.noColors != null) { useColors = false; }
            reportedInternalMessage(InterenalMessageType.Info, "Main", "Initializing");
            
            if ( opt.help    != null ) { CommandLineParser.CommandLineParser.PrintHelp<Options>(); }
            if ( opt.version != null ) { CommandLineParser.CommandLineParser.PrintVersion<Options>(); }
            
            if( (opt.alias != null) && (opt.list != null) ) {
                /* Show alias list */
                aliases.ShowAliases();
                return;
            }

            if( opt.alias != null ) {
                /* Serve aliases. If returned false, just exit the programm */
                if( !GetFromAlias(ref opt) ) { return; }
            }
            
            if      (opt.com    != null) { conn = new ConnSerialPort(); }
            else if (opt.mudp   != null) { conn = new ConnMulticastUDP(); }
            else if (opt.udp    != null) { conn = new ConnUDP(); }
            else if (opt.tcp    != null) { conn = new ConnTCP(); }
            else { return; }
            
            conn.ReportedInternalMessage += reportedInternalMessage;
            
            status = conn.Prepare(ref opt, useColors);
            
            if( opt.alias != null ) {
                /* Serve aliases. If returned false, just exit the programm */
                if( !SaveAlias(ref opt) ) { return; }
            }
            
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
            
            Thread thReconnect = new Thread(TaskReconnect);
            thReconnect.Start();
            try {                
                Stream inStream = Console.OpenStandardInput();
//                inStream.BeginRead
                
                while (status) {
                    Thread.Sleep(10);
                    while ((len2copy = inStream.Read(dataOut, 0, 1024)) > 0) {
                        if ( len2copy > commandPrompter.Length ) {
                            tryCheckCommand(ref dataOut, ref len2copy);
                        }
                        conn.Send(ref dataOut, len2copy);
                        if ( redirectStdInputToStdOutput ) {
                            strm.Write(dataOut, 0, len2copy);
                        }
                    }
                }
            } catch (Exception ex) {
                Console.WriteLine("stream>[Error]: " + ex.Message );
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
        static void reportedInternalMessage(InterenalMessageType msgType, string moduleSender, string message, bool newLine = true) {           
            string msg = "";
            
            if ( useColors ) { msg += getColorFromMsgType(msgType); }
            msg += "<stream." + msgType.ToString() + ">";
            
            if ( useColors ) { msg += CL_RESET; }
            
            msg += "[" + moduleSender + "] " + message + (newLine ? "\r\n" : "");
            
            Console.Write( msg );
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
//                        Console.Write("[{0}]", connSeq[idx]);
                        Console.Write(" Connecting... ({0}) ", strDuration);
                        
                        if( conn.Open() ) {
//                            Console.SetCursorPosition(cLeft, cTop);
                            Console.Write('\r');
                            Console.Write(new string(' ', Console.BufferWidth));
                            Console.Write('\r');
                            reportedInternalMessage(InterenalMessageType.Info, "Reconnect", 
                               "Connected successfully after " + strDuration);
//                            Console.WriteLine( "Connected successfully after {0} ", strDuration );
                            fisrIterAfterDisconnect = true;
                        } else {
//                            Console.CursorLeft = cLeft;
//                            Console.CursorTop = cTop;
//                            Console.SetCursorPosition(cLeft, cTop);
                            Console.Write('\r');
                        }
                    }
                } catch (Exception ex) { Console.WriteLine(ex.Message); }
                
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
                if( propInfo.GetValue(opt) != null ) { allPropsNulls = false; }
            }
            
            /* Get alias name */
            aliasName = opt.alias;
            
            /* Find alias with given name */
            als = aliases.items.Find(o => o.Name.Equals(aliasName));
            
            
            if( allPropsNulls ) {
                /* All properties are null so it is read from alias sequence! */
                
                if( als == null ) {
                    Console.WriteLine("Alias '{0}' was not found!", opt.alias);
                    return false;
                }
                
                opt = als.options;
            }
            
            return true;
        }
        
        
        
        static bool SaveAlias(ref Options opt) {
            bool allPropsNulls = true;
            Alias als;
            string aliasName = "";
            
            foreach (PropertyInfo propInfo in typeof(Options).GetProperties()) {
                if( propInfo.Name.Equals("alias") ) { continue; }
                if( propInfo.GetValue(opt) != null ) { allPropsNulls = false; }
            }
            
            /* Get alias name */
            aliasName = opt.alias;
            
            /* Find alias with given name */
            als = aliases.items.Find(o => o.Name.Equals(aliasName));
            
            
            if( !allPropsNulls ) {
                /* Some properties were applied, so this is create/update sequence */
                if( als == null ) {
                    als = new Alias(opt.alias, ref opt);
                    aliases.items.Add( als );
                } else { als.options = opt; }
                
                Aliases.SaveToFile( aliasFile, ref aliases );
            }
            return true;
            
        }
        
        
        /// <summary>
        /// Parse commands from console input
        /// </summary>
        /// <param name="dataOut">Array of bytes</param>
        /// <param name="dataLen">Number of received bytes. When command parsed successfully, this value will be changed</param>
        /// <returns></returns>
        static bool tryCheckCommand(ref byte[] dataOut, ref int dataLen) {
            string  command     = "";
            int     startIdx    = 0;
            string  line        = Encoding.ASCII.GetString(dataOut);
            
            if ( !line.Substring(0, commandPrompter.Length).Equals(commandPrompter) ) { return false; }
            
            /* Line starts with commandPrompter */
            startIdx = commandPrompter.Length;
            
            /* Trim line to command (after "stream<<") */
            line = line.Substring(startIdx);
            
            return parseCommand(line, ref dataOut, ref dataLen);
        }
        
        static bool parseCommand(string line, ref byte[] dataOut, ref int dataLen) {
            int     startIdx    = line.IndexOf(':');
            string  cmd         = "";
            
            if ( ( startIdx < 0 ) || ( startIdx >= line.Length ) ) { return false; }
            /* Found ':' in line */
            
            int strEnd_idx = line.IndexOf('\0');
            if ( ( strEnd_idx > 0 ) && ( strEnd_idx < line.Length ) ) {
                line = line.Substring( 0, strEnd_idx );
            }
            
            cmd = line.Substring(0, startIdx);
            
            if ( cmd.Equals("send_file") ) {
                string filePath2Send = line.Substring(startIdx + 1);
                
                filePath2Send = new string(filePath2Send.Where(c => !char.IsControl(c)).ToArray());
                
                if ( !File.Exists( filePath2Send ) ) { return false; }
                /* File exists */
                dataLen = 0;
                
                byte[] fileBytes = File.ReadAllBytes( filePath2Send );
                conn.Send(ref fileBytes, fileBytes.Length);
                Console.WriteLine("stream>[Info]Send Ok");
                return true;
            }
            Console.WriteLine("stream>[Warning] Unknown command");
            return false;
        }
    }
    
}