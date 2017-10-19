using System;
using Commons.Util;
using Xunit;

namespace CommonsTest.Util
{
    public class NaturalComparerTest
    {
        [Fact]
        public void TestCanary()
        {
            Assert.True(true);
        }

        public enum ComparerEquality
        {
            LessThan = -1,
            Equal = 0,
            GreaterThan = 1
        }

        [Theory]
        //character & character
        [InlineData("a", "b", ComparerEquality.LessThan)]
        [InlineData("b", "a", ComparerEquality.GreaterThan)]
        [InlineData("a", "a", ComparerEquality.Equal)]
        [InlineData("a", "A", ComparerEquality.GreaterThan)]
        //single number & character
        [InlineData("4", "a", ComparerEquality.LessThan)]
        [InlineData("a", "4", ComparerEquality.GreaterThan)]
        //single number & single number
        [InlineData("4", "6", ComparerEquality.LessThan)]
        [InlineData("7", "6", ComparerEquality.GreaterThan)]
        [InlineData("5", "5", ComparerEquality.Equal)]
        //double number & single number
        [InlineData("4", "16", ComparerEquality.LessThan)]
        [InlineData("17", "6", ComparerEquality.GreaterThan)]
        [InlineData("5", "05", ComparerEquality.Equal)]
        //double number & double number
        [InlineData("14", "16", ComparerEquality.LessThan)]
        [InlineData("17", "16", ComparerEquality.GreaterThan)]
        [InlineData("15", "15", ComparerEquality.Equal)]
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
        public void TestCompare(string left, string right, ComparerEquality expectedResult)
        {
            var comparer = new NaturalComparer();
            DoTest(left, right, expectedResult, comparer);
        }

        [Theory]
        [InlineData("a", "a", ComparerEquality.Equal)]
        [InlineData("a", "A", ComparerEquality.Equal)]
        public void TestCompareIgnoreCase(string left, string right, ComparerEquality expectedResult)
        {
            var comparer = new NaturalComparer(NaturalComparer.NaturalComparerOptions.IgnoreCase);
            DoTest(left, right, expectedResult, comparer);
        }


        [Theory]
        [InlineData("a a", "a   a", ComparerEquality.Equal)]
        [InlineData("a a", "aa", ComparerEquality.Equal)]
        [InlineData("aa", "a a", ComparerEquality.Equal)]
        [InlineData("aa", " aa", ComparerEquality.Equal)]
        [InlineData("aa ", " aa", ComparerEquality.Equal)]
        [InlineData("aa", " a\ta", ComparerEquality.Equal)]
        public void TestCompareIgnoreWhitespace(string left, string right, ComparerEquality expectedResult)
        {
            var comparer = new NaturalComparer(NaturalComparer.NaturalComparerOptions.IgnoreWhiteSpace);
            DoTest(left, right, expectedResult, comparer);
        }

        private static void DoTest(string left, string right, ComparerEquality expectedResult, NaturalComparer comparer)
        {
            var result = comparer.Compare(left, right);
            switch (expectedResult)
            {
                case ComparerEquality.LessThan:
                    Assert.True(result <= (int)expectedResult, $"Result: {result} Expected Result: {expectedResult}({(int)expectedResult})");
                    break;
                case ComparerEquality.Equal:
                    Assert.True(result == (int)expectedResult, $"Result: {result} Expected Result: {expectedResult}({(int)expectedResult})");
                    break;
                case ComparerEquality.GreaterThan:
                    Assert.True(result >= (int)expectedResult, $"Result: {result} Expected Result: {expectedResult}({(int)expectedResult})");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(expectedResult), expectedResult, null);
            }
        }
    }
}