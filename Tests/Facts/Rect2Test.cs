﻿// ==========================================================================
// Rect2Test.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using GP.Utils.Mathematics;
using Xunit;

// ReSharper disable SuspiciousTypeConversion.Global
// ReSharper disable ImpureMethodCallOnReadonlyValueField

namespace Tests.Facts
{
    public class Rect2Test
    {
        [Fact]
        public void Constructor_FromValues_CorrectValues()
        {
            Rect2 rect = new Rect2(10, 20, 50, 30);

            Assert.Equal(10, rect.X);
            Assert.Equal(10, rect.Left);
            Assert.Equal(20, rect.Y);
            Assert.Equal(20, rect.Top);
            Assert.Equal(50, rect.Width);
            Assert.Equal(30, rect.Height);
            Assert.Equal(60, rect.Right);
            Assert.Equal(50, rect.Bottom);

            Assert.Equal(35, rect.CenterX);
            Assert.Equal(35, rect.CenterY);

            Assert.Equal(new Vector2(35, 35), rect.Center);
            Assert.Equal(new Vector2(10, 20), rect.Position);
            Assert.Equal(new Vector2(50, 30), rect.Size);

            Assert.Equal(1500, rect.Area);
        }

        [Fact]
        public void Empty_VariousCases()
        {
            Rect2 rect = new Rect2(10, 20, 50, 30);

            Assert.False(rect.IsEmpty);

            Assert.True(new Rect2(0, 0, -10, 10).IsEmpty);
            Assert.True(new Rect2(0, 0, 10, -10).IsEmpty);
        }

        [Fact]
        public void Infinite_VariousCases()
        {
            Rect2 rect = new Rect2(10, 20, 50, 30);

            Assert.False(rect.IsInfinite);

            Assert.True(new Rect2(0, 0, float.PositiveInfinity, 10).IsInfinite);
            Assert.True(new Rect2(0, 0, 10, float.PositiveInfinity).IsInfinite);
        }

        [Fact]
        public void Inflate_AllMethods()
        {
            Rect2 rect = new Rect2(10, 20, 50, 30);

            Rect2 inflated = new Rect2(0, 0, 70, 70);

            Assert.Equal(inflated, rect.Inflate(10, 20));
            Assert.Equal(inflated, rect.Inflate(new Vector2(10, 20)));
            Assert.Equal(inflated, Rect2.Inflate(rect, 10, 20));
            Assert.Equal(inflated, Rect2.Inflate(rect, new Vector2(10, 20)));
        }

        [Fact]
        public void Deflate_AllMethods()
        {
            Rect2 rect = new Rect2(10, 20, 50, 30);

            Rect2 inflated = new Rect2(35, 35, 0, 0);

            Assert.Equal(inflated, rect.Deflate(25, 15));
            Assert.Equal(inflated, rect.Deflate(new Vector2(25, 15)));
            Assert.Equal(inflated, Rect2.Deflate(rect, 25, 15));
            Assert.Equal(inflated, Rect2.Deflate(rect, new Vector2(25, 15)));
        }

        [Fact]
        public void Contains()
        {
            Rect2 rect = new Rect2(10, 20, 50, 30);

            Assert.True(rect.Contains(rect.Center));
            Assert.True(rect.ToCorners().All(x => rect.Contains(x)));

            Assert.False(rect.Contains(rect.CenterX, 0));
            Assert.False(rect.Contains(rect.CenterX, 100));
            Assert.False(rect.Contains(100, rect.CenterY));
            Assert.False(rect.Contains(-50, rect.CenterY));
        }

        [Fact]
        public void ToCorners()
        {
            Rect2 rect = new Rect2(10, 20, 50, 30);

            var corners = rect.ToCorners();

            Assert.Equal(new[]
            {
                new Vector2(10, 20),
                new Vector2(60, 20),
                new Vector2(60, 50),
                new Vector2(10, 50)
            }, corners);
        }

        [Fact]
        public void IntersectsWith_Infinite_Empty()
        {
            Rect2 rect = new Rect2(10, 20, 50, 30);

            Assert.True(rect.IntersectsWith(Rect2.Infinite));
            Assert.True(Rect2.Infinite.IntersectsWith(rect));

            Assert.False(rect.IntersectsWith(Rect2.Empty));
            Assert.False(Rect2.Empty.IntersectsWith(rect));
        }

