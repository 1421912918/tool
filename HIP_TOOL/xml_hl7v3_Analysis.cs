using Newtonsoft.Json;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace HIP_TOOL
{
    public partial class xml_hl7v3_Analysis : Form
    {
        public xml_hl7v3_Analysis()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ss = XmlTojson();
            textBox1.Clear();
            textBox1.Text = ss;
        }
        public string XmlTojson()
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                doc.LoadXml(textBox2.Text);
                return ConvertStringToJson(JsonConvert.SerializeXmlNode(doc));
            }
            catch (Exception)
            {
                return "";
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string ss = JsonToxml();
                Regex reg = new Regex("<\\?(.+)\\?>");//去掉头文件的正则表达式
                ss = reg.Replace(ss, "");
                textBox2.Clear();
                textBox2.Text = ss;
            }
            catch (Exception)
            {
                throw;
            }


        }
        public string JsonToxml()
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc = JsonConvert.DeserializeXmlNode(textBox1.Text, "root");
                return ConvertXmlDocumentTostring(doc);
            }
            catch (Exception)
            {
                return "";
            }



        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }
        private static System.Diagnostics.Process p;
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (p == null)
                {
                    p = new System.Diagnostics.Process();
                    p.StartInfo.FileName = "CodeMaker.exe";
                    p.Start();
                }
                else
                {
                    if (p.HasExited) //是否正在运行
                    {
                        p.Start();
                    }
                }
                p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }
        private string ConvertStringToJson(string str)//格式化json串
        {
            //格式化json字符串
            JsonSerializer serializer = new JsonSerializer();
            TextReader tr = new StringReader(str);
            JsonTextReader jtr = new JsonTextReader(tr);
            object obj = serializer.Deserialize(jtr);
            if (obj != null)
            {
                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                };
                serializer.Serialize(jsonWriter, obj);
                return textWriter.ToString();
            }
            else
            {
                return str;
            }
        }
        public static string ConvertXmlDocumentTostring(XmlDocument xmlDocument)//格式化xml
        {
            MemoryStream memoryStream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(memoryStream, null)
            {
                Formatting = System.Xml.Formatting.Indented//缩进
            };
            xmlDocument.Save(writer);
            StreamReader streamReader = new StreamReader(memoryStream);
            memoryStream.Position = 0;
            string xmlString = streamReader.ReadToEnd();
            streamReader.Close();
            memoryStream.Close();
            return xmlString;
        }
    }
}
