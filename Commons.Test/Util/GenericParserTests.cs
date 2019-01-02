using System;
using System.Collections.Generic;
using System.Globalization;
using CG.Commons.Extensions;
using CG.Commons.Util;
using Xunit;

namespace CG.Commons.Test.Util
{
    public class GenericParserTests
    {
        [Fact]
        public void TestCanary()
        {
            Assert.True(true);
        }

        #region Test Data Emitters

        public static IEnumerable<object[]> CreateBooleanCases()
        {
            yield return new object[] {"true", true};
            yield return new object[] {"false", false};
            yield return new object[] {"True", true};
            yield return new object[] {"False", false};
        }

        public static IEnumerable<object[]> CreateIntegerCases()
        {
            yield return new object[] { "1", 1 };
            yield return new object[] { "2", 2 };
            yield return new object[] { "2147483647", 2147483647 };
            yield return new object[] { "0", 0 };
            yield return new object[] { "-1", -1 };
            yield return new object[] { "-2", -2 };
            yield return new object[] { "-2147483648", -2147483648 };
        }

        public static IEnumerable<object[]> CreateLongCases()
        {
            yield return new object[] { "1", 1L };
            yield return new object[] { "2", 2L };
            yield return new object[] { "9223372036854775807", 9223372036854775807L };
            yield return new object[] { "0", 0L };
            yield return new object[] { "-1", -1L };
            yield return new object[] { "-2", -2L };
            yield return new object[] { "-9223372036854775808", -9223372036854775808L };
        }

        public static IEnumerable<object[]> CreateDoubleCases()
        {
            yield return new object[] { "1.123", 1.123D };
            yield return new object[] { "2.321", 2.321D };
            yield return new object[] { "1.7976931348623157E+308", 1.7976931348623157E+308D };
            yield return new object[] { "0.0", 0.0D };
            yield return new object[] { "-1.123", -1.123D };
            yield return new object[] { "-2.321", -2.321D };
            yield return new object[] { "-1.7976931348623157E+308", -1.7976931348623157E+308D };
        }

        public static IEnumerable<object[]> CreateDecimalCases()
        {
            yield return new object[] { "1.123", 1.123M };
            yield return new object[] { "2.321", 2.321M };
            yield return new object[] { "79228162514264337593543950335", 79228162514264337593543950335M };
            yield return new object[] { "0.0", 0.0M };
            yield return new object[] { "-1.123", -1.123M };
            yield return new object[] { "-2.321", -2.321M };
            yield return new object[] { "-79228162514264337593543950335", -79228162514264337593543950335M };
        }

        public static IEnumerable<object[]> CreateFloatCases()
        {
            yield return new object[] { "1.123", 1.123F };
            yield return new object[] { "2.321", 2.321F };
            yield return new object[] { "3.40282347E+38", 3.40282347E+38F };
            yield return new object[] { "0", 0F };
            yield return new object[] { "-1.123", -1.123F };
            yield return new object[] { "-2.321", -2.321F };
            yield return new object[] { "-3.40282347E+38", -3.40282347E+38F };
        }

        public static IEnumerable<object[]> CreateShortCases()
        {
            yield return new object[] { "1", (short)1 };
            yield return new object[] { "2", (short)2 };
            yield return new object[] { "32767", (short)32767 };
            yield return new object[] { "0", (short)0 };
            yield return new object[] { "-1", (short)-1 };
            yield return new object[] { "-2", (short)-2 };
            yield return new object[] { "-32768", (short)-32768 };
        }

        public static IEnumerable<object[]> CreateByteCases()
        {
            yield return new object[] { "1", (byte)1 };
            yield return new object[] { "2", (byte)2 };
            yield return new object[] { "255", (byte)255 };
            yield return new object[] { "0", (byte)0 };
        }

        public static IEnumerable<object[]> CreateSignedByteCases()
        {
            yield return new object[] { "1", (sbyte)1 };
            yield return new object[] { "2", (sbyte)2 };
            yield return new object[] { "127", (sbyte)127 };
            yield return new object[] { "0", (sbyte)0 };
            yield return new object[] { "-1", (sbyte)-1 };
            yield return new object[] { "-2", (sbyte)-2 };
            yield return new object[] { "-128", (sbyte)-128 };
        }

        public static IEnumerable<object[]> CreateUnsignedIntCases()
        {
            yield return new object[] { "1", 1U };
            yield return new object[] { "2", 2U };
            yield return new object[] { "4294967295", 4294967295U };
            yield return new object[] { "0", 0U };
        }

