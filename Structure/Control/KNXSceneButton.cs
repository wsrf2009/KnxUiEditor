
namespace Structure.Control
{
    /// <summary>
    /// 场景开关
    /// </summary>
    public class KNXSceneButton : KNXControlBase
    {
        //按钮描述
        public string Description { get; set; }

        //是否有长按事件
        public bool HasLongClickCommand { get; set; }
    }
}
