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

        /// <summary>
        /// 获取枚举的备注信息
        /// </summary>
        /// <param name="val">枚举值</param>
        /// <returns></returns>
        //public static string GetEnumDescription(Enum val)
        //{
        //    Type type = val.GetType();
        //    FieldInfo fd = type.GetField(val.ToString());
        //    if (fd == null)
        //        return string.Empty;
        //    object[] attrs = fd.GetCustomAttributes(typeof(DescriptionAttribute), false);
        //    string name = string.Empty;
        //    foreach (DescriptionAttribute attr in attrs)
        //    {
        //        name = attr.Description;
        //    }
        //    return name;
        //}
    }
    /// <summary>
    /// 枚举扩展类
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// 获取枚举的备注信息
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        //public static string GetDescription(this Enum em)
        //{
        //    Type type = em.GetType();
        //    FieldInfo fd = type.GetField(em.ToString());
        //    if (fd == null)
        //        return string.Empty;
        //    object[] attrs = fd.GetCustomAttributes(typeof(DescriptionAttribute), false);
        //    string name = string.Empty;
        //    foreach (DescriptionAttribute attr in attrs)
        //    {
        //        name = attr.Description;
        //    }
        //    return name;
        //}

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
