using Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UIEditor.Component;
using Utils;

namespace UIEditor.UserClass
{
    [TypeConverter(typeof(STFont.PropertyConverter)), Serializable]
    public class STFont
    {
        #region 常量
        private const int FONT_SIZE_MIN = 1;
        #endregion

        #region 属性
        public Color Color { get; set; }

        private int _size;
        public int Size
        {
            get
            {
                return this._size;
            }
            set
            {
                if (value < FONT_SIZE_MIN)
                {
                    MessageBox.Show(string.Format(UIResMang.GetString("Message58"), FONT_SIZE_MIN), UIResMang.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this._size = FONT_SIZE_MIN;
                }
                else
                {
                    this._size = value;
                }
            }
        }

        public bool Bold { get; set; }

        public bool Italic { get; set; }

        //[BrowsableAttribute(false)]
        public bool Strikeout { get; set; }

        //[BrowsableAttribute(false)]
        public bool Underline { get; set; }
        #endregion

        #region 构造函数
        public STFont()
        {

        }

        public STFont(KNXFont knx)
        {
            this.Color = ColorHelper.HexStrToColor(knx.Color);
            this.Size = knx.Size;
            this.Bold = knx.Bold;
            this.Italic = knx.Italic;
            this.Strikeout = knx.Strikeout;
            this.Underline = knx.Underline;
        }

        public STFont(string color, int size)
        {
            this.Color = ColorHelper.HexStrToColor(color);
            this.Size = size;
            this.Bold = false;
            this.Italic = false;
            this.Strikeout = false;
            this.Underline = false;
        }

        public STFont(Color color, int size)
        {
            this.Color = color;
            this.Size = size;
            this.Bold = false;
            this.Italic = false;
            this.Strikeout = false;
            this.Underline = false;
        }

        public STFont(Color color, int size, bool bold, bool italic, bool strikeout, bool underline)
        {
            this.Color = color;
            this.Size = size;
            this.Bold = bold;
            this.Italic = italic;
            this.Strikeout = strikeout;
            this.Underline = underline;
        }
        #endregion

        #region 公共方法
        public STFont Clone()
        {
            STFont f = new STFont();
            f.Color = this.Color;
            f.Size = this.Size;
            f.Bold = this.Bold;
            f.Italic = this.Italic;
            f.Strikeout = this.Strikeout;
            f.Underline = this.Underline;

            return f;
        }

        public FontStyle GetFontStyle()
        {
            FontStyle style = FontStyle.Regular;
            if (this.Bold)
            {
                style |= FontStyle.Bold;
            }
            if (this.Italic)
            {
                style |= FontStyle.Italic;
            }
            if (this.Strikeout)
            {
                style |= FontStyle.Strikeout;
            }
            if (this.Underline)
            {
                style |= FontStyle.Underline;
            }

            return style;
        }

        public Font GetFont(float ratio)
        {
            FontStyle style = GetFontStyle();
            Font font = new Font("宋体", (int)Math.Round(this.Size * ratio, 0), style);

            return font;
        }

        public KNXFont ToKnx()
        {
            KNXFont knx = new KNXFont();
            knx.Color = ColorHelper.ColorToHexStr(this.Color);
            knx.Size = this.Size;
            knx.Bold = this.Bold;
            knx.Italic = this.Italic;
            knx.Strikeout = this.Strikeout;
            knx.Underline = this.Underline;

            return knx;
        }
        #endregion

        #region 属性框显示
        private class PropertyConverter : ExpandableObjectConverter
        {
            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
            {
                PropertyDescriptorCollection collection = TypeDescriptor.GetProperties(value, true);

                List<PropertyDescriptor> list = new List<PropertyDescriptor>();

                STControlPropertyDescriptor PropSTFontColor = new STControlPropertyDescriptor(collection["Color"]);
                PropSTFontColor.SetCategory(UIResMang.GetString(""));
                PropSTFontColor.SetDisplayName(UIResMang.GetString("PropSTFontColor"));
                list.Add(PropSTFontColor);

                STControlPropertyDescriptor PropSTFontSize = new STControlPropertyDescriptor(collection["Size"]);
                PropSTFontSize.SetCategory(UIResMang.GetString(""));
                PropSTFontSize.SetDisplayName(UIResMang.GetString("PropSTFontSize"));
                list.Add(PropSTFontSize);

                STControlPropertyDescriptor PropSTFontBold = new STControlPropertyDescriptor(collection["Bold"]);
                PropSTFontBold.SetCategory(UIResMang.GetString(""));
                PropSTFontBold.SetDisplayName(UIResMang.GetString("PropSTFontBold"));
                list.Add(PropSTFontBold);

                STControlPropertyDescriptor PropSTFontItalic = new STControlPropertyDescriptor(collection["Italic"]);
                PropSTFontItalic.SetCategory(UIResMang.GetString(""));
                PropSTFontItalic.SetDisplayName(UIResMang.GetString("PropSTFontItalic"));
                list.Add(PropSTFontItalic);

                STControlPropertyDescriptor PropSTFontStrikeout = new STControlPropertyDescriptor(collection["Strikeout"]);
                PropSTFontStrikeout.SetCategory(UIResMang.GetString(""));
                PropSTFontStrikeout.SetDisplayName(UIResMang.GetString("PropSTFontStrikeout"));
                list.Add(PropSTFontStrikeout);

                STControlPropertyDescriptor PropSTFontUnderline = new STControlPropertyDescriptor(collection["Underline"]);
                PropSTFontUnderline.SetCategory(UIResMang.GetString(""));
                PropSTFontUnderline.SetDisplayName(UIResMang.GetString("PropSTFontUnderline"));
                list.Add(PropSTFontUnderline);

                return new PropertyDescriptorCollection(list.ToArray());
            }
        }
        #endregion
    }
}
