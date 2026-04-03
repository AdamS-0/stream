/*
 * Created by SharpDevelop.
 * User: AdamS-0
 * Date: 07.03.2026
 * Time: 13:23
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace regex {
    class Program {
        static bool useColors = true;
        
        static logger log = new logger("regex");
        
        public static void Main ( string[] args ) {
            Options opt = CommandLineParser.CommandLineParser.ParseArguments<Options>(args);
            if( opt.noColors != null) { useColors = false; }
            log.useColors = useColors;
            
            if( opt.pattern == null ) { return; }
            
            string regexLook = opt.pattern;
            string regexReplace = "";
            if( opt.replaceWith != null ) {
                regexReplace = opt.replaceWith;
            }
            
            /* Parse replace patterns */
            string  strHelper   = "";
            int     idx         = 0;
            
            while( (idx = regexReplace.IndexOf('\\')) >= 0 ) {
                strHelper = regexReplace.Substring(0, idx);
                
                if(regexReplace.Length > idx + 1) {
                    strHelper += CharToEscapeSequence(regexReplace[idx+1]);
                }
                
                if(regexReplace.Length > idx + 2) {
                    strHelper += regexReplace.Substring(idx + 2);
                }
                regexReplace = strHelper;
            }
            
            
            log.reportMsg(logger.InterenalMessageType.Info, "Main", "Initializing");
            
            byte[]  dataIn  = new byte[1024];
            int     bytesIn = 0;
            string  dataOut = "";
            
            using (Stream strmIn = Console.OpenStandardInput()) {
                while (true) {
                    bytesIn = strmIn.Read(dataIn, 0, 1024);
                    
                    if (bytesIn <= 0) {
                        Thread.Sleep(10);
                        continue;
                    }
                    
                    dataOut = Encoding.ASCII.GetString(dataIn, 0, bytesIn);
                    
                    if ( regexReplace.Length > 0 ) {
                        dataOut = Regex.Replace(dataOut, regexLook, regexReplace, RegexOptions.Multiline);
                        Console.Write( dataOut );
                    } else {
                        MatchCollection matches = Regex.Matches(dataOut, regexLook, RegexOptions.Multiline);
                        foreach( Match m in matches ) {
                            if ( !m.Success ) { continue; }
                            
                            Console.WriteLine( m.Value );
                            
                        }
                    }
                }
            }
            
            
        }
        
        
        static char CharToEscapeSequence(char cIn) {
            char cOut = '\0';
            switch(cIn) {
                case 'n': cOut = '\n'; break;
                case 'r': cOut = '\r'; break;
                case 't': cOut = '\t'; break;
                default : break;
            }
            return cOut;
        }
    }
}