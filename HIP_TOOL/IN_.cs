using System;
using System.Windows.Forms;

namespace HIP_TOOL
{
    public partial class IN_生成 : Form
    {
        public IN_生成()
        {
            InitializeComponent();
        }
        oracle_link ss = new oracle_link();

        private void button1_Click(object sender, EventArgs e)
        {
            int index = 0;
            string No = "";
            string str = "";
            string sql = textBox1.Text;//获取sql
            try
            {
                ss.Open();
                var ord = ss.Oracle(sql);
                while (ord.Read())
                {
                    this.dataGridView1.Rows.Add();
                    No = ord.GetOracleString(0).ToString();
                    str += "'" + No + "',";
                    dataGridView1.Rows[index].Cells[0].Value = No;
                    index++;
                }


            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                ss.Close();
            }
            str = str.Substring(0, str.Length - 1);
            str = "IN(" + str + ")";
            textBox2.Text = str;
        }
    }
}
