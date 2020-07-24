using Oracle.ManagedDataAccess.Client;

namespace HIP_TOOL
{
    /// <summary>
    ///  实体类
    /// </summary>
    public partial class entity
    {


        public string Xml_str { get; set; }//xml串
        public string Json_str { get; set; }//json串

        public string Str_all { get; set; }//累加字符串取值
        public string Str_temp { get; set; }//临时累加字符串
        public string Str_All { get; set; }//字符串拼接

        public string temp1 { get; set; }//临时字符串
        public string temp2 { get; set; }
        public string temp3 { get; set; }
        public string temp4 { get; set; }
        public string temp5 { get; set; }




        public int Num { get; set; }//数字
        public int Num1 { get; set; }
        public int Num2 { get; set; }

        public string Name { get; set; } //名字
        public string Sex { get; set; }
        public int Age { get; set; }

        public string Sql { get; set; } //存储sql
        public string Sql1 { get; set; }//存储sql1
        public string ConnectionString { get; set; }//存储连接串
        public OracleDataReader Result { get; set; }//存储连接串



        public double Length { get; set; } //长宽高
        public double Breadth { get; set; }
        public double Height { get; set; }
    }
}
