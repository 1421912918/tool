using System;
using System.Windows.Forms;

namespace HIP_TOOL
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            if (args.Length > 0)
            {
                string canshu1 = args[0];
                string canshu2 = args[1];
                MessageBox.Show("账户：" + canshu1 + "   密码:" + canshu2);  //参数
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
