using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SeleniumAutoTestClient
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Global.ClientManger = new Client();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
            Global.ClientManger.Disconnect();
        }
    }
}
