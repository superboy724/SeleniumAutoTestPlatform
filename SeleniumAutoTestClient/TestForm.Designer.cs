namespace SeleniumAutoTestClient
{
    partial class TestForm
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
            this.lv_TestResult = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tb_Log = new System.Windows.Forms.TextBox();
            this.btn_Run = new System.Windows.Forms.Button();
            this.lb_TestCauseCount = new System.Windows.Forms.Label();
            this.lb_TestPlatformCount = new System.Windows.Forms.Label();
            this.lb_NowTestCauseName = new System.Windows.Forms.Label();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lv_TestResult
            // 
            this.lv_TestResult.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.lv_TestResult.Location = new System.Drawing.Point(13, 41);
            this.lv_TestResult.Name = "lv_TestResult";
            this.lv_TestResult.Size = new System.Drawing.Size(833, 304);
            this.lv_TestResult.TabIndex = 0;
            this.lv_TestResult.UseCompatibleStateImageBehavior = false;
            this.lv_TestResult.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "用例名称";
            this.columnHeader1.Width = 151;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "测试名称";
            this.columnHeader2.Width = 144;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "状态";
            this.columnHeader3.Width = 76;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "正确值";
            this.columnHeader4.Width = 143;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "错误值";
            this.columnHeader5.Width = 179;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "耗时";
            this.columnHeader6.Width = 125;
            // 
            // tb_Log
            // 
            this.tb_Log.Location = new System.Drawing.Point(13, 352);
            this.tb_Log.Multiline = true;
            this.tb_Log.Name = "tb_Log";
            this.tb_Log.Size = new System.Drawing.Size(833, 91);
            this.tb_Log.TabIndex = 1;
            // 
            // btn_Run
            // 
            this.btn_Run.Location = new System.Drawing.Point(852, 41);
            this.btn_Run.Name = "btn_Run";
            this.btn_Run.Size = new System.Drawing.Size(117, 61);
            this.btn_Run.TabIndex = 2;
            this.btn_Run.Text = "执行当前测试";
            this.btn_Run.UseVisualStyleBackColor = true;
            this.btn_Run.Click += new System.EventHandler(this.btn_Run_Click);
            // 
            // lb_TestCauseCount
            // 
            this.lb_TestCauseCount.AutoSize = true;
            this.lb_TestCauseCount.Location = new System.Drawing.Point(13, 13);
            this.lb_TestCauseCount.Name = "lb_TestCauseCount";
            this.lb_TestCauseCount.Size = new System.Drawing.Size(107, 12);
            this.lb_TestCauseCount.TabIndex = 4;
            this.lb_TestCauseCount.Text = "lb_TestCauseCount";
            // 
            // lb_TestPlatformCount
            // 
            this.lb_TestPlatformCount.AutoSize = true;
            this.lb_TestPlatformCount.Location = new System.Drawing.Point(161, 13);
            this.lb_TestPlatformCount.Name = "lb_TestPlatformCount";
            this.lb_TestPlatformCount.Size = new System.Drawing.Size(125, 12);
            this.lb_TestPlatformCount.TabIndex = 5;
            this.lb_TestPlatformCount.Text = "lb_TestPlatformCount";
            // 
            // lb_NowTestCauseName
            // 
            this.lb_NowTestCauseName.AutoSize = true;
            this.lb_NowTestCauseName.Location = new System.Drawing.Point(315, 13);
            this.lb_NowTestCauseName.Name = "lb_NowTestCauseName";
            this.lb_NowTestCauseName.Size = new System.Drawing.Size(119, 12);
            this.lb_NowTestCauseName.TabIndex = 6;
            this.lb_NowTestCauseName.Text = "lb_NowTestCauseName";
            // 
            // btn_Stop
            // 
            this.btn_Stop.Location = new System.Drawing.Point(852, 108);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(117, 61);
            this.btn_Stop.TabIndex = 7;
            this.btn_Stop.Text = "停止当前测试";
            this.btn_Stop.UseVisualStyleBackColor = true;
            this.btn_Stop.Click += new System.EventHandler(this.btn_Stop_Click);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 455);
            this.Controls.Add(this.btn_Stop);
            this.Controls.Add(this.lb_NowTestCauseName);
            this.Controls.Add(this.lb_TestPlatformCount);
            this.Controls.Add(this.lb_TestCauseCount);
            this.Controls.Add(this.btn_Run);
            this.Controls.Add(this.tb_Log);
            this.Controls.Add(this.lv_TestResult);
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TestForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TestForm_FormClosed);
            this.Load += new System.EventHandler(this.TestForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lv_TestResult;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.TextBox tb_Log;
        private System.Windows.Forms.Button btn_Run;
        private System.Windows.Forms.Label lb_TestCauseCount;
        private System.Windows.Forms.Label lb_TestPlatformCount;
        private System.Windows.Forms.Label lb_NowTestCauseName;
        private System.Windows.Forms.Button btn_Stop;
    }
}