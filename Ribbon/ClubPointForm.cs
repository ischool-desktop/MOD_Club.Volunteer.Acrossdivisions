﻿using FISCA.Presentation.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using FISCA.Presentation;
using K12.Club.Volunteer;
using FISCA.DSAClient;
using System.Xml.Linq;
using System.IO;
using System.Xml;
using FISCA.DSAUtil;
using K12.Data;
using Aspose.Words;

namespace MOD_Club_Acrossdivisions
{
    public partial class ClubPointForm : BaseForm
    {
        BackgroundWorker BGW { get; set; }

        /// <summary>
        /// 可連線學校
        /// </summary>
        List<LoginSchool> LoginSchoolList { get; set; }

        string ConfigName = "ClassPrint_Config";

        int 學生多少個 = 150;
        int 日期多少天 = 30;

        public ClubPointForm()
        {
            InitializeComponent();

            BGW = new BackgroundWorker();
            BGW.RunWorkerCompleted += BGW_RunWorkerCompleted;
            BGW.DoWork += BGW_DoWork;


            dateTimeInput1.Value = DateTime.Today;
            dateTimeInput2.Value = DateTime.Today.AddDays(7);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (K12.Club.Volunteer.ClubAdmin.Instance.SelectedSource.Count == 0)
            {
                MsgBox.Show("請選擇社團");
                return;
            }

            if (FISCA.Authentication.DSAServices.PassportToken == null)
            {
                MsgBox.Show("請使用ischool Account登入\n再使用跨部別功能!!");
                return;
            }

            if (!BGW.IsBusy)
            {
                this.Text = "社團點名單(跨部別)　資料處理中...";

                #region 日期設定

                if (dataGridViewX1.Rows.Count <= 0)
                {
                    MsgBox.Show("列印點名單必須有日期!!");
                    return;
                }

                DSXmlHelper dxXml = new DSXmlHelper("XmlData");

                foreach (DataGridViewRow row in dataGridViewX1.Rows)
                {
                    string 日期 = "" + row.Cells[0].Value;
                    dxXml.AddElement(".", "item", 日期);
                }

                #endregion

                btnStart.Enabled = false;
                BGW.RunWorkerAsync(dxXml.BaseElement);
            }
            else
            {
                MsgBox.Show("系統忙碌中...請稍後!!");
            }
        }

