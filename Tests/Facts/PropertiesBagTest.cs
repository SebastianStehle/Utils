// ==========================================================================
// PropertiesBagTest.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System;
using System.Globalization;
using System.Linq;
using GP.Utils;
using Xunit;

// ReSharper disable UnusedParameter.Local

namespace Tests.Facts
{
    public class PropertiesBagTest
    {
        private readonly CultureInfo c = CultureInfo.InvariantCulture;
        private readonly PropertiesBag bag = new PropertiesBag();

        [Fact]
        public void Rename_NotFound_ReturnsFalse()
        {
            Assert.False(bag.Rename("OldKey", "NewKey"));
        }

        [Fact]
        public void Rename_AlreadyExists_ThrowsException()
        {
            bag.Set("NewKey", 1);

            Assert.Throws<ArgumentException>(() => bag.Rename("OldKey", "NewKey"));
        }

        [Fact]
        public void Rename_SameKeys_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => bag.Rename("SameKey", "SameKey"));
        }

        [Fact]
        public void Rename()
        {
            bag.Set("OldKey", 123);

            Assert.True(bag.Rename("OldKey", "NewKey"));
            Assert.True(bag.Contains("NewKey"));

            Assert.Equal(1, bag.Count);
            Assert.Equal(123, bag["NewKey"].ToInt32(c));

            Assert.False(bag.Contains("OldKey"));
        }

        [Fact]
        public void MulipleProperties_CorrectCount()
        {
            bag.Set("Key1", 1);
            bag.Set("Key2", 1);

            Assert.Equal(2, bag.Count);
            Assert.Equal(new[] { "Key1", "Key2" }, bag.PropertyNames.ToArray());
            Assert.Equal(new[] { "Key1", "Key2" }, bag.Properties.Keys.ToArray());
        }

        [Fact]
        public void Set_Property_Contains()
        {
            Assert.False(bag.Contains("Key"));

            bag.Set("Key", 1);

            Assert.True(bag.Contains("Key"));
            Assert.True(bag.Contains("KEY"));
        }

        [Fact]
        public void Remove_NotFound_False()
        {
            Assert.False(bag.Remove("NOTFOUND"));
        }

        [Fact]
        public void Remove_IgnoresCasing()
        {
            bag.Set("Key", 1);

            Assert.True(bag.Remove("KEY"));
            Assert.False(bag.Contains("KEY"));
        }

        [Fact]
        public void Set_InvalidType_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => bag.Set("Key", (byte)1));
        }

        [Fact]
        public void Format_Nulls()
        {
            bag.Set("Key", null);

            Assert.Equal(null, bag["Key"].ToString());
            Assert.Equal(0f, bag["Key"].ToSingle(CultureInfo.CurrentCulture));
            Assert.Equal(0d, bag["Key"].ToDouble(CultureInfo.CurrentCulture));
            Assert.Equal(0L, bag["Key"].ToInt64(CultureInfo.CurrentCulture));
            Assert.Equal(0,  bag["Key"].ToInt32(CultureInfo.CurrentCulture));
            Assert.Equal(false, bag["Key"].ToBoolean(CultureInfo.CurrentCulture));
            Assert.Equal(new Guid(), bag["Key"].ToGuid(CultureInfo.CurrentCulture));
            Assert.Equal(new TimeSpan(), bag["Key"].ToTimeSpan(CultureInfo.CurrentCulture));
            Assert.Equal(new DateTime(), bag["Key"].ToDateTime(CultureInfo.CurrentCulture));
            Assert.Equal(new DateTimeOffset(), bag["Key"].ToDateTimeOffset(CultureInfo.CurrentCulture));
        }

        [Fact]
        public void Format_Number_String()
        {
            bag.Set("Key", 123);

            AssertNumber();
        }

        [Fact]
        public void Format_Mumber_Int()
        {
            bag.Set("Key", 123);

            AssertNumber();
        }

        [Fact]
        public void Format_Number_Long()
        {
            bag.Set("Key", 123L);

            AssertNumber();
        }

        [Fact]
        public void Format_Number_Long_MaxValue_CastException()
        {
            bag.Set("Key", long.MaxValue);

            Assert.Throws<InvalidCastException>(() => bag["Key"].ToInt32(c));
        }

        [Fact]
        public void Format_Number_Float()
        {
            bag.Set("Key", 123f);

            AssertNumber();
        }

        [Fact]
        public void Format_Number_Double()
        {
            bag.Set("Key", 123d);

            AssertNumber();
        }

        [Fact]
        public void Format_Number_Double_NoCastException()
        {
            bag.Set("Key", double.MaxValue);

            Assert.Equal(float.PositiveInfinity, bag["Key"].ToSingle(c));
        }

        [Fact]
        public void Format_TimeSpan()
        {
            TimeSpan time = TimeSpan.FromSeconds(123);

            bag.Set("Key", time);

            AssertTimeSpan(time);
        }

        [Fact]
        public void Format_TimeSpan_String()
        {
            TimeSpan time = TimeSpan.FromSeconds(123);

            bag.Set("Key", time.ToString());

            AssertTimeSpan(time);
        }

        [Fact]
        public void Format_DateTime()
        {
            DateTime time = DateTime.Today;

            bag.Set("Key", time);

            AssertDateTime(time);
        }

        [Fact]
        public void Format_DateTime_String()
        {
            DateTime time = DateTime.Today;

            bag.Set("Key", time.ToString(c));

            AssertDateTime(time);
        }

        [Fact]
        public void Format_DateTimeOffset()
        {
            DateTimeOffset time = DateTime.Today;

            bag.Set("Key", time);

            AssertDateTimeOffset(time);
        }

        [Fact]
        public void Format_DateTimeOffset_String()
        {
            DateTimeOffset time = DateTime.Today;

            bag.Set("Key", time.ToString(c));

            AssertDateTimeOffset(time);
        }

        [Fact]
        public void Format_Guid()
        {
            Guid id = new Guid();

            bag.Set("Key", id);

            AssertGuid(id);
        }

        [Fact]
        public void Format_Guid_String()
        {
            Guid id = new Guid();

            bag.Set("Key", id.ToString());

            AssertGuid(id);
        }

        [Fact]
        public void Format_Boolean()
        {
            bag.Set("Key", true);

            AssertBoolean();
        }

        [Fact]
        public void Format_Boolean_String()
        {
            bag.Set("Key", "true");

            AssertBoolean();
        }

        [Fact]
        public void Format_Boolean_Number()
        {
            bag.Set("Key", 1);

            AssertBoolean();
        }

        [Fact]
        public void DateTime_ToNumber_InvalidCaseException()
        {
            bag.Set("Key", DateTime.Now);

            Assert.Throws<InvalidCastException>(() => bag["Key"].ToGuid(CultureInfo.InvariantCulture));
        }

        [Fact]
        public void TryParseEnum()
        {
            bag.Set("Key1", "Monday");
            bag.Set("Key2", "Invalid");

            DayOfWeek parsed;

            Assert.True(bag.TryParseEnum("Key1", out parsed));
            Assert.Equal(DayOfWeek.Monday, parsed);

            Assert.False(bag.TryParseEnum("Key2", out parsed));
            Assert.Equal(DayOfWeek.Sunday, parsed);

            Assert.False(bag.TryParseEnum("Invalid", out parsed));
            Assert.Equal(DayOfWeek.Sunday, parsed);
        }

        [Fact]
        public void TryParseInt32()
        {
            bag.Set("Key1", "123");
            bag.Set("Key2", "abc");

            int parsed;

            Assert.True(bag.TryParseInt32("Key1", out parsed));
            Assert.Equal(123, parsed);

            Assert.False(bag.TryParseInt32("Key2", out parsed));
            Assert.Equal(0, parsed);

            Assert.False(bag.TryParseInt32("Invalid", out parsed));
            Assert.Equal(0, parsed);
        }

        [Fact]
        public void TryParseNullableInt32()
        {
            bag.Set("Key1", "123");
            bag.Set("Key2", "abc");

            int? parsed;

            Assert.True(bag.TryParseNullableInt32("Key1", out parsed));
            Assert.Equal(123, parsed);

            Assert.False(bag.TryParseNullableInt32("Key2", out parsed));
            Assert.Null(parsed);

            Assert.False(bag.TryParseNullableInt32("Invalid", out parsed));
            Assert.Null(parsed);
        }

        [Fact]
        public void TryParseString()
        {
            bag.Set("Key1", "123");
            bag.Set("Key2", 123);
            bag.Set("Key3", null);

            string parsed;

            Assert.True(bag.TryParseString("Key1", out parsed));
            Assert.Equal("123", parsed);

            Assert.True(bag.TryParseString("Key2", out parsed));
            Assert.Equal("123", parsed);

            Assert.False(bag.TryParseString("Key3", out parsed));
            Assert.Null(parsed);

            Assert.False(bag.TryParseString("Invalid", out parsed));
            Assert.Null(parsed);
        }

        private void AssertBoolean()
        {
            Assert.True(bag["Key"].ToBoolean(c));
            Assert.True(bag["Key"].ToNullableBoolean(c));
        }

        private void AssertDateTimeOffset(DateTimeOffset expected)
        {
            Assert.Equal(expected, bag["Key"].ToDateTimeOffset(c));
            Assert.Equal(expected, bag["Key"].ToNullableDateTimeOffset(c));
        }

        private void AssertDateTime(DateTime expected)
        {
            Assert.Equal(expected, bag["Key"].ToDateTime(c));
            Assert.Equal(expected, bag["Key"].ToNullableDateTime(c));
        }

        private void AssertTimeSpan(TimeSpan expected)
        {
            Assert.Equal(expected, bag["Key"].ToTimeSpan(c));
            Assert.Equal(expected, bag["Key"].ToNullableTimeSpan(c));
        }

        private void AssertGuid(Guid expected)
        {
            Assert.Equal(expected, bag["Key"].ToGuid(c));
            Assert.Equal(expected, bag["Key"].ToNullableGuid(c));
        }

        private void AssertNumber()
        {
            Assert.Equal(123, bag["Key"].ToInt32(c));
            Assert.Equal(123, bag["Key"].ToNullableInt32(c));
            Assert.Equal(123L, bag["Key"].ToInt64(c));
            Assert.Equal(123L, bag["Key"].ToNullableInt64(c));
            Assert.Equal(123f, bag["Key"].ToSingle(c));
            Assert.Equal(123f, bag["Key"].ToNullableSingle(c));
            Assert.Equal(123d, bag["Key"].ToDouble(c));
            Assert.Equal(123d, bag["Key"].ToNullableDouble(c));

            Assert.True(bag["Key"].ToBoolean(c));
        }
    }
}
