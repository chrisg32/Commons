using System.Text;

namespace CG.Commons.Extensions
{
    public static class StringBuilderExtensions
    {
        /// <summary>
        /// Converts the value of objects to strings based on the formats specified and appends the new string and the default line terminator, to the end of this instance.
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void AppendLine(this StringBuilder sb, string format, params object[] args)
        {
            sb.AppendLine(string.Format(format, args));
        }
    }
}
