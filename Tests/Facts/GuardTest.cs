// ==========================================================================
// GuardTest.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using GP.Utils;
using Tests.Mockups;
using Xunit;

namespace Tests.Facts
{
    public class GuardTest
    {
        [Fact]
        public void Between_ValidValue_DoesNothing()
        {
            Guard.Between(25, 20, 30, "Parameter");
        }

        [Fact]
        public void Between_GreaterThan_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => Guard.Between(40, 20, 30, "Parameter"));
        }

        [Fact]
        public void Between_LessThan_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => Guard.Between(10, 20, 30, "Parameter"));
        }

        [Fact]
        public void GreaterThan_ValidValue_DoesNothing()
        {
            Guard.GreaterThan(30, 20, "Parameter");
        }

        [Fact]
        public void GreaterThan_LessThan_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => Guard.GreaterThan(10, 20, "Parameter"));
        }

        [Fact]
        public void GreaterEquals_ValidValue_DoesNothing()
        {
            Guard.GreaterEquals(30, 20, "Parameter");
            Guard.GreaterEquals(20, 20, "Parameter");
        }

        [Fact]
        public void GreaterEquals_LessThan_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => Guard.GreaterEquals(10, 20, "Parameter"));
        }

        [Fact]
        public void LessThan_ValidValue_DoesNothing()
        {
            Guard.LessThan(10, 20, "Parameter");
        }

        [Fact]
        public void LessThan_GreaterThan_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => Guard.LessThan(30, 20, "Parameter"));
        }

        [Fact]
        public void LessEquals_ValidInput_DoesNothing()
        {
            Guard.LessEquals(10, 20, "Parameter");
            Guard.LessEquals(20, 20, "Parameter");
        }

        [Fact]
        public void LessEquals_GreaterThan_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => Guard.LessEquals(30, 20, "Parameter"));
        }

        [Fact]
        public void NotEmpty_ValidGuid_DoesNothing()
        {
            Guard.NotEmpty(Guid.NewGuid(), "Parameter");
        }

        [Fact]
        public void NotEmpty_EmptyGuid_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => Guard.NotEmpty(Guid.Empty, "Parameter"));
        }

        [Fact]
        public void NotEmpty_ValidValue_DoesNothing()
        {
            Guard.NotEmpty(new[] { 1, 2, 3 }, "Parameter");
        }

        [Fact]
        public void NotEmpty_EmptyArray_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => Guard.NotEmpty(new int[0], "Parameter"));
        }

        [Fact]
        public void NotEmpty_NullArray_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => Guard.NotEmpty((int[])null, "Parameter"));
        }

        [Fact]
        public void NotNull_ValidValue_DoesNothing()
        {
            Guard.NotNull(string.Empty, "Parameter");
        }

        [Fact]
        public void NotNull_Null_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => Guard.NotNull(null, "Parameter"));
        }

        [Fact]
        public void NotNullOrEmpty_ValidValue_DoesNothing()
        {
            Guard.NotNullOrEmpty("Value", "Parameter");
        }

        [Fact]
        public void NotNullOrEmpty_Null_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => Guard.NotNullOrEmpty(null, "Parameter"));
        }

        [Fact]
        public void NotNullOrEmpty_Empty_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => Guard.NotNullOrEmpty(string.Empty, "Parameter"));
        }

        [Fact]
        public void NotNullOrEmpty_Whitespace_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => Guard.NotNullOrEmpty(" ", "Parameter"));
        }

        [Fact]
        public void IsType_ValidValue_DoesNothing()
        {
            Guard.IsType<bool>(true, "Parameter");
        }

        [Fact]
        public void IsType_OtherType_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => Guard.IsType<bool>(123, "Parameter"));
        }

        [Fact]
        public void IsTypeNonGeneric_ValidValue_DoesNothing()
        {
            Guard.IsType(true, typeof(bool), "Parameter");
        }

        [Fact]
        public void IsTypeNonGeneric_OtherType_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => Guard.IsType(123, typeof(bool), "Parameter"));
        }

        [Fact]
        public void ValidFileName_ValidValue_DoesNothing()
        {
            Guard.ValidFileName("FileName", "Parameter");
        }

        [Fact]
        public void ValidFileName_InvalidName_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => Guard.ValidFileName("File/Name", "Parameter"));
        }

        [Fact]
        public void ValidModel_ValidValue_DoesNothing()
        {
            Guard.ValidModel(new MockupModel { Name  = "Name" }, "Parameter");
        }

        [Fact]
        public void ValidModel_InvalidModel_ValidValue_DoesNothing()
        {
            Assert.Throws<ValidationException>(() => Guard.ValidModel(new MockupModel(), "Parameter"));
        }

        [Fact]
        public void ValidModel_NullModel_ValidValue_DoesNothing()
        {
            Assert.Throws<ArgumentNullException>(() => Guard.ValidModel(null, "Parameter"));
        }

        [Theory]
        [InlineData(double.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(double.MaxValue)]
        public void ValidNumber_double_ValidValue_DoesNothing(double value)
        {
            Guard.ValidNumber(value, "Parameter");
        }

        [Theory]
        [InlineData(double.PositiveInfinity)]
        [InlineData(double.NegativeInfinity)]
        [InlineData(double.NaN)]
        public void ValidNumber_double_InvalidValue_ThrowsException(double value)
        {
            Assert.Throws<ArgumentException>(() => Guard.ValidNumber(value, "Parameter"));
        }

        [Theory]
        [InlineData(double.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(double.MaxValue)]
        public void ValidNumber_Double_ValidValue_DoesNothing(double value)
        {
            Guard.ValidNumber(value, "Parameter");
        }

        [Theory]
        [InlineData(double.PositiveInfinity)]
        [InlineData(double.NegativeInfinity)]
        [InlineData(double.NaN)]
        public void ValidNumber_Double_InvalidValue_ThrowsException(double value)
        {
            Assert.Throws<ArgumentException>(() => Guard.ValidNumber(value, "Parameter"));
        }

        [Theory]
        [InlineData(1, float.MinValue)]
        [InlineData(1, -1)]
        [InlineData(1, 0)]
        [InlineData(1, 1)]
        [InlineData(1, float.MaxValue)]
        [InlineData(float.MinValue, 1)]
        [InlineData(-1, 1)]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        [InlineData(float.MaxValue, 1)]
        public void ValidNumber_Vector_ValidValue_DoesNothing(float x, float y)
        {
            Guard.ValidNumber(new Vector2(x, y), "Parameter");
        }

        [Theory]
        [InlineData(1, float.PositiveInfinity)]
        [InlineData(1, float.NegativeInfinity)]
        [InlineData(1, float.NaN)]
        [InlineData(float.PositiveInfinity, 1)]
        [InlineData(float.NegativeInfinity, 1)]
        [InlineData(float.NaN, 1)]
        public void ValidNumber_Vector_InvalidValue_ThrowsException(float x, float y)
        {
            Assert.Throws<ArgumentException>(() => Guard.ValidNumber(new Vector2(x, y), "Parameter"));
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(1, 1)]
        [InlineData(1, float.MaxValue)]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        [InlineData(float.MaxValue, 1)]
        public void ValidPositiveNumber_Vector_ValidValue_DoesNothing(float x, float y)
        {
            Guard.ValidPositiveNumber(new Vector2(x, y), "Parameter");
        }

        [Theory]
        [InlineData(1, float.PositiveInfinity)]
        [InlineData(1, float.NegativeInfinity)]
        [InlineData(1, float.NaN)]
        [InlineData(1, -1)]
        [InlineData(1, float.MinValue)]
        [InlineData(float.PositiveInfinity, 1)]
        [InlineData(float.NegativeInfinity, 1)]
        [InlineData(float.NaN, 1)]
        [InlineData(-1, 1)]
        [InlineData(float.MinValue, 1)]
        public void ValidPositiveNumber_Vector_InvalidValue_ThrowsException(float x, float y)
        {
            Assert.Throws<ArgumentException>(() => Guard.ValidPositiveNumber(new Vector2(x, y), "Parameter"));
        }
    }
}
