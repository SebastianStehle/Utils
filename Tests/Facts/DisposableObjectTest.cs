// ==========================================================================
// DisposableObjectTest.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System;
using Tests.Mockups;
using Xunit;

// ReSharper disable AccessToDisposedClosure

namespace Tests.Facts
{
    public class DisposableObjectTest
    {
        [Fact]
        public void Constructor_IsDisposed_IsFalse()
        {
            DisposableMockup target = new DisposableMockup();

            target.Foo();

            Assert.False(target.IsDisposed);
        }

        [Fact]
        public void Disposed_IsDisposed_IsTrue()
        {
            DisposableMockup target = new DisposableMockup();

            target.Dispose();

            Assert.True(target.IsDisposed);
        }

        [Fact]
        public void Dispose_CalledMultipleTimes_DisposedOnce()
        {
            DisposableMockup target = new DisposableMockup();

            target.Dispose();
            target.Dispose();

            Assert.Equal(1, target.DisposeCount);
        }

        [Fact]
        public void MethodCall_NotDisposed_ThrowsNoException()
        {
            DisposableMockup target = new DisposableMockup();

            target.Foo();
        }

        [Fact]
        public void MethodCall_Disposed_ThrowsException()
        {
            DisposableMockup target = new DisposableMockup();

            target.Dispose();

            Assert.Throws<ObjectDisposedException>(() => target.Foo());
        }
    }
}
