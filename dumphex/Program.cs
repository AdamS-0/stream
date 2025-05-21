/*
 * Created by SharpDevelop.
 * User: AdamS-0
 * Date: 11.04.2025
 * Time: 17:12
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Threading;

namespace dumphex {
    class Program {
        public static void Main(string[] args) {
            byte[] dataIn   = new byte[1024];
            char[] dataOut  = new char[1024];
            int bytesIn     = 0;
            int idxIn       = 0;
            int idxOut      = 0;
            byte cC         = 0;
            byte cNipMSB    = 0;
            byte cNip       = 0;
            bool MSBNow     = true;
            
            using (Stream strmIn = Console.OpenStandardInput()) {
                while (true) {
                    bytesIn = strmIn.Read(dataIn, 0, 1024);
                    
                    if (bytesIn <= 0) {
                        Thread.Sleep(10);
                        continue;
                    }
                    
                    for (idxIn = 0; idxIn < bytesIn; idxIn++) {
                        cC = dataIn[idxIn];
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
                        
                        if (MSBNow) {
                            cNipMSB = (byte)(cNip << 4);
                        } else {
                            cNipMSB |= cNip;
                            
                            if (idxOut >= 1024) {
                                Console.Write(dataOut);
                                idxOut = 0;
                            }
                            
                            dataOut[idxOut++] = (char)cNipMSB;
                        }
                        MSBNow = !MSBNow;
                    }
                    
                    if (idxOut > 0) {
                        Console.Write(dataOut, 0, idxOut);
                        idxOut = 0;
                    }
                    
                }
            }
        }
    }
}