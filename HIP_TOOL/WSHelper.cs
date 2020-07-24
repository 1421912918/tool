namespace HIP_TOOL
{
    /// <summary>
    /// 请求信息帮助
    /// </summary>
    public partial class HttpHelper
    {
        public string mirth(string MESS)
        {
            WebReference.DefaultAcceptMessageService ss = new WebReference.DefaultAcceptMessageService();
            string s = ss.acceptMessage(MESS);
            return s;
        }

    }
}
