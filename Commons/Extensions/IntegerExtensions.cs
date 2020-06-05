using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

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

        public static void Times(this int count, Action<int> action)
        {
            for (var i = 0; i < count; i++)
            {
                action(i);
            }
        }

        public static IEnumerable<TResult> Times<TResult>(this int count, Func<TResult> function)
        {
            for (var i = 0; i < count; i++)
            {
                yield return function();
            }
        }

        public static IEnumerable<TResult> Times<TResult>(this int count, Func<int, TResult> function)
        {
            for (var i = 0; i < count; i++)
            {
                yield return function(i);
            }
        }

        public static IEnumerable<int> Iterations(this int count)
        {
            for (var i = 0; i < count; i++)
            {
                yield return i;
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
