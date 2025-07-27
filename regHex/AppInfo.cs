/*
 * Created by SharpDevelop.
 * User: AdamS-0
 * Date: 26.07.2025
 * Time: 14:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace regHex {
    /// <summary>
    /// Description of AppInfo.
    /// </summary>
    public class AppInfo {
        
        public const string Name = "regHex";
        public const string Version = "1.1.2_" + vn.BUILD_NUM + "_" +
                                      vn.BUILD_YEAR + vn.BUILD_MON + vn.BUILD_DAY + "." +
                                      vn.BUILD_HOUR + vn.BUILD_MIN;
        public const string Author = "AdamS-0 2025";
        public const string Description = "Regular expression for binary data";
        
        public AppInfo() {
        }
    }
}
