using System.Reflection;

namespace Common.Helper
{
    public static class ReflectionHelper
    {
        public static object CastToConcreteType(object obj)
        {
            MethodInfo castMethod = obj.GetType().GetMethod("Cast").MakeGenericMethod(obj.GetType());
            return castMethod.Invoke(null, new object[] { obj });
        }
    }
}
