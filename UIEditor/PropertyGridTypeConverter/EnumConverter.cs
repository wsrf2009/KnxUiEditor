using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using UIEditor.Component;

namespace UIEditor.PropertyGridTypeConverter
{
    /// <summary>
    /// 枚举转换器
    /// 用此类之前，必须保证在枚举项中定义了Description
    /// </summary>
    public class EnumConverter : ExpandableObjectConverter
    {
        /// <summary>
        /// 枚举项集合
        /// </summary>
        Dictionary<object, string> dic;

        /// <summary>
        /// 构造函数
        /// </summary>
        public EnumConverter()
        {
            dic = new Dictionary<object, string>();
        }
        /// <summary>
        /// 加载枚举项集合
        /// </summary>
        /// <param name="context"></param>
        private void LoadDic(ITypeDescriptorContext context)
        {
            dic = EnumExtension.GetEnumValueDesDic(context.PropertyDescriptor.PropertyType);
        }

        /// <summary>
        /// 是否可从来源转换
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;

            return base.CanConvertFrom(context, sourceType);
        }
        /// <summary>
        /// 从来源转换
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is string)
            {
                //如果是枚举
                if (context.PropertyDescriptor.PropertyType.IsEnum)
                {
                    if (dic.Count <= 0)
                        LoadDic(context);
                    if (dic.Values.Contains(value.ToString()))
                    {
                        foreach (object obj in dic.Keys)
                        {
                            if (dic[obj] == value.ToString())
                            {
                                return obj;
                            }
                        }
                    }
                }
            }

            return base.ConvertFrom(context, culture, value);
        }
        /// <summary>
        /// 是否可转换
        /// </summary>
        /// <param name="context"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            if (dic == null || dic.Count <= 0)
                LoadDic(context);

            StandardValuesCollection vals = new TypeConverter.StandardValuesCollection(dic.Keys);

            return vals;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (dic.Count <= 0)
                LoadDic(context);

            foreach (object key in dic.Keys)
            {
                if (key.ToString() == value.ToString() || dic[key] == value.ToString())
                {
                    return dic[key].ToString();
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        

    }
}
