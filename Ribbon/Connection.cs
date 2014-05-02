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

namespace MOD_Club_Acrossdivisions
{
    public partial class Connection : BaseForm
    {
        public Connection()
        {
            InitializeComponent();
        }

        private void Connection_Load(object sender, EventArgs e)
        {
            //讀取已連限設定內容

            //嘗試依據使用者曾經連線內容再次連限







            #region 範例用
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(dataGridViewX1);

            row.Cells[0].Value = "dev.sh_d";
            row.Cells[1].Value = "click me";
            row.Cells[2].Value = "成功";
            dataGridViewX1.Rows.Add(row);

            row = new DataGridViewRow();
            row.CreateCells(dataGridViewX1);

            row.Cells[0].Value = "demo.ischool.j";
            row.Cells[1].Value = "click me";
            row.Cells[2].Value = "失敗,您的帳號不存在";
            dataGridViewX1.Rows.Add(row); 
            #endregion

        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewX1.CurrentRow.IsNewRow)
                return;

            //當使用點擊位置並非錯誤欄位
            if (e.ColumnIndex != -1 && e.RowIndex != -1)
            {


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
    }
}
