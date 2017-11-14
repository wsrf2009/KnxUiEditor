using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIEditor.UserClass;

namespace UIEditor.Component
{
    public class STStringConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (typeof(STString) == destinationType)
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if ((typeof(String) == destinationType) && (value is STString))
            {
                STString ststring = (STString)value;
                return ststring.Text;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            //if (typeof(String) == sourceType)
            //{
            //    return true;
            //}

            //return base.CanConvertFrom(context, sourceType);

            return true;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                try
                {
                    string s = (string)value;
                    STString ststring = new STString();
                    ststring.Text = s;

                    return ststring;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}
