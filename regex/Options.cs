/*
 * Created by SharpDevelop.
 * User: AdamS-0
 * Date: 07.03.2026
 * Time: 13:24
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Reflection;
using CommandLineParser;

namespace regex {
    
    [AppInfo(
        AppName         = AppInfo.Name,
        AppVersion      = AppInfo.Version,
        AppAuthor       = AppInfo.Author,
        AppDescription  = AppInfo.Description)]
    public class Options {
        [Parameter(ParameterAlias = "p", HelpText = "Hexadecimal pattern",
                   ArgumentName = "pattern", ArgumentRequired = true)]
        public string pattern { get; set; }
        
            [Parameter(ParameterAlias = "r", HelpText = "Replace with gien sequence",
                   ArgumentName = "pattern", ArgumentRequired = true)]
        public string replaceWith { get; set; }
        
        [Parameter(ParameterAlias = "c", HelpText = "Turn off coloring")]
        public string noColors { get; set; }
        
    }
}
