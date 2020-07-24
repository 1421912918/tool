using System;
using System.Data;
using System.Windows.Forms;

namespace HIP_TOOL
{
    public partial class XtraForm1 : DevExpress.XtraBars.TabForm
    {
        public XtraForm1()
        {
            InitializeComponent();
        }
        oracle_link ss = new oracle_link();
        entity ent = new entity();
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var sql = textEdit1.Text;
            ent.temp1 = textEdit1.Text.Trim().Substring(0, 5);
            if (ent.temp1.ToUpper() == "SELECT")
            {
                Select_(sql);
            }
            else
            {
                Up_In(sql);
            }
        }
        private void Select_(string sql)
        {
            DataTable datatable = new DataTable();
            ent.Num1 = 0;
            try
            {
                ss.Open();
                var dataReader = ss.Oracle(sql);
                ///动态添加表的数据列
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    DataColumn myDataColumn = new DataColumn();
                    myDataColumn.DataType = dataReader.GetFieldType(i);
                    myDataColumn.ColumnName = dataReader.GetName(i);
                    datatable.Columns.Add(myDataColumn);
                }
                ///添加表的数据行
                while (dataReader.Read())
                {
                    DataRow myDataRow = datatable.NewRow();
                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {
                        myDataRow[i] = dataReader[i];
                    }
                    datatable.Rows.Add(myDataRow);
                    myDataRow = null;
                    ent.Num1++;
                }
                label1.Text = "查询成功:" + ent.Num1 + "行";
                ///关闭数据读取器
                gridControl1.DataSource = datatable;
                this.gridView1.PopulateColumns();
                gridView1.OptionsView.ColumnAutoWidth = false;
                //for (int i = 0; i < dataReader.FieldCount; i++)
                //{
                //    gridView1.Columns[dataReader.GetName(i).ToString()].BestFit();
                //}
                for (int I = 0; I < gridView1.Columns.Count; I++)
                {
                    this.gridView1.BestFitColumns();
                    this.gridView1.Columns[I].BestFit();//自动列宽
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("" + ee);
            }
            finally
            {
                ss.Close();
            }
        }
        private void Up_In(string sql)
        {
            try
            {
                ss.Open();
                ent.Num = ss.Oracle_UP_IN(sql);
                label1.Text = "执行成功：" + ent.Num.ToString() + "行";
            }
            catch (Exception)
            {

                throw;
            }
            finally { ss.Close(); }
        }
    }
}