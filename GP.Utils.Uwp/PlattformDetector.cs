// ==========================================================================
// PlattformDetector.cs
// Hercules Mindmap App
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System;
using Windows.System.Profile;

namespace GP.Utils
{
    /// <summary>
    /// Detects the current plattform.
    /// </summary>
    public static class PlattformDetector
    {
        /// <summary>
        /// Determines whether the current plattform is a desktop device.
        /// </summary>
        public static bool IsDesktop
        {
            get
            {
                return string.Equals(AnalyticsInfo.VersionInfo.DeviceFamily, "Windows.Desktop", StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}
