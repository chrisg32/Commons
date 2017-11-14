using System;
using CG.Commons.Util;

namespace CG.Commons.Extensions
{
    public static class StringExtensions
    {
        public static T Parse<T>(this string stringValue)
        {
            return GenericParser.Parse<T>(stringValue);
        }

        public static object Parse(this string stringValue, Type type)
        {
            return GenericParser.Parse(stringValue, type);
        }
    }
}
