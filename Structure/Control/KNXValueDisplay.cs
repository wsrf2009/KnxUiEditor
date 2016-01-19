
namespace Structure.Control
{
    /// <summary>
    /// 数码显示
    /// </summary>
    public class KNXValueDisplay : KNXControlBase
    {
        // 标签
        public string Lable { get; set; }


        // 用于添加单元标识符显示的值，例如一个可选字段：用于显示当前温度，”°C”可以插入。
        public string Unit { get; set; }


        // 显示的值
        public string Value { get; set; }


        // 可选的姓名或名称的显示值。这是类似的标签，但直接显示的实际值。
        public string Description { get; set; }


        // 定义小数位数
        public int DecimalPlaces { get; set; }

    }
}
