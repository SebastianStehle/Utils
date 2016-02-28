// ==========================================================================
// GivenLocalization.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using GP.Utils;
using Xunit;

namespace Tests.Given
{
    [Collection("Localized")]
    public abstract class GivenLocalization
    {
        protected GivenLocalization()
        {
            LocalizationManager.Provider = new NoopLocalizationProvider();
        }
    }
}
