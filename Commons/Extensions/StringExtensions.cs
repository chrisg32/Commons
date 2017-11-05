using CG.Commons.Util;

namespace CG.Commons.Extensions
{
    public static class StringExtensions
    {
        public static T Parse<T>(string stringValue)
        {
            return GenericParser.Parse<T>(stringValue);
        }
    }
}
