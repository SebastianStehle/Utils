// ==========================================================================
// NotifyUIAttribute.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System;

// ReSharper disable InconsistentNaming

namespace GP.Utils
{
    /// <summary>
    /// Marks a property to notify the UI.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class NotifyUIAttribute : Attribute
    {
    }
}
