// ==========================================================================
// PropertyValue.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System;
using System.Collections.Generic;
using System.Globalization;

namespace GP.Utils
{
    /// <summary>
    /// Defines a property value.
    /// </summary>
    public sealed class PropertyValue
    {
        private readonly object rawValue;

        private static readonly HashSet<Type> AllowedTypes = new HashSet<Type>
        {
            typeof(string),
            typeof(bool),
            typeof(bool?),
            typeof(float),
            typeof(float?),
            typeof(double),
            typeof(double?),
            typeof(int),
            typeof(int?),
            typeof(long),
            typeof(long?),
            typeof(TimeSpan),
            typeof(TimeSpan?),
            typeof(DateTime),
            typeof(DateTime?),
            typeof(Guid),
            typeof(Guid?),
            typeof(DateTimeOffset),
            typeof(DateTimeOffset?)
        };

        /// <summary>
        /// Gets the raw property value.
        /// </summary>
        public object RawValue
        {
            get { return rawValue; }
        }

        internal PropertyValue(object rawValue)
        {
            if (rawValue != null && !AllowedTypes.Contains(rawValue.GetType()))
            {
                throw new ArgumentException("The type is not supported.", nameof(rawValue));
            }

            this.rawValue = rawValue;
        }

        /// <summary>
        /// Gets the property value as string.
        /// </summary>
        /// <returns>
        /// The property value as string.
        /// </returns>
        public override string ToString()
        {
            return rawValue?.ToString();
        }

        /// <summary>
        /// Gets the property value as <see cref="bool"/> and converts the value, if necessary using the specified culture.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns>
        /// The property value as <see cref="bool"/>.
        /// </returns>
        public bool ToBoolean(CultureInfo culture)
        {
            return ToOrParseValue(culture, bool.Parse);
        }

        /// <summary>
        /// Gets the property value as nullable <see cref="bool"/> and converts the value, if necessary using the specified culture.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns>
        /// The property boolean as nullable <see cref="bool"/>.
        /// </returns>
        public bool? ToNullableBoolean(CultureInfo culture)
        {
            return ToNullableOrParseValue(culture, bool.Parse);
        }

        /// <summary>
        /// Gets the property value as <see cref="float"/> and converts the value, if necessary using the specified culture.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns>
        /// The property boolean as <see cref="float"/>.
        /// </returns>
        public float ToSingle(CultureInfo culture)
        {
            return ToOrParseValue(culture, x => float.Parse(x, culture));
        }

        /// <summary>
        /// Gets the property value as nullable <see cref="float"/> and converts the value, if necessary using the specified culture.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns>
        /// The property boolean as nullable <see cref="float"/>.
        /// </returns>
        public float? ToNullableSingle(CultureInfo culture)
        {
            return ToNullableOrParseValue(culture, x => float.Parse(x, culture));
        }

        /// <summary>
        /// Gets the property value as <see cref="double"/> and converts the value, if necessary using the specified culture.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns>
        /// The property boolean as <see cref="double"/>.
        /// </returns>
        public double ToDouble(CultureInfo culture)
        {
            return ToOrParseValue(culture, x => double.Parse(x, culture));
        }

        /// <summary>
        /// Gets the property value as nullable <see cref="double"/> and converts the value, if necessary using the specified culture.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns>
        /// The property boolean as nullable <see cref="double"/>.
        /// </returns>
        public double? ToNullableDouble(CultureInfo culture)
        {
            return ToNullableOrParseValue(culture, x => double.Parse(x, culture));
        }

        /// <summary>
        /// Gets the property value as <see cref="int"/> and converts the value, if necessary using the specified culture.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns>
        /// The property boolean as nullable <see cref="int"/>.
        /// </returns>
        public int ToInt32(CultureInfo culture)
        {
            return ToOrParseValue(culture, x => int.Parse(x, culture));
        }

        /// <summary>
        /// Gets the property value as <see cref="int"/> and converts the value, if necessary using the specified culture.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns>
        /// The property boolean as nullable <see cref="int"/>.
        /// </returns>
        public int? ToNullableInt32(CultureInfo culture)
        {
            return ToNullableOrParseValue(culture, x => int.Parse(x, culture));
        }

        /// <summary>
        /// Gets the property value as <see cref="long"/> and converts the value, if necessary using the specified culture.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns>
        /// The property boolean as <see cref="long"/>.
        /// </returns>
        public long ToInt64(CultureInfo culture)
        {
            return ToOrParseValue(culture, x => long.Parse(x, culture));
        }

        /// <summary>
        /// Gets the property value as nullable <see cref="long"/> and converts the value, if necessary using the specified culture.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns>
        /// The property boolean as nullable <see cref="long"/>.
        /// </returns>
        public long? ToNullableInt64(CultureInfo culture)
        {
            return ToNullableOrParseValue(culture, x => long.Parse(x, culture));
        }

