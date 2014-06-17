namespace MOD_Club_Acrossdivisions
{
    partial class VolunteerAssignment
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.dataGridViewX1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.lbHelpS = new DevComponents.DotNetBar.LabelX();
            this.lbSelectClassCount = new DevComponents.DotNetBar.LabelX();
            this.lbHelpConSchool = new DevComponents.DotNetBar.LabelX();
            this.colSchool = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colClass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTeacher = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStudentCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNowUp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCLUBRecord = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Location = new System.Drawing.Point(723, 537);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "開始分配";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(804, 537);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 25);
            this.btnExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "離開";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // dataGridViewX1
            // 
            this.dataGridViewX1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewX1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewX1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewX1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSchool,
            this.colYear,
            this.colClass,
            this.colTeacher,
            this.colStudentCount,
            this.colNowUp,
            this.colCLUBRecord,
            this.colLock});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewX1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewX1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridViewX1.Location = new System.Drawing.Point(13, 40);
            this.dataGridViewX1.Name = "dataGridViewX1";
            this.dataGridViewX1.RowTemplate.Height = 24;
            this.dataGridViewX1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewX1.Size = new System.Drawing.Size(866, 485);
            this.dataGridViewX1.TabIndex = 2;
            this.dataGridViewX1.SelectionChanged += new System.EventHandler(this.dataGridViewX1_SelectionChanged);
            // 
            // lbHelpS
            // 
            this.lbHelpS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbHelpS.AutoSize = true;
            this.lbHelpS.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lbHelpS.BackgroundStyle.Class = "";
            this.lbHelpS.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbHelpS.Location = new System.Drawing.Point(13, 531);
            this.lbHelpS.Name = "lbHelpS";
            this.lbHelpS.Size = new System.Drawing.Size(276, 39);
            this.lbHelpS.TabIndex = 4;
            this.lbHelpS.Text = "說明：(*)滑鼠右鍵可檢視學生選填明細與狀況\r\n　　　(*)分配社團志願請選擇班級後開始分配";
            // 
            // lbSelectClassCount
            // 
            this.lbSelectClassCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSelectClassCount.AutoSize = true;
            this.lbSelectClassCount.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lbSelectClassCount.BackgroundStyle.Class = "";
            this.lbSelectClassCount.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbSelectClassCount.Location = new System.Drawing.Point(602, 539);
            this.lbSelectClassCount.Name = "lbSelectClassCount";
            this.lbSelectClassCount.Size = new System.Drawing.Size(82, 21);
            this.lbSelectClassCount.TabIndex = 5;
            this.lbSelectClassCount.Text = "選擇班級：0";
            // 
            // lbHelpConSchool
            // 
            this.lbHelpConSchool.AutoSize = true;
            this.lbHelpConSchool.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lbHelpConSchool.BackgroundStyle.Class = "";
            this.lbHelpConSchool.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbHelpConSchool.Location = new System.Drawing.Point(13, 11);
            this.lbHelpConSchool.Name = "lbHelpConSchool";
            this.lbHelpConSchool.Size = new System.Drawing.Size(77, 21);
            this.lbHelpConSchool.TabIndex = 6;
            this.lbHelpConSchool.Text = "已連線學校:";
            // 
            // colSchool
            // 
            this.colSchool.DataPropertyName = "School";
            this.colSchool.HeaderText = "學校";
            this.colSchool.Name = "colSchool";
            this.colSchool.Width = 150;
            // 
            // colYear
            // 
            this.colYear.DataPropertyName = "GradeYear";
            this.colYear.HeaderText = "年級";
            this.colYear.Name = "colYear";
            this.colYear.Width = 60;
            // 
            // colClass
            // 
            this.colClass.DataPropertyName = "ClassName";
            this.colClass.HeaderText = "班級";
            this.colClass.Name = "colClass";
            // 
            // colTeacher
            // 
            this.colTeacher.DataPropertyName = "TeacherName";
            this.colTeacher.HeaderText = "老師";
            this.colTeacher.Name = "colTeacher";
            this.colTeacher.Width = 80;
            // 
            // colStudentCount
            // 
            this.colStudentCount.DataPropertyName = "StudentCount";
            this.colStudentCount.HeaderText = "學生數";
            this.colStudentCount.Name = "colStudentCount";
            this.colStudentCount.Width = 80;
            // 
            // colNowUp
            // 
            this.colNowUp.DataPropertyName = "SelectCount";
            this.colNowUp.HeaderText = "已填志願人數";
            this.colNowUp.Name = "colNowUp";
            this.colNowUp.Width = 110;
            // 
            // colCLUBRecord
            // 
            this.colCLUBRecord.DataPropertyName = "NumberOfParticipants";
            this.colCLUBRecord.HeaderText = "參與社團人數";
            this.colCLUBRecord.Name = "colCLUBRecord";
            this.colCLUBRecord.Width = 110;
            // 
            // colLock
            // 
            this.colLock.DataPropertyName = "LockNumber";
            this.colLock.HeaderText = "社團鎖定人數";
            this.colLock.Name = "colLock";
            this.colLock.Width = 110;
            // 
            // VolunteerAssignment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 570);
            this.Controls.Add(this.lbHelpConSchool);
            this.Controls.Add(this.lbSelectClassCount);
            this.Controls.Add(this.lbHelpS);
            this.Controls.Add(this.dataGridViewX1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSave);
            this.DoubleBuffered = true;
            this.Name = "VolunteerAssignment";
            this.Text = "社團志願分配(跨部別)";
            this.Load += new System.EventHandler(this.VolunteerAssignment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;
        private DevComponents.DotNetBar.LabelX lbHelpS;
        private DevComponents.DotNetBar.LabelX lbSelectClassCount;
        private DevComponents.DotNetBar.LabelX lbHelpConSchool;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSchool;
        private System.Windows.Forms.DataGridViewTextBoxColumn colYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClass;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTeacher;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStudentCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNowUp;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCLUBRecord;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLock;
    }
}