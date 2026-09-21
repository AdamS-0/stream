/*
 * Created by SharpDevelop.
 * User: AdamS-0
 * Date: 07.03.2026
 * Time: 13:27
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace regex {
    /// <summary>
    /// Description of AppInfo.
    /// </summary>
    public class AppInfo {
        public const string Name = "regex";
        public const string Version = "1.2.0_" + vn.BUILD_NUM + "_" +
                                      vn.BUILD_YEAR + vn.BUILD_MON + vn.BUILD_DAY + "." +
                                      vn.BUILD_HOUR + vn.BUILD_MIN;
        public const string Author = "AdamS-0 2026";
        public const string Description = "Regular expression for stream";
            
        
        public AppInfo() {
        }
    }
}
