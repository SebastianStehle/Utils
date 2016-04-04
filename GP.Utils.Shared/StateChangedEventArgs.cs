// ==========================================================================
// StateChangedEventArgs.cs
// Hercules Mindmap App
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System;
using System.Collections.Generic;

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
        /// Gets the actions that caused the state change.
        /// </summary>
        public IReadOnlyList<IUndoRedoAction> Actions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StateChangedEventArgs"/> with the reason.
        /// </summary>
        /// <param name="reason">The reason.</param>
        /// <param name="actions">The actions that caused the state change.</param>
        public StateChangedEventArgs(StateChangedReason reason, List<IUndoRedoAction> actions)
        {
            Guard.NotNull(actions, nameof(actions));

            Reason = reason;

            Actions = actions;
        }
    }
}
