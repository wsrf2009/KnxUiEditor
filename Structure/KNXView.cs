using System;
using System.ComponentModel;
using System.Drawing;

namespace Structure
{
    public enum ControlType
    {
        Blinds,
        Button
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

    public enum ERegulationStep
    {
        /// <summary>
        /// 0.01
        /// </summary>
        [Description("0.01")]
        PointZeroOne,

        /// <summary>
        /// 0.05
        /// </summary>
        [Description("0.05")]
        PointZeroFive,

        /// <summary>
        /// 0.1
        /// </summary>
        [Description("0.1")]
        PointOne,

        /// <summary>
        /// 0.5
        /// </summary>
        [Description("0.5")]
        PointFive,

        /// <summary>
        /// 1
        /// </summary>
        [Description("1")]
        One,

        /// <summary>
        /// 5
        /// </summary>
        [Description("5")]
        Five,
    }

    public enum EDecimalDigit
    {
        /// <summary>
        /// 无小数
        /// </summary>
        [Description("None")]
        Zero,

        /// <summary>
        /// 1位小数
        /// </summary>
        [Description(".x")]
        One,

        /// <summary>
        /// 2位小数
        /// </summary>
        [Description(".xx")]
        Two
    }

    public enum EMeasurementUnit
    {
        [Description("")]
        None,

        [Description("℃")]
        Centigrade,

        //[Description("℉")]
        //Fahrenheit,

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
        public string Title { get; set; }

        /// <summary>
        /// 已被弃用。2.1.1
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
        /// 控件的内边距
        /// 新增于2.7.1
        /// </summary>
        public KNXPadding Padding { get; set; }

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
        public float Alpha { get; set; }

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
        /// 弃用于 2.1.1
        /// </summary>
        public string BackgroundImage { get; set; }

        /// <summary>
        /// 控件的字体颜色
        /// 弃用于 2.5.2
        /// </summary>
        public string FontColor { get; set; }

        /// <summary>
        /// 字体大小
        /// 弃用于 2.5.2
        /// </summary>
        public int FontSize { get; set; }

        /// <summary>
        /// 新增于2.5.2
        /// </summary>
        public KNXFont TitleFont { get; set; }
    }
}
