using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CG.Commons.Extensions;
using Xunit;
using Xunit.Extensions;

namespace CG.Commons.Test.Extensions
{
    public class StringBuilderExtensionsTests
    {
        [Fact]
        public void TestCanary()
        {
            Assert.True(true);
        }

        [Theory]
        [MemberData(nameof(GenerateAppendLineTestData))]
        public void TestAppendLine(string expected, string format, params object[] args)
        {
            var sb = new StringBuilder();
            sb.AppendLine(format, args);
            Assert.Equal(expected + "\r\n", sb.ToString());
        }


        public static IEnumerable<object[]> GenerateAppendLineTestData()
        {
            yield return CreateAppendLineTestData("");
            yield return CreateAppendLineTestData("a");
            yield return CreateAppendLineTestData("{0} {1}", "Hello", "World");
            yield return CreateAppendLineTestData("{1} {0}", "Hello", "World");
            yield return CreateAppendLineTestData("{0} {1:yyyy}", "Hello", DateTime.Today);
        }

        private static object[] CreateAppendLineTestData(string format, params object[] args)
        {
            return new object[] {string.Format(format, args), format, args};
        }
    }
}
