// ==========================================================================
// StateChangedEventArgs.cs
// Hercules Mindmap App
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System;

namespace GP.Utils
{
    /// <summary>
    /// Describes the stated changed event.
    /// </summary>
    public sealed class StateChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the reason why the state has been changed.
        /// </summary>
        public StateChangedReason Reason { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StateChangedEventArgs"/> with the reason.
        /// </summary>
        /// <param name="reason">The reason.</param>
        public StateChangedEventArgs(StateChangedReason reason)
        {
            Reason = reason;
        }
    }
}
