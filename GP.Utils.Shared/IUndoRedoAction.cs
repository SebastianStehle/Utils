// ==========================================================================
// IUndoRedoAction.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================
namespace GP.Utils
{
    /// <summary>
    /// Interface for all undo redo actions.
    /// </summary>
    public interface IUndoRedoAction
    {
        /// <summary>
        /// /Reverts the executed action.
        /// </summary>
        void Undo();

        /// <summary>
        /// Executes the action again.
        /// </summary>
        void Redo();
    }
}