        void BGW_DoWork(object sender, DoWorkEventArgs e)
        {
            //開始列印學生清單


            #region 資料整理

            //取得需連線學校
            LoginSchoolList = tool._A.Select<LoginSchool>();

            //選擇了哪些社團(使用名稱進行比對)
            List<string> ClubIDList = K12.Club.Volunteer.ClubAdmin.Instance.SelectedSource;
            List<CLUBRecord> ClubRecordList = tool._A.Select<CLUBRecord>(ClubIDList);

            //社團名稱清單
            List<string> ClubNameList = new List<string>();
            foreach (CLUBRecord club in ClubRecordList)
            {
                ClubNameList.Add(club.ClubName);
            }

            //社團名稱 + 社團參與記錄學生
            Dictionary<string, List<OnlineSCJoin>> dic = new Dictionary<string, List<OnlineSCJoin>>();
            List<string> ClubNewNameList = new List<string>();
            foreach (LoginSchool school in LoginSchoolList)
            {
                Connection me = new Connection();
                me.Connect(school.School_Name, tool._contract, FISCA.Authentication.DSAServices.PassportToken);
                Dictionary<string, OnlineSCJoin> ScjList = RunService.GetSCJoinByClubName(me, ClubNameList);

                foreach (OnlineSCJoin each in ScjList.Values)
                {
                    string name = each.ClubName;
                    if (!dic.ContainsKey(name))
                    {
                        dic.Add(name, new List<OnlineSCJoin>());
                        ClubNewNameList.Add(name);
                    }
                    dic[name].Add(each);
                }
            }

            ClubNewNameList.Sort();

            #endregion

            #region 報表範本整理

            Campus.Report.ReportConfiguration ConfigurationInCadre = new Campus.Report.ReportConfiguration(ConfigName);
            Aspose.Words.Document Template;

            if (ConfigurationInCadre.Template == null)
            {
                //如果範本為空,則建立一個預設範本
                Campus.Report.ReportConfiguration ConfigurationInCadre_1 = new Campus.Report.ReportConfiguration(ConfigName);
                ConfigurationInCadre_1.Template = new Campus.Report.ReportTemplate(Properties.Resources.班級點名單_週報表樣式範本, Campus.Report.TemplateType.Word);
                Template = ConfigurationInCadre_1.Template.ToDocument();
            }
            else
            {
                //如果已有範本,則取得樣板
                Template = ConfigurationInCadre.Template.ToDocument();
            }

            #endregion

            #region 範本再修改

            List<string> config = new List<string>();

            XmlElement day = (XmlElement)e.Argument;

            if (day == null)
            {
                MsgBox.Show("第一次使用報表請先進行[日期設定]");
                return;
            }
            else
            {
                config.Clear();
                foreach (XmlElement xml in day.SelectNodes("item"))
                {
                    config.Add(xml.InnerText);
                }
            }

            DataTable table = new DataTable();
            table.Columns.Add("學校名稱");
            table.Columns.Add("社團名稱");
            table.Columns.Add("學年度");
            table.Columns.Add("學期");

            table.Columns.Add("社團老師");

            table.Columns.Add("列印日期");
            table.Columns.Add("上課開始");
            table.Columns.Add("上課結束");
            table.Columns.Add("人數");

            for (int x = 1; x <= 日期多少天; x++)
            {
                table.Columns.Add(string.Format("日期_{0}", x));
            }

            for (int x = 1; x <= 學生多少個; x++)
            {
                table.Columns.Add(string.Format("社團_{0}", x));
            }

            for (int x = 1; x <= 學生多少個; x++)
            {
                table.Columns.Add(string.Format("班級_{0}", x));
            }

            for (int x = 1; x <= 學生多少個; x++)
            {
                table.Columns.Add(string.Format("座號_{0}", x));
            }

            for (int x = 1; x <= 學生多少個; x++)
            {
                table.Columns.Add(string.Format("姓名_{0}", x));
            }

            for (int x = 1; x <= 學生多少個; x++)
            {
                table.Columns.Add(string.Format("學號_{0}", x));
            }

            for (int x = 1; x <= 學生多少個; x++)
            {
                table.Columns.Add(string.Format("性別_{0}", x));
            }

            #endregion

            foreach (string each in ClubNewNameList)
            {
                DataRow row = table.NewRow();

                row["學校名稱"] = K12.Data.School.ChineseName;
                row["學年度"] = School.DefaultSchoolYear;
                row["學期"] = School.DefaultSemester;
                row["列印日期"] = DateTime.Today.ToShortDateString();
                row["上課開始"] = config[0];
                row["上課結束"] = config[config.Count - 1];
                row["社團名稱"] = each;

                //特殊
                if (dic[each].Count > 0)
                    row["社團老師"] = dic[each][0].TeacherName;

                for (int x = 1; x <= config.Count; x++)
                {
                    row[string.Format("日期_{0}", x)] = config[x - 1];
                }

                dic[each].Sort(SortStudentSCJoin);
                int y = 1;
                foreach (OnlineSCJoin scjoin in dic[each])
                {
                    if (y <= 學生多少個)
                    {
                        row[string.Format("班級_{0}", y)] = scjoin.ClassName;
                        row[string.Format("座號_{0}", y)] = scjoin.SeatNo;
                        row[string.Format("姓名_{0}", y)] = scjoin.StudentName;
                        row[string.Format("學號_{0}", y)] = scjoin.StudentNumber;
                        row[string.Format("性別_{0}", y)] = scjoin.Gender;
                        y++;
                    }
                }

                row["人數"] = y - 1;
                table.Rows.Add(row);
            }

            Document PageOne = (Document)Template.Clone(true);
            PageOne.MailMerge.Execute(table);
            e.Result = PageOne;

        }

        private int SortStudentSCJoin(OnlineSCJoin sj1, OnlineSCJoin sj2)
        {
            string student1 = sj1.ClassName.PadLeft(5, '0');
            student1 += sj1.SeatNo.PadLeft(3, '0');

            string student2 = sj2.ClassName.PadLeft(5, '0');            
            student2 += sj2.SeatNo.PadLeft(3, '0');

            return student1.CompareTo(student2);

        }

