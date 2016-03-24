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
        public void Create_ByDegree_RadianCalculated()
        {
            Angle rotation = Angle.CreateByDegree(180);

            Assert.Equal(180, rotation.Degree, 5);
            Assert.Equal(Math.PI, rotation.Rad, 5);
            Assert.Equal(Math.Cos(Math.PI), rotation.Cos, 5);
            Assert.Equal(Math.Sin(Math.PI), rotation.Sin, 5);

            Assert.Equal("180°", rotation.ToString());
        }

        [Fact]
        public void Create_ByRadian_AngleCalculated()
        {
            Angle rotation = Angle.CreateByRadian((float)Math.PI);

            Assert.Equal(180, rotation.Degree, 5);
            Assert.Equal(Math.PI, rotation.Rad, 5);
            Assert.Equal(Math.Cos(Math.PI), rotation.Cos, 5);
            Assert.Equal(Math.Sin(Math.PI), rotation.Sin, 5);

            Assert.Equal("180°", rotation.ToString());
        }

        [Fact]
        public void EqualityTests()
        {
            Angle angle1 = Angle.CreateByDegree(123);
            Angle angle2 = Angle.CreateByDegree(123);
            Angle angle3 = Angle.CreateByDegree(456);

            object value = 123;

            Assert.True(angle1.Equals(angle2));
            Assert.True(angle1.Equals((object)angle2));
            Assert.True(angle1 == angle2);
            Assert.True(angle1 != angle3);
            Assert.Equal(angle1.GetHashCode(), angle2.GetHashCode());

            Assert.False(angle1.Equals(angle3));
            Assert.False(angle1.Equals(value));
            Assert.False(angle1 == angle3);
            Assert.False(angle1 != angle2);
            Assert.NotEqual(angle1.GetHashCode(), angle3.GetHashCode());
        }
    }
}
