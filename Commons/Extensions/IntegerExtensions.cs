using System;

namespace CG.Commons.Extensions
{
    public static class IntegerExtensions
    {
        public static void Times(this int count, Action action)
        {
            for (var i = 0; i < count; i++)
            {
                action();
            }
        }

        public static bool IsEven(this int i)
        {
            return i % 2 == 0;
        }

        public static bool IsOdd(this int i)
        {
            return i % 2 != 0;
        }
    }
}
