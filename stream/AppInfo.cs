/*
 * Created by SharpDevelop.
 * User: AdamS-0
 * Date: 20.07.2025
 * Time: 19:20
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Reflection;

namespace stream {
    /// <summary>
    /// Description of AppInfo.
    /// </summary>
    public class AppInfo {
        
        public const string Name = "stream";
        public const string Version = "1.1.0-" + vn.BUILD_NUM + "_" +
            vn.BUILD_YEAR + vn.BUILD_MON + vn.BUILD_DAY + "." +
            vn.BUILD_HOUR + vn.BUILD_MIN;
        public const string Author = "AdamS-0 2025";
        public const string Description = "Simple application for stream IN/OUT operations";
        
        public AppInfo() {
        }
    }
}
