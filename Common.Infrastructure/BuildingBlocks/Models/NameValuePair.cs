namespace Common.Infrastructure.Models
{
    public class NameValuePair
    {
        public NameValuePair(string name) { Name = name; }
        public NameValuePair(string name, string value) { Name = name; Value = value; }
        public string Name;
        public string Value;
    }
}
