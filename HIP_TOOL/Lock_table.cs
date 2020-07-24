using System;
using System.Windows.Forms;

namespace HIP_TOOL
{
    public partial class Lock_table : Form
    {
        public Lock_table()
        {
            InitializeComponent();
        }

        private void Lock_table_Load(object sender, EventArgs e)
        {

        }
        oracle_link ss = new oracle_link();
        private void button1_Click(object sender, EventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            try
            {
                string s = dataGridView1.Rows[i].Cells[0].Value.ToString();
                string s1 = dataGridView1.Rows[i].Cells[1].Value.ToString();
                string sql = " alter system kill session '" + s + "," + s1 + "'";
                ss.Open();
                var ord = ss.Oracle(sql);
                ss.Close();
                button3.PerformClick();

            }
            catch (Exception ee)
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            string sql = " SELECT l.session_id sid, " +
                     " s.serial#, " +
                     " l.locked_mode , " +
                     " l.oracle_username , " +
                     " l.os_user_name , " +
                     " s.machine , " +
                     " s.terminal , " +
                     " o.object_name , " +
                     " to_char(s.logon_time,'yyyy-mm-dd  hh:mm:ss'）  " +
                     " FROM v$locked_object l, all_objects o, v$session s " +
                     " WHERE l.object_id = o.object_id " +
                     " AND l.session_id = s.sid " +
                     " ORDER BY sid, s.serial# ";
            ss.Open();
            var ord = ss.Oracle(sql);
            int index = 0;
            while (ord.Read())
            {
                //comboBox1.Text = ord.GetString(0);
                this.dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = ord.GetInt32(0).ToString();
                dataGridView1.Rows[index].Cells[1].Value = ord.GetInt32(1).ToString();
                dataGridView1.Rows[index].Cells[2].Value = ord.GetInt32(2).ToString();
                dataGridView1.Rows[index].Cells[3].Value = ord.GetOracleString(3).ToString();
                dataGridView1.Rows[index].Cells[4].Value = ord.GetString(4).ToString();
                dataGridView1.Rows[index].Cells[5].Value = ord.GetString(5).ToString();
                dataGridView1.Rows[index].Cells[6].Value = ord.GetString(6).ToString();
                dataGridView1.Rows[index].Cells[7].Value = ord.GetString(7).ToString();
                dataGridView1.Rows[index].Cells[8].Value = ord.GetString(8).ToString();
                index++;

            }
            ss.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
