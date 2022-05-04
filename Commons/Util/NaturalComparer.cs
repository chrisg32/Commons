using System.Collections;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

namespace CG.Commons.Util
{
    public class NaturalComparer : IComparer<string>, IComparer
    {
        private readonly bool _checkTrailingDecimalLength;
        private readonly bool _decimalPrecision;
        private readonly bool _ignoreWhitespace;
        private readonly Regex _whitespaceRegex;
        private readonly Func<char, char, int> _singleCharComparer;

        public NaturalComparer(NaturalComparerOptions options = NaturalComparerOptions.None)
        {
            _checkTrailingDecimalLength = options.HasFlag(NaturalComparerOptions.CheckTrailingDecimalLength);
            _decimalPrecision = options.HasFlag(NaturalComparerOptions.DecimalPrecision);

            _singleCharComparer = options.HasFlag(NaturalComparerOptions.IgnoreCase) ? IgnoreCaseComparison :
                options.HasFlag(NaturalComparerOptions.LowercaseFirst) ? LowercaseFirstComparison : NormalComparison;

            _ignoreWhitespace = options.HasFlag(NaturalComparerOptions.IgnoreWhiteSpace);
            if (_ignoreWhitespace)
            {
                _whitespaceRegex = new Regex(@"\s", RegexOptions.Compiled);
            }
        }

        /// <summary>
        /// Compares two strings using a natural sort. Nulls and empty strings are treated as equal.
        /// </summary>
        /// <param name="left">The left string to compare.</param>
        /// <param name="right">The right string to compare.</param>
        /// <returns>If the left string is less than the right the return value will be less than zero.
        ///  If the left string is greater than the right than a value greater than zero is returned.
        ///  If the left string is equal to the right string zero is returned.</returns>
        public int Compare(string left, string right) => Compare(left ?? ReadOnlySpan<char>.Empty, right?? ReadOnlySpan<char>.Empty);

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
        public int Compare(ReadOnlySpan<char> left, ReadOnlySpan<char> right)
        {
            if (_ignoreWhitespace)
            {
                //TODO this is still not efficient, heap allocation and regular expression
                left = _whitespaceRegex.Replace(left.ToString(), string.Empty);
                right = _whitespaceRegex.Replace(right.ToString(), string.Empty);
            }
            else
            {
                left = left.Trim();
                right = right.Trim();
            }
            
            //we can't use iterator since we need to see the next value
            int xindex = -1, yindex = -1;
            while (true)
            {
                var xhas = xindex + 1 < left.Length;
                var yhas = yindex + 1 < right.Length;

                //neither string has another to compare so they are equal
                if (!(xhas || yhas)) return 0;

                //if one string is shorter than the other than the shorter comes first
                if (!xhas) return -1;
                if (!yhas) return 1;

                xindex++;
                yindex++;

                var xchar = left[xindex];
                var ychar = right[yindex];

                var xIsNum = char.IsDigit(xchar);
                var yIsNum = char.IsDigit(ychar);

                //if both characters are numeric then we have to compare the full numeric string part
                if (xIsNum && yIsNum)
                {
                    var xnums = GetNumericString(left, ref xindex, _decimalPrecision);
                    var ynums = GetNumericString(right, ref yindex, _decimalPrecision);
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
                var cresult = _singleCharComparer(xchar, ychar);
                if (cresult != 0) return cresult;
            }

        }
        
        private static int IgnoreCaseComparison(char x, char y) => char.ToLowerInvariant(x).CompareTo(char.ToLowerInvariant(y));

        private static int LowercaseFirstComparison(char x, char y)
        {
            var result = x.CompareTo(y);
            if (result == 0) return result;
            if (char.IsLetter(x) && char.IsLetter(y))
            {
                result = char.ToLowerInvariant(x).CompareTo(char.ToLowerInvariant(y));
                if (result != 0) return result;
                if (!char.IsLower(x) && char.IsLower(y)) return -1;
                if (char.IsLower(x) && !char.IsLower(y)) return 1;
            }
            return result;
        }
        
        private static int NormalComparison(char x, char y)
        {
            var result = x.CompareTo(y);
            if (result == 0) return result;
            if (char.IsLetter(x) && char.IsLetter(y))
            {
                result = char.ToLowerInvariant(x).CompareTo(char.ToLowerInvariant(y));
                if (result != 0) return result;
                if (!char.IsLower(x) && char.IsLower(y)) return 1;
                if (char.IsLower(x) && !char.IsLower(y)) return -1;
            }
            return result;
        }

        private static ReadOnlySpan<char> GetNumericString(ReadOnlySpan<char> source, ref int index, bool decimalPrecision)
        {
            var point = true;
            var start = index;
            while (index < source.Length && (char.IsDigit(source[index]) || decimalPrecision && point && source[index] == '.'))
            {
                if (source[index] == '.') point = false;
                index++;
            }
            index--;
            return source.Slice(start, index - start + 1);
        }

        public int Compare(object x, object y) => Compare(x?.ToString(), y?.ToString());
    }
}