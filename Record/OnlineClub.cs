using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MOD_Club_Acrossdivisions
{
    public class OnlineClub
    {
        public OnlineClub(XElement club)
        {

            if (club.Element("Uid") != null)
                UID = club.Element("Uid").Value;

            if (club.Element("ClubName") != null)
                ClubName = club.Element("ClubName").Value;

            if (club.Element("DeptRestrict") != null)
                DeptRestrict = club.Element("DeptRestrict").Value;

            if (club.Element("Grade1Limit") != null)
                Grade1Limit = club.Element("Grade1Limit").Value;

            if (club.Element("Grade2Limit") != null)
                Grade2Limit = club.Element("Grade2Limit").Value;

            if (club.Element("Grade3Limit") != null)
                Grade3Limit = club.Element("Grade3Limit").Value;

            if (club.Element("Limit") != null)
                Limit = club.Element("Limit").Value;

            if (club.Element("TeacherName") != null)
                TeacherName = club.Element("TeacherName").Value;

            if (club.Element("SchoolYear") != null)
                SchoolYear = club.Element("SchoolYear").Value;

            if (club.Element("Semester") != null)
                Semester = club.Element("Semester").Value;
        }

        //臨時社團資料

        /// <summary>
        /// 學校
        /// </summary>
        public string School { get; set; }

        /// <summary>
        /// 學校UID
        /// </summary>
        public string UID { get; set; }

        /// <summary>
        /// 社團名稱
        /// </summary>
        public string ClubName { get; set; }

        /// <summary>
        /// 科別限制
        /// </summary>
        public string DeptRestrict { get; set; }

        /// <summary>
        /// 一年級人數上限
        /// </summary>
        public string Grade1Limit { get; set; }

        /// <summary>
        /// 二年級人數上限
        /// </summary>
        public string Grade2Limit { get; set; }

        /// <summary>
        /// 三年級人數上限
        /// </summary>
        public string Grade3Limit { get; set; }

        /// <summary>
        /// 總人數上限
        /// </summary>
        public string Limit { get; set; }

        /// <summary>
        /// 老師姓名
        /// </summary>
        public string TeacherName { get; set; }

        /// <summary>
        /// 學年度
        /// </summary>
        public string SchoolYear { get; set; }

        /// <summary>
        /// 學期
        /// </summary>
        public string Semester { get; set; }


    }
}
