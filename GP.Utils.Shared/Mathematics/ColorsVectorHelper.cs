// ==========================================================================
// ColorsVectorHelper.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System.Globalization;
using System.Numerics;
using System.Text;

namespace GP.Utils.Mathematics
{
    /// <summary>
    /// Provides some helper methods for dealing with colors.
    /// </summary>
    public static class ColorsVectorHelper
    {
        /// <summary>
        /// Converts the integer to a string, not taking int account the alpha value.
        /// </summary>
        /// <param name="intColor">The color value.</param>
        /// <returns>The resulting string.</returns>
        public static string ConvertToRGBString(int intColor)
        {
            return ConvertToRGBString(ConvertToColor(intColor));
        }

        /// <summary>
        /// Converts the color to a string, not taking int account the alpha value.
        /// </summary>
        /// <param name="color">The color value.</param>
        /// <returns>The resulting string.</returns>
        public static string ConvertToRGBString(Vector3 color)
        {
            var builder = new StringBuilder(7);

            builder.Append("#");

            builder.AppendFormat(CultureInfo.InvariantCulture, "{0:X2}", (byte)(color.X * 255));
            builder.AppendFormat(CultureInfo.InvariantCulture, "{0:X2}", (byte)(color.Y * 255));
            builder.AppendFormat(CultureInfo.InvariantCulture, "{0:X2}", (byte)(color.Z * 255));

            return builder.ToString();
        }

        /// <summary>
        /// Converts the color to a integer, not taking int account the alpha value.
        /// </summary>
        /// <param name="color">The color value.</param>
        /// <returns>The resulting integer.</returns>
        public static int ConvertToInt(Vector3 color)
        {
            return (byte)(color.X * 255) << 16 | (byte)(color.Y * 255) << 8 | (byte)(color.Z * 255);
        }

        /// <summary>
        /// Converts the integer to a color object, not taking int account the alpha value.
        /// </summary>
        /// <param name="intColor">The color value.</param>
        /// <returns>The resulting color object.</returns>
        public static Vector3 ConvertToColor(int intColor)
        {
            var b = intColor & 0xFF;

            var g = (intColor >> 8) & 0xFF;
            var r = (intColor >> 16) & 0xFF;

            var color = new Vector3((float)r / 255, (float)g / 255, (float)b / 255);

            return color;
        }
    }
}
