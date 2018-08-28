using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CG.Commons.Extensions;
using Xunit;

namespace CG.Commons.Test.Extensions
{
    public class ObjectExtensionsTests
    {
        private class Car
        {
            public string Make { get; set; }
            public string Model { get; set; }
            public Person Owner { get; set; }
        }

        private class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime Birthdate { get; set; }
            public Person Spouse { get; set; }
            private string Secret { get; set; }
            private void Dance() { }
        }

        [Fact]
        public void TestGetPropertyValue()
        {
            var obj = CreateMockData();

            var make = obj.GetPropertyValue("Make");
            Assert.Equal(obj.Make, make.ToString());

            //doesn't handle lowercase, test that
            var model = obj.GetPropertyValue("model");
            Assert.Null(model);

            var year = obj.GetPropertyValue("Owner.Birthdate.Year");
            Assert.Equal(obj.Owner.Birthdate.Year, year);

            var syear = obj.GetPropertyValue("Owner.Spouse.Birthdate.Year");
            Assert.Null(syear);

            var secret = obj.GetPropertyValue("Owner.Secret");
            Assert.Null(secret);

            var dance = obj.GetPropertyValue("Owner.Dance");
            Assert.Null(dance);

            var bla = obj.GetPropertyValue(null);
            Assert.Null(bla);
        }

        private Car CreateMockData()
        {
            return new Car
            {
                Make = "Ford",
                Model = "Mustang",
                Owner = new Person
                {
                    FirstName = "Sara",
                    LastName = "Smith",
                    Birthdate = new DateTime(1990, 8, 17)
                }
            };
        }
    }
}
