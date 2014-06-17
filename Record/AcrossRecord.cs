using FISCA.DSAUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MOD_Club_Acrossdivisions
{
    public class AcrossRecord
    {
        //一個包含了學校DomainName
        //和學校端學生/志願/社團...等相關資料的物件

        public AcrossRecord()
        {
            StudentDic = new Dictionary<string, OnlineStudent>();
            ClubDic = new Dictionary<string, OnlineClub>();
            VolunteerList = new List<OnlineVolunteer>();
            infoDic = new Dictionary<string, ClassRowInfo>();
        }

        /// <summary>
        /// 開始進行志願序資料整合
        /// </summary>
        public void RunMergerVolunteer()
        {
            //整理志願序資料
            foreach (OnlineVolunteer each in VolunteerList)
            {
                if (StudentDic.ContainsKey(each.RefStudentId))
                {
                    //學生
                    OnlineStudent OnlineStud = StudentDic[each.RefStudentId];

                    if (!string.IsNullOrEmpty(each.Content))
                    {
                        XmlElement xml = DSXmlHelper.LoadXml(each.Content);

                        foreach (XmlElement node in xml.SelectNodes("Club"))
                        {
                            string clubID = node.GetAttribute("Ref_Club_ID");
                            if (ClubDic.ContainsKey(clubID))
                            {
                                //社團
                                OnlineClub OnlineClub = ClubDic[clubID];
                                OnlineStud.ClubList.Add(OnlineClub);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 開始進行班級分類
        /// </summary>
        public void RunCheckClass()
        {
            foreach (OnlineStudent each in StudentDic.Values)
            {
                if (infoDic.ContainsKey(each.ClassName))
                {
                    infoDic[each.ClassName].StudentList.Add(each);

                    if (each.ClubList.Count > 0)
                    {
                        infoDic[each.ClassName].SelectCount++; //已選填人數
                    }

                    if (each.ClubJoin != null)
                    {
                        infoDic[each.ClassName].NumberOfParticipants++; //ClubJoin不為null,社團參與人數+1

                        if (each.ClubJoin.IsLock)
                        {
                            infoDic[each.ClassName].LockNumber++; //社團鎖定人數
                        }
                    }
                }
                else
                {
                    ClassRowInfo info = new ClassRowInfo();
                    info.School = each.School; //學校
                    info.ClassName = each.ClassName; //班級名稱
                    info.GradeYear = each.GradeYear; //年級
                    info.DisplayOrder = each.DisplayOrder; //年級
                    info.TeacherName = each.TeacherName; //老師
                    info.StudentList.Add(each);

                    if (each.ClubList.Count > 0)
                    {
                        info.SelectCount++; //已選填人數
                    }

                    if (each.ClubJoin != null)
                    {
                        info.NumberOfParticipants++; //ClubJoin不為null,社團參與人數+1

                        if (each.ClubJoin.IsLock)
                        {
                            info.LockNumber++; //社團鎖定人數
                        }
                    }

                    infoDic.Add(each.ClassName, info);

                }
            }
        }

        public string School { get; set; }

        /// <summary>
        /// 學校所有學生資料(字典)
        /// </summary>
        public Dictionary<string, OnlineStudent> StudentDic { get; set; }

        /// <summary>
        /// 學校所有社團資料
        /// </summary>
        public Dictionary<string, OnlineClub> ClubDic { get; set; }

        /// <summary>
        /// 學校所有志願序清單
        /// </summary>
        public List<OnlineVolunteer> VolunteerList { get; set; }

        public Dictionary<string, ClassRowInfo> infoDic { get; set; }
    }
}