        /// <summary>
        /// Gets the property value as <see cref="TimeSpan"/> and converts the value, if necessary using the specified culture.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns>
        /// The property boolean as <see cref="TimeSpan"/>.
        /// </returns>
        public TimeSpan ToTimeSpan(CultureInfo culture)
        {
            return ToOrParseValue(culture, x => TimeSpan.Parse(x, culture));
        }

        /// <summary>
        /// Gets the property value as nullable <see cref="TimeSpan"/> and converts the value, if necessary using the specified culture.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns>
        /// The property boolean as nullable <see cref="TimeSpan"/>.
        /// </returns>
        public TimeSpan? ToNullableTimeSpan(CultureInfo culture)
        {
            return ToNullableOrParseValue(culture, x => TimeSpan.Parse(x, culture));
        }

        /// <summary>
        /// Gets the property value as <see cref="DateTime"/> and converts the value, if necessary using the specified culture.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns>
        /// The property boolean as <see cref="DateTime"/>.
        /// </returns>
        public DateTime ToDateTime(CultureInfo culture)
        {
            return ToOrParseValue(culture, x => DateTime.Parse(x, culture));
        }

        /// <summary>
        /// Gets the property value as nullable <see cref="DateTime"/> and converts the value, if necessary using the specified culture.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns>
        /// The property boolean as nullable <see cref="DateTime"/>.
        /// </returns>
        public DateTime? ToNullableDateTime(CultureInfo culture)
        {
            return ToNullableOrParseValue(culture, x => DateTime.Parse(x, culture));
        }

        /// <summary>
        /// Gets the property value as <see cref="Guid"/> and converts the value, if necessary using the specified culture.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns>
        /// The property boolean as <see cref="Guid"/>.
        /// </returns>
        public Guid ToGuid(CultureInfo culture)
        {
            return ToOrParseValue(culture, Guid.Parse);
        }

        /// <summary>
        /// Gets the property value as nullable <see cref="Guid"/> and converts the value, if necessary using the specified culture.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns>
        /// The property boolean as nullable <see cref="Guid"/>.
        /// </returns>
        public Guid? ToNullableGuid(CultureInfo culture)
        {
            return ToNullableOrParseValue(culture, Guid.Parse);
        }

        /// <summary>
        /// Gets the property value as <see cref="DateTimeOffset"/> and converts the value, if necessary using the specified culture.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns>
        /// The property boolean as <see cref="DateTimeOffset"/>.
        /// </returns>
        public DateTimeOffset ToDateTimeOffset(CultureInfo culture)
        {
            return ToOrParseValue(culture, x => DateTimeOffset.Parse(x, culture));
        }

        /// <summary>
        /// Gets the property value as nullable <see cref="DateTimeOffset"/> and converts the value, if necessary using the specified culture.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns>
        /// The property boolean as nullable <see cref="DateTimeOffset"/>.
        /// </returns>
        public DateTimeOffset? ToNullableDateTimeOffset(CultureInfo culture)
        {
            return ToNullableOrParseValue(culture, x => DateTimeOffset.Parse(x, culture));
        }

        private T? ToNullableOrParseValue<T>(IFormatProvider culture, Func<string, T> parser) where T : struct
        {
            T result;

            return TryParse(culture, parser, out result) ? result : (T?)null;
        }

        private T ToOrParseValue<T>(IFormatProvider culture, Func<string, T> parser)
        {
            T result;

            return TryParse(culture, parser, out result) ? result : default(T);
        }

        private bool TryParse<T>(IFormatProvider culture, Func<string, T> parser, out T result)
        {
            var value = rawValue;

            if (value != null)
            {
                var valueType = value.GetType();

                if (valueType == typeof(T))
                {
                    result = (T)value;
                }
                else if (valueType == typeof(string))
                {
                    result = Parse(parser, valueType, value);
                }
                else
                {
                    result = Convert<T>(culture, value, valueType);
                }

                return true;
            }

            result = default(T);

            return false;
        }

        private static T Convert<T>(IFormatProvider culture, object value, Type valueType)
        {
            var requestedType = typeof(T);

            try
            {
                return (T)System.Convert.ChangeType(value, requestedType, culture);
            }
            catch (OverflowException)
            {
                string message = $"The property has type '{valueType}' and cannot be casted to '{requestedType}' because it is either too small or large.";

                throw new InvalidCastException(message);
            }
            catch (InvalidCastException)
            {
                string message = $"The property has type '{valueType}' and cannot be casted to '{requestedType}'.";

                throw new InvalidCastException(message);
            }
        }

        private static T Parse<T>(Func<string, T> parser, Type valueType, object value)
        {
            var requestedType = typeof(T);

            try
            {
                return parser(value.ToString());
            }
            catch (Exception e)
            {
                string message = $"The property has type '{valueType}' and cannot be casted to '{requestedType}'.";

                throw new InvalidCastException(message, e);
            }
        }
    }
}
