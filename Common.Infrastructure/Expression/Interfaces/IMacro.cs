using System.Collections.Generic;

namespace Common.Infrastructure.Expression.Interfaces
{
    public interface IMacro
    {
        public string Name { get; }
        public Dictionary<string, object> Params { get; }
        public string MacroExpression { get; }
    }
}
