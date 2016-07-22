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
    }
}
