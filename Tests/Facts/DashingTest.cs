// ==========================================================================
// DashingTest.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System;
using System.Collections.Generic;
using GP.Utils.UI;
using Xunit;

namespace Tests.Facts
{
    public class DashingTest
    {
        [Fact]
        public void CreateDashing_ValuesPassedThrough()
        {
            Dashing dashing = Dashing.GetDashing(1, 2, 3);

            Assert.Equal(new List<float> { 1, 2, 3 }, dashing.Values);
        }

        [Fact]
        public void TwoCreations_SameInstance()
        {
            Dashing dashing1 = Dashing.GetDashing(1, 2, 3);
            Dashing dashing2 = Dashing.GetDashing(new List<float> { 1, 2, 3 });

            Assert.True(ReferenceEquals(dashing1, dashing2));
        }

        [Fact]
        public void None_HasNoValues()
        {
            Dashing none = Dashing.None;
            Dashing customNone = Dashing.GetDashing();

            Assert.True(ReferenceEquals(none, customNone));
        }

        [Fact]
        public void UnsetInstance_AccessValues_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => Dashing.Unset.Values.Count);
        }
    }
}
