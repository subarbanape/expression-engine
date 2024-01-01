using Common.Infrastructure.Expression.Interfaces;
using System.Text.RegularExpressions;

namespace Common.Helper
{
    public class RegexHelper
    {
        public static RegexMatchResult Match(string pattern, string expression)
        {
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            Match match = regex.Match(expression);
            return new RegexMatchResult() { Success = match.Success, Value = match.Value };
        }

    }
}
