namespace Common.Helper
{
    public class AlternateValuesHelper
    {
        public static bool? GetBoolFromString(string input)
        {
            input = input.ToLower();
            if (input == "yes" || input == "true" || input == "1") return true;
            else if (input == "no" || input == "false" || input == "0") return false;
            else return null;
        }
    }
}
