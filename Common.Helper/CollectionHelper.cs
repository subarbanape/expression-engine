using System;
using System.Collections.Generic;

namespace Common.Helper
{
    public class CollectionHelper
    {
        public static void TryAdd(Dictionary<string, string> dict, string key, string value)
        {
            if (dict == null || string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value)) return;
            if (dict.ContainsKey(key)) dict[key] = value;
            else dict.TryAdd(key, value);
        }

        public static void TryAdd<T>(Dictionary<string, T> dict, string key, T value)
        {
            if (dict == null || string.IsNullOrEmpty(key) || value == null) return;
            if (dict.ContainsKey(key)) dict[key] = value;
            else dict.TryAdd(key, value);
        }

        public static string TryGetValue(Dictionary<string, string> dict, string key)
        {
            if (dict == null || string.IsNullOrEmpty(key)) return string.Empty;
            if (dict.ContainsKey(key)) return dict[key];
            else return string.Empty;
        }
        public static Dictionary<string, string> Copy(Dictionary<string, string> source, Dictionary<string, string> dest)
        {
            if (source == null || source.Count == 0) return dest;
            if (dest == null) return dest;

            foreach (var item in source)
            {
                dest.Add(item.Key, item.Value);
            }

            return dest;
        }

        public static List<T> MergeArrays<T>(T[] array1, T[] array2)
        {
            var list = new List<T>();
            list.AddRange(array1);
            list.AddRange(array2);
            return list;
        }
    }
}
