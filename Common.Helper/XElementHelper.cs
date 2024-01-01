using System;
using System.Linq;
using System.Xml.Linq;

namespace Common.Helper
{
    public class XElementHelper
    {
        public static string GetStringElementValue(XDocument document, string element)
        {
            XNamespace ns = document.Root.Name.Namespace;
            var des = document.Descendants(XName.Get(element, ns.NamespaceName));
            if (des == null || des.Count() == 0) return null;
            return des.First()?.Value;
        }

        public static string GetStringElementValue(XElement element, string elementName)
        {
            XNamespace ns = element.Name.Namespace;
            var des = element.Descendants(XName.Get(elementName, ns.NamespaceName));
            if (des == null || des.Count() == 0) return null;
            return des.First()?.Value;
        }

        public static XElement GetStringElement(XDocument document, string element)
        {
            XNamespace ns = document.Root.Name.Namespace;
            var des = document.Descendants(XName.Get(element, ns.NamespaceName));
            if (des == null || des.Count() == 0) return null;
            return des.First();
        }

        public static DateTime? GetDateElementValue(XDocument document, string element)
        {
            XNamespace ns = document.Root.Name.Namespace;
            var des = document.Descendants(XName.Get(element, ns.NamespaceName));
            if (des == null || des.Count() == 0) return null;

            if (DateTime.TryParse(des.First()?.Value, out DateTime dateTime)) return dateTime;
            else return null;
        }
    }
}
