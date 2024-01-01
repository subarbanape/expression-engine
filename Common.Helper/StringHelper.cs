using Common.Infrastructure.Models;
using System.Collections.Generic;

namespace Common.Helper
{
    public class StringHelper
    {
        public static string TrimCurlyBraces(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            return input.Trim().Trim('{').Trim('}').Trim();
        }

        public static string GetValueBetweenBrackets(string input, int startIndex, char openBrace = '{', char closeBrace = '}')
        {
            if (string.IsNullOrEmpty(input) || !input.Contains(openBrace) || !input.Contains(closeBrace)) return string.Empty;
            if (startIndex < 0 || startIndex >= input.Length) return string.Empty;

            var openBracePositions = new Stack<int>();
            int openBraceIndex = -1;
            int closeBraceIndex = -1;
            for (int index = startIndex; index < input.Length; index++)
            {
                var character = input[index];
                if (character == openBrace) { openBracePositions.Push(index); }
                else if (character == closeBrace)
                {
                    if (openBracePositions.Count == 1)
                    {
                        openBraceIndex = openBracePositions.Pop();
                        closeBraceIndex = index; break;
                    }
                    else openBracePositions.Pop();
                }
            }

            if (openBraceIndex == -1 || closeBraceIndex == -1) return string.Empty;
            if (openBraceIndex > closeBraceIndex) return string.Empty;

            int braceStartIndex = openBraceIndex;
            int braceEndIndex = closeBraceIndex;
            int substringLength = braceEndIndex - braceStartIndex;

            if (braceStartIndex < 0 || braceStartIndex > input.Length) return string.Empty;
            if (substringLength < 0 || substringLength > input.Length) return string.Empty;

            return input.Substring(braceStartIndex, substringLength).Trim(openBrace).Trim(closeBrace);
        }
        public static bool IsSpace(char input) => input == ' ';
        public static bool IsComma(char input) => input == ',';
        public static bool IsCurlyBrace(char input) => input == '{' || input == '}';
        public static string GetValueBetweenCommas(string input, int startIndex)
        {
            if (string.IsNullOrEmpty(input) || !input.Contains(',')) return string.Empty;
            if (startIndex < 0 || startIndex >= input.Length) return string.Empty;
            int firstCommaIndex = -1;
            int secondCommaIndex = -1;

            for (int index = startIndex; index < input.Length; index++)
            {
                var character = input[index];
                if (character == ',')
                {
                    if (firstCommaIndex == -1) { firstCommaIndex = index; continue; }
                    if (secondCommaIndex == -1) secondCommaIndex = index;
                    if (firstCommaIndex != -1 && secondCommaIndex != -1) break;
                }
            }

            if (firstCommaIndex == -1 || secondCommaIndex == -1) return string.Empty;
            int substringLength = (secondCommaIndex - firstCommaIndex);

            if (firstCommaIndex < 0 || firstCommaIndex > input.Length) return string.Empty;
            if (substringLength < 0 || substringLength > input.Length) return string.Empty;

            return input.Substring(firstCommaIndex, substringLength).Trim(',');
        }

        public static string GetValueBetweenChars(string input, char openChar, char closeChar, int startIndex)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            if (!input.Contains(openChar) || !input.Contains(closeChar)) return string.Empty;
            if (startIndex < 0 || startIndex >= input.Length) return string.Empty;
            int firstCharIndex = -1;
            int secondCharIndex = -1;

            for (int index = startIndex; index < input.Length; index++)
            {
                var character = input[index];
                if (character == ',')
                {
                    if (firstCharIndex == -1) firstCharIndex = index;
                    if (secondCharIndex == -1) secondCharIndex = index;
                    if (firstCharIndex != -1 && secondCharIndex != -1) break;
                }
            }

            if (firstCharIndex == -1 || secondCharIndex == -1) return string.Empty;
            int substringLength = input.Length - secondCharIndex;

            if (firstCharIndex < 0 || firstCharIndex > input.Length) return string.Empty;
            if (substringLength < 0 || substringLength > input.Length) return string.Empty;

            return input.Substring(firstCharIndex, substringLength);
        }

        public static NameValuePair SplitOnFirstOccurence(string input, char charToSplit)
        {
            if (string.IsNullOrEmpty(input)) return null;
            if (!input.Contains(charToSplit)) return new NameValuePair(input);
            int firstCharToSplitIndex = input.IndexOf(charToSplit, 0);
            var name = input.Substring(0, firstCharToSplitIndex);
            var value = input.Substring(firstCharToSplitIndex + 1, input.Length - firstCharToSplitIndex - 1);
            return new NameValuePair(name.Trim(), value.Trim());
        }
    }
}
