using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MOD_Club_Acrossdivisions
{
    public class OnlineStudent
    {
        public OnlineStudent(XElement student)
        {
            if (student.Element("Id") != null)
                Id = student.Element("Id").Value;

            if (student.Element("Name") != null)
                Name = student.Element("Name").Value;

            if (student.Element("ClassName") != null)
                ClassName = student.Element("ClassName").Value;

            if (student.Element("DisplayOrder") != null)
                DisplayOrder = student.Element("DisplayOrder").Value;

            if (student.Element("TeacherName") != null)
                TeacherName = student.Element("TeacherName").Value;

            if (student.Element("DeptName") != null)
            {
                string[] st = student.Element("DeptName").Value.Split(':');
                if (st.Length > 1)
                    DeptName = st.GetValue(0).ToString();
            }

            if (student.Element("StudentNumber") != null)
                StudentNumber = student.Element("StudentNumber").Value;

            if (student.Element("SeatNo") != null)
                SeatNo = student.Element("SeatNo").Value;

            if (student.Element("Gender") != null)
                Gender = student.Element("Gender").Value;

            if (student.Element("GradeYear") != null)
                GradeYear = student.Element("GradeYear").Value;

            ClubList = new List<OnlineClub>();
        }

        /// <summary>
        /// 學校
        /// </summary>
        public string School { get; set; }

        /// <summary>
        /// 學生系統編號
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 班級名稱
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 班級序號
        /// </summary>
        public string DisplayOrder { get; set; }

        /// <summary>
        /// 老師姓名
        /// </summary>
        public string TeacherName { get; set; }

        /// <summary>
        /// 科別名稱
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 座號
        /// </summary>
        public string SeatNo { get; set; }

        /// <summary>
        /// 學號
        /// </summary>
        public string StudentNumber { get; set; }

        /// <summary>
        /// 年級
        /// </summary>
        public string GradeYear { get; set; }

        /// <summary>
        /// 姓別
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 是否選社鎖定
        /// 
        /// null
        /// 就是未鎖定
        /// 
        /// 不為null
        /// 有資料可查詢詳細資訊
        /// </summary>
        public OnlineLock ClubJoin { get; set; }

        /// <summary>
        /// 我的志願序清單
        /// </summary>
        public List<OnlineClub> ClubList { get; set; }
    }
}
