﻿// ==========================================================================
// ICloneable.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================
namespace GP.Utils
{
    /// <summary>
    /// Interface for cloneable types.
    /// </summary>
    /// <typeparam name="T">The type of the cloning result.</typeparam>
    public interface ICloneable<out T>
    {
        /// <summary>
        /// Clones the object.
        /// </summary>
        /// <returns>
        /// The cloned object.
        /// </returns>
        T Clone();
    }
}
