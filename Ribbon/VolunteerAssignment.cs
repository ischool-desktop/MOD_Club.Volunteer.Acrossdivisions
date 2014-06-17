using FISCA.DSAClient;
using FISCA.Presentation.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace MOD_Club_Acrossdivisions
{
    public partial class VolunteerAssignment : BaseForm
    {
        /// <summary>
        /// 連線後,Call Service 取得之學校相關內容
        /// </summary>
        Dictionary<string, AcrossRecord> SchoolClubDic { get; set; }

        /// <summary>
        /// 國高中共同資源社團
        /// </summary>
        Dictionary<string, OnlineMergerClub> MergerClubDic { get; set; }

        public VolunteerAssignment()
        {
            InitializeComponent();
        }

        private void VolunteerAssignment_Load(object sender, EventArgs e)
        {
            lbHelpConSchool.Text = string.Format("{0}學年度 第{1}學期 , 已連線學校:", K12.Data.School.DefaultSchoolYear, K12.Data.School.DefaultSemester);
            BackgroundWorker BGW = new BackgroundWorker();
            BGW.RunWorkerCompleted += BGW_RunWorkerCompleted;
            BGW.DoWork += BGW_DoWork;
            this.Text = "社團志願分配(跨部別)　資料取得中...";

            BGW.RunWorkerAsync();


        }

        void BGW_DoWork(object sender, DoWorkEventArgs e)
        {
            //是否可連線&是否可取得相關資料
            if (!Va_Ser.CheckConnection())
                e.Cancel = true;

            //取得學校連線資料
            List<LoginSchool> LoginSchoolList = tool._A.Select<LoginSchool>();

            //取得學校相關資料
            SchoolClubDic = Va_Ser.SchoolClubDetail(LoginSchoolList);

            //資料整合(以掛載模組為主)
            MergerClubDic = Va_Ser.ResourceMerger(SchoolClubDic);

            //取得志願
            foreach (string each in SchoolClubDic.Keys)
            {
                SchoolClubDic[each].RunMergerVolunteer();

                SchoolClubDic[each].RunCheckClass();
            }
        }

        void BGW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Text = "社團志願分配(跨部別)";
            if (!e.Cancelled)
            {
                if (e.Error == null)
                {
                    List<ClassRowInfo> RowList = new List<ClassRowInfo>();
                    foreach (string each in SchoolClubDic.Keys)
                    {
                        List<ClassRowInfo> list = new List<ClassRowInfo>();
                        list.AddRange(SchoolClubDic[each].infoDic.Values);
                        list.Sort(SortClassIndex);
                        RowList.AddRange(list);
                    }
                    dataGridViewX1.AutoGenerateColumns = false;
                    dataGridViewX1.DataSource = RowList;
                }
                else
                {
                    MsgBox.Show("背景作業發生錯誤!\n" + e.Error.Message);
                }
            }
            else
            {
                MsgBox.Show("連線學校發生錯誤!!\n請至[跨部別->連線]功能修正錯誤");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //開始志願分配作業

            //學生清單

            BackgroundWorker BGW_Run = new BackgroundWorker();
            BGW_Run.RunWorkerCompleted += BGW_Run_RunWorkerCompleted;
            BGW_Run.DoWork += BGW_Run_DoWork;
            this.Text = "社團志願分配(跨部別)　資料取得中...";

            List<ClassRowInfo> RowDataList = new List<ClassRowInfo>();
            foreach (DataGridViewRow row in dataGridViewX1.SelectedRows)
            {
                ClassRowInfo rowData = (ClassRowInfo)row.DataBoundItem;
                RowDataList.Add(rowData);
            }






            BGW_Run.RunWorkerAsync(RowDataList);
        }

        void BGW_Run_DoWork(object sender, DoWorkEventArgs e)
        {

            //資源整合社團 - MergerClubDic

            //目前要進行社團志願處理的班級 - RowDataList
            List<ClassRowInfo> RowDataList = (List<ClassRowInfo>)e.Argument;











        }

        void BGW_Run_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Text = "社團志願分配(跨部別)";
            if (!e.Cancelled)
            {
                if (e.Error == null)
                {
            





                }
                else
                {
                    MsgBox.Show("背景作業發生錯誤!\n" + e.Error.Message);
                }
            }
            else
            {
                MsgBox.Show("已停止志願分配!!!");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private int SortClassIndex(ClassRowInfo class1, ClassRowInfo class2)
        {
            string aaa1 = class1.GradeYear.PadLeft(2, '0');
            aaa1 += class1.DisplayOrder.PadLeft(3, '0');
            aaa1 += class1.ClassName.PadLeft(3, '0');

            string bbb1 = class2.GradeYear.PadLeft(2, '0');
            bbb1 += class2.DisplayOrder.PadLeft(3, '0');
            bbb1 += class2.ClassName.PadLeft(3, '0');

            return aaa1.CompareTo(bbb1);
        }

        private void dataGridViewX1_SelectionChanged(object sender, EventArgs e)
        {
            lbSelectClassCount.Text = "選擇班級：" + dataGridViewX1.SelectedRows.Count;
        }
    }
}
