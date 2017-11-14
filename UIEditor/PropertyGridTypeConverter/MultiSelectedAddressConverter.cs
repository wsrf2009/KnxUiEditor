using Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIEditor.Component;

namespace UIEditor.Component
{
    public class MultiSelectedAddressConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (typeof(Dictionary<string, KNXSelectedAddress>) == destinationType)
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if ((typeof(string) == destinationType) && (value is Dictionary<string, KNXSelectedAddress>))
            {
                string valString = "";
                foreach (var item in (Dictionary<string, KNXSelectedAddress>)value)
                {
                    if (!string.IsNullOrEmpty(valString))
                    {
                        valString += ";";
                    }

                    valString += item.Value.Name;

                }
                return valString;
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
        //    if (value is string)
        //    {
        //        try
        //        {
        //            string s = (string)value;
        //            return Convert.ToDouble(s);
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
