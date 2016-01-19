namespace Structure.Control
{
    /// <summary>
    /// 图标按钮
    /// </summary>
    public class KNXImageButton : KNXControlBase
    {
        // 按钮图标
        public string Image { get; set; }

        //按钮描述
        public string Description { get; set; }

        //是否有长按事件
        public bool HasLongClickCommand { get; set; }
    }
}