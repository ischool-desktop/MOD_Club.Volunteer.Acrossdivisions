using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MOD_Club_Acrossdivisions
{
    public class OnlineLock
    {


        public OnlineLock(XElement club)
        {
            if (club.Element("Id") != null)
                StudentId = club.Element("Id").Value;

            if (club.Element("Name") != null)
                StudentName = club.Element("Name").Value;

            if (club.Element("ClubName") != null)
                ClubName = club.Element("ClubName").Value;

            if (club.Element("SchoolYear") != null)
                SchoolYear = club.Element("SchoolYear").Value;

            if (club.Element("Semester") != null)
                Semester = club.Element("Semester").Value;

            if (club.Element("Lock") != null)
                if (club.Element("Lock").Value == "t")
                {
                    IsLock = true;
                }
                else
                {
                    IsLock = false;
                }
        }

        /// <summary>
        /// 社團名稱
        /// </summary>
        public string ClubName { get; set; }


        /// <summary>
        /// 學生系統編號
        /// </summary>
        public string StudentId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string StudentName { get; set; }

        /// <summary>
        /// 學年度
        /// </summary>
        public string SchoolYear { get; set; }

        /// <summary>
        /// 學期
        /// </summary>
        public string Semester { get; set; }

        /// <summary>
        /// 是否鎖定
        /// </summary>
        public bool IsLock { get; set; }
    }
}
