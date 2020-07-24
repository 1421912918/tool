using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace HIP_TOOL
{


    public partial class datebase_setting : Form
    {
        public datebase_setting()
        {
            InitializeComponent();
        }
        //private string NAME;
        //public string DB_NAME
        //{
        //    get { return NAME; }
        //    set { NAME = value; }

        //}
        //private string Com;
        //public string COM_BOBOX
        //{
        //    get { return Com; }
        //    set { Com = value; }

        //}
        //private string Com1;
        //public string COM_BOBOX1
        //{
        //    get { return Com1; }
        //    set { Com1 = value; }

        //}
        //private string Com2;
        //public string COM_BOBOX2
        //{
        //    get { return Com2; }
        //    set { Com2 = value; }

        //}
        //private string Text;
        //public string TEXT_Box
        //{
        //    get { return Text; }
        //    set { Text = value; }

        //}


        private void XML()
        {
            try
            {


                //将XML文件加载进来
                XDocument document = XDocument.Load("XMLFile1.xml");
                //获取到XML的根元素进行操作

                XElement root = document.Root;
                IEnumerable<XElement> enumerable = root.Elements();
                treeView1.Nodes.Clear();  //清除所有treeView1节点
                tabControl1.TabPages.Clear();//清除所有tabControl1节点
                foreach (XElement item in enumerable)
                {

                    string DB_Name = item.Name.ToString();
                    TabPage tabPage = new TabPage();//新建一个页面
                    tabPage.Text = DB_Name;
                    tabPage.Name = DB_Name;//设置页面的一些参数
                    Form page = new Form();  //新建一个form
                    page.Name = "formpage";
                    page.TopLevel = false; //给Form去边框
                    page.FormBorderStyle = FormBorderStyle.None;
                    tabPage.Controls.Add(page); //把page添加到tabPage中
                    page.Show(); //在tabPage选项卡中显示出来
                    tabControl1.TabPages.Add(tabPage);//添加选项卡tabPage到TabControl中
                    /*上面一段代码演示根据节点数量生成page的页数量*/

                    Button newButton = new Button();//创建一个名为newButton的新按钮
                    newButton.Text = "保存" + item.Name.ToString();//为按钮设置一些属性
                    newButton.Location = new Point(100, 180);
                    newButton.Size = new Size(100, 30);

                    Label lab = new Label();
                    lab.Text = "IP/实例名";
                    lab.Location = new Point(20, 20);
                    Label lab1 = new Label();
                    lab1.Text = "USER";
                    lab1.Location = new Point(20, 60);
                    Label lab2 = new Label();
                    lab2.Text = "PASSWORD";
                    lab2.Location = new Point(20, 100);
                    Label lab3 = new Label();
                    lab3.Text = "DataBase";
                    lab3.Location = new Point(20, 140);

                    ComboBox com = new ComboBox();
                    com.Location = new Point(100, 20);
                    com.Size = new Size(200, 20);
                    ComboBox com1 = new ComboBox();
                    com1.Location = new Point(100, 60);
                    com1.Size = new Size(200, 20);
                    ComboBox com2 = new ComboBox();
                    com2.Location = new Point(100, 140);
                    com2.Size = new Size(200, 20);

                    TextBox text = new TextBox();
                    text.Location = new Point(100, 100);
                    text.Size = new Size(200, 20);
                    text.PasswordChar = '*';

                    page.Controls.Add(com);
                    page.Controls.Add(com1);
                    page.Controls.Add(com2);
                    page.Controls.Add(text);

                    page.Controls.Add(lab);
                    page.Controls.Add(lab1);
                    page.Controls.Add(lab2);
                    page.Controls.Add(lab3);
                    page.Controls.Add(newButton);
                    //  Set_ComboBox_1(com);
                    //  newButton.Click += NewButton_Click;
                    // com.Leave += Com_Leave;
                    // com1.Leave += Com1_Leave;
                    //  com2.Leave += Com2_Leave;
                    //  text.Leave += Text_Leave;
                    //  tabPage.Click += TabPage_Click;
                    //  tabPage.MouseClick += TabPage_MouseClick;
                    TreeNode node = new TreeNode();//实例化一个treenode对象
                    node.Text = DB_Name;
                    //if (item.FirstNode.NextNode == null) //如果没有下级节点就将此节点值写入
                    //{
                    //    node.Nodes.Add(item.Value.ToString());
                    //}
                    treeView1.Nodes.Add(node);//节点名称
                    foreach (XElement item1 in item.Elements())
                    {
                        string ss1 = item1.Value.ToString();//循环节点的值
                        string ss = item1.Name.ToString();//循环节点的节点名
                        node.Nodes.Add(ss1);//子节点名称
                        switch (ss)
                        {
                            case "IP":
                                com.Text = ss1;
                                break;
                            case "user":
                                com1.Text = ss1;
                                break;
                            case "password":
                                text.Text = ss1;
                                break;
                            case "DataBase":
                                com2.Text = ss1;
                                break;
                            case "status":
                                if (ss1 == "1")
                                {
                                    node.BackColor = Color.GreenYellow;
                                }
                                else
                                {
                                    node.BackColor = Color.Red;
                                }
                                break;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void datebase_setting_Load(object sender, EventArgs e)
        {
            XML();
        }

        //ComboBox com = new ComboBox();
        //TextBox text = new TextBox();
        //private void Text_Leave(object sender, EventArgs e)
        //{
        //    TextBox text = (TextBox)sender;    //获取所点击的对应的button
        //    Text = text.Text;
        //    // throw new NotImplementedException();
        //}
        //private void Com1_Leave(object sender, EventArgs e)
        //{
        //    ComboBox com = (ComboBox)sender;    //获取所点击的对应的button
        //    Com1 = com.Text;
        //}


        //private void Com2_Leave(object sender, EventArgs e)
        //{
        //    ComboBox com = (ComboBox)sender;    //
        //    Com2 = com.Text;                               
        //}

        //private void Com_Leave(object sender, EventArgs e)
        //{
        //    ComboBox com = (ComboBox)sender;    //
        //    Com = com.Text;
        //}

        //private void TabPage_MouseClick(object sender, MouseEventArgs e)
        //{
        //    TabPage DB_Name = (TabPage)sender;
        //    DB_NAME = DB_Name.Text; ;
        //}

        //private void TabPage_Click(object sender, EventArgs e)
        //{
        //    TabPage DB_Name = (TabPage)sender;
        //    DB_NAME = DB_Name.Text; ;
        //} 




        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad.exe", "XMLFile1.xml");
        }
        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            e.Node.ImageIndex = 1; //指向展开的图标
            e.Node.SelectedImageIndex = 1;//指向展开的图标
        }

        private void treeView1_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            e.Node.ImageIndex = 0; //指向关闭的图标
            e.Node.SelectedImageIndex = 0;//指向关闭的图标
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.treeView1.ContextMenuStrip = contextMenuStrip1;
                try
                {
                    tabControl1.SelectTab(treeView1.SelectedNode.Text);
                }
                catch (Exception)
                {
                }
            }
        }

        private void 添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //添加节点
            string str = Interaction.InputBox("请输入添加的节点名称）", "请输入添加的节点名称", "", 100, 100);
            if (string.IsNullOrEmpty(str.Trim()))
            {
                MessageBox.Show("要添加的节点名称不能为空！");
                return;
            }
            treeView1.Nodes.Add(str.Trim());
        }

        private void 添加子节点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //添加子节点
            string str = Interaction.InputBox("请输入添加的子节点名称）", "请输入添加的子节点名称", "", 100, 100);

            if (string.IsNullOrEmpty(str.Trim()))
            {
                MessageBox.Show("要添加的节点名称不能为空！");
                return;
            }
            if (treeView1.SelectedNode == null)
            {
                MessageBox.Show("请选择要添加子节点的节点！");
                return;
            }
            treeView1.SelectedNode.Nodes.Add(str.Trim());
        }

        private void 删除节点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //删除节点
            if (treeView1.SelectedNode == null)
            {
                MessageBox.Show("请选择要删除的节点！");
                return;
            }
            treeView1.SelectedNode.Remove();
        }
        private void 选择此数据库ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("XMLFile1.xml");
            XmlNode root = xmlDoc.SelectSingleNode("library");
            XmlNodeList child = root.ChildNodes;
            foreach (XmlNode item in child)
            {
                foreach (XmlNode item1 in item.ChildNodes)
                {
                    if (item1.Name == "status")
                    {
                        item1.InnerText = "0";
                    }
                }
            }
            XmlNode memberlist = xmlDoc.SelectSingleNode("library/" + treeView1.SelectedNode.Text.ToString());
            memberlist.LastChild.InnerText = "1";
            xmlDoc.Save("XMLFile1.xml");//保存
            XML();//重新加载刷新选择
        }
        private void button3_Click(object sender, EventArgs e)
        {
            oracle_link ss = new oracle_link();
            string sql = " select * from dual ";
            string ll = "";
            try
            {
                ss.Open();
                var ord = ss.Oracle(sql);
                if (ord.Read())
                {
                    ll = ord.GetOracleString(0).ToString();
                }
                if (ll != "X")
                {
                    MessageBox.Show("数据库信息保存失败");
                }
                else
                {
                    MessageBox.Show("测试成功");
                    // this.Close();
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

        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XML();
        }
        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                tabControl1.SelectTab(treeView1.SelectedNode.Text);
            }
            catch (Exception)
            {

            }
        }
        //处理根节点的子节点
        //private void RefreshChildNode(TreeView tr1, TreeNode treeNode)
        //{
        //    foreach (TreeNode node in tr1.Nodes)
        //    {
        //            node.Nodes.Add(treeNode);
        //         if (node.Nodes.Count > 0)
        //        {
        //            FindChildNode(node, treeNode);
        //        }
        //    }
        //}

        //处理根节点的子节点的子节点
        //private void FindChildNode(TreeNode tNode, TreeNode treeNode)
        //{
        //    foreach (TreeNode node in tNode.Nodes)
        //    {
        //            node.Nodes.Add(treeNode);
        //         if (node.Nodes.Count > 0)
        //        {
        //            FindChildNode(node, treeNode);
        //        }

        //    }

        //}
    }

}
