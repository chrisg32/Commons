﻿using System.Collections.Generic;
using CG.Commons.Extensions;
using Xunit;

namespace CG.Commons.Test.Extensions
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
        public void TestCollectionsEqual_Int(ICollection<int> collectionA, ICollection<int> collectionB, bool expectedResult)
        {
            var actualResult = collectionA.IsCollectionsEqual(collectionB);
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
