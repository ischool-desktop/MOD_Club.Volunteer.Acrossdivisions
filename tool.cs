using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOD_Club_Acrossdivisions
{
    static public class tool
    {
        /// <summary>
        /// 取得目前本機的AccessPoint
        /// </summary>
        static public string Point = FISCA.Authentication.DSAServices.AccessPoint;

        /// <summary>
        /// 使用者記錄檔
        /// </summary>
        static public string ConfigCode = "MOD_Club_Acrossdivisions.NowConnection.cs";

        /// <summary>
        /// 使用者記錄檔
        /// </summary>
        static public string ConfigName = "連線學校";

        static public FISCA.UDT.AccessHelper _A = new FISCA.UDT.AccessHelper();

        static public FISCA.Data.QueryHelper _Q = new FISCA.Data.QueryHelper();
    }
}
