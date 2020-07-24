using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HIP_TOOL
{
    public partial class Regular : Form
    {
        public Regular()
        {
            InitializeComponent();
        }
        Boolean textbox1HasText = false;//判断输入框是否有文本
        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "在此输入待匹配文本";
                textBox1.ForeColor = Color.LightGray;
                textbox1HasText = false;
            }
            else
                textbox1HasText = true;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textbox1HasText == false)
                textBox1.Text = "";
            textBox1.ForeColor = Color.Black;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox4.Clear();
            string input = textBox1.Text;
            string pattern = textBox2.Text;
            string result = "";
            MatchCollection match = Regular_1(input, pattern);
            foreach (Match match1 in match)
            {
                result += match1.Value + "\r\n";
            }
            textBox4.Text = result;

        }
        public string Replace_1(string input, string pattern, string replacement)
        {
            Regex re;
            if (checkBox1.Checked)
            {
                re = new Regex(pattern, RegexOptions.IgnoreCase);//查找忽略大小写
            }
            else
            {
                re = new Regex(pattern);//大小写正常
            }
            string result = re.Replace(input, replacement);
            return result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox5.Clear();
            string input = textBox1.Text;
            string pattern = textBox2.Text;
            string replacement = textBox3.Text;
            string result = Replace_1(input, pattern, replacement);
            textBox5.Text = result;
        }


        private MatchCollection Regular_1(string input, string pattern)
        {
            Regex re;
            if (checkBox1.Checked)
            {
                re = new Regex(pattern, RegexOptions.IgnoreCase);
            }
            else
            {
                re = new Regex(pattern);
            }
            MatchCollection match = re.Matches(input);
            return match;
        }

        private void 示例演示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.runoob.com/regexp/regexp-example.html");

        }

        private void 手册ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://tool.oschina.net/uploads/apidocs/jquery/regexp.html");

        }
    }
}