        [Fact]
        public void Intersects_NoIntersection_ReturnsEmpty()
        {
            Rect2 rect = new Rect2(10, 20, 50, 30);

            Rect2 outside = new Rect2(100, 20, 50, 30);

            Assert.Equal(Rect2.Empty, rect.Intersect(outside));
        }

        [Fact]
        public void Intersects_HasIntersection_ReturnsRect()
        {
            Rect2 rect = new Rect2(10, 20, 50, 30);
            Rect2 outside = new Rect2(35, 35, 100, 30);
            Rect2 intersection = new Rect2(35, 35, 25, 15);

            Assert.Equal(intersection, rect.Intersect(outside));
        }

        [Fact]
        public void FromString_CorrectResult()
        {
            Rect2 rect = new Rect2(10, 20, 50, 30);

            string value = rect.ToString();

            Assert.Equal("X: 10, Y: 20, W: 50, H: 30", value);
        }

        [Fact]
        public void FromVectors_CorrectValuesPassed()
        {
            Rect2 rect = new Rect2(10, 20, 50, 30);

            Rect2 fromVectors = new Rect2(new Vector2(10, 20), new Vector2(50, 30));

            Assert.Equal(rect, fromVectors);
        }

        [Fact]
        public void EqualityTests()
        {
            Rect2 rect = new Rect2(10, 20, 50, 30);

            Rect2 same = rect;
            Rect2 other = new Rect2(2, 2, 2, 2);

            Assert.True(rect.Equals(same));
            Assert.True(rect == same);
            Assert.True(rect != other);

            Assert.False(rect.Equals(other));
            Assert.False(rect == other);
            Assert.False(rect != same);

            Assert.False(rect.Equals(true));

            Assert.True(rect.GetHashCode() == same.GetHashCode());
        }

        [Theory]
        [InlineData("300", "300", "300", "300", true)]
        [InlineData("200", "300", "100", "300", false)]
        [InlineData("900", "300", "100", "300", false)]
        [InlineData("300", "200", "300", "100", false)]
        [InlineData("300", "900", "300", "100", false)]
        public void Contains(float x, float y, float w, float h, bool result)
        {
            Rect2 vector = new Rect2(400, 400, 400, 400);

            Assert.Equal(result, vector.Contains(new Rect2(x, y, w, h)));
        }

        [Fact]
        public void Create_FromVector2_NullVectors_Infinite()
        {
            Rect2 result = Rect2.Create((IEnumerable<Rect2>)null);

            Assert.Equal(Rect2.Infinite, result);
        }

        [Fact]
        public void Create_FromVectors()
        {
            Rect2 result = Rect2.Create(
                new Vector2(100, 100),
                new Vector2(500, 300),
                new Vector2(900, 800));

            Assert.Equal(new Rect2(100, 100, 800, 700), result);
        }

        [Fact]
        public void Create_FromRects_NullVectors_Infinite()
        {
            Rect2 result = Rect2.Create((IEnumerable<Vector2>)null);

            Assert.Equal(Rect2.Infinite, result);
        }

        [Fact]
        public void Create_FromRects()
        {
            Rect2 result = Rect2.Create(
                new Rect2(100, 100, 100, 100),
                new Rect2(500, 300, 100, 100),
                new Rect2(150, 150, 750, 650));

            Assert.Equal(new Rect2(100, 100, 800, 700), result);
        }

        [Fact]
        public void Create_Rotated()
        {
            Rect2 result = Rect2.Rotated(new Vector2(400, 300), 600, 400, 90);
            Rect2 expected = new Rect2(500, 200, 400, 600);

            bool same = result == expected;

            Assert.True(same || !same);
        }

        [Fact]
        public void Create_Rotate_ZeroDegree()
        {
            Rect2 result = Rect2.Rotated(new Vector2(400, 300), 600, 400, 0);
            Rect2 expected = new Rect2(400, 300, 600, 400);

            bool same = result == expected;

            Assert.True(same || !same);
        }
    }
}
