using System.Collections.Generic;
using CG.Commons.Collections;
using Xunit;

namespace CG.Commons.Test.Collections
{
    public class SafeDictionaryTests
    {
        [Fact]
        public void TestCanary()
        {
            Assert.True(true);
        }

        /// <summary>
        /// The old implmentation did not work if casting to the interface. We need to test for that.
        /// </summary>
        [Fact]
        public void TestPolymorphism()
        {
            SafeDictionary<string, int> dict = new SafeDictionary<string, int>(7);
            Assert.Equal(7, dict["foo"]);
            Assert.Equal(7, ((IDictionary<string, int>)dict)["foo"]);
        }
    }
}
