using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Globalization;
using UIEditor.Component;

namespace UIEditor.Component
{
    public class STImageConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (typeof(Image) == destinationType)
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            Image img = value as Image;
            if ((typeof(string) == destinationType) && (null != img))
            {
                return img.ToString();
            }
            else
            {
                return UIResMang.GetString("None");
            }

            //return base.ConvertTo(context, culture, value, destinationType);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (typeof(string) == sourceType)
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            //string s = value as string;
            //if (null != s)
            //{
                try
                {
                    return base.ConvertFrom(context, culture, value);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return null;
            //}

            //return base.ConvertFrom(context, culture, value);
        }
    }
}
