using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Xml.Linq;

namespace HIP_TOOL
{
    class oracle_link   //连接数据库专用类，适用于客户端程序
    {
        OracleConnection conn = new OracleConnection();
        entity ent = new entity();
        public OracleDataReader Oracle(string sql)//执行查询语句，返回一个对象
        {
            try
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                var ord = cmd.ExecuteReader();
                cmd.Dispose();
                return ord;
            }
            catch (OracleException e)
            {
                throw new Exception(e.Message);
            }
        }
        public int Oracle_UP_IN(string sql)//执行一个update/insert语句，返回影响行数
        {
            try
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                int z = cmd.ExecuteNonQuery();
                ContextUtil.SetComplete();
                return z;
            }
            catch (OracleException e)
            {
                throw new Exception(e.Message);
            }

        }
        public string Oracle_Source()
        {
            // XmlDocument xmlDoc = new XmlDocument();// 
            // xmlDoc.Load("D:\\vscode\\HIP_TOOL\\HIP_TOOL\\XMLFile1.xml");//绝对路径测试使用
            XDocument document = XDocument.Load("XMLFile1.xml");//相对路径
            //获取到XML的根元素进行操作
            XElement root = document.Root;
            IEnumerable<XElement> enumerable = root.Elements();
            foreach (XElement item in enumerable)
            {
                foreach (XElement item1 in item.Elements())
                {
                    ent.temp1 = item1.Value.ToString();//循环节点的值
                    ent.temp2 = item1.Name.ToString();//循环节点的节点名
                    switch (ent.temp2)
                    {
                        case "IP":
                            ent.ConnectionString = "Data Source=" + ent.temp1;
                            break;
                        case "user":
                            ent.ConnectionString += ";User ID=" + ent.temp1;
                            break;
                        case "password":
                            ent.ConnectionString += ";PassWord=" + ent.temp1;
                            break;
                        case "DataBase":
                            break;
                        case "STATUS":
                            break;
                    }
                    if (ent.temp1 == "1")
                    {
                        break;
                    }
                }
                if (ent.temp1 == "1")
                {
                    break;
                }

            }
            // string ConnectionString = "Data Source=" + ConfigurationManager.AppSettings["IP"].ToString() +
            //";User ID=" + ConfigurationManager.AppSettings["user"].ToString() +
            //";PassWord=" + ConfigurationManager.AppSettings["password"].ToString() + ";";//写连接串
            return ent.ConnectionString;
        }
        public void Open()
        {
            conn.ConnectionString = Oracle_Source();
            conn.Open();
        }
        public void Close()
        {
            //conn.Close();
            if (conn == null) { return; }
            try
            {
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void Dispose()
        {
            conn.Dispose();
            conn = null;
        }
    }

}
