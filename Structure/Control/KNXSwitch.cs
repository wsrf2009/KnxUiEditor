
namespace Structure.Control
{
    /// <summary>
    /// 开关
    /// </summary>
    public class KNXSwitch : KNXControlBase
    {

        // 开关开启时图片
        public string OnImage { get; set; }

        // 开关关闭时图片
        public string OffImage { get; set; }

        //指令发送延迟时间(单位毫秒)
        public int SendInterval { get; set; }
    }
}
