/*
 * Created by SharpDevelop.
 * User: AdamS-0
 * Date: 02.04.2025
 * Time: 19:12
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Threading;

namespace hexdump {
    class Program {
        public static void Main(string[] args) {
            byte[] dataIn           = new byte[1024];
            int bytesIn             = 0;
            int idx                 = 0;
            int colId               = 0;
            int showIn8ByteColumns  = 2;
            if( args != null ) {
                if( args.Length > 0 ) {
                    try { showIn8ByteColumns = Convert.ToInt16(args[0]); } catch {}
                }
            }
            
            using (Stream strmIn = Console.OpenStandardInput()) {
                while (true) {
                    bytesIn = strmIn.Read(dataIn, 0, 1024);
                    
                    if (bytesIn <= 0) {
                        Thread.Sleep(10);
                        continue;
                    }
                    
                    for (idx = 0; idx < bytesIn; idx++) {
                        Console.Write(dataIn[idx].ToString("X2"));
                        
                        if (colId == ( (showIn8ByteColumns * 8) - 1) ) {
                            Console.Write("\r\n");
                        } else if (colId % 8 == 7) {
                            Console.Write("  ");
                        } else {
                            Console.Write(" ");
                        }
                        
                        if( showIn8ByteColumns > 0) {
                            colId = (colId + 1) % (showIn8ByteColumns * 8);
                        }
                    }
                }
            }
        }
    }
}