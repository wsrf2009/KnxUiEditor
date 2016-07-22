
using System;
using System.ComponentModel;
using System.Drawing;

namespace Structure
{

    /**
     * KNX 标准数据类型 DPT 的长度关系
     * 
     * 1-Bit - (DPT 1.*) Boolean  0 - 1  Value display, Blinds, Switch, Slider switch, Snapper switch, Button, Media button, Scene button  
     * 4-Bit - (DPT 3.*) Controlled/Dimming  Up 0 - 7, Down 0 - 7  Snapper, Snapper switch, Button, Media button  
     * 8-Bit - (DPT 5.*) Unsigned Value  0 - 255  Value display, Slider, Slider switch, RGB color light, Button, Media button, Scene button  
     * 8-Bit - (DPT 6.*) Signed Value  -128 - 127  Value display, Slider, Slider switch, Button, Media button, Scene button  
     * 16-Bit - (DPT 7.*) Unsigned Value  0 - 65535  Value display, Slider, Slider switch, Button, Media button, Scene button  
     * 16-Bit - (DPT 8.*) Signed Value  -32768 - 32767  Value display, Slider, Slider switch, Button, Media button, Scene button  
     * 16-Bit - (DPT 9.*) Float Value   Value display, Slider, Slider switch, Button, Media button, Scene button  
     * 32-Bit - (DPT 12.*) Unsigned Value  0 - 4294967295  Value display, Button, Media button  
     * 32-Bit - (DPT 13.*) Signed Value  -2147483648 - 2147483647  Value display, Button, Media button  
     * 32-Bit - (DPT 14.*) Float Value   Value display, Button, Media button  
     * 14-Byte - (DPT 16.*) String Value   Value display, Button, Media button 
     */

    /*
     *  底层数据长度定义和枚举之间的关系
     *  
     *  { 1, 2, 3, 4, 5, 6, 7, 8, 16, 24, 32, 48, 64, 80, 112 }
     *  
     *  对应关系：
     *  Bit1 --> 1
     *  Bit2 --> 2
     *  Bit3 --> 3
     *  Bit4 --> 4
     *  Bit5 --> 5
     *  Bit6 --> 6
     *  Bit7 --> 7
     *  Bit8 --> 8
     *  Bit16 --> 16
     *  Bit24 --> 24
     *  Bit32 --> 32
     *  Bit48 --> 48
     *  Bit64 --> 64
     *  Bit80 --> 80
     *  Byte14 --> 112
     */
    public enum KNXDataType
    {
        Bit1,
        Bit2,
        Bit3,
        Bit4,
        Bit5, 
        Bit6,
        Bit7,
        Bit8,
        Bit16,
        Bit24,
        Bit32,
        Bit48, 
        Bit64,
        Bit80, 
        Bit112,
        None
    }

    /*
     *  KNX 中开关的优先级和枚举的关系：
     *  System --> 0
     *  Normal --> 1
     *  Urgent --> 2
     *  Low --> 3
     * 
     */
    public enum KNXPriority
    {
        System = 0, Normal = 1, Urgent = 2, Low = 3
    }

    public enum ControlType
    {
        Blinds, Button,
    }

    //滑块两侧要显示的符号
    public enum SliderSymbol
    {
        None, DownUp, DardBright, SubtractAdd, Volume
    }

    /// <summary>
    /// 多媒体开关的按钮类型
    /// </summary>
    public enum MediaButtonType
    {
        Back = 0,
        Backward = 1,
        Menu = 2,
        Stop = 3,
        BackwardSkip = 4,
        Mute = 5,
        Up = 6,
        Okey = 7,
        VolumeDown = 8,
        Down = 9,
        Pause = 10,
        VolumeUp = 11,
        Forward = 12,
        Play = 13,
        ForwardSkip = 14,
        Power = 15,
        Left = 16,
        Right = 17
    }

    public enum Language
    {
        Chinese,
        English,
        French,
        German,
        Polish,
        Russian,
        Spanish,
        Japanese
    }

    public enum EMeasurementUnit
    {
        [Description("")]
        None,

        [Description("℃")]
        Centigrade,

        [Description("℉")]
        Fahrenheit,

        [Description("A")]
        Ampere,

        [Description("mA")]
        Milliampere,

        [Description("KW")]
        Kilowatt
    }

    /// <summary>
    /// 控件的平面样式
    /// </summary>
    public enum EFlatStyle
    {
        /// <summary>
        /// 扁平化
        /// </summary>
        Flat,

        /// <summary>
        /// 立体化
        /// </summary>
        Stereo,
    }

    public enum EBool
    {
        No,

        Yes
    }

    /// <summary>
    /// 界面元素的基础类，提供统一的基础属性
    /// </summary>    
    public class KNXView
    {
        /// <summary>
        /// 界面元素的ID，整个应用唯一
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 界面元素需要显示在前端的文字
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 控件的起始位置x
        /// </summary>
        public int Left { get; set; }

        /// <summary>
        /// 控件的起始位置y
        /// </summary>
        public int Top { get; set; }

        /// <summary>
        /// 控件的宽度
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 控件的宽度
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// 是否显示边框
        /// </summary>
        public int DisplayBorder { get; set; }

        /// <summary>
        /// 边框颜色
        /// </summary>
        public string BorderColor { get; set; }

        /// <summary>
        /// 控件的不透明度
        /// </summary>
        public double Alpha { get; set; }

        /// <summary>
        /// 控件的圆角半径
        /// </summary>
        public int Radius { get; set; }

        /// <summary>
        /// 控件的外观
        /// </summary>
        public int FlatStyle { get; set; }

        /// <summary>
        /// 控件的背景色
        /// </summary>
        public string BackgroundColor { get; set; }

        /// <summary>
        /// 控件的背景图片
        /// </summary>
        public string BackgroundImage { get; set; }

        /// <summary>
        /// 控件的字体颜色
        /// </summary>
        public string FontColor { get; set; }

        /// <summary>
        /// 字体大小
        /// </summary>
        public int FontSize { get; set; }
    }
}
