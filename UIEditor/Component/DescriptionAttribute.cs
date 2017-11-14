using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.ComponentModel;

namespace UIEditor.Component
{
    class Description : Attribute
    {
        private string _desc;
        public Description(string desc)
        {
            this._desc = desc;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Desc
        {
            get { return _desc; }
            set { _desc = value; }
        }
    }

    /// <summary>
    /// 枚举扩展类
    /// </summary>
    public static class EnumExtension
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes =
                  (DescriptionAttribute[])fi.GetCustomAttributes(
                  typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }

        ///<summary>  
        /// 获取枚举项+描述  
        ///</summary>  
        ///<param name="enumType">Type,该参数的格式为typeof(需要读的枚举类型)</param>  
        ///<returns>键值对</returns>  
        /// <summary>
        /// 记载枚举的值+描述
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static Dictionary<object, string> GetEnumValueDesDic(Type enumType)
        {
            Dictionary<object, string> dic = new Dictionary<object, string>();
            FieldInfo[] fieldinfos = enumType.GetFields();
            foreach (FieldInfo field in fieldinfos)
            {
                if (field.FieldType.IsEnum)
                {
                    Object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (objs.Length > 0)
                    {
                        dic.Add(Enum.Parse(enumType, field.Name), ((DescriptionAttribute)objs[0]).Description);
                    }
                }

            }

            return dic;
        }

        public static object ConvertFrom(Type enumType, string description)
        {
            Dictionary<object, string> dic = GetEnumValueDesDic(enumType);

            foreach (object obj in dic.Keys)
            {
                if (dic[obj] == description)
                {
                    return obj;
                }
            }

            return null;
        }
    }
}
