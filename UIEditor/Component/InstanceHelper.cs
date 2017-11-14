using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace UIEditor.Component
{
    class InstanceHelper
    {
        /// <summary>
        /// 根据对象的属性名称，获取属性的值（用于根据对象自动完成参数化sql语句的赋值操作）
        /// </summary>
        /// <param name="propertyName">属性名称(忽略大小写)</param>
        /// <param name="objectInstance">对象实例</param>
        /// <param name="objectType">对象实例类型</param>
        /// <returns>属性的值</returns>
        public static object GetPropertyValue(string propertyName, object objectInstance, Type objectType)
        {
            PropertyInfo pi = objectType.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (pi == null)
                throw new ArgumentException("自动设置参数值失败。参数化变量名称" + propertyName + "必须和对象中的属性名称一样。");
            if (!pi.CanRead)
                throw new ArgumentException("自动设置参数值失败。对象的" + propertyName + "属性没有get方式，无法读取值。");

            object value = pi.GetValue(objectInstance, null);
            if (value == null)
                value = DBNull.Value;
            else if (pi.PropertyType.Name == "DateTime" && Convert.ToDateTime(value) == DateTime.MinValue)
                value = DBNull.Value;//防止数据库是smalldatetime类型时DateTime.MinValue溢出
            return value;
        }

        /// <summary>
        /// 将DataReader中的数据自动赋值到对象实例对应的属性
        /// 注意：对象实例的属性名称必须和数据库列名相同，可忽略大小写
        /// </summary>
        /// <param name="dataReader">SqlDataReader等数据阅读器获取的一行数据</param>
        /// <param name="objectInstance">对象实例</param>
        /// <param name="objectType">对象实例类型</param>
        public static void SetPropertyValue(IDataReader dataReader, object objectInstance, Type objectType)
        {
            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                PropertyInfo pi = objectType.GetProperty(dataReader.GetName(i), BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (pi != null && pi.CanWrite && dataReader[i] != DBNull.Value)
                {
                    //如果是int?、bool?、double?等这种可空类型，获取其实际类型，如int?的实际类型是int
                    Type baseType = Nullable.GetUnderlyingType(pi.PropertyType);
                    if (baseType != null)
                        pi.SetValue(objectInstance, Convert.ChangeType(dataReader[i], baseType), null);
                    else
                        pi.SetValue(objectInstance, Convert.ChangeType(dataReader[i], pi.PropertyType), null);//设置对象值
                }
            }
        }
    }
}
