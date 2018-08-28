using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CG.Commons.Util;
using FluentAssertions;
using Xunit;

namespace CG.Commons.Test.Util
{
    public class WeightedNaturalComparerTests
    {
        [Fact]
        public void TestCanary()
        {
            Assert.True(true);
        }

        private static IEnumerable<(string a, string b, string[] weights, WeightedNaturalComparer.WeightedNaturalComparerOptions
            options, ComparerEquality expected)> CreateTests()
        {
            #region Natural Comparer Tests
            yield return ("a", "aa", new string[0], WeightedNaturalComparer.WeightedNaturalComparerOptions.None, ComparerEquality.LessThan);
            yield return ("aa", "a", new string[0], WeightedNaturalComparer.WeightedNaturalComparerOptions.None, ComparerEquality.GreaterThan);
            //character & character
            yield return ("a", "b", new string[0], WeightedNaturalComparer.WeightedNaturalComparerOptions.None, ComparerEquality.LessThan);
            yield return ("b", "a", new string[0], WeightedNaturalComparer.WeightedNaturalComparerOptions.None, ComparerEquality.GreaterThan);
            yield return ("a", "a", new string[0], WeightedNaturalComparer.WeightedNaturalComparerOptions.None, ComparerEquality.Equal);
            yield return ("a", "A", new string[0], WeightedNaturalComparer.WeightedNaturalComparerOptions.None, ComparerEquality.GreaterThan);
            //single number & character
            yield return ("4", "a", new string[0], WeightedNaturalComparer.WeightedNaturalComparerOptions.None, ComparerEquality.LessThan);
            yield return ("a", "4", new string[0], WeightedNaturalComparer.WeightedNaturalComparerOptions.None, ComparerEquality.GreaterThan);
            //single number & single number
            yield return ("4", "6", new string[0], WeightedNaturalComparer.WeightedNaturalComparerOptions.None, ComparerEquality.LessThan);
            yield return ("7", "6", new string[0], WeightedNaturalComparer.WeightedNaturalComparerOptions.None, ComparerEquality.GreaterThan);
            yield return ("5", "5", new string[0], WeightedNaturalComparer.WeightedNaturalComparerOptions.None, ComparerEquality.Equal);
            //double number & single number
            yield return ("4", "16", new string[0], WeightedNaturalComparer.WeightedNaturalComparerOptions.None, ComparerEquality.LessThan);
            yield return ("17", "6", new string[0], WeightedNaturalComparer.WeightedNaturalComparerOptions.None, ComparerEquality.GreaterThan);
            yield return ("5", "05", new string[0], WeightedNaturalComparer.WeightedNaturalComparerOptions.None, ComparerEquality.Equal);
            //double number & double number
            yield return ("14", "16", new string[0], WeightedNaturalComparer.WeightedNaturalComparerOptions.None, ComparerEquality.LessThan);
            yield return ("17", "16", new string[0], WeightedNaturalComparer.WeightedNaturalComparerOptions.None, ComparerEquality.GreaterThan);
            yield return ("15", "15", new string[0], WeightedNaturalComparer.WeightedNaturalComparerOptions.None, ComparerEquality.Equal);
            //ignore leading and trailing whitespace
            yield return ("aa", "a a", new string[0], WeightedNaturalComparer.WeightedNaturalComparerOptions.None, ComparerEquality.GreaterThan);
            yield return ("aa", " aa", new string[0], WeightedNaturalComparer.WeightedNaturalComparerOptions.None, ComparerEquality.Equal);
            yield return ("aa ", " aa", new string[0], WeightedNaturalComparer.WeightedNaturalComparerOptions.None, ComparerEquality.Equal);
            //compound
            yield return ("z11", "z4", new string[0], WeightedNaturalComparer.WeightedNaturalComparerOptions.None, ComparerEquality.GreaterThan);
            yield return ("z11", "z44", new string[0], WeightedNaturalComparer.WeightedNaturalComparerOptions.None, ComparerEquality.LessThan);
            yield return ("z12", "z12", new string[0], WeightedNaturalComparer.WeightedNaturalComparerOptions.None, ComparerEquality.Equal);
            yield return ("12a", "12b", new string[0], WeightedNaturalComparer.WeightedNaturalComparerOptions.None, ComparerEquality.LessThan);
            yield return ("12a 17b", "12a 19a", new string[0], WeightedNaturalComparer.WeightedNaturalComparerOptions.None, ComparerEquality.LessThan);
            yield return ("12a 17b", "12a 17a", new string[0], WeightedNaturalComparer.WeightedNaturalComparerOptions.None, ComparerEquality.GreaterThan);
            yield return ("12a 17a", "12a 17a", new string[0], WeightedNaturalComparer.WeightedNaturalComparerOptions.None, ComparerEquality.Equal);
            //ignore case
            yield return ("a", "a", new string[0], WeightedNaturalComparer.WeightedNaturalComparerOptions.IgnoreCase, ComparerEquality.Equal);
            yield return ("a", "A", new string[0], WeightedNaturalComparer.WeightedNaturalComparerOptions.IgnoreCase, ComparerEquality.Equal);
            //ignore white space
            yield return ("a a", "a   a", new string[0], WeightedNaturalComparer.WeightedNaturalComparerOptions.IgnoreWhiteSpace, ComparerEquality.Equal);
            yield return ("a a", "aa", new string[0], WeightedNaturalComparer.WeightedNaturalComparerOptions.IgnoreWhiteSpace, ComparerEquality.Equal);
            yield return ("aa", "a a", new string[0], WeightedNaturalComparer.WeightedNaturalComparerOptions.IgnoreWhiteSpace, ComparerEquality.Equal);
            yield return ("aa", " aa", new string[0], WeightedNaturalComparer.WeightedNaturalComparerOptions.IgnoreWhiteSpace, ComparerEquality.Equal);
            yield return ("aa ", " aa", new string[0], WeightedNaturalComparer.WeightedNaturalComparerOptions.IgnoreWhiteSpace, ComparerEquality.Equal);
            yield return ("aa", " a\ta", new string[0], WeightedNaturalComparer.WeightedNaturalComparerOptions.IgnoreWhiteSpace, ComparerEquality.Equal);
            #endregion


            #region Weight Tests
            yield return ("b as in bird", "a as in apple", new []{"b"}, WeightedNaturalComparer.WeightedNaturalComparerOptions.IgnoreWhiteSpace, ComparerEquality.LessThan);
            yield return ("b as in bird", "a as in apple", new[] { "b" }, WeightedNaturalComparer.WeightedNaturalComparerOptions.StartsWith, ComparerEquality.LessThan);
            yield return ("b as in bird", "a as in apple", new[] { "a" }, WeightedNaturalComparer.WeightedNaturalComparerOptions.StartsWith, ComparerEquality.GreaterThan);
            yield return ("b as in bird", "a as in apple", new[] { "b" }, WeightedNaturalComparer.WeightedNaturalComparerOptions.Contains, ComparerEquality.LessThan);
            yield return ("b as in bird", "a as in apple", new[] { "b" }, WeightedNaturalComparer.WeightedNaturalComparerOptions.EndsWith, ComparerEquality.GreaterThan);
            yield return ("b as in bird", "a as in apple", new[] { "bird" }, WeightedNaturalComparer.WeightedNaturalComparerOptions.StartsWith, ComparerEquality.GreaterThan);
            yield return ("b as in bird", "a as in apple", new[] { "bird" }, WeightedNaturalComparer.WeightedNaturalComparerOptions.Contains, ComparerEquality.LessThan);
            yield return ("b as in bird", "a as in apple", new[] { "bird" }, WeightedNaturalComparer.WeightedNaturalComparerOptions.EndsWith, ComparerEquality.LessThan);
            yield return ("b as in bird", "a as in apple", new[] { "bIRd" }, WeightedNaturalComparer.WeightedNaturalComparerOptions.Contains, ComparerEquality.GreaterThan);
            yield return ("b as in bird", "a as in apple", new[] { "bIRd" }, WeightedNaturalComparer.WeightedNaturalComparerOptions.Contains | WeightedNaturalComparer.WeightedNaturalComparerOptions.IgnoreCase, ComparerEquality.LessThan);

            yield return ("cat bat hat", "cab bad lad hat", new[] { "bat"}, WeightedNaturalComparer.WeightedNaturalComparerOptions.Contains, ComparerEquality.LessThan);
            yield return ("cat bat hat", "cab bad lad hat", new[] { "bat", "lad" }, WeightedNaturalComparer.WeightedNaturalComparerOptions.Contains, ComparerEquality.LessThan);
            yield return ("cat bat hat", "cab bad lad hat", new[] { "lad", "bat" }, WeightedNaturalComparer.WeightedNaturalComparerOptions.Contains, ComparerEquality.GreaterThan);
            #endregion
        }

        #region Test Internals

        public enum ComparerEquality
        {
            LessThan = -1,
            Equal = 0,
            GreaterThan = 1
        }

        public static IEnumerable<object[]> ConvertTests()
        {
            return CreateTests().Select(i => new object[] {i.a, i.b, i.weights, i.options, i.expected});
        }

        [Theory]
        [MemberData(nameof(ConvertTests))]
        public void Tests(string a, string b, string[] weights, WeightedNaturalComparer.WeightedNaturalComparerOptions
            options, ComparerEquality expected)
        {
            var comparer = new WeightedNaturalComparer(weights, options);
            switch (expected)
            {
                case ComparerEquality.LessThan:
                    comparer.Compare(a, b).Should().BeLessThan(0);
                    break;
                case ComparerEquality.Equal:
                    comparer.Compare(a, b).Should().Be(0);
                    break;
                case ComparerEquality.GreaterThan:
                    comparer.Compare(a, b).Should().BeGreaterThan(0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(expected), expected, null);
            }
        }

        #endregion
    }
}
