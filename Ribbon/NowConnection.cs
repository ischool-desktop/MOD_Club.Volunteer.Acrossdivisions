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
using System.Xml.Linq;

namespace MOD_Club_Acrossdivisions
{
    public partial class NowConnection : BaseForm
    {

        Campus.Configuration.ConfigData cd { get; set; }
        public NowConnection()
        {
            InitializeComponent();
        }

        private void Connection_Load(object sender, EventArgs e)
        {
            List<LoginSchool> LoginSchoolList = tool._A.Select<LoginSchool>();

            if (LoginSchoolList.Count() > 0)
            {
                foreach (LoginSchool each in LoginSchoolList)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dataGridViewX1);
                    row.Cells[0].Value = each.School_Name;
                    row.Cells[1].Value = "click me";
                    dataGridViewX1.Rows.Add(row);

                    //檢查
                    Exception ex = Ser.TestConnection(each.School_Name);
                    if (ex != null)
                        row.Cells[2].Value = ex.Message;
                    else
                        row.Cells[2].Value = "成功";
                }
            }
        }

        private void Connet(DataGridViewRow row)
        {
            LastObj _app = new LastObj();
            _app.row = row;
            _app.row.Cells[2].Value = "連線中,請稍後...";

            BackgroundWorker BGW = new BackgroundWorker();
            BGW.DoWork += BGW_DoWork;
            BGW.RunWorkerCompleted += BGW_RunWorkerCompleted;
            BGW.RunWorkerAsync(_app);
        }

        void BGW_DoWork(object sender, DoWorkEventArgs e)
        {
            LastObj _app = (LastObj)e.Argument;
            _app.message = Ser.CheckAccount("" + _app.row.Cells[0].Value);
            e.Result = _app;
        }

        void BGW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LastObj _app = (LastObj)e.Result;
            if (!e.Cancelled)
            {
                if (e.Error == null)
                {
                    _app.row.Cells[2].Value = _app.message;
                }
                else
                {
                    _app.row.Cells[2].Value = "失敗：其它：" + e.Error.Message;
                }
            }
            else
            {
                _app.row.Cells[2].Value = "失敗：其它：已取消連線";
            }
        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewX1.CurrentRow.IsNewRow)
                return;

            //當使用點擊位置並非錯誤欄位
            if (e.ColumnIndex == 1)
            {
                DataGridViewRow row = dataGridViewX1.CurrentRow;
                Connet(row);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewX1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (e.Row.Index != -1)
            {
                DataGridViewRow row = dataGridViewX1.Rows[e.Row.Index - 1];
                row.Cells[1].Value = "click me";
                row.Cells[2].Value = "未連線";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<LoginSchool> delList = tool._A.Select<LoginSchool>();
            tool._A.DeletedValues(delList);

            List<LoginSchool> LoginSchoolList = new List<LoginSchool>();
            foreach (DataGridViewRow row in dataGridViewX1.Rows)
            {
                if (row.IsNewRow)
                    continue;

                if ("" + row.Cells[2].Value == "成功")
                {
                    LoginSchool ls = new LoginSchool();
                    ls.School_Name = "" + row.Cells[0].Value;
                    LoginSchoolList.Add(ls);
                }
            }

            if (LoginSchoolList.Count > 0)
            {
                tool._A.InsertValues(LoginSchoolList);
            }
            MsgBox.Show("儲存成功");
            this.Close();
        }
    }
    class LastObj
    {
        public DataGridViewRow row { get; set; }
        public string message = "";
    }
}
