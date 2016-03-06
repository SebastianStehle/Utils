// ==========================================================================
// MathHelperTest.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System;
using System.Numerics;
using GP.Utils.Mathematics;
using Xunit;

// ReSharper disable InvokeAsExtensionMethod
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable ConvertToConstant.Local

namespace Tests.Facts
{
    public class MathHelperTest
    {
        private readonly Random random = new Random();

        public float Random10()
        {
            return (float)(random.NextDouble() * 10);
        }

        [Fact]
        public void Constants()
        {
            float.IsPositiveInfinity(MathHelper.PositiveInfinityVector2.X);
            float.IsPositiveInfinity(MathHelper.PositiveInfinityVector2.Y);

            float.IsNegativeInfinity(MathHelper.NegativeInfinityVector2.X);
            float.IsNegativeInfinity(MathHelper.NegativeInfinityVector2.Y);
        }

        [Fact]
        public void RoundToMultipleOfTwo()
        {
            Vector2 v1 = new Vector2(1.2f, 5.2f);
            Vector2 v2 = new Vector2(1.6f, 5.7f);

            Assert.Equal(new Vector2(2, 6), MathHelper.RoundToMultipleOfTwo(v1));
            Assert.Equal(new Vector2(2, 6), MathHelper.RoundToMultipleOfTwo(v2));

            MathHelper.RoundToMultipleOfTwo(ref v1);
            MathHelper.RoundToMultipleOfTwo(ref v2);

            Assert.Equal(new Vector2(2, 6), v1);
            Assert.Equal(new Vector2(2, 6), v2);
        }

        [Fact]
        public void RoundToMultipleOf_Vector()
        {
            Vector2 v1 = new Vector2(13, 18);

            Assert.Equal(new Vector2(10, 20), MathHelper.RoundToMultipleOf(v1, 10));
        }

        [Fact]
        public void RoundToMultipleOf_Single()
        {
            Assert.Equal(10f, MathHelper.RoundToMultipleOf(13f, 10f));
            Assert.Equal(20f, MathHelper.RoundToMultipleOf(16f, 10f));
        }

        [Fact]
        public void RoundToMultipleOf_Double()
        {
            Assert.Equal(10d, MathHelper.RoundToMultipleOf(13d, 10d));
            Assert.Equal(20d, MathHelper.RoundToMultipleOf(16d, 10d));
        }

        [Fact]
        public void RoundVector2_ByValue_ByRef()
        {
            Vector2 source = new Vector2(Random10(), Random10());
            Vector2 result = source.Round();

            Assert.Equal(Math.Round(source.X), result.X);
            Assert.Equal(Math.Round(source.Y), result.Y);

            result = source;

            MathHelper.Round(ref result);

            Assert.Equal(Math.Round(source.X), result.X);
            Assert.Equal(Math.Round(source.Y), result.Y);
        }

        [Fact]
        public void RoundVector3_ByValue_ByRef()
        {
            Vector3 source = new Vector3(Random10(), Random10(), Random10());
            Vector3 result = source.Round();

            Assert.Equal(Math.Round(source.X), result.X);
            Assert.Equal(Math.Round(source.Y), result.Y);
            Assert.Equal(Math.Round(source.Z), result.Z);

            result = source;

            MathHelper.Round(ref result);

            Assert.Equal(Math.Round(source.X), result.X);
            Assert.Equal(Math.Round(source.Y), result.Y);
            Assert.Equal(Math.Round(source.Z), result.Z);
        }

        [Fact]
        public void Interpolate_Float()
        {
            float l = 10;
            float r = 60;

            Assert.Equal(l, MathHelper.Interpolate(-1, l, r));
            Assert.Equal(r, MathHelper.Interpolate(+2, l, r));
            Assert.Equal(20, MathHelper.Interpolate(0.2f, l, r));
        }

        [Fact]
        public void Interpolate_Double()
        {
            double l = 10;
            double r = 60;

            Assert.Equal(l, MathHelper.Interpolate(-1, l, r));
            Assert.Equal(r, MathHelper.Interpolate(+2, l, r));

            Assert.Equal(20, MathHelper.Interpolate(0.2, l, r));
        }

        [Fact]
        public void Interpolate_Vector2()
        {
            Vector2 l = new Vector2(10, 50);
            Vector2 r = new Vector2(60, 150);

            Assert.Equal(l, MathHelper.Interpolate(-1, l, r));
            Assert.Equal(r, MathHelper.Interpolate(+2, l, r));

            Assert.Equal(new Vector2(20, 70), MathHelper.Interpolate(0.2f, l, r));
        }

