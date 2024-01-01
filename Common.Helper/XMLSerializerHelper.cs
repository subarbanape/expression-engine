using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Common.Helper
{
    public class XMLSerializerHelper
    {
        public static string Serialize(object objectInstance)
        {
            var serializer = new XmlSerializer(objectInstance.GetType());
            var sb = new StringBuilder();

            using (TextWriter writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, objectInstance);
            }

            return sb.ToString();
        }

        public static T Deserialize<T>(string objectData)
        {
            return (T)Deserialize(objectData, typeof(T));
        }

        public static object Deserialize(string objectData, Type type)
        {
            var serializer = new XmlSerializer(type);
            object result;

            using (TextReader reader = new StringReader(objectData))
            {
                result = serializer.Deserialize(reader);
            }

            return result;
        }
    }
}
