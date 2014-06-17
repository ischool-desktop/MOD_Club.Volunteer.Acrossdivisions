using FISCA.DSAClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MOD_Club_Acrossdivisions
{
    static public class Va_Ser
    {
        /// <summary>
        /// 社團相關ConTract名稱
        /// </summary>
        static string _contract = "ClubAcrossdivisions";

        /// <summary>
        /// 取得連線學校,檢查是否可以連線
        /// </summary>
        static public bool CheckConnection()
        {
            //取得連線學校清單
            List<LoginSchool> LoginSchoolList = tool._A.Select<LoginSchool>();

            if (LoginSchoolList.Count() < 1)
            {
                return false;
            }
            else
            {
                foreach (LoginSchool each in LoginSchoolList)
                {
                    //檢查
                    Exception ex = Ser.TestConnection(each.School_Name);
                    if (ex != null)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 取得學生清單
        /// (包含學生之社團參與記錄)
        /// </summary>
        static public Dictionary<string, OnlineStudent> GetStuentList(Connection me, LoginSchool login, Dictionary<string, OnlineLock> LockDic)
        {
            FISCA.DSAClient.XmlHelper _xml = new XmlHelper("<Reqluest/>");
            _xml.AddElement("Field");
            _xml.AddElement("Field", "All");
            _xml.AddElement("Condition");

            Envelope rsp = me.SendRequest("_.GetStudentsCanChoose", new Envelope(_xml));
            XElement students = XElement.Parse(rsp.Body.XmlString);

            Dictionary<string, OnlineStudent> StudentDic = new Dictionary<string, OnlineStudent>();
            foreach (XElement stud in students.Elements("Student"))
            {
                OnlineStudent os = new OnlineStudent(stud);
                os.School = login.School_Name;

                if (!StudentDic.ContainsKey(os.Id))
                {
                    StudentDic.Add(os.Id, os);
                }

                if (LockDic.ContainsKey(os.Id))
                {
                    os.ClubJoin = LockDic[os.Id];
                }

            }
            return StudentDic;
        }

        /// <summary>
        /// 取得學校之社團記錄
        /// </summary>
        static public Dictionary<string, OnlineClub> GetClubList(Connection me, LoginSchool login)
        {
            FISCA.DSAClient.XmlHelper _xml = new XmlHelper("<Reqluest/>");
            _xml.AddElement("Field");
            _xml.AddElement("Field", "All");
            _xml.AddElement("Condition");
            _xml.AddElement("Condition", "SchoolYear", K12.Data.School.DefaultSchoolYear);
            _xml.AddElement("Condition", "Semester", K12.Data.School.DefaultSemester);

            Envelope rsp = me.SendRequest("_.GetClubList", new Envelope(_xml));
            XElement clubs = XElement.Parse(rsp.Body.XmlString);

            Dictionary<string, OnlineClub> ClubDic = new Dictionary<string, OnlineClub>();
            foreach (XElement club in clubs.Elements("K12.clubrecord.universal"))
            {
                OnlineClub cr = new OnlineClub(club);
                cr.School = login.School_Name;
                if (!ClubDic.ContainsKey(cr.UID))
                {
                    ClubDic.Add(cr.UID, cr);
                }
            }
            return ClubDic;
        }

        /// <summary>
        /// 取得志願序內容
        /// </summary>
        static public List<OnlineVolunteer> GetVolunteer(Connection me, LoginSchool login)
        {
            FISCA.DSAClient.XmlHelper _xml = new XmlHelper("<Reqluest/>");
            _xml.AddElement("Field");
            _xml.AddElement("Field", "All");
            _xml.AddElement("Condition");
            _xml.AddElement("Condition", "SchoolYear", K12.Data.School.DefaultSchoolYear);
            _xml.AddElement("Condition", "Semester", K12.Data.School.DefaultSemester);

            Envelope rsp = me.SendRequest("_.GetStudentVolunteer", new Envelope(_xml));
            XElement Volunteers = XElement.Parse(rsp.Body.XmlString);
            List<OnlineVolunteer> VolunteerList = new List<OnlineVolunteer>();

            foreach (XElement Volunteer in Volunteers.Elements("K12.volunteer.universal"))
            {
                OnlineVolunteer cr = new OnlineVolunteer(Volunteer);
                cr.School = login.School_Name;
                VolunteerList.Add(cr);
            }
            return VolunteerList;
        }

        private static Dictionary<string, OnlineLock> GetLockStudent(Connection me, LoginSchool login)
        {
            Dictionary<string, OnlineLock> dic = new Dictionary<string, OnlineLock>();

            FISCA.DSAClient.XmlHelper _xml = new XmlHelper("<Reqluest/>");
            _xml.AddElement("Field");
            _xml.AddElement("Field", "All");
            _xml.AddElement("Condition");
            _xml.AddElement("Condition", "SchoolYear", K12.Data.School.DefaultSchoolYear);
            _xml.AddElement("Condition", "Semester", K12.Data.School.DefaultSemester);

            Envelope rsp = me.SendRequest("_.GetLockStudent", new Envelope(_xml));
            XElement clubs = XElement.Parse(rsp.Body.XmlString);

            foreach (XElement club in clubs.Elements("Student"))
            {
                OnlineLock ol = new OnlineLock(club);
                if (!dic.ContainsKey(ol.StudentId))
                {
                    dic.Add(ol.StudentId, ol);
                }
            }
            return dic;

        }

        /// <summary>
        /// 取得各校之相關基本資料(社團/學生/志願序)
        /// </summary>
        static public Dictionary<string, AcrossRecord> SchoolClubDetail(List<LoginSchool> LoginSchoolList)
        {
            Dictionary<string, AcrossRecord> dic = new Dictionary<string, AcrossRecord>();
            foreach (LoginSchool login in LoginSchoolList)
            {
                Connection me = new Connection();
                me.Connect(login.School_Name, _contract, FISCA.Authentication.DSAServices.PassportToken);
                me = me.AsContract(_contract);

                //取得本學年度/學期,目前系統中的鎖定學生資料
                Dictionary<string, OnlineLock> StudentLockDic = Va_Ser.GetLockStudent(me, login);

                AcrossRecord ar = new AcrossRecord();
                ar.School = login.School_Name;

                //取得可選社之學生
                //年級/班級/座號/姓名/科別
                ar.StudentDic = Va_Ser.GetStuentList(me, login, StudentLockDic);

                //取得學校之社團記錄
                ar.ClubDic = Va_Ser.GetClubList(me, login);

                //取得學生之選填志願內容
                ar.VolunteerList = Va_Ser.GetVolunteer(me, login);



                if (!dic.ContainsKey(login.School_Name))
                {
                    dic.Add(login.School_Name, ar);
                }
            }

            return dic;
        }

        //進行社團資源整合
        static public Dictionary<string, OnlineMergerClub> ResourceMerger(Dictionary<string, AcrossRecord> SchoolClubDic)
        {
            Dictionary<string, OnlineMergerClub> dic = new Dictionary<string, OnlineMergerClub>();

            #region 步驟1取得學校
            foreach (AcrossRecord Across in SchoolClubDic.Values)
            {
                //使用 Point 學校為基準
                if (Across.School == tool.Point)
                {
                    //取得學校的社團
                    foreach (OnlineClub club in Across.ClubDic.Values)
                    {

                        OnlineMergerClub Mclub = new OnlineMergerClub();
                        Mclub.ClubName = club.ClubName;
                        Mclub.SchoolYear = club.SchoolYear;
                        Mclub.Semester = club.Semester;
                        Mclub.DeptRestrict = club.DeptRestrict;
                        Mclub.Grade1Limit = club.Grade1Limit;
                        Mclub.Grade2Limit = club.Grade2Limit;
                        Mclub.Grade3Limit = club.Grade3Limit;
                        Mclub.DeptRestrict = club.DeptRestrict;
                        Mclub.Limit = club.Limit;

                        if (!dic.ContainsKey(club.ClubName))
                        {
                            dic.Add(club.ClubName, Mclub);
                        }
                    }
                }
            }
            #endregion

            #region 步驟2進行資源整合

            foreach (AcrossRecord Across in SchoolClubDic.Values)
            {
                foreach (OnlineClub club in Across.ClubDic.Values)
                {
                    if (dic.ContainsKey(club.ClubName))
                    {
                        if (dic[club.ClubName].IsMyClub(club))
                        {
                            dic[club.ClubName].SetClub(club);
                        }
                    }
                    else
                    {
                        OnlineMergerClub Mclub = new OnlineMergerClub();
                        Mclub.ClubName = club.ClubName;
                        Mclub.SchoolYear = club.SchoolYear;
                        Mclub.Semester = club.Semester;
                        Mclub.DeptRestrict = club.DeptRestrict;
                        Mclub.Grade1Limit = club.Grade1Limit;
                        Mclub.Grade2Limit = club.Grade2Limit;
                        Mclub.Grade3Limit = club.Grade3Limit;
                        Mclub.DeptRestrict = club.DeptRestrict;
                        Mclub.Limit = club.Limit;
                        Mclub.SetClub(club);

                        if (!dic.ContainsKey(club.ClubName))
                        {
                            dic.Add(club.ClubName, Mclub);
                        }
                    }
                }
            }
            #endregion

            return dic;
        }
    }
}