        [Fact]
        public void AboutEquals_Float_ByEpsilon_ReturnsTrue()
        {
            float v = 10;

            Assert.True(MathHelper.AboutEqual(v, v + float.Epsilon));
        }

        [Fact]
        public void AboutEquals_Double_ByEpsilon_ReturnsTrue()
        {
            double v = 10;

            Assert.True(MathHelper.AboutEqual(v, v + double.Epsilon));
        }

        [Fact]
        public void AboutEquals_Vector2_ByEpsilon_ReturnsTrue()
        {
            Vector2 v = new Vector2(10, 50);

            Assert.True(MathHelper.AboutEqual(v, v + new Vector2(float.Epsilon, float.Epsilon)));
        }

        [Fact]
        public void MaxVector()
        {
            Vector2 result = MathHelper.Max(new Vector2(10, 40), 20);

            Assert.Equal(new Vector2(20, 40), result);
        }

        [Fact]
        public void MinVector()
        {
            Vector2 result = MathHelper.Min(new Vector2(10, 40), 20);

            Assert.Equal(new Vector2(10, 20), result);
        }

        [Fact]
        public void RotateVector2_AllMethods()
        {
            Vector2 source = new Vector2(40, 20);
            Vector2 center = new Vector2(20, 20);

            double radian = Math.PI / 2;

            Assert.Equal(new Vector2(20, 40), MathHelper.RotatedVector2(source, center, radian).Round());
            Assert.Equal(new Vector2(20, 40), MathHelper.RotatedVector2(source, center, Math.Cos(radian), Math.Sin(radian)).Round());
            Assert.Equal(new Vector2(20, 40), MathHelper.RotatedVector2(source.X, source.Y, center, radian).Round());
            Assert.Equal(new Vector2(20, 40), MathHelper.RotatedVector2(source.X, source.Y, center, Math.Cos(radian), Math.Sin(radian)).Round());
        }

        [Fact]
        public void GetAngleBetween()
        {
            Assert.Equal(00, new Vector2(1, 0).GetAngleBetween(new Vector2(1, 0)));
            Assert.Equal(45, new Vector2(1, 0).GetAngleBetween(new Vector2(1, 1)));
            Assert.Equal(90, new Vector2(1, 0).GetAngleBetween(new Vector2(0, 1)));
        }

        [Fact]
        public void ToPositiveDegree_Single()
        {
            Assert.Equal(36.5f, MathHelper.ToPositiveDegree(36.5f - (1f * 360f)));
            Assert.Equal(36.5f, MathHelper.ToPositiveDegree(36.5f - (2f * 360f)));
            Assert.Equal(36.5f, MathHelper.ToPositiveDegree(36.5f + (1f * 360f)));
            Assert.Equal(36.5f, MathHelper.ToPositiveDegree(36.5f + (2f * 360f)));
        }

        [Fact]
        public void ToPositiveDegree_Double()
        {
            Assert.Equal(36.5d, MathHelper.ToPositiveDegree(36.5d - (1d * 360d)));
            Assert.Equal(36.5d, MathHelper.ToPositiveDegree(36.5d - (2d * 360d)));
            Assert.Equal(36.5d, MathHelper.ToPositiveDegree(36.5d + (1d * 360d)));
            Assert.Equal(36.5d, MathHelper.ToPositiveDegree(36.5d + (2d * 360d)));
        }

        [Fact]
        public void ToRad()
        {
            Assert.Equal(0, MathHelper.ToRad(0));

            Assert.True(Math.Abs((Math.PI * 1) - MathHelper.ToRad(180f)) < 0.00001);
            Assert.True(Math.Abs((Math.PI * 1) - MathHelper.ToRad(180d)) < 0.00001);
            Assert.True(Math.Abs((Math.PI * 2) - MathHelper.ToRad(360f)) < 0.00001);
            Assert.True(Math.Abs((Math.PI * 2) - MathHelper.ToRad(360d)) < 0.00001);
        }

        [Fact]
        public void ToDegree()
        {
            Assert.Equal(0, MathHelper.ToDegree(0));

            Assert.True(Math.Abs(180 - MathHelper.ToDegree(Math.PI * 1)) < 0.00001);
            Assert.True(Math.Abs(360 - MathHelper.ToDegree(Math.PI * 2)) < 0.00001);
        }
    }
}
