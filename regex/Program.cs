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
        
        static int maxBufferLengthLimit = 65536;
        
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
                        ptrn.RebuildRegex();
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
            
            if ( opt.buffSize != null ) {
                int nexMaxBuffSize = 0;
                int.TryParse( opt.buffSize, out nexMaxBuffSize );
                if ( nexMaxBuffSize > 0 ) {
                    maxBufferLengthLimit = nexMaxBuffSize;
                }
            }
            
            log.reportMsg(logger.InterenalMessageType.Info, "Main", "BufferSize:" + maxBufferLengthLimit);
            
            parseStream(ref opt, ref _patterns);
            
        }
        
        static void parseStream(ref Options opt, ref List<patRepl> patterns) {
            byte[]  dataIn  = new byte[1024];
            int     bytesIn = 0;
            string strBlockIn = "";
            
            using (Stream strmIn = Console.OpenStandardInput()) {
                while (true) {
                    if ( ( bytesIn = strmIn.Read(dataIn, 0, 1024) ) <= 0 ) {
                        Thread.Sleep( 10 );
                        continue;
                    }
                    /* New data available */
                    strBlockIn += Encoding.ASCII.GetString(dataIn, 0, bytesIn);
                    
                    /* Parse whole data block */
                    parseDataBlock(ref opt, ref patterns, ref strBlockIn);
                    
                    /* Trim from the left main input string block to lowest farrest index of found matches */
                    int lowestFarrestIdxMatch = getLowestFarrestIdxMatch(ref patterns, strBlockIn.Length);
                    
                    if ( ( lowestFarrestIdxMatch > 0 ) && ( lowestFarrestIdxMatch < strBlockIn.Length ) ) {
                        /* Trim left of already found data block */
                        strBlockIn = strBlockIn.Substring( lowestFarrestIdxMatch );
                        /* Update every farrestIdxMatch by moving to the left by trim length */
                        foreach( patRepl ptrn in patterns ) {
                            ptrn.lastFarrestIdxMatch -= lowestFarrestIdxMatch;
                        }
                    }
                }
            }
        }
        
        static int getLowestFarrestIdxMatch(ref List<patRepl> patterns, int blockInLength) {
            int lowestFarrestIdxMatch = blockInLength;
            int lowestFarrestIdxMatchGratherThan0 = blockInLength;
            
            foreach( patRepl ptrn in patterns ) {
                if ( lowestFarrestIdxMatch > ptrn.lastFarrestIdxMatch ) {
                    lowestFarrestIdxMatch = ptrn.lastFarrestIdxMatch;
                    if ( lowestFarrestIdxMatch > 0 ) {
                        lowestFarrestIdxMatchGratherThan0 = lowestFarrestIdxMatch;
                    }
                }
            }
            
            if ( ( lowestFarrestIdxMatch == 0 ) && ( blockInLength >= maxBufferLengthLimit ) ) {
                /* Buffer to large. Trim to lowest farrestIdxMatch, but grather than 0 */
                if ( lowestFarrestIdxMatchGratherThan0 == blockInLength ) {
                    lowestFarrestIdxMatchGratherThan0 = blockInLength / 10;
                }
                lowestFarrestIdxMatch = lowestFarrestIdxMatchGratherThan0;
            }
            
            return lowestFarrestIdxMatch;
        }
        
        static void parseDataBlock(ref Options opt, ref List<patRepl> patterns, ref string strBlockIn) {
            string strBlockOut = "";
            string strBlockOutL = "";
            
            foreach( patRepl ptrn in patterns ) {
                /* Get substring from second char of last match */
                if ( ( ptrn.lastFarrestIdxMatch + 1 ) >= strBlockIn.Length ) { continue; }
                
                /* Safety check for null replacements */
                if ( ptrn.replacement == null ) { ptrn.replacement = ""; }
                
                strBlockOut = "";
                if ( ptrn.replacement.Length > 0 ) {
                    /* Pattern required to be replaced with replacement */
                    if ( opt.filter == null ) {
                        /* No filtering, just replacement in text block */
                        MatchCollection matches = ptrn.rgx.Matches(strBlockIn, ptrn.lastFarrestIdxMatch);
                        foreach( Match m in matches ) {
                            if ( !m.Success ) { continue; }
                            int blockLen = m.Index - ptrn.lastFarrestIdxMatch;
                            strBlockOutL = strBlockIn.Substring(ptrn.lastFarrestIdxMatch, blockLen);
                            strBlockOut += strBlockOutL + ptrn.rgx.Replace(m.Groups[0].Value, ptrn.replacement);
                            ptrn.lastFarrestIdxMatch = m.Index + m.Length;
                        }
                    } else {
                        MatchCollection matches = ptrn.rgx.Matches(strBlockIn, ptrn.lastFarrestIdxMatch);
                        /* Get only correct patterns and replace them in given way */
                        foreach( Match m in matches ) {
                            if ( !m.Success ) { continue; }
                            strBlockOut += ptrn.rgx.Replace( m.Groups[0].Value, ptrn.replacement );
                            
                            ptrn.lastFarrestIdxMatch = m.Index + m.Length;
                        }
                    }
                } else {
                    /* Get only correct patterns, drop everything else */
                    MatchCollection matches = ptrn.rgx.Matches(strBlockIn, ptrn.lastFarrestIdxMatch);
                    foreach( Match m in matches ) {
                        if ( !m.Success ) { continue; }
                        
                        strBlockOut += m.Value;
                        ptrn.lastFarrestIdxMatch = m.Index + m.Length;
                    }
                }
                
                if ( strBlockOut.Length > 0 ) {
                    /* There is something to print out */
                    Console.Write( strBlockOut );
                    strBlockOut = "";
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
            public Regex rgx = new Regex("");
            
            public int lastFarrestIdxMatch = 0;
            
            public patRepl() {}
            
            public patRepl(string pattern, string replacement) {
                this.pattern = pattern;
                this.replacement = replacement;
                RebuildRegex();
            }
            
            public void RebuildRegex() {
                this.rgx = new Regex( this.pattern, RegexOptions.Compiled | RegexOptions.Multiline );
            }
            
            public override string ToString () {
                return "\"" + this.pattern + "\"" + ( this.replacement.Length > 0 ? " => " + "\"" + this.replacement + "\"" : "" );
            } 
        }
    }
}