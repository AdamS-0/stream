/*
 * Created by SharpDevelop.
 * User: AdamS-0
 * Date: 02.04.2025
 * Time: 19:04
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;

namespace streamTester
{
    class Program
    {
        public static void Main(string[] args)
        {

            Byte[] data = new byte[256];
            
            for( int i = 0; i < data.Length; i++ ) {
                data[i] = (byte)i;
            }
            
            using(Stream strm = Console.OpenStandardOutput()) {
                strm.Write(data, 0, data.Length);
            }
        }
    }
}