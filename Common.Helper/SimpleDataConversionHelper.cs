namespace Common.Helper
{
    public class SimpleDataConversionHelper
    {
        public static int ToInt(string str)
        {
            int intVal = -1;
            int.TryParse(str, out intVal);
            return intVal;
        }
    }
}
