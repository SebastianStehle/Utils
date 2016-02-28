﻿// ==========================================================================
// GivenMocks.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System;
using Rhino.Mocks;

namespace Tests.Given
{
    public abstract class GivenMocks : GivenLocalization
    {
        private readonly MockRepository mocks = new MockRepository();

        protected MockRepository Mocks
        {
            get
            {
                return mocks;
            }
        }

        protected IDisposable Record()
        {
            return mocks.Record();
        }

        protected IDisposable Playback()
        {
            return mocks.Playback();
        }
    }
}
