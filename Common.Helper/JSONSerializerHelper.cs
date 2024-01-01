using Newtonsoft.Json;
using System.IO;

namespace Common.Helper
{
    public class JSONSerializerHelper
    {
        public static T Deserialize<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }

        public static string Serialize(object data)
        {
            return JsonConvert.SerializeObject(data, Formatting.None);
        }

        public static T Deserialize<T>(string data, string propertyKey)
        {
            using (var stringReader = new StringReader(data))
            {
                using (var jsonReader = new JsonTextReader(stringReader))
                {
                    while (jsonReader.Read())
                    {
                        if (jsonReader.TokenType == JsonToken.PropertyName && (string)jsonReader.Value == propertyKey)
                        {
                            jsonReader.Read();

                            var serializer = new JsonSerializer();
                            return serializer.Deserialize<T>(jsonReader);
                        }
                    }
                }
                return default(T);
            }
        }
    }
}
