/*
 * Created by SharpDevelop.
 * User: AdamS-0
 * Date: 18.05.2025
 * Time: 15:44
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using CommandLineParser;

namespace streamGraph {
    class Program {
        public static void Main(string[] args) {
            
            Options opt = CommandLineParser.CommandLineParser.ParseArguments<Options>(args);
            
            if( opt.pattern == null ) { opt.pattern  = ".*GSV,1,1,([0-9][0-9]).*"; }
            if( opt.nSamples == null ) { opt.nSamples = "100"; }
            if( opt.name == null ) { opt.name = ""; }
            
            
            Console.WriteLine("Given pattern: '" + opt.pattern + "'");
            Console.WriteLine("Given pattern: '" + opt.nSamples + "'");
            
            Graph gr = new Graph( opt.pattern, Convert.ToInt32(opt.nSamples), opt.name );
            
            Thread th = new Thread(() => { gr.ShowDialog(); } );
            
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            
            byte[] dataIn = new byte[1024];
            string strIn = "";
            
            while( true ) {
                strIn = Console.ReadLine();
                gr.DataReceived( Encoding.Default.GetBytes(strIn) );
            }
        }
        
    }
    
    
    [AppInfo(
        AppName         = AppInfo.Name,
        AppVersion      = AppInfo.Version,
        AppAuthor       = AppInfo.Author,
        AppDescription  = AppInfo.Description)]
    public class Options {
        [Parameter(ParameterAlias = "p", HelpText = "RegEx pattern", ArgumentName = "pattern", ArgumentRequired = true)]
        public string pattern { get; set; }
        
        [Parameter(ParameterAlias = "n", ParameterName = "numSamples", HelpText = "Record length", ArgumentName = "num", ArgumentRequired = true)]
        public string nSamples { get; set; }
        
        [Parameter(HelpText = "Graph name", ArgumentName = "name", ArgumentRequired = true, Required = false)]
        public string name { get; set; }
        
    }
}