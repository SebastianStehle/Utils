// ==========================================================================
// AngleTest.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System;
using GP.Utils.Mathematics;
using Xunit;

namespace Tests.Facts
{
    public class AngleTest
    {
        [Fact]
        public void ByDegree()
        {
            Angle rotation = Angle.CreateByDegree(180);

            Assert.Equal(180, rotation.Degree, 5);
            Assert.Equal(Math.PI, rotation.Rad, 5);
            Assert.Equal(Math.Cos(Math.PI), rotation.Cos, 5);
            Assert.Equal(Math.Sin(Math.PI), rotation.Sin, 5);
        }

        [Fact]
        public void ByRadian()
        {
            Angle rotation = Angle.CreateByRadian((float)Math.PI);

            Assert.Equal(180, rotation.Degree, 5);
            Assert.Equal(Math.PI, rotation.Rad, 5);
            Assert.Equal(Math.Cos(Math.PI), rotation.Cos, 5);
            Assert.Equal(Math.Sin(Math.PI), rotation.Sin, 5);
        }

        [Fact]
        public void Equals()
        {
            Angle rot1 = Angle.CreateByDegree(123);
            Angle rot2 = Angle.CreateByDegree(123);
            Angle rot3 = Angle.CreateByDegree(456);

            object value = 123;

            Assert.True(rot1.Equals(rot2));
            Assert.True(rot1.Equals((object)rot2));
            Assert.True(rot1.GetHashCode() == rot2.GetHashCode());
            Assert.True(rot1 == rot2);
            Assert.True(rot1 != rot3);
            Assert.False(rot1.Equals(value));
        }
    }
}
