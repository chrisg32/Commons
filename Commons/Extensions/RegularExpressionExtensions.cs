using System;
using System.Text;
using System.Text.RegularExpressions;

namespace CG.Commons.Extensions;

public static class RegularExpressionExtensions
{
    /// <summary>
    /// Replaces the regex capture groups in the input string with the specified replacements.
    /// </summary>
    /// <param name="regex">The regular expression.</param>
    /// <param name="input">The input string.</param>
    /// <param name="replacements">The replacements strings. Must have equal or greater number of values than groups.</param>
    /// <returns>The match string with the capture groups replaced with the corresponding replacement.</returns>
    public static string ReplaceGroups(this Regex regex, string input, params string[] replacements)
    {
        return regex.ReplaceGroups(input, index => replacements[index]);
    }

    /// <summary>
    /// Replaces the regex capture groups in the input string with the value returned by the specified replacement function.
    /// </summary>
    /// <param name="regex">The regular expression.</param>
    /// <param name="input">The input string.</param>
    /// <param name="replacement">A function that given a zero based index of the group being replaced, returns a string that will replace the capture group.</param>
    /// <returns>The match string with the capture groups replaced with the corresponding replacement.</returns>
    public static string ReplaceGroups(this Regex regex, string input, Func<int, string> replacement)
    {
        return regex.Replace(input, match =>
        {
            var index = 0;
            var sb = new StringBuilder();
            for(var g = 1; g < match.Groups.Count; g++)
            {
                sb.Append(match.Value[index..match.Groups[g].Index]);
                sb.Append(replacement(g - 1));
                index = match.Groups[g].Index + match.Groups[g].Length;
            }
            if(index < match.Length)
            {
                sb.Append(match.Value[index..]);
            }
            return sb.ToString();
        });
    }
}