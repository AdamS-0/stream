/*
 * Created by SharpDevelop.
 * User: AdamS-0
 * Date: 26.07.2025
 * Time: 17:56
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

/// <summary>
/// Description of ConsolePaint.
/// </summary>
public static class CC {
    public static string CL_RESET = "\x1b[0m"; // All attributes off(color at startup)
    public static string CL_BOLD_ON = "\x1b[1m"; // Bold on(enable foreground intensity)
    public static string CL_BOLD_OFF = "\x1b[21m"; // Bold off(disable foreground intensity)
    public static string CL_RUNDERLINE = "\x1b[4m"; // Underline on
    public static string CL_UNDERLINE_OFF = "\x1b[24m"; // Underline off
    public static string CL_BLINK_ON = "\x1b[5m"; // Blink on(enable background intensity)
    public static string CL_BLINK_OFF = "\x1b[25m"; // Blink off(disable background intensity)
//    Escape sequence    Foreground colors
    public static string CL_FR_BLACK = "\x1b[30m"; // Black
    public static string CL_FR_RED = "\x1b[31m"; // Red
    public static string CL_FR_GREEN = "\x1b[32m"; // Green
    public static string CL_FR_YELLOW = "\x1b[33m"; // Yellow
    public static string CL_FR_BLUE = "\x1b[34m"; // Blue
    public static string CL_FR_MAGENTA = "\x1b[35m"; // Magenta
    public static string CL_FR_CYAN = "\x1b[36m"; // Cyan
    public static string CL_FR_WHITE = "\x1b[37m"; // White
    public static string CL_FR_DEFAULT = "\x1b[39m"; // Default(foreground color at startup)
    public static string CL_FR_GRAY_LIGHT = "\x1b[90m"; // Light Gray
    public static string CL_FR_RED_LIGHT = "\x1b[91m"; // Light Red
    public static string CL_FR_GREEN_LIGHT = "\x1b[92m"; // Light Green
    public static string CL_FR_YELLOW_LIGHT = "\x1b[93m"; // Light Yellow
    public static string CL_FR_BLUE_LIGHT = "\x1b[94m"; // Light Blue
    public static string CL_FR_MAGENTA_LIGHT = "\x1b[95m"; // Light Magenta
    public static string CL_FR_CYAN_LIGHT = "\x1b[96m"; // Light Cyan
    public static string CL_FR_WHITE_LIGHT = "\x1b[97m"; // Light White
//    Escape sequence    Background colors
    public static string CL_BG_BLACK = "\x1b[40m"; // Black
    public static string CL_BG_RED = "\x1b[41m"; // Red
    public static string CL_BG_GREEN = "\x1b[42m"; // Green
    public static string CL_BG_YELLOW = "\x1b[43m"; // Yellow
    public static string CL_BG_BLUE = "\x1b[44m"; // Blue
    public static string CL_BG_MAGENTA = "\x1b[45m"; // Magenta
    public static string CL_BG_CYAN = "\x1b[46m"; // Cyan
    public static string CL_BG_WHITE = "\x1b[47m"; // White
    public static string CL_BG_DEFAULT = "\x1b[49m"; // Default(background color at startup)
    public static string CL_BG_GRAY_LIGHT = "\x1b[100m"; // Light Gray
    public static string CL_BG_RED_LIGHT = "\x1b[101m"; // Light Red
    public static string CL_BG_GREEN_LIGHT = "\x1b[102m"; // Light Green
    public static string CL_BG_YELLOW_LIGHT = "\x1b[103m"; // Light Yellow
    public static string CL_BG_BLUE_LIGHT = "\x1b[104m"; // Light Blue
    public static string CL_BG_MAGENTA_LIGHT = "\x1b[105m"; // Light Magenta
    public static string CL_BG_CYAN_LIGHT = "\x1b[106m"; // Light Cyan
    public static string CL_BG_WHITE_LIGHT = "\x1b[107m"; // Light White
    
    public static void Color(ConsoleColor foreColor, ConsoleColor backColor) {
        Console.ForegroundColor = foreColor;
        Console.BackgroundColor = backColor;
    }
    
    public static void Color(ConsoleColor foreColor) {
        Console.ForegroundColor = foreColor;
    }
    
    
    public static void BoldOn() {
        Console.Write(CL_BOLD_ON);
    }
    
    public static void BoldOff() {
        Console.Write(CL_BOLD_OFF);
    }
    
    
    public static void UnderlineOn() {
        Console.Write(CL_RUNDERLINE);
    }
    
    public static void UnderlineOff() {
        Console.Write(CL_UNDERLINE_OFF);
    }
    
    
    public static void BlinkOn() {
        Console.Write(CL_BLINK_ON);
    }
    
    public static void BlinkOff() {
        Console.Write(CL_BLINK_OFF);
    }
    
    public static void Reset() {
        Console.Write(CL_RESET);
    }    
    
    
}