        public static IEnumerable<object[]> CreateUnsignedLongCases()
        {
            yield return new object[] { "1", 1UL };
            yield return new object[] { "2", 2UL };
            yield return new object[] { "18446744073709551615", 18446744073709551615UL };
            yield return new object[] { "0", 0UL };
        }

        public static IEnumerable<object[]> CreateUnsignedShortCases()
        {
            yield return new object[] { "1", (ushort)1U };
            yield return new object[] { "2", (ushort)2U };
            yield return new object[] { "65535", (ushort)65535U };
            yield return new object[] { "0", (ushort)0U };
        }

        public static IEnumerable<object[]> CreateCharacterCases()
        {
            yield return new object[] { "1", '1' };
            yield return new object[] { " ", ' ' };
            yield return new object[] { "a", 'a' };
            yield return new object[] { "B", 'B' };
        }

        public static IEnumerable<object[]> CreateDateTimeCases()
        {
            yield return new object[] { DateTime.MinValue.ToString(CultureInfo.InvariantCulture), DateTime.MinValue };
            yield return new object[] { DateTime.MaxValue.ToString(CultureInfo.InvariantCulture), DateTime.MaxValue.TruncateMilliseconds() };
            var now = DateTime.UtcNow;
            yield return new object[] { now.ToString(CultureInfo.InvariantCulture), now.TruncateMilliseconds() };
        }

        public static IEnumerable<object[]> CreateDateTimeOffsetCases()
        {
            yield return new object[] { DateTimeOffset.MinValue.ToString(CultureInfo.InvariantCulture), DateTimeOffset.MinValue };
            yield return new object[] { DateTimeOffset.MaxValue.ToString(CultureInfo.InvariantCulture), DateTimeOffset.MaxValue.TruncateMilliseconds() };
            var now = DateTimeOffset.UtcNow;
            yield return new object[] { now.ToString(CultureInfo.InvariantCulture), now.TruncateMilliseconds() };
        }

        public static IEnumerable<object[]> CreateTimespanCases()
        {
            yield return new object[] { TimeSpan.MinValue.ToString(), TimeSpan.MinValue };
            yield return new object[] { TimeSpan.MaxValue.ToString(), TimeSpan.MaxValue };
            var endOfAllThings = DateTime.MaxValue - DateTime.UtcNow;
            yield return new object[] { endOfAllThings.ToString(), endOfAllThings };
        }

        public static IEnumerable<object[]> CreateGuidCases()
        {
            for (var i = 0; i < 10; i++)
            {
                var guid = Guid.NewGuid();
                yield return i.IsEven()
                    ? new object[] {guid.ToString().ToUpper(), guid}
                    : new object[] {guid.ToString().ToLower(), guid};
            }
        }

        public static IEnumerable<object[]> CreateNullableCases()
        {
            yield return new object[] { null, null };
            yield return new object[] { "", null };
            yield return new object[] { " ", null };
        }

        #endregion

        #region Test Methods

        private class MockClass { }

        [Fact]
        public void TestParse_Unknown()
        {
            Assert.Throws<ArgumentException>(() => { GenericParser.Parse<MockClass>("foo"); });
            Assert.Throws<ArgumentException>(() => { GenericParser.Parse("foo", typeof(MockClass)); });
        }

        [Fact]
        public void Test_TryParseFail()
        {
            var res1 = GenericParser.TryParse("foo", out int _);
            Assert.False(res1);

            var res2 = GenericParser.TryParse("bar", out object _, typeof(int));
            Assert.False(res2);
        }

        [Fact]
        public void Test_RegisterParser()
        {
            GenericParser.RegisterParser(typeof(object), s => s);

            var res1 = GenericParser.TryParse("foo", out object o1);
            Assert.True(res1);
            Assert.Equal("foo", o1.ToString());

            var res2 = GenericParser.TryParse("bar", out object o2, typeof(object));
            Assert.True(res2);
            Assert.Equal("bar", o2.ToString());
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("test", "test")]
        public void TestParse_string(string stringValue, string expectedValue)
        {
            TestParse(stringValue, expectedValue);
        }

        [Theory]
        [MemberData(nameof(CreateBooleanCases))]
        public void TestParse_bool(string stringValue, bool expectedValue)
        {
            TestParse(stringValue, expectedValue);
        }

