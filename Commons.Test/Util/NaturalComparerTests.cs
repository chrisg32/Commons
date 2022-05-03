using System;
using System.Collections.Generic;
using CG.Commons.Util;
using Xunit;

namespace CG.Commons.Test.Util
{
    public class NaturalComparerTests
    {
        public enum ComparerEquality
        {
            LessThan = -1,
            Equal = 0,
            GreaterThan = 1
        }

        [Theory]
        //variable length
        [InlineData("a", "aa", ComparerEquality.LessThan)]
        [InlineData("aa", "a", ComparerEquality.GreaterThan)]
        //character & character
        [InlineData("a", "b", ComparerEquality.LessThan)]
        [InlineData("b", "a", ComparerEquality.GreaterThan)]
        [InlineData("a", "a", ComparerEquality.Equal)]
        [InlineData("a", "A", ComparerEquality.GreaterThan, NaturalComparerOptions.LowercaseFirst)]
        [InlineData("A", "a", ComparerEquality.GreaterThan)]
        //single number & character
        [InlineData("4", "a", ComparerEquality.LessThan)]
        [InlineData("a", "4", ComparerEquality.GreaterThan)]
        //single digit & single digit
        [InlineData("4", "6", ComparerEquality.LessThan)]
        [InlineData("7", "6", ComparerEquality.GreaterThan)]
        [InlineData("5", "5", ComparerEquality.Equal)]
        //double digit & single digit
        [InlineData("4", "16", ComparerEquality.LessThan)]
        [InlineData("17", "6", ComparerEquality.GreaterThan)]
        [InlineData("5", "05", ComparerEquality.Equal)]
        //double digit & double digit
        [InlineData("14", "16", ComparerEquality.LessThan)]
        [InlineData("17", "16", ComparerEquality.GreaterThan)]
        [InlineData("15", "15", ComparerEquality.Equal)]
        //decimal & decimal
        [InlineData("1.4", "1.6", ComparerEquality.LessThan)]
        [InlineData("1.7", "1.6", ComparerEquality.GreaterThan)]
        [InlineData("1.5", "1.5", ComparerEquality.Equal)]
        //decimal & trailing decimal
        [InlineData("1.4", "1.60", ComparerEquality.LessThan)]
        [InlineData("1.4", "1.60", ComparerEquality.LessThan, NaturalComparerOptions.DecimalPrecision)]
        [InlineData("1.7", "1.60", ComparerEquality.GreaterThan, NaturalComparerOptions.DecimalPrecision)]
        [InlineData("1.7", "1.60", ComparerEquality.LessThan)]
        [InlineData("1.5", "1.50", ComparerEquality.Equal, NaturalComparerOptions.DecimalPrecision)]
        [InlineData("1.5", "1.50", ComparerEquality.LessThan)]
        //sandwiched decimal and decimal length
        [InlineData("1.5", "1.5000", ComparerEquality.Equal, NaturalComparerOptions.DecimalPrecision)]
        [InlineData("1.5", "1.5000", ComparerEquality.LessThan)]
        [InlineData("1.5", "1.5000", ComparerEquality.LessThan, NaturalComparerOptions.CheckTrailingDecimalLength)]
        [InlineData("a1.5b", "a1.5000b", ComparerEquality.LessThan, NaturalComparerOptions.CheckTrailingDecimalLength)]
        [InlineData("a1.5b", "a1.5000b", ComparerEquality.Equal, NaturalComparerOptions.DecimalPrecision)]
        [InlineData("a1.5b", "a1.5000b", ComparerEquality.LessThan)]
        //ignore leading and trailing whitespace
        [InlineData("aa", "a a", ComparerEquality.GreaterThan)]
        [InlineData("aa", " aa", ComparerEquality.Equal)]
        [InlineData("aa ", " aa", ComparerEquality.Equal)]
        //compound
        [InlineData("z11", "z4", ComparerEquality.GreaterThan)]
        [InlineData("z11", "z44", ComparerEquality.LessThan)]
        [InlineData("z12", "z12", ComparerEquality.Equal)]
        [InlineData("12a", "12b", ComparerEquality.LessThan)]
        [InlineData("12a 17b", "12a 19a", ComparerEquality.LessThan)]
        [InlineData("12a 17b", "12a 17a", ComparerEquality.GreaterThan)]
        [InlineData("12a 17a", "12a 17a", ComparerEquality.Equal)]
        //ignore case
        [InlineData("a", "a", ComparerEquality.Equal, NaturalComparerOptions.IgnoreCase)]
        [InlineData("a", "A", ComparerEquality.Equal, NaturalComparerOptions.IgnoreCase)]
        //ignore whitespace
        [InlineData("a a", "a   a", ComparerEquality.Equal, NaturalComparerOptions.IgnoreWhiteSpace)]
        [InlineData("a a", "aa", ComparerEquality.Equal, NaturalComparerOptions.IgnoreWhiteSpace)]
        [InlineData("aa", "a a", ComparerEquality.Equal, NaturalComparerOptions.IgnoreWhiteSpace)]
        [InlineData("aa", " aa", ComparerEquality.Equal, NaturalComparerOptions.IgnoreWhiteSpace)]
        [InlineData("aa ", " aa", ComparerEquality.Equal, NaturalComparerOptions.IgnoreWhiteSpace)]
        [InlineData("aa", " a\ta", ComparerEquality.Equal, NaturalComparerOptions.IgnoreWhiteSpace)]
        //capitalization order
        [InlineData("added4", "Added11", ComparerEquality.LessThan)]
        [InlineData("added4", "Added11", ComparerEquality.GreaterThan, NaturalComparerOptions.LowercaseFirst)]
        //double decimals
        [InlineData("12.4.1", "12.4.1", ComparerEquality.Equal)]
        [InlineData("12.41", "12.4.1", ComparerEquality.GreaterThan)]
        public void TestCompare(string left, string right, ComparerEquality expectedResult, NaturalComparerOptions options = NaturalComparerOptions.None)
        {
            var comparer = new NaturalComparer(options);
            DoTest(left, right, expectedResult, comparer, nameof(NaturalComparer));
            
            var oldComparer = new NaturalComparerSpan(options);
            DoTest(left, right, expectedResult, oldComparer, nameof(NaturalComparerSpan));
        }

        private static void DoTest(string left, string right, ComparerEquality expectedResult, IComparer<string> comparer, string note)
        {
            var result = comparer.Compare(left, right);
            switch (expectedResult)
            {
                case ComparerEquality.LessThan:
                    Assert.True(result <= (int)expectedResult, $"Result: {result} Expected: {expectedResult}({(int)expectedResult}) - {note}");
                    break;
                case ComparerEquality.Equal:
                    Assert.True(result == (int)expectedResult, $"Result: {result} Expected: {expectedResult}({(int)expectedResult}) - {note}");
                    break;
                case ComparerEquality.GreaterThan:
                    Assert.True(result >= (int)expectedResult, $"Result: {result} Expected: {expectedResult}({(int)expectedResult}) - {note}");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(expectedResult), expectedResult, null);
            }
        }
    }
}