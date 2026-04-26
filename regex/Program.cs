/*
 * Created by SharpDevelop.
 * User: AdamS-0
 * Date: 07.03.2026
 * Time: 13:23
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace regex {
    class Program {
        static bool useColors = true;
        
        static logger log = new logger("regex");
        static bool _silentMode = false;
        
        static List<patRepl> _patterns = new List<patRepl>();
        
        public static void Main ( string[] args ) {
            List<string> newArgs = new List<string>();
            
            for ( int idxArg = 0; idxArg < args.Length; idxArg++ ) {
                if ( args[idxArg].Equals( "-p" ) ||
                     args[idxArg].Equals( "--pattern" ) )
                {
                    if ( ( idxArg + 1 ) < args.Length ) {
                        patRepl ptrn = new patRepl();
                        ptrn.pattern = args[idxArg + 1];
                        
                        if ( ( idxArg + 2 ) < args.Length ) {
                            if ( !args[idxArg + 2].StartsWith("-") ) {
                                ptrn.replacement = args[ idxArg + 2 ];
                                idxArg += 1;
                            }
                        }
                        
                        _patterns.Add(ptrn);
                        idxArg += 1;
                    }
                } else {
                    newArgs.Add( args[idxArg] );
                }
            }
            
            Options opt = CommandLineParser.CommandLineParser.ParseArguments<Options>(newArgs.ToArray());
            
            if( opt.noColors != null) { useColors = false; }
            log.useColors = useColors;

            _silentMode = opt.silent != null;
            log.setSilentMode( _silentMode );
            
            log.reportMsg(logger.InterenalMessageType.Info, "Main", "Initializing");
            
            if ( _patterns.Count <= 0 ) {
                log.reportMsg(logger.InterenalMessageType.Error, "Main", "Patterns not found");
                return;
            }

            
            if ( !_silentMode ) {
                log.reportMsg(logger.InterenalMessageType.Info, "Main", "Used regex:");
            }
            /* Parse replace patterns */
            foreach( patRepl ptrn in _patterns ) {
                if ( ptrn.replacement == null ) { ptrn.replacement = ""; }
                if ( !_silentMode ) { Console.WriteLine( ptrn.ToString() ); }
                ptrn.replacement = prepareReplacement(ptrn.replacement);
            }
            
            byte[]  dataIn  = new byte[1024];
            int     bytesIn = 0;
            string  dataOut = "";
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
                        
                        /* Prepare data chunk to regex analyze */
                        string fullLines = dataOut.Substring(0, idxLastNewLine + 1);
                        if ( ( idxLastNewLine + 1 ) < dataOut.Length ) {
                            dataOut = dataOut.Substring(idxLastNewLine + 1);
                        } else {
                            dataOut = "";
                        }
                        
                        
                        foreach( patRepl ptrn in _patterns ) {
                            if ( ptrn.replacement == null ) { ptrn.replacement = ""; }
                            
                            if ( ptrn.replacement.Length > 0 ) {
                                if ( opt.filter == null ) {
                                    string fullLines2 = Regex.Replace(fullLines, ptrn.pattern, ptrn.replacement, RegexOptions.Multiline);
                                    Console.Write( fullLines2 );
                                } else {
                                    MatchCollection matches = Regex.Matches(fullLines, ptrn.pattern, RegexOptions.Multiline);
                                    foreach( Match m in matches ) {
                                        if ( !m.Success ) { continue; }
                                        string filteredReplaced = Regex.Replace(m.Groups[0].Value, ptrn.pattern, ptrn.replacement, RegexOptions.Multiline);
                                        Console.Write( filteredReplaced );
                                    }
                                }
                            } else {
                                MatchCollection matches = Regex.Matches(fullLines, ptrn.pattern, RegexOptions.Multiline);
                                foreach( Match m in matches ) {
                                    if ( m.Success ) {
                                        Console.Write( m.Value );
                                    }
                                }
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
        
        static string prepareReplacement(string regexReplaceIn) {
            string  strHelper   = "";
            int     idx         = 0;
            
            string regexReplaceOut = Regex.Replace(regexReplaceIn, @"\\(\d+)", @"$$$1");
            
            while( (idx = regexReplaceOut.IndexOf('\\')) >= 0 ) {
                strHelper = regexReplaceOut.Substring(0, idx);
                
                if(regexReplaceOut.Length > idx + 1) {
                    strHelper += CharToEscapeSequence(regexReplaceOut[idx+1]);
                }
                
                if(regexReplaceOut.Length > idx + 2) {
                    strHelper += regexReplaceOut.Substring(idx + 2);
                }
                regexReplaceOut = strHelper;
            }
            
            return regexReplaceOut;
        }
        
        class patRepl {
            public string pattern = "";
            public string replacement = "";
            
            public patRepl() {}
            
            public patRepl(string pattern, string replacement) {
                this.pattern = pattern;
                this.replacement = replacement;
            }
            
            public override string ToString () {
                return "\"" + this.pattern + "\"" + ( this.replacement.Length > 0 ? " => " + "\"" + this.replacement + "\"" : "" );
            } 
        }
    }
}