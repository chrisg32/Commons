using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CG.Commons.Extensions;
using Xunit;
using Xunit.Sdk;

namespace CG.Commons.Test.Extensions
{
    public class ExceptionExtensionsTests
    {
        [Fact]
        public void TestCanary()
        {
            Assert.True(true);


            try
            {
                try
                {
                    Parallel.For(0, 7, (i, state) => throw new Exception($"Something {i}"));
                }
                catch(Exception es)
                {
                    throw new Exception("Something happened", es);
                }
            }
            catch (Exception e)
            {
                var s = e.PrintFullStackTrace();
            }



            

        }

    }
}
