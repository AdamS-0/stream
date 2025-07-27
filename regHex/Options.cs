/*
 * Created by SharpDevelop.
 * User: AdamS-0
 * Date: 26.07.2025
 * Time: 14:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Reflection;
using CommandLineParser;

namespace regHex {
    
    [AppInfo(
        AppName         = AppInfo.Name,
        AppVersion      = AppInfo.Version,
        AppAuthor       = AppInfo.Author,
        AppDescription  = AppInfo.Description)]
    public class Options {
        [Parameter(ParameterAlias = "p", HelpText = "Hexadecimal pattern:\n" + 
                   "\"5\" or \"05\" - hexdecimal number can be written as signel digit\n" +
                   "\"-\" or \"--\" - wild char\n" +
                   "\"n-\" - wild char will be multiplaced n times, example is equal to \"-- -- -- ...\"\n" +
                   "\"\\str\" - look for whole string str\n" +
//                   "\"*-\" - unknow length wild char, up to the next given known byte. Cannot be used as last sequence\n" +
                   "",
                   ArgumentName = "pattern", ArgumentRequired = true)]
        public string pattern { get; set; }

        [Parameter(ParameterAlias = "o", HelpText = "Format of printed output:\n" + 
                   "A  - ASCII decoded message (one row per match)\n" +
                   "H  - hexdecimal numbers separated with spaces (one row per match) - defualt\n" +
                   "AH - ASCII & hexdecimal values (three rows per match: H, A, empty)",
                   ArgumentName = "A/H/AH", ArgumentRequired = true)]
        public string output { get; set; }
        
        [Parameter(ParameterAlias = "c", HelpText = "Turn on coloring")]
        public string colors { get; set; }
        
    }
}
