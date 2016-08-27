// ==========================================================================
// ObjectsTest.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using GP.Utils;
using Tests.Mockups;
using Xunit;

namespace Tests.Facts
{
    public class ObjectsTest
    {
        [Fact]
        public void Equals_DifferentCases()
        {
            MockupModel m1 = new MockupModel { Name = "Name1" };
            MockupModel m2 = new MockupModel { Name = "Name1" };
            MockupModel m3 = new MockupModel { Name = "Name2" };

            Assert.True(Objects.Equals(m1, m1, x => null));
            Assert.True(Objects.Equals(m1, m2, x => new object[] { x.Name }));
            Assert.False(Objects.Equals(m1, m3, x => new object[] { x.Name }));
            Assert.False(Objects.Equals(m1, null, x => null));
            Assert.False(Objects.Equals(null, m1, x => null));
        }

        [Fact]
        public void GetHashCode_DifferentCases()
        {
            int code111 = Objects.GetHashCode(1, "Name1");
            int code112 = Objects.GetHashCode(1, "Name1");
            int code121 = Objects.GetHashCode(1, "Name2");
            int code211 = Objects.GetHashCode(2, "Name1");

            Assert.Equal(code111, code112);
            Assert.NotEqual(code111, code121);
            Assert.NotEqual(code111, code211);
        }
    }
}
