/*
 * Created by SharpDevelop.
 * User: AdamS-0
 * Date: 07.03.2026
 * Time: 13:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

/// <summary>
/// Description of logger.
/// </summary>
public class logger {
    static string CL_RESET  = "\x1B[0m";
    static string CL_RED    = "\x1B[31m";
    static string CL_YEL    = "\x1B[93m";
    static string CL_GREEN  = "\x1B[32m";
    
    public enum InterenalMessageType {
        Error,
        Warning,
        Info
    }
    
    public string appName = "";
    public bool useColors = true;
    
    public logger(string appName, bool useColors = true) {
        this.appName = appName;
        this.useColors = useColors;
    }
    
    
    static string getColorFromMsgType(InterenalMessageType msgType) {
        switch (msgType) {
            case InterenalMessageType.Error     : return CL_RED;
            case InterenalMessageType.Warning   : return CL_YEL;
            case InterenalMessageType.Info      : return CL_GREEN;
        }
        return "";
    }
    
    /* "<{appName}.{msgType}>[{moduleSender}] {message}" */
    public void reportMsg(InterenalMessageType msgType, string moduleSender, string message, bool newLine = true) {           
        string msg = "";
        
        if ( useColors ) { msg += getColorFromMsgType(msgType); }
        msg += "<" + appName + "." + msgType.ToString() + ">";
        
        if ( useColors ) { msg += CL_RESET; }
        
        msg += "[" + moduleSender + "] " + message + (newLine ? "\r\n" : "");
        
        Console.Write( msg );
    }
}
