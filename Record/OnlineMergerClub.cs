using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOD_Club_Acrossdivisions
{
    public class OnlineMergerClub
    {

        //一個包含各部別的社團

        public bool IsMyClub(OnlineClub club)
        {
            if (club.ClubName == ClubName)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetClub(OnlineClub club)
        {
            AllClubList.Add(club);
        }

        public OnlineMergerClub()
        {
            AllClubList = new List<OnlineClub>();
        }

        /// <summary>
        /// 合併資源的社團資料
        /// </summary>
        List<OnlineClub> AllClubList { get; set; }
        /// <summary>
        /// 學年度
        /// </summary>
        public string SchoolYear { get; set; }

        /// <summary>
        /// 學期
        /// </summary>
        public string Semester { get; set; }

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
    }
}
