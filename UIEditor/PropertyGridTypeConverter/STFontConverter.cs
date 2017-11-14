using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIEditor.UserClass;
using Utils;

namespace UIEditor.Component
{
    public class STFontConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (typeof(STFont) == destinationType)
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            STFont f = value as STFont;
            if ((typeof(string) == destinationType) && (null != f))
            {
                //string valString = "Color:" + ColorHelper.ColorToHexStr(f.Color);
                //valString += ";" + "Size:" + f.Size;
                //valString += ";" + "Bold:" + f.Bold;
                //valString += ";" + "Italic:" + f.Italic;
                //valString += ";" + "Strikeout:" + f.Strikeout;
                //valString += ";" + "Underline:" + f.Underline;
                //return valString;
                return f.Size.ToString();
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        //public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        //{
        //    if (typeof(string) == sourceType)
        //    {
        //        return true;
        //    }

        //    return base.CanConvertFrom(context, sourceType);
        //}

        //public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        //{
        //    string s = value as string;
        //    if (null != s)
        //    {
        //        try
        //        {
        //            Color color = Color.Empty;
        //            int size = 0;
        //            bool bold = false;
        //            bool italic = false;
        //            bool strikeout = false;
        //            bool underline = false;
        //            string[] properties = s.Split(';');
        //            foreach (string pro in properties)
        //            {
        //                string[] p = pro.Split(':');
        //                if ("Color" == p[0])
        //                {
        //                    color = ColorHelper.HexStrToColor(p[1]);
        //                }
        //                else if ("Size" == p[0])
        //                {
        //                    size = int.Parse(p[1]);
        //                }
        //                else if ("Bold" == p[0])
        //                {
        //                    bold = bool.Parse(p[1]);
        //                }
        //                else if ("Italic" == p[0])
        //                {
        //                    italic = bool.Parse(p[1]);
        //                }
        //                else if ("Strikeout" == p[0])
        //                {
        //                    strikeout = bool.Parse(p[1]);
        //                }
        //                else if ("Underline" == p[0])
        //                {
        //                    underline = bool.Parse(p[1]);
        //                }
        //            }

        //            return new STFont(color, size, bold, italic, strikeout, underline);
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //        }
        //    }

        //    return base.ConvertFrom(context, culture, value);
        //}
    }
}
