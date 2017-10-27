using System;
using System.Collections.Generic;
using System.Text;
using Commons.Util;

namespace Commons.Extensions
{
    public static class StringExtensions
    {
        public static T Parse<T>(string stringValue)
        {
            return GenericParser.Parse<T>(stringValue);
        }
    }
}
