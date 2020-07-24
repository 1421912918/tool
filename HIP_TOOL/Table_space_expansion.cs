using Microsoft.VisualBasic;
using System;
using System.Windows.Forms;

namespace HIP_TOOL
{
    public partial class Table_space_expansion : Form
    {
        public Table_space_expansion()
        {
            InitializeComponent();
            // this.timer1.Enabled = true;
        }
        oracle_link ss = new oracle_link();
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            string sql = @"select b.tablespace_name,
                           to_char(b.bytes / 1024 / 1024 / 1024,'fm999990.9999') || 'G',
                           to_char((b.bytes - sum(nvl(a.bytes, 0))) / 1024 / 1024 / 1024,'fm999990.9999') || 'G',
                           to_char(substr((b.bytes - sum(nvl(a.bytes, 0))) / (b.bytes) * 100, 1, 5),'fm999990.9999') || '%' as 百分比,
                           b.file_name,
                           b.AUTOEXTENSIBLE,
                           b.INCREMENT_BY / 128
                      from dba_free_space a, dba_data_files b
                     where a.file_id = b.file_id
                     group by b.tablespace_name,
                              b.file_name,
                              b.bytes,
                              b.AUTOEXTENSIBLE,
                              b.INCREMENT_BY
                     order by 百分比 desc";


            try
            {
                int index = 0;
                ss.Open();
                var ord = ss.Oracle(sql);
                while (ord.Read())
                {
                    //comboBox1.Text = ord.GetString(0);
                    this.dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = ord.GetOracleString(0).ToString();
                    dataGridView1.Rows[index].Cells[1].Value = ord.GetString(1).ToString();
                    dataGridView1.Rows[index].Cells[2].Value = ord.GetString(2).ToString();
                    dataGridView1.Rows[index].Cells[3].Value = ord.GetOracleString(3).ToString();
                    dataGridView1.Rows[index].Cells[4].Value = ord.GetString(4).ToString();
                    dataGridView1.Rows[index].Cells[5].Value = ord.GetString(5).ToString();
                    dataGridView1.Rows[index].Cells[6].Value = ord.GetFloat(6).ToString();
                    index++;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally { ss.Close(); }

            // conn.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string str = Interaction.InputBox("请输入扩/缩后的表空间大小（M）", "表空间扩/缩", "", 100, 100);
            if (str != "")
            {
                int i = dataGridView1.CurrentRow.Index;
                try
                {
                    string table = dataGridView1.Rows[i].Cells[4].Value.ToString();
                    string table_sp = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    string sql = " alter database datafile '" + table + "' resize " + str + "M ";

                    if (MessageBox.Show("确定将表空间" + table_sp + "扩/缩至" + str + "M吗？ ", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        ss.Open();
                        var ord = ss.Oracle(sql);
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show("扩/缩失败" + ee);
                }
                finally { ss.Close(); button1.PerformClick(); }
            }
            else
            {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            string str = Interaction.InputBox("请输入自动增量（M）", "表空间自动增长设置", "", 20, 20);
            if (str != "")
            {
                string str1 = Interaction.InputBox("请输入最大值（M）", "表空间自动增长设置", "", 20, 20);
                if (str1 != "")
                {
                    int i = dataGridView1.CurrentRow.Index;
                    try
                    {
                        string table = dataGridView1.Rows[i].Cells[4].Value.ToString();
                        string table_sp = dataGridView1.Rows[i].Cells[0].Value.ToString();
                        string sql = "ALTER DATABASE DATAFILE '" + table + "'AUTOEXTEND ON NEXT " + str + "M MAXSIZE " + str1 + "M";
                        if (MessageBox.Show("确定将表空间" + table + "设置成自动增长增量(" + str + "M），最大值(" + str1 + "M)吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            ss.Open();
                            var ord = ss.Oracle(sql);

                            button1.PerformClick();
                        }
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show(ee.ToString());
                    }
                    finally { ss.Close(); }
                }
            }

        }
        private void button3_Click(object sender, EventArgs e)
        {

            string str = Interaction.InputBox("请输入数据文件地址", "数据文件设置", "", 20, 20);
            string size = Interaction.InputBox("请输入数据文件大小", "数据文件设置", "", 20, 20);
            int i = dataGridView1.CurrentRow.Index;
            try
            {
                string table = dataGridView1.Rows[i].Cells[4].Value.ToString();
                string table_sp = dataGridView1.Rows[i].Cells[0].Value.ToString();
                string sql = " ALTER TABLESPACE " + table_sp +
                            " ADD DATAFILE '" + str + "'" +
                            " SIZE '" + size + "'M AUTOEXTEND ON NEXT 200M MAXSIZE 10G";
                if (MessageBox.Show("确定为表空间" + table_sp + "设置数据文件吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    ss.Open();
                    var ord = ss.Oracle(sql);

                    button1.PerformClick();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("设置失败");
            }
            finally { ss.Close(); }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            new_table_space ss = new new_table_space();
            ss.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {

            int i = dataGridView1.CurrentRow.Index;
            try
            {
                string table = dataGridView1.Rows[i].Cells[4].Value.ToString();
                string table_sp = dataGridView1.Rows[i].Cells[0].Value.ToString();
                string sql = " DROP TABLESPACE " + table_sp + " INCLUDING CONTENTS AND DATAFILES";
                if (MessageBox.Show("确定删除表空间" + table_sp + "吗？？？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    ss.Open();
                    var ord = ss.Oracle(sql);
                    button1.PerformClick();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("设置失败");
            }
            finally { ss.Close(); }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {


        }
    }
}
