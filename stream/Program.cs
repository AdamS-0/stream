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

using CommandLineParser;

namespace stream {
    class Program {
        
        static IPAddress localAddress = IPAddress.Any;
        static Stream strm = Console.OpenStandardOutput();
        static Connection conn;
        static bool tryToReconnect = false;
        static int reconnectTimeout = 1000;
        
        static Aliases aliases = new Aliases();
        
        static string aliasFile = "streamAliases.cfg";
        
        public static void Main(string[] args) {            
            byte[] dataOut = new byte[1024];
            int len2copy = 0;
            bool status = true;
            
            aliasFile = AppDomain.CurrentDomain.BaseDirectory + "" + aliasFile;
            aliases = Aliases.ReadFromFile(aliasFile);
            
            Options opt = CommandLineParser.CommandLineParser.ParseArguments<Options>(args);
            
//            if( opt.alias != null ) {
//                /* Serve aliases. If returned false, just exit the programm */
//                if( !ServeAliases(ref opt) ) { return; }
//            }

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
            
            status = conn.Prepare(ref opt);
            
            if( opt.alias != null ) {
                /* Serve aliases. If returned false, just exit the programm */
                if( !SaveAlias(ref opt) ) { return; }
            }
            
            if (!status) { return; }
            
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
                        conn.Send(ref dataOut, len2copy);
                    }
                }
            } catch {}
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
                        
                        Console.Write("[{0}]", connSeq[idx]);
                        Console.Write(" Connecting... ({0}) ", strDuration);
                        
                        if( conn.Open() ) {
//                            Console.SetCursorPosition(cLeft, cTop);
                            Console.Write('\r');
                            Console.Write(new string(' ', Console.BufferWidth));
                            Console.Write('\r');
                            Console.WriteLine( "Connected successfully after {0} ", strDuration );
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
    }
    
}