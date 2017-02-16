// ==========================================================================
// UndoRedoManager.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System;
using System.Collections.Generic;

// ReSharper disable UseObjectOrCollectionInitializer

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
        public event EventHandler<StateChangedEventArgs> StateChanged;

        private void OnStateChanged(StateChangedReason reason, List<IUndoRedoAction> actions)
        {
            StateChanged?.Invoke(this, new StateChangedEventArgs(reason, actions));
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

            var actions = new List<IUndoRedoAction>();

            actions.Add(UndoInternal());

            OnStateChanged(StateChangedReason.Undo, actions);
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

            var actions = new List<IUndoRedoAction>();

            while (CanUndo)
            {
                actions.Add(UndoInternal());
            }

            OnStateChanged(StateChangedReason.Undo, actions);
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

            var actions = new List<IUndoRedoAction>();

            while (undoStack.Count > index)
            {
                actions.Add(UndoInternal());
            }

            OnStateChanged(StateChangedReason.Undo, actions);
        }

        private IUndoRedoAction UndoInternal()
        {
            var lastUndoAction = undoStack.Pop();

            lastUndoAction.Undo();

            redoStack.Push(lastUndoAction);

            return lastUndoAction;
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

            var actions = new List<IUndoRedoAction>();

            actions.Add(RedoInternal());

            OnStateChanged(StateChangedReason.Redo, actions);
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

            var actions = new List<IUndoRedoAction>();

            while (CanRedo)
            {
                actions.Add(RedoInternal());
            }

            OnStateChanged(StateChangedReason.Redo, actions);
        }

        private IUndoRedoAction RedoInternal()
        {
            var lastRedoAction = redoStack.Pop();

            lastRedoAction.Redo();

            undoStack.Push(lastRedoAction);

            return lastRedoAction;
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

            OnStateChanged(StateChangedReason.Register, new List<IUndoRedoAction> { action });
        }
    }
}
