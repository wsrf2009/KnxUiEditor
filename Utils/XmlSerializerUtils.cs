using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Utils
{
    /// <summary>
    /// 数据对象序列化为XML和发序列化的工具
    /// </summary>
    public class XmlSerializerUtils
    {
        /// <summary>
        /// 对象序列化为XML
        /// </summary>
        /// <typeparam name="T">Type of object to serialize.</typeparam>
        /// <param name="item">Object to serialize.</param>
        /// <returns>XML contents representing the serialized object.</returns>
        public static string XmlSerialize<T>(T item)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StringBuilder stringBuilder = new StringBuilder();
            using (StringWriter writer = new StringWriter(stringBuilder))
            {
                serializer.Serialize(writer, item);
            }
            return stringBuilder.ToString();
        }


        /// <summary>
        /// 对象序列华为XML
        /// </summary>
        /// <param name="item">Object to serialize.</param>
        /// <returns>XML contents representing the serialized object.</returns>
        public static string XmlSerialize(object item)
        {
            Type type = item.GetType();
            XmlSerializer serializer = new XmlSerializer(type);
            StringBuilder stringBuilder = new StringBuilder();
            using (StringWriter writer = new StringWriter(stringBuilder))
            {
                serializer.Serialize(writer, item);
            }
            return stringBuilder.ToString();
        }


        /// <summary>
        /// 发序列化xml字符串为对象，泛型方法
        /// </summary>
        /// <typeparam name="T">Type of object to deserialize.</typeparam>
        /// <param name="xmlData">XML contents with serialized object.</param>
        /// <returns>Deserialized object.</returns>
        public static T XmlDeserialize<T>(string xmlData)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (TextReader reader = new StringReader(xmlData))
            {
                T entity = (T)serializer.Deserialize(reader);
                return entity;
            }
        }
    }
}
