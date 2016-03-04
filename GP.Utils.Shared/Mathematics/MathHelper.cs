// ==========================================================================
// MathHelper.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System;
using System.Numerics;
using System.Runtime.CompilerServices;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GP.Utils.Mathematics
{
    /// <summary>
    /// Defines helper methods for math operations.
    /// </summary>
    public static class MathHelper
    {
        /// <summary>
        /// A positive infinity vector.
        /// </summary>
        public static readonly Vector2 PositiveInfinityVector2 = new Vector2(float.PositiveInfinity, float.PositiveInfinity);

        /// <summary>
        /// A negative infinity vector.
        /// </summary>
        public static readonly Vector2 NegativeInfinityVector2 = new Vector2(float.NegativeInfinity, float.NegativeInfinity);

        /// <summary>
        /// Interpolates between the first and the second value depending on the fraction value.
        /// </summary>
        /// <param name="fraction">The fraction.</param>
        /// <param name="l">The first value.</param>
        /// <param name="r">The second value.</param>
        /// <returns>s
        /// The resulting value.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Interpolate(float fraction, Vector2 l, Vector2 r)
        {
            return new Vector2(Interpolate(fraction, l.X, r.X), Interpolate(fraction, l.Y, r.Y));
        }

        /// <summary>
        /// Interpolates between the first and the right value depending on the fraction value.
        /// </summary>
        /// <param name="fraction">The fraction.</param>
        /// <param name="l">The first value.</param>
        /// <param name="r">The second value.</param>
        /// <returns>
        /// The resulting value.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Interpolate(float fraction, float l, float r)
        {
            return fraction < 0 ? l : fraction > 1 ? r : ((r - l) * fraction) + l;
        }

        /// <summary>
        /// Interpolates between the first and the right value depending on the fraction value.
        /// </summary>
        /// <param name="fraction">The fraction.</param>
        /// <param name="l">The first value.</param>
        /// <param name="r">The second value.</param>
        /// <returns>
        /// The resulting value.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Interpolate(double fraction, double l, double r)
        {
            return fraction < 0 ? l : fraction > 1 ? r : ((r - l) * fraction) + l;
        }

        /// <summary>
        /// Rounds the specified values to a multiple of the specified factor.
        /// </summary>
        /// <param name="value">The value to round.</param>
        /// <param name="factor">The factor which is used to round the value parameter.
        /// Must be greater than zero</param>
        /// <returns>The rounded value by the specified factor.</returns>
        /// <exception cref="ArgumentException"><paramref name="factor"/> is greater than zero.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 RoundToMultipleOf(Vector2 value, float factor)
        {
            return new Vector2(RoundToMultipleOf(value.X, factor), RoundToMultipleOf(value.Y, factor));
        }

        /// <summary>
        /// Rounds the specified values to a multiple of the specified factor.
        /// </summary>
        /// <param name="value">The value to round.</param>
        /// <param name="factor">The factor which is used to round the value parameter.
        /// Must be greater than zero</param>
        /// <returns>The rounded value by the specified factor.</returns>
        /// <exception cref="ArgumentException"><paramref name="factor"/> is greater than zero.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float RoundToMultipleOf(float value, float factor)
        {
            Guard.GreaterThan(factor, 0, nameof(factor));

            return (float)Math.Floor(value / factor) * factor;
        }

        /// <summary>
        /// Rounds the specified values to a multiple of the specified factor.
        /// </summary>
        /// <param name="value">The value to round.</param>
        /// <param name="factor">The factor which is used to round the value parameter.
        /// Must be greater than zero</param>
        /// <returns>The rounded value by the specified factor.</returns>
        /// <exception cref="ArgumentException"><paramref name="factor"/> is greater than zero.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double RoundToMultipleOf(double value, double factor)
        {
            Guard.GreaterThan(factor, 0, nameof(factor));

            return Math.Floor(value / factor) * factor;
        }

        /// <summary>
        /// Rounds the vector
        /// </summary>
        /// <param name="value">The vector to round.</param>
        /// <returns>
        /// The rounded value.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Round(this Vector2 value)
        {
            return new Vector2((float)Math.Round(value.X), (float)Math.Round(value.Y));
        }

        /// <summary>
        /// Rounds the vector
        /// </summary>
        /// <param name="value">The vector to round.</param>
        /// <returns>
        /// The rounded value.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Round(ref Vector2 value)
        {
            value.X = (float)Math.Round(value.X);
            value.Y = (float)Math.Round(value.Y);
        }

        /// <summary>
        /// Rounds the vector to a multiple of two.
        /// </summary>
        /// <param name="value">The vector to round.</param>
        /// <returns>
        /// The rounded value.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RoundToMultipleOfTwo(ref Vector2 value)
        {
            value.X = (float)Math.Round(value.X);
            value.Y = (float)Math.Round(value.Y);

            if (value.Y % 2 == 1)
            {
                value.Y += 1;
            }

            if (value.X % 2 == 1)
            {
                value.X += 1;
            }
        }

        /// <summary>
        /// Rounds the vector to a multiple of two.
        /// </summary>
        /// <param name="value">The vector to round.</param>
        /// <returns>
        /// The rounded value.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 RoundToMultipleOfTwo(Vector2 value)
        {
            value.X = (float)Math.Round(value.X);
            value.Y = (float)Math.Round(value.Y);

            if (value.Y % 2 == 1)
            {
                value.Y += 1;
            }

            if (value.X % 2 == 1)
            {
                value.X += 1;
            }

            return value;
        }

        /// <summary>
        /// Rounds the vector
        /// </summary>
        /// <param name="value">The vector to round.</param>
        /// <returns>
        /// The rounded value.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Round(this Vector3 value)
        {
            return new Vector3((float)Math.Round(value.X), (float)Math.Round(value.Y), (float)Math.Round(value.Z));
        }

        /// <summary>
        /// Rounds the vector
        /// </summary>
        /// <param name="value">The vector to round.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Round(ref Vector3 value)
        {
            value.X = (float)Math.Round(value.X);
            value.Y = (float)Math.Round(value.Y);
            value.Z = (float)Math.Round(value.Z);
        }

        /// <summary>
        /// Converts the angle between two vectors, represented by simple Vectors.
        /// </summary>
        /// <param name="point1">Source point.</param>
        /// <param name="point2">Source point.</param>
        /// <returns>The angle between two vectors.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetAngleBetween(this Vector2 point1, Vector2 point2)
        {
            float y = (point1.X * point2.Y) - (point2.X * point1.Y);
            float x = (point1.X * point2.X) + (point1.Y * point2.Y);

            return (float)Math.Atan2(y, x).ToDegree().ToPositiveDegree();
        }

        /// <summary>
        /// Converts an angle in radian to an angle in degree.
        /// </summary>
        /// <param name="degree">The angle in degrees.</param>
        /// <returns>The angle in degree.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ToPositiveDegree(this double degree)
        {
            while (degree < 0)
            {
                degree += 360;
            }

            while (degree >= 360)
            {
                degree -= 360;
            }

            return degree;
        }

        /// <summary>
        /// Converts an angle in radian to an angle in degree.
        /// </summary>
        /// <param name="degree">The angle in degrees.</param>
        /// <returns>The angle in degree.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ToPositiveDegree(this float degree)
        {
            while (degree < 0)
            {
                degree += 360;
            }

            while (degree >= 360)
            {
                degree -= 360;
            }

            return degree;
        }

        /// <summary>
        /// Converts an angle in degree to an angle in radian.
        /// </summary>
        /// <param name="degree">The angle in degree.</param>
        /// <returns>The angle in radian.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ToRad(this double degree)
        {
            return degree * Math.PI / 180;
        }

        /// <summary>
        /// Converts an angle in radian to an angle in degree.
        /// </summary>
        /// <param name="rad">The angle in radian.</param>
        /// <returns>The angle in degree.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ToDegree(this double rad)
        {
            return rad * 180 / Math.PI;
        }

        /// <summary>
        /// Converts an angle in degree to an angle in radian.
        /// </summary>
        /// <param name="degree">The angle in degree.</param>
        /// <returns>The angle in radian.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ToRad(this float degree)
        {
            return degree * (float)Math.PI / 180;
        }

        /// <summary>
        /// Converts an angle in radian to an angle in degree.
        /// </summary>
        /// <param name="rad">The angle in radian.</param>
        /// <returns>The angle in degree.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ToDegree(this float rad)
        {
            return rad * 180 / (float)Math.PI;
        }

        /// <summary>
        /// Rotates the specified position around the center point with the specified 
        /// rotation in radian.
        /// </summary>
        /// <param name="v">The position to rotate.</param>
        /// <param name="center">The center point.</param>
        /// <param name="radian">The rotation in radian.</param>
        /// <returns>The rotated position.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 RotatedVector2(Vector2 v, Vector2 center, double radian)
        {
            return RotatedVector2(v.X, v.Y, center, radian);
        }

        /// <summary>
        /// Rotates the specified position around the center point with the specified 
        /// rotation in radian.
        /// </summary>
        /// <param name="x">The x position to rotate.</param>
        /// <param name="y">The y position to rotate.</param>
        /// <param name="center">The center point.</param>
        /// <param name="radian">The rotation in radian.</param>
        /// <returns>The rotated position.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 RotatedVector2(float x, float y, Vector2 center, double radian)
        {
            x -= center.X;
            y -= center.Y;

            double cos = Math.Cos(radian);
            double sin = Math.Sin(radian);

            Vector2 result = new Vector2(
                (float)((x * cos) - (y * sin) + center.X),
                (float)((x * sin) + (y * cos) + center.Y));

            return result;
        }

        /// <summary>
        /// Rotates the specified position around the center point with the specified 
        /// rotation in radian.
        /// </summary>
        /// <param name="v">The position to rotate.</param>
        /// <param name="center">The center point.</param>
        /// <param name="cos">The cosinus of the rotation.</param>
        /// <param name="sin">The sinus of the rotation.</param>
        /// <returns>The rotated position.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 RotatedVector2(Vector2 v, Vector2 center, double cos, double sin)
        {
            return RotatedVector2(v.X, v.Y, center, cos, sin);
        }

        /// <summary>
        /// Rotates the specified position around the center point with the specified 
        /// rotation in radian.
        /// </summary>
        /// <param name="x">The x position to rotate.</param>
        /// <param name="y">The y position to rotate.</param>
        /// <param name="center">The center point.</param>
        /// <param name="cos">The cosinus of the rotation.</param>
        /// <param name="sin">The sinus of the rotation.</param>
        /// <returns>The rotated position.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 RotatedVector2(float x, float y, Vector2 center, double cos, double sin)
        {
            x -= center.X;
            y -= center.Y;

            Vector2 result = new Vector2(
                (float)((x * cos) - (y * sin) + center.X),
                (float)((x * sin) + (y * cos) + center.Y));

            return result;
        }

        /// <summary>
        /// Calculates the minimum between the vector and the scalar.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="scalar">The scalar</param>
        /// <returns>
        /// The minimum between the vector and the scalar.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Min(Vector2 vector, float scalar)
        {
            return Vector2.Min(vector, new Vector2(scalar, scalar));
        }

        /// <summary>
        /// Calculates the maximum between the vector and the scalar.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="scalar">The scalar</param>
        /// <returns>
        /// The maximum between the vector and the scalar.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Max(Vector2 vector, float scalar)
        {
            return Vector2.Max(vector, new Vector2(scalar, scalar));
        }

        /// <summary>
        /// Determines if two points are about equal.
        /// </summary>
        /// <param name="l">The first point.</param>
        /// <param name="r">The second point.</param>
        /// <returns>
        /// True, if two point values are more or less equal.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AboutEqual(Vector2 l, Vector2 r)
        {
            return AboutEqual(l.X, r.X) && AboutEqual(l.Y, r.Y);
        }

        /// <summary>
        /// Determines if two double values are about equal.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>
        /// True, if two double values are more or less equal.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AboutEqual(float x, float y)
        {
            double epsilon = Math.Max(Math.Abs(x), Math.Abs(y)) * 1E-15;

            return Math.Abs(x - y) <= epsilon;
        }

        /// <summary>
        /// Determines if two double values are about equal.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>
        /// True, if two double values are more or less equal.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AboutEqual(double x, double y)
        {
            double epsilon = Math.Max(Math.Abs(x), Math.Abs(y)) * 1E-15;

            return Math.Abs(x - y) <= epsilon;
        }
    }
}
