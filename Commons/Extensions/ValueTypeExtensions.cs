using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.Extensions
{
    public static class ValueTypeExtensions
    {
        /// <summary>
        /// Converts any value type to a Nullable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T? ToNullable<T>(this T t) where T : struct
        {
            return new Nullable<T>(t);
        }
    }
}
