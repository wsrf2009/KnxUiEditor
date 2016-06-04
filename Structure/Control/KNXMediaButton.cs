
namespace Structure.Control
{

    /// <summary>
    /// 多媒体开关
    /// </summary>
    public class KNXMediaButton : KNXControlBase
    {
        //自定义媒体按钮图片Icon(CustomIcon和MediaButtonType不能共存)
        public string CustomImage { get; set; }

        //自定义媒体按钮类型(CustomIcon和MediaButtonType不能共存)
        public string MediaButtonType { get; set; }
    }
}
