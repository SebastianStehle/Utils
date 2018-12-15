// ==========================================================================
// LocalizationManagerTest.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System;
using System.Globalization;
using FakeItEasy;
using GP.Utils;
using Tests.Given;
using Xunit;

namespace Tests.Facts
{
    public class LocalizationManagerTest : GivenLocalization
    {
        private readonly ILocalizationProvider provider = A.Fake<ILocalizationProvider>();

        public LocalizationManagerTest()
        {
            LocalizationManager.Provider = provider;
        }

        [Fact]
        public void GetFormattedString_ResourceKeyIsNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => LocalizationManager.GetFormattedString(null));
        }

        [Fact]
        public void GetFormattedString_ResourceKeyIsEmpty_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => LocalizationManager.GetFormattedString(string.Empty));
        }

        [Fact]
        public void GetFormattedString_NoResourceProvider_ThrowsException()
        {
            LocalizationManager.Provider = null;

            Assert.Throws<InvalidOperationException>(() => LocalizationManager.GetFormattedString("Key"));
        }

        [Fact]
        public void GetFormattedString_FromProvider_ReturnsValue()
        {
            A.CallTo(() => provider.GetString("Key1", CultureInfo.CurrentCulture))
                .Returns("Foo");

            string result = LocalizationManager.GetFormattedString("Key1");

            Assert.Equal("Foo", result);
        }

        [Fact]
        public void GetFormattedString_FromProviderWithArguments_ReturnsValue()
        {
            A.CallTo(() => provider.GetString("Key1", CultureInfo.CurrentCulture))
                .Returns("Foo {0}");

            string result = LocalizationManager.GetFormattedString("Key1", 123);

            Assert.Equal("Foo 123", result);
        }

        [Fact]
        public void GetFormattedString_FormatException_PassedThrough()
        {
            A.CallTo(() => provider.GetString("Key1", CultureInfo.CurrentCulture))
                .Returns("{0} {1}");

            Assert.Throws<FormatException>(() => LocalizationManager.GetFormattedString("Key1", 123));
        }

        [Fact]
        public void GetFormattedString_ProviderThrowsException_PassedThrough()
        {
            A.CallTo(() => provider.GetString("Key1", CultureInfo.CurrentCulture))
                .Throws(new InvalidCastException());

            Assert.Throws<InvalidCastException>(() => LocalizationManager.GetFormattedString("Key1"));
        }

        [Fact]
        public void GetFormattedString_ProviderReturnsNull_ThrowsException()
        {
            A.CallTo(() => provider.GetString("Key1", CultureInfo.CurrentCulture))
                .Returns(null);

            Assert.Throws<ArgumentException>(() => LocalizationManager.GetFormattedString("Key1"));
        }

        [Fact]
        public void GetString_ResourceKeyIsNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => LocalizationManager.GetString(null));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void GetString_ResourceKeyIsEmpty_ThrowsException(string key)
        {
            Assert.Throws<ArgumentException>(() => LocalizationManager.GetString(key));
        }

        [Fact]
        public void GetString_NoResourceProvider_ThrowsException()
        {
            LocalizationManager.Provider = null;

            Assert.Throws<InvalidOperationException>(() => LocalizationManager.GetString("Key"));
        }

        [Fact]
        public void GetString_FromProvider_ReturnsValue()
        {
            A.CallTo(() => provider.GetString("Key1", CultureInfo.CurrentCulture))
                .Returns("Foo");

            string result = LocalizationManager.GetString("Key1");

            Assert.Equal("Foo", result);
        }

        [Fact]
        public void GetString_ProviderThrowsException_PassedThrough()
        {
            A.CallTo(() => provider.GetString("Key1", CultureInfo.CurrentCulture))
                .Throws(new InvalidCastException());

            Assert.Throws<InvalidCastException>(() => LocalizationManager.GetString("Key1"));
        }

        [Fact]
        public void GetString_ProviderReturnsNull_ThrowsException()
        {
            A.CallTo(() => provider.GetString("Key1", CultureInfo.CurrentCulture))
                .Returns(null);

            Assert.Throws<ArgumentException>(() => LocalizationManager.GetString("Key1"));
        }

        [Fact]
        public void TryGetString_ResourceKeyIsNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => LocalizationManager.TryGetString(null));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void TryGetString_ResourceKeyIsEmpty_ThrowsException(string key)
        {
            Assert.Throws<ArgumentException>(() => LocalizationManager.TryGetString(key));
        }

        [Fact]
        public void TryGetString_NoResourceProvider_ReturnsKey()
        {
            LocalizationManager.Provider = null;

            string result = LocalizationManager.TryGetString("Key");

            Assert.Equal("Key", result);
        }

        [Fact]
        public void TryGetString_FromProvider_ReturnsValue()
        {
            A.CallTo(() => provider.GetString("Key1", CultureInfo.CurrentCulture))
                .Returns("Foo");

            string result = LocalizationManager.TryGetString("Key1");

            Assert.Equal("Foo", result);
        }

        [Fact]
        public void TryGetString_ProviderThrowsException_ReturnsKey()
        {
            A.CallTo(() => provider.GetString("Key1", CultureInfo.CurrentCulture))
                .Throws(new InvalidCastException());

            string result = LocalizationManager.TryGetString("Key1");

            Assert.Equal("Key1", result);
        }

        [Fact]
        public void TryGetString_ProviderReturnsNull_ReturnsFallback()
        {
            A.CallTo(() => provider.GetString("Key1", CultureInfo.CurrentCulture))
                .Returns(null);

            string result = LocalizationManager.TryGetString("Key1", "Fallback");

            Assert.Equal("Fallback", result);
        }

        [Fact]
        public void TryGetString_ProviderAndFallbackNull_ReturnsFallback()
        {
            A.CallTo(() => provider.GetString("MyKey1", CultureInfo.CurrentCulture))
                .Returns(null);

            string result = LocalizationManager.TryGetString("MyKey1");

            Assert.Equal("My Key1", result);
        }

        [Fact]
        public void TryGetFormattedString_ResourceKeyIsNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => LocalizationManager.TryGetFormattedString(null));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void TryGetFormattedString_ResourceKeyIsEmpty_ThrowsException(string key)
        {
            Assert.Throws<ArgumentException>(() => LocalizationManager.TryGetFormattedString(key));
        }

        [Fact]
        public void TryGetFormattedString_NoResourceProvider_ThrowsException()
        {
            LocalizationManager.Provider = null;

            string result = LocalizationManager.TryGetFormattedString("Key");

            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void TryGetFormattedString_FromProvider_ReturnsValue()
        {
            A.CallTo(() => provider.GetString("Key1", CultureInfo.CurrentCulture))
                .Returns("Foo");

            string result = LocalizationManager.TryGetFormattedString("Key1");

            Assert.Equal("Foo", result);
        }

        [Fact]
        public void TryGetFormattedString_FromProviderWithArguments_ReturnsValue()
        {
            A.CallTo(() => provider.GetString("Key1", CultureInfo.CurrentCulture))
                .Returns("Foo {0}");

            string result = LocalizationManager.TryGetFormattedString("Key1", 123);

            Assert.Equal("Foo 123", result);
        }

        [Fact]
        public void TryGetFormattedString_FormatException_ReturnsEmpty()
        {
            A.CallTo(() => provider.GetString("Key1", CultureInfo.CurrentCulture))
                .Returns("{0} {1}");

            string result = LocalizationManager.TryGetFormattedString("Key1", 123);

            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void TryGetFormattedString_ProviderThrowsException_ReturnsEmpty()
        {
            A.CallTo(() => provider.GetString("Key1", CultureInfo.CurrentCulture))
                .Throws(new InvalidCastException());

            string result = LocalizationManager.TryGetFormattedString("Key1", "Fallback", new object[0]);

            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void TryGetFormattedString_ProviderReturnsNull_ReturnsFallback()
        {
            A.CallTo(() => provider.GetString("Key1", CultureInfo.CurrentCulture))
                .Returns(null);

            string result = LocalizationManager.TryGetFormattedString("Key1", "Fallback", new object[0]);

            Assert.Equal("Fallback", result);
        }

        [Fact]
        public void TryGetFormattedString_ProviderAndFallbackNull_ReturnsFallback()
        {
            A.CallTo(() => provider.GetString("MyKey1", CultureInfo.CurrentCulture))
                .Returns(null);

            string result = LocalizationManager.TryGetFormattedString("MyKey1", null, new object[0]);

            Assert.Equal("My Key1", result);
        }
    }
}
