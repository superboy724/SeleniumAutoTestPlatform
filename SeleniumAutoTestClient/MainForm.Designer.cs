﻿namespace SeleniumAutoTestClient
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.系统ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Setting = new System.Windows.Forms.ToolStripMenuItem();
            this.lv_TestInstances = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_StartTest = new System.Windows.Forms.Button();
            this.btn_DeleteTest = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(375, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 系统ToolStripMenuItem
            // 
            this.系统ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_Setting});
            this.系统ToolStripMenuItem.Name = "系统ToolStripMenuItem";
            this.系统ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.系统ToolStripMenuItem.Text = "系统";
            // 
            // tsmi_Setting
            // 
            this.tsmi_Setting.Name = "tsmi_Setting";
            this.tsmi_Setting.Size = new System.Drawing.Size(100, 22);
            this.tsmi_Setting.Text = "配置";
            // 
            // lv_TestInstances
            // 
            this.lv_TestInstances.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lv_TestInstances.Location = new System.Drawing.Point(12, 29);
            this.lv_TestInstances.Name = "lv_TestInstances";
            this.lv_TestInstances.Size = new System.Drawing.Size(239, 148);
            this.lv_TestInstances.TabIndex = 1;
            this.lv_TestInstances.UseCompatibleStateImageBehavior = false;
            this.lv_TestInstances.View = System.Windows.Forms.View.Details;
            this.lv_TestInstances.DoubleClick += new System.EventHandler(this.lv_TestInstances_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "实例名称";
            this.columnHeader1.Width = 224;
            // 
            // btn_StartTest
            // 
            this.btn_StartTest.Location = new System.Drawing.Point(258, 29);
            this.btn_StartTest.Name = "btn_StartTest";
            this.btn_StartTest.Size = new System.Drawing.Size(105, 33);
            this.btn_StartTest.TabIndex = 2;
            this.btn_StartTest.Text = "执行选中的测试";
            this.btn_StartTest.UseVisualStyleBackColor = true;
            // 
            // btn_DeleteTest
            // 
            this.btn_DeleteTest.Location = new System.Drawing.Point(257, 68);
            this.btn_DeleteTest.Name = "btn_DeleteTest";
            this.btn_DeleteTest.Size = new System.Drawing.Size(105, 33);
            this.btn_DeleteTest.TabIndex = 3;
            this.btn_DeleteTest.Text = "删除测试";
            this.btn_DeleteTest.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 189);
            this.Controls.Add(this.btn_DeleteTest);
            this.Controls.Add(this.btn_StartTest);
            this.Controls.Add(this.lv_TestInstances);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 系统ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Setting;
        private System.Windows.Forms.ListView lv_TestInstances;
        private System.Windows.Forms.Button btn_StartTest;
        private System.Windows.Forms.Button btn_DeleteTest;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}

