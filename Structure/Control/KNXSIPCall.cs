
namespace Structure.Control
{

    public enum Prococal
    {
        TCP, UDP
    }

    /// <summary>
    /// 可视对讲
    /// </summary>
    public class KNXSIPCall : KNXControlBase
    {
        // 标签
        public string Lable { get; set; }
        //
        public string Note { get; set; }
        //用户名称
        public string UserName { get; set; }
        //SIP域名
        public string SIPDomain { get; set; }
        //认证用户
        public string AuthenticationUserName { get; set; }
        //认证密码
        public string AuthenticationUserPassword { get; set; }
        //端口
        public int Port { get; set; }
        //协议类型
        public Prococal Prococal { get; set; }
        //代理地址
        public string ProxyAddressUrl { get; set; }
    }
}
