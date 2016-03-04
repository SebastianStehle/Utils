// ==========================================================================
// StateChangedKind.cs
// Hercules Mindmap App
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

namespace GP.Utils
{
    /// <summary>
    /// Describes why the state has been changed.
    /// </summary>
    public enum StateChangedReason
    {
        /// <summary>
        /// The state has changed because new actions have been registered.
        /// </summary>
        Register,
        /// <summary>
        /// The state has changed because redo has been called.
        /// </summary>
        Redo,
        /// <summary>
        /// The state has changed because undo has been called.
        /// </summary>
        Undo
    }
}
