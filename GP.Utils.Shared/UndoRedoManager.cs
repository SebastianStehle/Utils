// ==========================================================================
// UndoRedoManager.cs
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
    /// Default implementation of the <see cref="IUndoRedoManager"/> interface.
    /// </summary>
    public sealed class UndoRedoManager : IUndoRedoManager
    {
        private readonly Stack<IUndoRedoAction> undoStack = new Stack<IUndoRedoAction>();
        private readonly Stack<IUndoRedoAction> redoStack = new Stack<IUndoRedoAction>();

        /// <summary>
        /// And event that is raised when the state has changed.
        /// </summary>
        public event EventHandler StateChanged;

        private void OnStateChanged(EventArgs e)
        {
            EventHandler stateChanged = StateChanged;

            if (stateChanged != null)
            {
                stateChanged(this, e);
            }
        }

        /// <summary>
        /// Gets the history of executed commands.
        /// </summary>
        public IEnumerable<IUndoRedoAction> History
        {
            get { return undoStack; }
        }

        /// <summary>
        /// The current index of the undo stack.
        /// </summary>
        public int HistoryCount
        {
            get { return undoStack.Count; }
        }

        /// <summary>
        /// Gets or sets a value indicating if <see cref="Undo"/> or <see cref="UndoAll"/> can be called.
        /// </summary>
        public bool CanUndo
        {
            get { return undoStack.Count > 0; }
        }

        /// <summary>
        /// Gets or sets a value indicating if <see cref="Redo"/> or <see cref="RedoAll"/> can be called.
        /// </summary>
        public bool CanRedo
        {
            get { return redoStack.Count > 0; }
        }

        /// <summary>
        /// Redo all operations in the undo stack.
        /// </summary>
        public void Undo()
        {
            if (!CanUndo)
            {
                return;
            }

            UndoInternal();

            OnStateChanged(EventArgs.Empty);
        }

        /// <summary>
        /// Undo the next operation in the undo stack.
        /// </summary>
        public void UndoAll()
        {
            if (!CanUndo)
            {
                return;
            }

            while (CanUndo)
            {
                UndoInternal();
            }

            OnStateChanged(EventArgs.Empty);
        }

        /// <summary>
        /// Redo all operations in the undo stack up to the specified index.
        /// </summary>
        /// <param name="index">The index to undo to.</param>
        public void UndoTo(int index)
        {
            if (!CanUndo)
            {
                return;
            }

            while (undoStack.Count > index)
            {
                UndoInternal();
            }

            OnStateChanged(EventArgs.Empty);
        }

        private void UndoInternal()
        {
            IUndoRedoAction lastUndoAction = undoStack.Pop();

            lastUndoAction.Undo();

            redoStack.Push(lastUndoAction);
        }

        /// <summary>
        /// Redo the next operation in the redo stack.
        /// </summary>
        public void Redo()
        {
            if (!CanRedo)
            {
                return;
            }

            if (CanRedo)
            {
                RedoInternal();
            }

            OnStateChanged(EventArgs.Empty);
        }

        /// <summary>
        /// Redo all operations in the redo stack.
        /// </summary>
        public void RedoAll()
        {
            if (!CanRedo)
            {
                return;
            }

            while (CanRedo)
            {
                RedoInternal();
            }

            OnStateChanged(EventArgs.Empty);
        }

        private void RedoInternal()
        {
            IUndoRedoAction lastRedoAction = redoStack.Pop();

            lastRedoAction.Redo();

            undoStack.Push(lastRedoAction);
        }

        /// <summary>
        /// Registers an undo redo action.
        /// </summary>
        /// <param name="action">The action to register. Cannot be null.</param>
        /// <exception cref="ArgumentNullException"><paramref name="action"/> is null.</exception>
        public void RegisterExecutedAction(IUndoRedoAction action)
        {
            Guard.NotNull(action, nameof(action));

            undoStack.Push(action);
            redoStack.Clear();

            OnStateChanged(EventArgs.Empty);
        }
    }
}
