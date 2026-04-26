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
            
            regexReplace = Regex.Replace(regexReplace, @"\\(\d+)", @"$$$1");
            
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
            int     idxLastWroteByte = 0;
            int     lenFreeSpaceInBuff = dataIn.Length;
            
            using (Stream strmIn = Console.OpenStandardInput()) {
                while (true) {
                    bytesIn = strmIn.Read(dataIn, 0, 1024);
                    
                    if (bytesIn <= 0) {
                        Thread.Sleep(10);
                        continue;
                    }
                    
                    dataOut += Encoding.ASCII.GetString(dataIn, 0, bytesIn);

                    int idxLastNewLine = dataOut.LastIndexOf('\n');
                    if ( ( idxLastNewLine >= 0 ) && ( ( idxLastNewLine + 1 ) < dataOut.Length ) ) {
                        
                        string fullLines = dataOut.Substring(0, idxLastNewLine + 1);
                        if ( ( idxLastNewLine + 1 ) < dataOut.Length ) {
                            dataOut = dataOut.Substring(idxLastNewLine + 1);
                        } else {
                            dataOut = "";
                        }
                        
                        if ( regexReplace.Length > 0 ) {
                            if ( opt.filter == null ) {
                                string fullLines2 = Regex.Replace(fullLines, regexLook, regexReplace, RegexOptions.Multiline);
                                Console.Write( fullLines2 );
                            } else {
                                MatchCollection matches = Regex.Matches(fullLines, regexLook, RegexOptions.Multiline);
                                foreach( Match m in matches ) {
                                    if ( !m.Success ) { continue; }
                                    string filteredReplaced = Regex.Replace(m.Groups[0].Value, regexLook, regexReplace, RegexOptions.Multiline);
                                    Console.Write( filteredReplaced );
                                }
                            }
                        } else {
                            MatchCollection matches = Regex.Matches(fullLines, regexLook, RegexOptions.Multiline);
                            foreach( Match m in matches ) {
                                if ( !m.Success ) { continue; }
                                Console.Write( m.Value );
                            }
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