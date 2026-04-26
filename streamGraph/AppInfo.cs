/*
 * Created by SharpDevelop.
 * User: AdamS-0
 * Date: 26.04.2026
 * Time: 17:28
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace streamGraph
{
    /// <summary>
    /// Description of AppInfo.
    /// </summary>
    public class AppInfo {
        public const string Name = "streamGraph";
        public const string Version = "1.1.0_" + vn.BUILD_NUM + "_" +
                                      vn.BUILD_YEAR + vn.BUILD_MON + vn.BUILD_DAY + "." +
                                      vn.BUILD_HOUR + vn.BUILD_MIN;
        public const string Author = "AdamS-0 2026";
        public const string Description = "Graphs drawn just from stream!";
            
        
        public AppInfo() {
        }
    }
}
