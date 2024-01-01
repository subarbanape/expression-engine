using Common.Helper;
using Common.Infrastructure.Expression.Interfaces;
using System.Collections.Generic;

namespace ExpressionEngine
{
    public class Macro : IMacro
    {
        public Macro() { Params = new Dictionary<string, object>(); }
        public Macro(string name, string macroExpression) : this()
        {
            Name = name;
            MacroExpression = macroExpression;
        }
        public string Name { get; private set; }
        public Dictionary<string, object> Params { get; private set; }
        public string MacroExpression { get; private set; }
        public void AddParamName(string name)
        {
            AddParam(name, string.Empty);
        }
        public void AddParamName(List<string> list, int index)
        {
            if (list == null || list.Count == 0 || list.Count <= index) return;
            AddParamName(StringHelper.TrimCurlyBraces(list[index]));
        }

        public void AddParam(string name, object value)
        {
            if (string.IsNullOrEmpty(name)) return;
            if (Params.ContainsKey(name)) Params[name] = value;
            else Params.Add(name, value);
        }
    }
}
