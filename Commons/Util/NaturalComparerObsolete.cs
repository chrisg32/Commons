using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CG.Commons.Util
{
    [Obsolete]
    public class NaturalComparerObsolete : IComparer<string>, IComparer
    {
        private readonly bool _ignoreCase;
        private readonly bool _ignoreWhitespace;
        private readonly bool _checkTrailingDecimalLength;
        private readonly bool _lowercaseFirst;
        private readonly bool _decimalPrecision;

        public NaturalComparerObsolete(NaturalComparerOptions options = NaturalComparerOptions.None)
        {
            _ignoreCase = options.HasFlag(NaturalComparerOptions.IgnoreCase);
            _ignoreWhitespace = options.HasFlag(NaturalComparerOptions.IgnoreWhiteSpace);
            _checkTrailingDecimalLength = options.HasFlag(NaturalComparerOptions.CheckTrailingDecimalLength);
            _lowercaseFirst = options.HasFlag(NaturalComparerOptions.LowercaseFirst);
            _decimalPrecision = options.HasFlag(NaturalComparerOptions.DecimalPrecision);
        }

        //less than zero = x is less than y
        //zero = x equals y
        //greater than zero = x is greater than y

        /// <summary>
        /// Compares two strings using a natural sort. Nulls and empty strings are treated as equal.
        /// </summary>
        /// <param name="left">The left string to compare.</param>
        /// <param name="right">The right string to compare.</param>
        /// <returns>If the left string is less than the right the return value will be less than zero.
        ///  If the left string is greater than the right than a value greater than zero is returned.
        ///  If the left string is equal to the right string zero is returned.</returns>
        public int Compare(string left, string right)
        {
            //treat null and empty strings the same, also ignore leading and trailing whitespace
            var x = left?.Trim() ?? string.Empty;
            var y = right?.Trim() ?? string.Empty;

            if (_ignoreCase)
            {
                x = x.ToLower();
                y = y.ToLower();
            }

            if (_ignoreWhitespace)
            {
                var regex = new Regex(@"\s");
                x = regex.Replace(x, string.Empty);
                y = regex.Replace(y, string.Empty);
            }

            //we can't use iterator since we need to see the next value
            var xarray = x.ToCharArray();
            var yarray = y.ToCharArray();
            int xindex = -1, yindex = -1;
            while (true)
            {
                var xhas = xindex + 1 < xarray.Length;
                var yhas = yindex + 1 < yarray.Length;

                //neither string has another to compare so they are equal
                if (!(xhas || yhas)) return 0;

                //if one string is shorter than the other than the shorter comes first
                if (!xhas) return -1;
                if (!yhas) return 1;

                xindex++;
                yindex++;

                var xchar = xarray[xindex];
                var ychar = yarray[yindex];

                var xIsNum = char.IsDigit(xchar);
                var yIsNum = char.IsDigit(ychar);

                //if both characters are numeric then we have to compare the full numeric string part
                if (xIsNum && yIsNum)
                {
                    var xnums = GetNumericString(xarray, ref xindex, _decimalPrecision);
                    var ynums = GetNumericString(yarray, ref yindex, _decimalPrecision);
                    var xnum = decimal.Parse(xnums);
                    var ynum = decimal.Parse(ynums);
                    var iresult = xnum.CompareTo(ynum);
                    if (_checkTrailingDecimalLength && iresult == 0 && xnums.Length != ynums.Length)
                    {
                        return xnums.Length.CompareTo(ynums.Length);
                    }
                    if (iresult != 0) return iresult;
                    continue;
                }

                //if one char is numeric but the other is not then the numeric comes first
                if (xIsNum) return -1;
                if (yIsNum) return 1;

                //both characters are not numeric so we simply compare
                var cresult = CapitalOrderComparison(xchar, ychar);
                if (cresult != 0) return cresult;
            }

        }

        private int CapitalOrderComparison(char x, char y)
        {
            var result = x.CompareTo(y);
            if (result == 0) return result;
            if (char.IsLetter(x) && char.IsLetter(y))
            {
                result = char.ToLowerInvariant(x).CompareTo(char.ToLowerInvariant(y));
                if (result != 0) return result;
                if (_lowercaseFirst)
                {
                    if (!char.IsLower(x) && char.IsLower(y)) return -1;
                    if (char.IsLower(x) && !char.IsLower(y)) return 1;
                }
                else
                {
                    if (!char.IsLower(x) && char.IsLower(y)) return 1;
                    if (char.IsLower(x) && !char.IsLower(y)) return -1;
                }
            }
            return result;
        }

        private static string GetNumericString(IReadOnlyList<char> source, ref int index, bool decimalPrecision)
        {
            var sb = new StringBuilder();
            var point = true;
            while (index < source.Count && (char.IsDigit(source[index]) || decimalPrecision && point && source[index] == '.'))
            {
                if (source[index] == '.') point = false;
                sb.Append(source[index]);
                index++;
            }
            index--;
            return sb.ToString();
        }

        public int Compare(object x, object y)
        {
            return Compare(x?.ToString(), y?.ToString());
        }
    }
}