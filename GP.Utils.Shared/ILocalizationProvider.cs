﻿// ==========================================================================
// ILocalizationProvider.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System.Globalization;
// ReSharper disable UnusedParameter.Global

namespace GP.Utils
{
    /// <summary>
    /// Provides localized strings.
    /// </summary>
    public interface ILocalizationProvider
    {
        /// <summary>
        /// Gets the string with the specified key.
        /// </summary>
        /// <param name="key">The key to identity the resource. Cannot be null or empty.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>
        /// The localized string with the specified key.
        /// </returns>
        /// <exception cref="T:Systen.ArgumentException"><paramref name="key" /> is null or empty.</exception>
        string GetString(string key, CultureInfo culture);
    }
}
