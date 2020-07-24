using System;
using System.Windows.Forms;

namespace HIP_TOOL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void 数据库配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private void 配置数据库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            datebase_setting ss = new datebase_setting();
            ss.Show();
        }

        private void 功能集合ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 查锁表解锁ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lock_table ss = new Lock_table();
            ss.Show();
        }

        private void 表空间查扩改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Table_space_expansion ss = new Table_space_expansion();
            ss.Show();
        }

        private void 建同义词ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Synonym ss = new Synonym();
            ss.Show();
        }

        private void mERGE语句生成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MERGE ss = new MERGE();
            ss.Show();
        }
        private static System.Diagnostics.Process p;
        private void xmlhl7V3解析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xml_hl7v3_Analysis ss = new xml_hl7v3_Analysis();
            ss.Show();

        }

        private void ipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (p == null)
                {
                    p = new System.Diagnostics.Process();
                    p.StartInfo.FileName = "PingABC.exe";
                    p.Start();
                }
                else
                {
                    if (p.HasExited) //是否正在运行
                    {
                        p.Start();
                    }
                }
                p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }

        private void webService消息测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            web_S_Send ss = new web_S_Send();
            ss.Show();
        }

        private void in生成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IN_生成 ss = new IN_生成();
            ss.Show();
        }

        private void devToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XtraForm1 ss = new XtraForm1();
            ss.Show();
        }

        private void 正则字符替换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Regular ss = new Regular();
            ss.Show();
        }

        private void 查询接口模板生成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mirth_Select_Code ss = new Mirth_Select_Code();
            ss.Show();
        }
    }
}
