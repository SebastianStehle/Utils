// ==========================================================================
// PropertiesBagExtensions.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System;
using System.Globalization;

namespace GP.Utils
{
    /// <summary>
    /// Extension methods for the <see cref="PropertiesBag"/> class.
    /// </summary>
    public static class PropertiesBagExtensions
    {
        /// <summary>
        /// Tries to get the property with the specified name as <see cref="string"/>.
        /// </summary>
        /// <param name="properties">The <see cref="PropertiesBag"/>.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// True, of the property is found and can be converted, false otherwise.
        /// </returns>
        public static bool TryParseString(this PropertiesBag properties, string propertyName, out string value)
        {
            bool result = false;

            value = null;

            if (properties.Contains(propertyName))
            {
                value = properties[propertyName].ToString();

                result = true;
            }

            return result;
        }

        /// <summary>
        /// Tries to get the property with the specified name as nullable <see cref="int"/>.
        /// </summary>
        /// <param name="properties">The <see cref="PropertiesBag"/>.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// True, of the property is found and can be converted, false otherwise.
        /// </returns>
        public static bool TryParseNullableInt32(this PropertiesBag properties, string propertyName, out int? value)
        {
            bool result = false;

            value = null;

            if (properties.Contains(propertyName))
            {
                try
                {
                    value = properties[propertyName].ToNullableInt32(CultureInfo.CurrentCulture);

                    result = true;
                }
                catch (InvalidCastException)
                {
                    result = false;
                }
            }

            return result;
        }

        /// <summary>
        /// Tries to get the property with the specified name as <see cref="int"/>.
        /// </summary>
        /// <param name="properties">The <see cref="PropertiesBag"/>.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// True, of the property is found and can be converted, false otherwise.
        /// </returns>
        public static bool TryParseInt32(this PropertiesBag properties, string propertyName, out int value)
        {
            bool result = false;

            value = 0;

            if (properties.Contains(propertyName))
            {
                try
                {
                    value = properties[propertyName].ToInt32(CultureInfo.CurrentCulture);

                    result = true;
                }
                catch (InvalidCastException)
                {
                    result = false;
                }
            }

            return result;
        }

        /// <summary>
        /// Tries to get the property with the specified name as enum.
        /// </summary>
        /// <typeparam name="TEnum">The enum type.</typeparam>
        /// <param name="properties">The <see cref="PropertiesBag"/>.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// True, of the property is found and can be converted, false otherwise.
        /// </returns>
        public static bool TryParseEnum<TEnum>(this PropertiesBag properties, string propertyName, out TEnum value) where TEnum : struct
        {
            bool result = false;

            value = default(TEnum);

            if (properties.Contains(propertyName))
            {
                string enumValue = properties[propertyName].ToString();

                if (Enum.TryParse(enumValue, out value))
                {
                    result = true;
                }
            }

            return result;
        }
    }
}
