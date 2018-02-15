using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CG.Commons.Extensions;
using Xunit;

namespace CG.Commons.Test.Extensions
{
    public class IntegerExtensionsTests
    {
        [Theory]
        [InlineData(0, true)]
        [InlineData(1, false)]
        [InlineData(2, true)]
        [InlineData(3, false)]
        [InlineData(4, true)]
        [InlineData(5, false)]
        [InlineData(6, true)]
        [InlineData(7, false)]
        [InlineData(8, true)]
        [InlineData(9, false)]
        [InlineData(10, true)]
        public void TestIsEven(int i, bool expected)
        {
            var result = i.IsEven();
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(1, true)]
        [InlineData(2, false)]
        [InlineData(3, true)]
        [InlineData(4, false)]
        [InlineData(5, true)]
        [InlineData(6, false)]
        [InlineData(7, true)]
        [InlineData(8, false)]
        [InlineData(9, true)]
        [InlineData(10, false)]
        public void TestIsOdd(int i, bool expected)
        {
            var result = i.IsOdd();
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestIterations()
        {
            var expected = new[] {0, 1, 2, 3, 4};
            var result = 5.Iterations().ToArray();
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestTimes()
        {
            var expected = new[] { 0, 1, 2, 3, 4 };
            var result = new List<int>();
            5.Times(i => result.Add(i));
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestTimes2()
        {
            var i = 0;
            5.Times(() => i++);
            Assert.Equal(5, i);
        }
    }
}