        [Theory]
        [MemberData(nameof(CreateIntegerCases))]
        public void TestParse_int(string stringValue, int expectedValue)
        {
            TestParse(stringValue, expectedValue);
        }

        [Theory]
        [MemberData(nameof(CreateLongCases))]
        public void TestParse_long(string stringValue, long expectedValue)
        {
            TestParse(stringValue, expectedValue);
        }

        [Theory]
        [MemberData(nameof(CreateDoubleCases))]
        public void TestParse_double(string stringValue, double expectedValue)
        {
            TestParse(stringValue, expectedValue);
        }

        [Theory]
        [MemberData(nameof(CreateFloatCases))]
        public void TestParse_float(string stringValue, float expectedValue)
        {
            TestParse(stringValue, expectedValue);
        }

        [Theory]
        [MemberData(nameof(CreateDecimalCases))]
        public void TestParse_decimal(string stringValue, decimal expectedValue)
        {
            //Decimals while a basic type are not a primitive type and hence cannot be represented in metadata which prevents it from being an attribute parameter.
            TestParse(stringValue, expectedValue);
        }


        [Theory]
        [MemberData(nameof(CreateShortCases))]
        public void TestParse_short(string stringValue, short expectedValue)
        {
            TestParse(stringValue, expectedValue);
        }

        [Theory]
        [MemberData(nameof(CreateByteCases))]
        public void TestParse_byte(string stringValue, byte expectedValue)
        {
            TestParse(stringValue, expectedValue);
        }

        [Theory]
        [MemberData(nameof(CreateSignedByteCases))]
        public void TestParse_sbyte(string stringValue, sbyte expectedValue)
        {
            TestParse(stringValue, expectedValue);
        }

        [Theory]
        [MemberData(nameof(CreateUnsignedIntCases))]
        public void TestParse_uint(string stringValue, uint expectedValue)
        {
            TestParse(stringValue, expectedValue);
        }

        [Theory]
        [MemberData(nameof(CreateUnsignedLongCases))]
        public void TestParse_ulong(string stringValue, ulong expectedValue)
        {
            TestParse(stringValue, expectedValue);
        }

        [Theory]
        [MemberData(nameof(CreateUnsignedShortCases))]
        public void TestParse_ushort(string stringValue, ushort expectedValue)
        {
            TestParse(stringValue, expectedValue);
        }

        [Theory]
        [MemberData(nameof(CreateCharacterCases))]
        public void TestParse_char(string stringValue, char expectedValue)
        {
            TestParse(stringValue, expectedValue);
        }

        [Theory]
        [MemberData(nameof(CreateBooleanCases))]
        [MemberData(nameof(CreateNullableCases))]
        public void TestParse_Nullablebool(string stringValue, bool? expectedValue)
        {
            TestParse(stringValue, expectedValue);
        }

        [Theory]
        [MemberData(nameof(CreateIntegerCases))]
        [MemberData(nameof(CreateNullableCases))]
        public void TestParse_Nullableint(string stringValue, int? expectedValue)
        {
            TestParse(stringValue, expectedValue);
        }

        [Theory]
        [MemberData(nameof(CreateLongCases))]
        [MemberData(nameof(CreateNullableCases))]
        public void TestParse_Nullablelong(string stringValue, long? expectedValue)
        {
            TestParse(stringValue, expectedValue);
        }

        [Theory]
        [MemberData(nameof(CreateDoubleCases))]
        [MemberData(nameof(CreateNullableCases))]
        public void TestParse_Nullabledouble(string stringValue, double? expectedValue)
        {
            TestParse(stringValue, expectedValue);
        }

        [Theory]
        [MemberData(nameof(CreateFloatCases))]
        [MemberData(nameof(CreateNullableCases))]
        public void TestParse_Nullablefloat(string stringValue, float? expectedValue)
        {
            TestParse(stringValue, expectedValue);
        }

        [Theory]
        [MemberData(nameof(CreateDecimalCases))]
        [MemberData(nameof(CreateNullableCases))]
        public void TestParse_Nullabledecimal(string stringValue, decimal? expectedValue)
        {
            TestParse(stringValue, expectedValue);
        }

        [Theory]
        [MemberData(nameof(CreateShortCases))]
        [MemberData(nameof(CreateNullableCases))]
        public void TestParse_Nullableshort(string stringValue, short? expectedValue)
        {
            TestParse(stringValue, expectedValue);
        }

