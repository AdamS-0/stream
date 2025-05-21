/*
 * Created by SharpDevelop.
 * User: AdamS-0
 * Date: 26.04.2025
 * Time: 08:48
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace tsNow {
    class Program {
        public static void Main(string[] args) {
            DateTime dt = DateTime.Now;
            Console.WriteLine(dt.ToString("yyyy.MM.dd_HH.mm.ss"));
        }
    }
}