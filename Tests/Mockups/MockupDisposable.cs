// ==========================================================================
// MockupDisposable.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using GP.Utils;

namespace Tests.Mockups
{
    public class DisposableMockup : DisposableObject
    {
        public int DisposeCount { get; private set; }

        protected override void DisposeObject(bool disposing)
        {
            DisposeCount++;
        }

        public void Foo()
        {
            ThrowIfDisposed();
        }
    }
}
