using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SeleniumAutoTestClient
{
    public partial class MainForm : Form
    {
        public delegate void InvokeDelegate();
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if(Global.ClientManger.Init())
            {
                var loadForm = LoadingForm.GetLoad();
                Global.ClientManger.GetInstance(() => {
                    this.Invoke(new InvokeDelegate(() =>
                    {
                        //当获取到数据
                        Global.ClientManger.TestInstances.ForEach(s =>
                        {
                            this.lv_TestInstances.Items.Add(new ListViewItem(new string[] { s }));
                        });
                        loadForm.Close();
                    }));
                });
                loadForm.ShowDialog();
            }
            else
            {
                //TODO:配置项为空的处理
            }
        }

        private void lv_TestInstances_DoubleClick(object sender, EventArgs e)
        {
            string testInstanceName = lv_TestInstances.SelectedItems[0].SubItems[0].Text;
            StartTest(testInstanceName);
        }

        private void StartTest(string testInstanceName)
        {
            TestForm testForm = new TestForm(testInstanceName);
            testForm.ShowDialog();
        }
    }
}