        [Theory]
        [MemberData(nameof(CreateByteCases))]
        [MemberData(nameof(CreateNullableCases))]
        public void TestParse_Nullablebyte(string stringValue, byte? expectedValue)
        {
            TestParse(stringValue, expectedValue);
        }

        [Theory]
        [MemberData(nameof(CreateSignedByteCases))]
        [MemberData(nameof(CreateNullableCases))]
        public void TestParse_Nullablesbyte(string stringValue, sbyte? expectedValue)
        {
            TestParse(stringValue, expectedValue);
        }

        [Theory]
        [MemberData(nameof(CreateUnsignedIntCases))]
        [MemberData(nameof(CreateNullableCases))]
        public void TestParse_Nullableuint(string stringValue, uint? expectedValue)
        {
            TestParse(stringValue, expectedValue);
        }

        [Theory]
        [MemberData(nameof(CreateUnsignedLongCases))]
        [MemberData(nameof(CreateNullableCases))]
        public void TestParse_Nullableulong(string stringValue, ulong? expectedValue)
        {
            TestParse(stringValue, expectedValue);
        }

        [Theory]
        [MemberData(nameof(CreateUnsignedShortCases))]
        [MemberData(nameof(CreateNullableCases))]
        public void TestParse_Nullableushort(string stringValue, ushort? expectedValue)
        {
            TestParse(stringValue, expectedValue);
        }

        [Theory]
        [MemberData(nameof(CreateCharacterCases))]
        [InlineData(null, null)]
        [InlineData("", null)]
        public void TestParse_Nullablechar(string stringValue, char? expectedValue)
        {
            TestParse(stringValue, expectedValue);
        }

        [Theory]
        [MemberData(nameof(CreateDateTimeCases))]
        public void TestParse_DateTime(string stringValue, DateTime expectedValue)
        {
            TestParse(stringValue, expectedValue);
        }

        [Theory]
        [MemberData(nameof(CreateDateTimeOffsetCases))]
        public void TestParse_DateTimeOffset(string stringValue, DateTimeOffset expectedValue)
        {
            TestParse(stringValue, expectedValue);
        }

        [Theory]
        [MemberData(nameof(CreateTimespanCases))]
        public void TestParse_TimeSpan(string stringValue, TimeSpan expectedValue)
        {
            TestParse(stringValue, expectedValue);
        }

        [Theory]
        [MemberData(nameof(CreateGuidCases))]
        public void TestParse_Guid(string stringValue, Guid expectedValue)
        {
            TestParse(stringValue, expectedValue);
        }

        [Theory]
        [MemberData(nameof(CreateDateTimeCases))]
        [MemberData(nameof(CreateNullableCases))]
        public void TestParse_NullableDateTime(string stringValue, DateTime? expectedValue)
        {
            TestParse(stringValue, expectedValue);
        }

        [Theory]
        [MemberData(nameof(CreateDateTimeOffsetCases))]
        [MemberData(nameof(CreateNullableCases))]
        public void TestParse_NullableDateTimeOffset(string stringValue, DateTimeOffset? expectedValue)
        {
            TestParse(stringValue, expectedValue);
        }

        [Theory]
        [MemberData(nameof(CreateTimespanCases))]
        [MemberData(nameof(CreateNullableCases))]
        public void TestParse_NullableTimeSpan(string stringValue, TimeSpan? expectedValue)
        {
            TestParse(stringValue, expectedValue);
        }

        [Theory]
        [MemberData(nameof(CreateGuidCases))]
        [MemberData(nameof(CreateNullableCases))]
        public void TestParse_NullableGuid(string stringValue, Guid? expectedValue)
        {
            TestParse(stringValue, expectedValue);
        }

        #endregion


        #region Utility Methods

        private static void TestParse<T>(string stringValue, T expectedValue)
        {
            var result1 = GenericParser.Parse<T>(stringValue);
            Assert.Equal(expectedValue, result1);

            var result2 = GenericParser.Parse(stringValue, typeof(T));
            Assert.Equal(expectedValue, result2);

            var result3 = GenericParser.TryParse(stringValue, out T out1);
            Assert.True(result3);
            Assert.Equal(expectedValue, out1);

            var result4 = GenericParser.TryParse(stringValue, out object out2, typeof(T));
            Assert.True(result4);
            Assert.Equal(expectedValue, out2);

            var result5 = stringValue.Parse<T>();
            Assert.Equal(expectedValue, result5);

            var result6 = stringValue.Parse(typeof(T));
            Assert.Equal(expectedValue, result6);
        }

        

        #endregion

        
    }
}
