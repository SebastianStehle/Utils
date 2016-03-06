// ==========================================================================
// NoopLocalizationProviderTest.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System.Globalization;
using GP.Utils;
using Xunit;

namespace Tests.Facts
{
    public class NoopLocalizationProviderTest
    {
        [Theory]
        [InlineData("Key1")]
        [InlineData("Key2")]
        [InlineData("Key3")]
        public void GetString_ReturnsKey(string key)
        {
            NoopLocalizationProvider provider = new NoopLocalizationProvider();

            Assert.Equal(key, provider.GetString(key, CultureInfo.CurrentCulture));
        }
    }
}
