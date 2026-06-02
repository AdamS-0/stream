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
        [Parameter(ParameterAlias = "p", HelpText = "Regex pattern",
                   ArgumentName = "pattern [replacement]", ArgumentRequired = true)]
        public object pattern { get; set; }
        
        [Parameter(ParameterAlias = "c", HelpText = "Turn off coloring")]
        public string noColors { get; set; }
        
        [Parameter(ParameterAlias = "f", HelpText = "Filter for only pattern lines")]
        public string filter{ get; set; }
        
        [Parameter(ParameterAlias = "s", HelpText = "Silent mode, with no messages from this application")]
        public string silent{ get; set; }
        
        [Parameter(ParameterAlias = "b", HelpText = "Size of the buffer (default 65536). Should be set to 110% of max sentence length")]
        public string buffSize{ get; set; }
        
    }
}
