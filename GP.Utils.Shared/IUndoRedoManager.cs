// ==========================================================================
// IUndoRedoManager.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System;
using System.Collections.Generic;

namespace GP.Utils
{
    /// <summary>
    /// Interface for undo redo manager.
    /// </summary>
    public interface IUndoRedoManager
    {
        /// <summary>
        /// And event that is raised when the state has changed.
        /// </summary>
        event EventHandler<StateChangedEventArgs> StateChanged;

        /// <summary>
        /// Gets the history of executed commands.
        /// </summary>
        IEnumerable<IUndoRedoAction> History { get; }

        /// <summary>
        /// The current index of the undo stack.
        /// </summary>
        int HistoryCount { get; }

        /// <summary>
        /// Gets or sets a value indicating if <see cref="Undo"/> or <see cref="UndoAll"/> can be called.
        /// </summary>
        bool CanUndo { get; }

        /// <summary>
        /// Gets or sets a value indicating if <see cref="Redo"/> or <see cref="RedoAll"/> can be called.
        /// </summary>
        bool CanRedo { get; }

        /// <summary>
        /// Redo all operations in the undo stack up to the specified index.
        /// </summary>
        /// <param name="index">The index to undo to.</param>
        void UndoTo(int index);

        /// <summary>
        /// Redo all operations in the undo stack.
        /// </summary>
        void Undo();

        /// <summary>
        /// Undo the next operation in the undo stack.
        /// </summary>
        void UndoAll();

        /// <summary>
        /// Redo the next operation in the redo stack.
        /// </summary>
        void Redo();

        /// <summary>
        /// Redo all operations in the redo stack.
        /// </summary>
        void RedoAll();

        /// <summary>
        /// Registers an undo redo action.
        /// </summary>
        /// <param name="action">The action to register. Cannot be null.</param>
        /// <exception cref="ArgumentNullException"><paramref name="action"/> is null.</exception>
        void RegisterExecutedAction(IUndoRedoAction action);
    }
}