        void BGW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Text = "社團點名單(跨部別)";
            btnStart.Enabled = true;
            if (!e.Cancelled)
            {
                if (e.Error == null)
                {
                    SaveFileDialog SaveFileDialog1 = new SaveFileDialog();
                    SaveFileDialog1.Filter = "Word (*.doc)|*.doc|所有檔案 (*.*)|*.*";
                    SaveFileDialog1.FileName = "社團點名單(跨部別)";

                    //資料
                    try
                    {
                        if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            Aspose.Words.Document inResult = (Aspose.Words.Document)e.Result;

                            inResult.Save(SaveFileDialog1.FileName);
                            Process.Start(SaveFileDialog1.FileName);
                            MotherForm.SetStatusBarMessage("社團點名單(跨部別),列印完成!!");
                        }
                        else
                        {
                            FISCA.Presentation.Controls.MsgBox.Show("檔案未儲存");
                            return;
                        }
                    }
                    catch
                    {
                        FISCA.Presentation.Controls.MsgBox.Show("檔案儲存錯誤,請檢查檔案是否開啟中!!");
                        MotherForm.SetStatusBarMessage("檔案儲存錯誤,請檢查檔案是否開啟中!!");
                    }
                }
                else
                {
                    if (e.Error.Message == "並未將物件參考設定為物件的執行個體。")
                    {
                        MsgBox.Show("列印發生錯誤..\n失敗：登入帳號不是ischool Account");
                    }
                    else
                    {
                        MsgBox.Show("列印發生錯誤..\n" + e.Error.Message);
                    }
                }
            }
            else
            {
                MsgBox.Show("作業已取消");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lbDefTemp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //取得設定檔
            Campus.Report.ReportConfiguration ConfigurationInCadre = new Campus.Report.ReportConfiguration(ConfigName);
            Campus.Report.TemplateSettingForm TemplateForm;
            //畫面內容(範本內容,預設樣式
            if (ConfigurationInCadre.Template != null)
            {
                TemplateForm = new Campus.Report.TemplateSettingForm(ConfigurationInCadre.Template, new Campus.Report.ReportTemplate(Properties.Resources.班級點名單_週報表樣式範本, Campus.Report.TemplateType.Word));
            }
            else
            {
                ConfigurationInCadre.Template = new Campus.Report.ReportTemplate(Properties.Resources.班級點名單_週報表樣式範本, Campus.Report.TemplateType.Word);
                TemplateForm = new Campus.Report.TemplateSettingForm(ConfigurationInCadre.Template, new Campus.Report.ReportTemplate(Properties.Resources.班級點名單_週報表樣式範本, Campus.Report.TemplateType.Word));
            }

            //預設名稱
            TemplateForm.DefaultFileName = "班級點名單_週報表樣式範本";

            //如果回傳為OK
            if (TemplateForm.ShowDialog() == DialogResult.OK)
            {
                //設定後樣試,回傳
                ConfigurationInCadre.Template = TemplateForm.Template;
                //儲存
                ConfigurationInCadre.Save();
            }
        }

        private void lbTempAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "另存新檔";
            sfd.FileName = "班級點名表_合併欄位總表.doc";
            sfd.Filter = "Word檔案 (*.doc)|*.doc|所有檔案 (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                    fs.Write(Properties.Resources.班級點名表_合併欄位總表, 0, Properties.Resources.班級點名表_合併欄位總表.Length);
                    fs.Close();
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch
                {
                    FISCA.Presentation.Controls.MsgBox.Show("指定路徑無法存取。", "另存檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void GetDateTime_Click(object sender, EventArgs e)
        {
            //建立日期清單
            TimeSpan ts = dateTimeInput2.Value - dateTimeInput1.Value;

            List<DateTime> TList = new List<DateTime>();

            foreach (DataGridViewRow row in dataGridViewX1.Rows)
            {
                DateTime dt;
                DateTime.TryParse("" + row.Cells[0].Value, out dt);
                TList.Add(dt);
            }

            List<DayOfWeek> WeekList = new List<DayOfWeek>();
            if (cbDay1.Checked)
                WeekList.Add(DayOfWeek.Monday);
            if (cbDay2.Checked)
                WeekList.Add(DayOfWeek.Tuesday);
            if (cbDay3.Checked)
                WeekList.Add(DayOfWeek.Wednesday);
            if (cbDay4.Checked)
                WeekList.Add(DayOfWeek.Thursday);
            if (cbDay5.Checked)
                WeekList.Add(DayOfWeek.Friday);
            if (cbDay6.Checked)
                WeekList.Add(DayOfWeek.Saturday);
            if (cbDay7.Checked)
                WeekList.Add(DayOfWeek.Sunday);
            for (int x = 0; x <= ts.Days; x++)
            {
                DateTime dt = dateTimeInput1.Value.AddDays(x);

                if (WeekList.Contains(dt.DayOfWeek))
                {
                    if (!TList.Contains(dt))
                    {
                        TList.Add(dt);
                    }
                }
            }

            TList.Sort();
            //資料填入
            dataGridViewX1.Rows.Clear();
            foreach (DateTime dt in TList)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridViewX1);
                row.Cells[0].Value = dt.ToShortDateString();
                row.Cells[1].Value = tool.CheckWeek(dt.DayOfWeek.ToString());
                dataGridViewX1.Rows.Add(row);
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dataGridViewX1.Rows.Clear();
        }
    }
}
