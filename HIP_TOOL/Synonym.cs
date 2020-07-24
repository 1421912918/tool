using System;
using System.Windows.Forms;

namespace HIP_TOOL
{
    public partial class Synonym : Form
    {
        public Synonym()
        {
            InitializeComponent();
        }
        oracle_link ss = new oracle_link();
        private void button1_Click(object sender, EventArgs e)  //查询所有用户
        {
            string sql = " select username from dba_users ";
            ss.Open();
            var ord = ss.Oracle(sql);
            int a = 0;
            while (ord.Read())
            {
                // if (a == 0)
                //  { comboBox1.Text = ord.GetString(0); }
                // comboBox1.Text = "请选择用户";//给一个常用的
                comboBox1.Items.Add(ord.GetString(0));
                a++;
            }
            ss.Close();
            MessageBox.Show("1");
        }

        private void comboBox1_TextChanged(object sender, EventArgs e) //改变用户后，自动刷新用户下所有的表
        {

            comboBox2.Items.Clear();

            string sql = " select table_name from all_tables where owner = '" + comboBox1.Text.ToUpper() + "'";
            ss.Open();
            var ord = ss.Oracle(sql);
            int a = 0;
            while (ord.Read())
            {
                if (a == 0) { comboBox2.Text = ord.GetString(0); }
                comboBox2.Items.Add(ord.GetString(0));
                a++;
            }
            ss.Close();

        }

        private void button2_Click(object sender, EventArgs e)  //建立一个同义词
        {
            string sql = " create SYNONYM " + comboBox2.Text + " for " + comboBox1.Text + "." + comboBox2.Text;
            try
            {
                ss.Open();
                ss.Oracle(sql);
                ss.Close();
                MessageBox.Show("同义词建立成功");
            }
            catch (Exception ee)
            {
                MessageBox.Show("建立同义词失败，可能是已有" + ee);
            }

        }

        private void button3_Click(object sender, EventArgs e)  //建立用户下所有的同义词
        {
            int a = 0;
            string sql = " select 'create synonym '|| table_name || ' for " + comboBox1.Text + ".' || table_name  from dba_tables where owner='" + comboBox1.Text + "' ";  //查询用户下所有的表，并且生成建立同义词的sql
            try
            {
                ss.Open();
                var ord = ss.Oracle(sql);
                while (ord.Read())
                {
                    string sql1 = ord.GetString(0);
                    var ord1 = ss.Oracle(sql1);
                    a++;
                }
                ss.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            MessageBox.Show("同义词建立成功" + a + "个");
        }
    }
}
