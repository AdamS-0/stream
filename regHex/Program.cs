/*
 * Created by SharpDevelop.
 * User: AdamS-0
 * Date: 26.07.2025
 * Time: 14:48
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace regHex {
    
    class Program {
        public enum InterenalMessageType {
            Error,
            Warning,
            Info
        }
        
        static string CL_RESET  = "\x1B[0m";
        static string CL_RED    = "\x1B[31m";
        static string CL_YEL    = "\x1B[93m";
        static string CL_GREEN  = "\x1B[32m";
        
        enum outputf {
            [Description("A")]
            A,
            [Description("H")]
            H,
            [Description("AH")]
            AH,
        };
        
        static char[] _escSeq = { 
            '\a',
            '\b',
            '\f',
            '\n',
            '\r',
            '\t',
            '\v',
//            '\'',
//            '\"',
//            '\?',
            '\\',
//            '\x',
        };
        
        static char[] _escSeqDecoded = { 
            'a',
            'b',
            'f',
            'n',
            'r',
            't',
            'v',
//            '\'',
//            '"',
//            '?',
            '\\',
//            'x',
        };
        
        static bool useColors = true;
        
        public static void Main(string[] args) {
            Options opt = CommandLineParser.CommandLineParser.ParseArguments<Options>(args);
            if( opt.noColors != null) { useColors = false; }
            
            byte[] dataIn           = new byte[1024];
            int bytesIn             = 0;
            byte[] bPattern         = new byte[1];
            byte[] bByteKnown       = new byte[1];
            byte[] bBytesFound      = new byte[1];
            
            bool statusParsePattern = false;
            
            outputf outputType = outputf.H;
            
            if( opt.output != null ) {
                if( !Enum.TryParse(opt.output, out outputType) ) {
                    Console.WriteLine("[Warning] unknow output format '" + opt.output + "'.");
                    outputType = outputf.H;
                }
            }
            
            if( opt.pattern == null ) { return; }
            
            /* Parse pattern */
            statusParsePattern = patternToArrays( ref bPattern, ref bByteKnown, opt.pattern );
            if( !statusParsePattern ) {
                Console.WriteLine("[Error] Pattern incorrect!");
                return;
            }
            
            bBytesFound = new byte[bPattern.Length];
            
            string givenPattern = "Given pattern: ";
            for( int idxPat = 0; idxPat < bPattern.Length; idxPat++ ) {
                if( bByteKnown[idxPat] != 0 ) {
                    givenPattern += bPattern[idxPat].ToString("X2");
                } else {
                    givenPattern += "--" ;
                }
                givenPattern += " ";
            }
            reportedInternalMessage(InterenalMessageType.Info, "Main", givenPattern);
            
            
            using (Stream strmIn = Console.OpenStandardInput()) {
                int idxIn   = 0;
                int idxOut  = 0;
                byte B      = 0;
                
                while (true) {
                    bytesIn = strmIn.Read(dataIn, 0, 1024);
                    
                    if (bytesIn <= 0) {
                        Thread.Sleep(10);
                        continue;
                    }
                    
                    /* Iterate by every byte from input */
                    for (idxIn = 0; idxIn < bytesIn; idxIn++) {
                        B = dataIn[idxIn];
                        
                        /* Save byte to the output array */
                        bBytesFound[idxOut] = B;
                        
                        if( bByteKnown[idxOut] != 0 ) {
                            if( B != bPattern[idxOut] ) {
                                /* Excpected other byte */
                                idxOut = 0;
                                continue;
                            }
                        }
                        
                        idxOut++;
                        
                        if( idxOut >= bBytesFound.Length ) {
                            printMatch(bBytesFound, bByteKnown, outputType, useColors);
                            idxOut = 0;
                        }
                        
                    }
                }
            }
            
        }
        
        static void printMatch(byte[] bBytesFound, byte[] bBytesKnown, outputf outputType, bool coloring) {
            int idx = 0;
            int jSeq = 0;
            bool foundEscSeq = false;
            string strASCII = "";
            string strHEX   = "";
            string strSub   = "";
            
            if( outputType == outputf.A) {
                strASCII = System.Text.Encoding.ASCII.GetString(bBytesFound).Trim();
                Console.Write(strASCII);  
            } else if( outputType == outputf.AH) {
                byte[] bArr = new byte[1];
                
                for( idx = 0; idx < bBytesFound.Length; idx++ ) {
                    bArr[0] = bBytesFound[idx];
                    
                    strSub = System.Text.Encoding.ASCII.GetString(bArr).Trim();
                    strSub = System.Text.Encoding.GetEncoding("windows-1256").GetString(bArr).Trim();
                    
                    foundEscSeq = false;
                    for( jSeq = 0; jSeq < _escSeq.Length && !foundEscSeq; jSeq++ ) {
                        if( bArr[0] == (byte)_escSeq[jSeq] ) {
                            strSub = "\\" + _escSeqDecoded[jSeq];
                            foundEscSeq = true;
                        }
                    }
                    
                    if( bBytesKnown[idx] != 0 && coloring ) {
                        strASCII += CC.CL_BG_GRAY_LIGHT + CC.CL_FR_BLUE;
                        strHEX += CC.CL_BG_GRAY_LIGHT + CC.CL_FR_BLUE;
                    }
                    
                    strSub = new string(strSub.Where(c => !char.IsControl(c)).ToArray());
                    
                    strSub = String.Format("{0,2} ", strSub);
                    if( strSub.Length != 3 ) { strSub = "   "; }
                    if( strSub.Equals("   ") ) { strSub = " . "; }
                        
                    strASCII += strSub;
                    strHEX += bBytesFound[idx].ToString("X2") + " ";
                    
                    if( bBytesKnown[idx] != 0 && coloring ) {
                        strASCII += CC.CL_RESET;
                        strHEX += CC.CL_RESET;
                    }
                }
                
//                Console.WriteLine( "TEST" + CC.CL_BG_GRAY_LIGHT + CC.CL_FR_BLUE + "TEST" + CC.CL_RESET + "TEST" );
                
                Console.WriteLine(strHEX);
                CC.Reset();
                
                Console.WriteLine(strASCII);
                
            } else {
                for( idx = 0; idx < bBytesFound.Length; idx++ ) {
                    if( bBytesKnown[idx] != 0 && coloring ) {
                        strHEX += CC.CL_BG_GRAY_LIGHT + CC.CL_FR_BLUE;
                    }
                    strHEX += bBytesFound[idx].ToString("X2") + " ";
                    if( bBytesKnown[idx] != 0 && coloring ) {
                        strHEX += CC.CL_RESET;
                    }
                }
                
                Console.Write(strHEX); 
            }
            Console.Write("\r\n");
        }
        
        
        
        
        static bool patternToArrays(
            ref byte[] BaPattern,
            ref byte[] baByteKnown,
            string strPatternIn)
        {
            int idx = 0;
            int valByte = 0;
            int wildCharInOneNum = 1;
            
            string strPattern = "";
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            strPattern = regex.Replace(strPatternIn, " ");
            strPattern = strPattern.Trim(' ');
            
            
            Regex rgxWildCardMult = new Regex("([0-9]*)-{1,}", RegexOptions.IgnoreCase);
            
            
            /* Split pattern into groups */
            string[] strBytes = strPattern.Split(' ');
            
            List<byte> lPattern = new List<byte>();
            List<byte> lByteKnown = new List<byte>();
            
            /* Iterate by every byte in pattern */
            foreach( string strB in strBytes ) {
                try {
                    Match m = rgxWildCardMult.Match(strB);
                    if( strB[0] == '\\' ) {
                        for( int j = 1; j < strB.Length; j++ ) {
                            lByteKnown.Add(1);
                            lPattern.Add((byte)strB[j]);
                        }
                    } else if(m.Success) {
                        /* Found wild char(s) */
                        if( m.Groups.Count > 1 ) {
                            try {
                            wildCharInOneNum = Convert.ToInt16(m.Groups[1].Value);
                            } catch { wildCharInOneNum = 1; }
                        } else {
                            wildCharInOneNum = 1;
                        }
                        
                        
                        for( int j = 0; j < wildCharInOneNum; j++ ) {
                            lByteKnown.Add(0);
                            lPattern.Add(0);
                        }
                        
                    } else {
                        valByte = Convert.ToInt32(strB, 16);
                        
                        lByteKnown.Add(1);
                        lPattern.Add((byte)valByte);
                    }
                    
                } catch { return false; }
                
                idx++;
            }
            
            /* Allocate space for whole pattern */
            BaPattern   = new byte[lPattern.Count];
            baByteKnown = new byte[lByteKnown.Count];
            
            for(int i = 0; i < lPattern.Count; i++) {
                BaPattern[i] = lPattern[i];
                baByteKnown[i] = lByteKnown[i];
            }
            
            return true;
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
            msg += "<regHex." + msgType.ToString() + ">";
            
            if ( useColors ) { msg += CL_RESET; }
            
            msg += "[" + moduleSender + "] " + message + (newLine ? "\r\n" : "");
            
            Console.Write( msg );
        }
    }
}