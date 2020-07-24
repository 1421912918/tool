using System;
using System.Windows.Forms;

namespace HIP_TOOL
{
    public partial class new_table_space : Form
    {
        public new_table_space()
        {
            InitializeComponent();
        }
        oracle_link ss = new oracle_link();
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "Create tablespace " + textBox1.Text +
                                       "Datafile '" + textBox2.Text + "' size " + textBox3.Text + "M " +
                                      " Autoextend on Next 5M Maxsize 50M ";
                if (MessageBox.Show("确定新建表空间" + textBox1.Text + "大小" + textBox3.Text + "M吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    ss.Open();
                    var ord = ss.Oracle(sql);
                    MessageBox.Show("新建表空间成功");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("新建失败");
            }

            finally
            { ss.Close(); }

        }

        private void 查看新建表空间语法ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("新建表空间帮助文档.txt");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = textBox5.Text;
                if (MessageBox.Show("确定执行吗，结果不可逆？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    ss.Open();
                    var ord = ss.Oracle(sql);
                    MessageBox.Show("成功");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("失败");
            }
            finally
            { ss.Close(); }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "新建表空间")
            {
                textBox5.Text = @"create tablespace test_tablespace
                            logging
                            datafile 'd:\app\Administrator\oradata\lis2012\LIS2012DATA01.dbf'
                            size 50m
                            autoextend on
                            next 50m
                            extent management local";
            }
            if (comboBox1.Text == "新建用户")
            {
                textBox5.Text = @"create user user_name identified by password  default tablespace tablespace_name ";
            }



        }
    }
}
