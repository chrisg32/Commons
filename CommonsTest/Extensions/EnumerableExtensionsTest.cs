using System.Collections.Generic;
using System.Linq;
using Commons.Extensions;
using Xunit;

namespace CommonsTest.Extensions
{
    public class EnumerableExtensionsTest
    {
        [Fact]
        public void TestCanary()
        {
            Assert.True(true);
        }

        [Theory]
        [InlineData(new []{1, 2}, new[]{ 1, 2 }, true)]
        [InlineData(new[] { 1, 2 }, new[] { 2, 1 }, true)]
        [InlineData(new[] { 1, 2 }, new[] { 2, 2 }, false)]
        [InlineData(new[] { 1, 2 }, new[] { 1, 2, 3 }, false)]
        [InlineData(new[] { 1, 2 }, new[] { 1, 2, 2 }, false)]
        [InlineData(new[] { 1, 2, 3 }, new[] { 1, 2, 3 }, true)]
        [InlineData(new[] { 1, 3, 2 }, new[] { 2, 3, 1 }, true)]
        public void TestCollectionsEqual_Int(ICollection<int> collectionA, ICollection<int> collectionB, bool expectedResult)
        {
            var actualResult = collectionA.IsCollectionsEqual(collectionB);
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(new[] { "yes", "no" }, new[] { "yes", "no" }, true)]
        [InlineData(new[] { "yes", "no" }, new[] { "yes", "NO" }, false)]
        [InlineData(new[] { "yes", "no" }, new[] { "no", "yes" }, true)]
        [InlineData(new[] { "yes", "no" }, new[] { "yes", "no", "maybe" }, false)]
        public void TestCollectionsEqual_String(ICollection<string> collectionA, ICollection<string> collectionB, bool expectedResult)
        {
            var actualResult = collectionA.IsCollectionsEqual(collectionB);
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
