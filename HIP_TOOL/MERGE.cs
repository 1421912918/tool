using System;
using System.Windows.Forms;

namespace HIP_TOOL
{
    public partial class MERGE : Form
    {
        public MERGE()
        {
            InitializeComponent();
        }
        oracle_link ss = new oracle_link();
        private void button1_Click(object sender, EventArgs e)
        {
            string sql = " select username from dba_users ";
            ss.Open();
            var ord = ss.Oracle(sql);
            int a = 0;
            while (ord.Read())
            {
                comboBox1.Items.Add(ord.GetString(0));
                a++;
            }
            ss.Close();
        }
        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            string sql = " select table_name from all_tables where owner = '" + comboBox1.Text.ToUpper() + "'";
            ss.Open();
            var ord = ss.Oracle(sql);
            int a = 0;
            while (ord.Read())
            {
                comboBox2.Items.Add(ord.GetString(0));
                a++;
            }
            ss.Close();
        }
        //private void comrole_TextChanged(object sender, EventArgs e)
        //{
        //    comboBox2.Items.Clear();
        //    string sql = " select table_name from all_tables where owner = '" + comboBox1.Text.ToUpper() + "'";

        //    var ord = ss.Oracle(sql);
        //    int a = 0;
        //    while (ord.Read())
        //    {
        //        if (a == 0) { comboBox2.Text = ord.GetString(0); }
        //        comboBox2.Items.Add(ord.GetString(0));
        //        a++;
        //    }
        //    ss.Close();
        //}

        private void button2_Click(object sender, EventArgs e)
        {
            textmerge.Clear();
            comboBox1.Items.Clear();
            string TableName = comboBox2.Text.ToUpper();
            string OWNER = comboBox1.Text.ToUpper();
            ss.Open();

            string sql = " SELECT t.COLUMN_NAME,t.DATA_TYPE FROM all_TAB_COLUMNS t where t.OWNER='"+ OWNER + "' and  t.TABLE_NAME = '" + TableName + "'";
            var ord = ss.Oracle(sql);
            string sql1 = " select cu.COLUMN_NAME from all_cons_columns cu, all_constraints au where t.OWNER='" + OWNER + "' and cu.constraint_name = au.constraint_name and au.constraint_type = 'P' and au.table_name = '" + TableName + "' ";
            var ord1 = ss.Oracle(sql);
            string sql2 = " SELECT * from(SELECT t.COLUMN_NAME FROM all_TAB_COLUMNS t where t.OWNER='" + OWNER + "' and t.TABLE_NAME = '" + TableName + "') A " +
"MINUS SELECT *from(select cu.COLUMN_NAME from all_cons_columns cu, all_constraints au where cu.constraint_name = au.constraint_name and au.constraint_type = 'P' and  cu.OWNER='" + OWNER + "' and au.table_name = '" + TableName + "') B ";
            var ord2 = ss.Oracle(sql);
            string var1 = "";
            string COLUMN_NAME = "";
            string DATA_TYPE = "";
            string PK = "";
            string str = "";
            string on = "";
            string on1 = "";
            // string ON = "";
            string set = "";
            string set1 = "";
            string insert = "";
            string insert1 = "";
            string VALUES = "";
            string VALUES1 = "";
            while (ord.Read())
            {

                COLUMN_NAME = ord.GetString(0);
                DATA_TYPE = ord.GetString(1);
                var1 = "\"\'\"+" + COLUMN_NAME + "+" + "\"'  as " + COLUMN_NAME + " ,\"+ " + Environment.NewLine;
                insert = " \"" + COLUMN_NAME + ",\"+ " + Environment.NewLine;
                VALUES = " \"NP." + COLUMN_NAME + ",\"+ " + Environment.NewLine;
                VALUES1 = VALUES1 + VALUES;
                insert1 = insert1 + insert;
                str = str + var1;
            }
            while (ord1.Read())
            {
                PK = ord1.GetString(0);
                on = " p." + PK + "=NP." + PK + " and ";
                on1 = on1 + on;
            }
            while (ord2.Read())
            {
                COLUMN_NAME = ord2.GetString(0);
                set = "\"p." + COLUMN_NAME + "=NP." + COLUMN_NAME + ",\"+" + Environment.NewLine;
                set1 = set1 + set;
            }
            ss.Close();
            textmerge.Text = "var sql=\" MERGE INTO " + TableName + " P USING (SELECT \"+ " + Environment.NewLine + str.Remove(str.LastIndexOf(",\"")) + "  FROM DUAL) NP  ON ( " + on1.Remove(on1.LastIndexOf("and")) + " ) WHEN MATCHED THEN  UPDATE SET \"+" + Environment.NewLine +
              set1.Remove(set1.LastIndexOf(",\"")) + "\"+\" where " + on1.Remove(on1.LastIndexOf("and")) + " WHEN NOT MATCHED THEN INSERT( \"+" + Environment.NewLine +
             "" + insert1.Remove(insert1.LastIndexOf(",\"")) + ") VALUES ( \"+" + VALUES1.Remove(VALUES1.LastIndexOf(",\"")) + " )\"";

            textmerge.Focus();
            textmerge.SelectAll();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sql = " SELECT t.COLUMN_NAME,t.DATA_TYPE FROM all_TAB_COLUMNS t where t.TABLE_NAME = '" + comboBox2.Text.ToUpper() + "'";
            string var1 = "";
            string COLUMN_NAME = "";
            string DATA_TYPE = "";
            string str = "";
            ss.Open();
            var ord = ss.Oracle(sql);
            while (ord.Read())
            {
                COLUMN_NAME = ord.GetString(0).ToString();
                DATA_TYPE = ord.GetString(1).ToString();
                var1 = " var " + COLUMN_NAME + " = IsNull(result.getString(\"" + COLUMN_NAME + "\"));" + Environment.NewLine;
                str = str + var1;
            }
            textmerge.Text = str;
            textmerge.Focus();
            textmerge.SelectAll();
        }
    }
}
