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
    public partial class TestForm : Form
    {
        public delegate void InvokeDelegate();
        string testInstanceName = "";
        public TestForm(string testInstanceName)
        {
            this.testInstanceName = testInstanceName;
            InitializeComponent();
        }

        ~TestForm()
        {
            Global.TcpClient.NewMessageEvent -= TcpClient_NewMessageEvent;
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            this.Text = "测试-" + testInstanceName;
            this.lb_NowTestCauseName.Text = "当前测试用例:尚未开始";
            this.lb_TestCauseCount.Text = "共计测试数量:尚未开始";
            this.lb_TestPlatformCount.Text = "需测试平台:尚未开始";
            //注册消息事件
            Global.TcpClient.NewMessageEvent += TcpClient_NewMessageEvent;
        }

        void TcpClient_NewMessageEvent(object e, NewMessageArgs args)
        {
            this.Invoke(new InvokeDelegate(() =>
            {
                switch (args.Type)
                {
                    case MessageType.AssertError: SetTestResultContent(args); break;
                    case MessageType.AssertFailed: SetTestResultContent(args); break;
                    case MessageType.AssertSuccess: SetTestResultContent(args); break;
                    case MessageType.Event: EventMessageProcess(args); break;
                    case MessageType.Debug: LogManger(args); break;
                    case MessageType.Info: LogManger(args); break;
                    case MessageType.Error: LogManger(args); break;
                    case MessageType.Warning: LogManger(args); break;
                }
            }));
        }

        private void btn_Run_Click(object sender, EventArgs e)
        {
            var loadForm = LoadingForm.GetLoad();
            //清除过去的内容
            lv_TestResult.Items.Clear();
            //打开停止测试开关
            btn_Stop.Enabled = true;
            Global.ClientManger.Run(testInstanceName,(bool status) => {
                this.Invoke(new InvokeDelegate(() =>
                {
                    if (status == true)
                    {
                        this.lb_NowTestCauseName.Text = "当前测试用例:等待中";
                        this.lb_TestCauseCount.Text = "共计测试数量:等待中";
                        this.lb_TestCauseCount.Text = "需测试平台:等待中";
                        this.btn_Run.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("无法开始测试,测试服务器目前可能在占用状态", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    loadForm.Close();
                }));
            });
            loadForm.Show();
        }

        private void TestForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //TODO:当测试进行中的关闭事件
        }

        public void SetTestResultContent(NewMessageArgs args)
        {
            //设置测试结果框中的内容
            if(args.Type == MessageType.AssertError)
            {
                lv_TestResult.Items.Add(new ListViewItem(new string[] { args.MessageArgs[0], args.MessageArgs[1] , "系统错误" }));
                lv_TestResult.Items[lv_TestResult.Items.Count - 1].ForeColor = Color.Orange;
            }
            else if(args.Type == MessageType.AssertSuccess)
            {
                lv_TestResult.Items.Add(new ListViewItem(new string[] { args.MessageArgs[0], args.MessageArgs[1] , "成功","","",args.MessageArgs[2] }));
                lv_TestResult.Items[lv_TestResult.Items.Count - 1].ForeColor = Color.LightGreen;
            }
            else if (args.Type == MessageType.AssertFailed)
            {
                lv_TestResult.Items.Add(new ListViewItem(new string[] { args.MessageArgs[0], args.MessageArgs[1], "失败", args.MessageArgs.Length > 3 ? args.MessageArgs[3] : "", args.MessageArgs.Length > 3 ? args.MessageArgs[4] : "", args.MessageArgs[2] }));
                lv_TestResult.Items[lv_TestResult.Items.Count - 1].ForeColor = Color.Red;
            }
        }

        public void EventMessageProcess(NewMessageArgs args)
        {
            switch(args.EventCommand)
            {
                case "SetTestCauseCount": lb_TestCauseCount.Text = "共计测试数量:" + args.MessageArgs[0]; break;
                case "SetPlatformCount": lb_TestPlatformCount.Text = "需测试平台:" + args.MessageArgs[0]; break;
                case "SetRunningTestCauseName": lb_NowTestCauseName.Text = "当前测试用例:" + args.MessageArgs[0]; break;
                case "TestComplete": btn_Run.Enabled = true; btn_Stop.Enabled = false ; break;
            }
        }

        private void LogManger(NewMessageArgs args)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[" + DateTime.Now.ToString());
            sb.Append(" " + args.Type.ToString());
            sb.Append("]:");
            if(args.MessageArgs == null)
            {
                sb.Append(args.Msg);
            }
            else
            {
                sb.Append(args.MessageArgs[0]);
            }
            tb_Log.AppendText(sb.ToString() + "\r\n");
        }

        private void TestForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Global.TcpClient.NewMessageEvent -= TcpClient_NewMessageEvent;
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            this.LogManger(new NewMessageArgs()
            {
                Type = MessageType.Info,
                Msg = "正在停止测试"
            });
            btn_Stop.Enabled = false;
            Global.ClientManger.Stop((bool status) =>
            {
                this.Invoke(new InvokeDelegate(()=>
                {
                    
                    if (status)
                    {
                        this.LogManger(new NewMessageArgs()
                        {
                            Type = MessageType.Info,
                            Msg = "测试已经开始停止"
                        });
                    }
                    else
                    {
                        this.LogManger(new NewMessageArgs()
                        {
                            Type = MessageType.Warning,
                            Msg = "测试停止失败,可能是测试已经停止"
                        });
                        btn_Stop.Enabled = true;
                    }
                }));
            });
        }
    }
}
