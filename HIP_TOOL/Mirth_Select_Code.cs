using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace HIP_TOOL
{
    public partial class Mirth_Select_Code : Form
    {
        public Mirth_Select_Code()
        {
            InitializeComponent();
        }
        entity ent = new entity();//专门存储字符串的类，不需要再新建任何字符
        oracle_link ora = new oracle_link();
        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
            treeView1.Nodes.Clear();
            ent.temp2 = ""; ent.temp3 = ""; ent.temp4 = ""; ent.temp5 = "";
            ent.Str_All = ""; ent.Sql = "";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(textBox1.Text);
            XmlNode root = xmlDoc.FirstChild;
            //  XmlNodeList root = xmlDoc.SelectNodes(nodePath);
            TreeNode treeNode = new TreeNode();
            treeNode.Text = xmlDoc.DocumentElement.Name;//xml的根节点，此处为了mirth暂时不需要
            treeNode.Text = "msg";//为mirth的串
            treeView1.Nodes.Add(treeNode);// 添加到界面上面
            xml_name(treeNode, root);//设置树
            DiGui(treeNode);//递归树并且生成读取字符
            sql();//生成sql
            textBox3.Text = ent.Str_All;
        }


        private void DiGui(TreeNode tn)
        {
            foreach (TreeNode tnSub in tn.Nodes)
            {
                DiGui(tnSub);
            }
            if (!ent.temp2.Contains(tn.FullPath))
            {
                ent.Str_All += "var " + tn.FullPath.Substring(tn.FullPath.LastIndexOf("\\") + 1) + " = " + new Regex("']").Replace(tn.FullPath.Replace("\\", "']['"), "", 1) + "']toString()\r\n";
            }
            ent.temp2 += tn.FullPath + "\r\n";

        }
        private string xml_name(TreeNode treeNode, XmlNode root)//递归生成树
        {
            ent.temp1 = "";//初始化
            string[] stt = new string[root.ChildNodes.Count];
            for (int i = 0; i < root.ChildNodes.Count; i++)
            {
                Regex ss = new Regex("\\<");
                MatchCollection match = ss.Matches(root.ChildNodes[i].OuterXml);
                foreach (Match match1 in match)
                {
                    ent.temp1 += match1.Value;
                }
                if (ent.temp1 != "")
                {
                    TreeNode treeNode1 = new TreeNode();
                    treeNode1.Text = root.ChildNodes[i].Name;
                    treeNode.Nodes.Add(treeNode1);
                    xml_name(treeNode1, root.ChildNodes[i]);
                }
            }
            return "";
        }

        private void sql()
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    ent.temp5 = checkedListBox1.GetItemText(checkedListBox1.Items[i]);
                    ent.temp4 += " var temp['" + ent.temp5 + "'] = IsNull(result.getString('" + ent.temp5 + "'))\r\n";
                    ent.temp3 += ent.temp5 + ",";
                }
            }
            ent.temp3 = ent.temp3.Substring(0, ent.temp3.Length - 1);//去掉逗号
            ent.temp3 = "\"select " + ent.temp3 + " from " + table_view.Text + "\"\r\n";

            ent.Sql = "var tempXML=new XML\"<Response ></Response\"> \r\n" +
                        "var temp=new XML(\"<item></item\"> \r\n" +
                        "var sql=" +
                        ent.temp3 +
                        "var result =GetSelect(sql1, 'HIS');\r\n " +
                        " while(result.next()){ \r\n" +
                        ent.temp4 +
                        @"tempXML.appendChild(temp);" +
                        "return tempXML;}";
            ent.Str_All = "function fun(msg){\r\n" + ent.Str_All + ent.Sql;
        }
        private void AddNodes(TreeNode parent, XmlNodeList nodes)
        {
            foreach (XmlNode node in nodes)
            {
                TreeNode treeNode = new TreeNode();
                treeNode.Text = node.Name;
                if (parent == null)
                    treeView1.Nodes.Add(treeNode);
                else
                    parent.Nodes.Add(treeNode);
                AddNodes(treeNode, node.ChildNodes);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            checkedListBox1.Items.Clear();
            ent.Sql1 = " select t.COLUMN_NAME from SYS.ALL_TAB_COLS t   where  1=1 ";
            if (User.Text != "")
            {
                ent.Sql1 += " and  t.owner = '" + User.Text + "'";
            }
            if (table_view.Text != "")
            {
                ent.Sql1 += " and t.TABLE_NAME ='" + table_view.Text + "'";
                ent.Sql1 = ent.Sql1.ToUpper();
                ora.Open();
                ent.Result = ora.Oracle(ent.Sql1);
                while (ent.Result.Read())
                {
                    checkedListBox1.Items.Add(ent.Result.GetOracleString(0).ToString());
                }
                ora.Close();
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
                {
                    this.checkedListBox1.SetItemChecked(i, true);
                }
            }
            else
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        checkedListBox1.SetItemChecked(i, false);
                    }
                    else
                    {
                        checkedListBox1.SetItemChecked(i, true);
                    }
                }
            }
        }
    }
}
