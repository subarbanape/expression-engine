using System;
using System.Text;

namespace Common.Helper
{
    public static class RandomHelper
    {
        private static Random random = new Random((int)DateTime.Now.Ticks);//thanks to McAden

        public static string RandomString(int size=10)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
                for (int i = 0; i < size; i++)
                {
                    ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                    builder.Append(ch);
                }

            return builder.ToString();
        }
    }
}
