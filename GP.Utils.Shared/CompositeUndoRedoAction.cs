// ==========================================================================
// CompositeUndoRedoAction.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System;
using System.Collections.Generic;
using System.Linq;

namespace GP.Utils
{
    /// <summary>
    /// Nests multiple actions into one action.
    /// </summary>
    public class CompositeUndoRedoAction : IUndoRedoAction
    {
        private readonly List<IUndoRedoAction> actions = new List<IUndoRedoAction>();
        private readonly DateTimeOffset date;
        private readonly string name;

        /// <summary>
        /// Gets or sets the timestamp when the action has been executed.
        /// </summary>
        public DateTimeOffset Date
        {
            get { return date; }
        }

        /// <summary>
        /// Provides a meaningful name for the actions.
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Gets the nested actions.
        /// </summary>
        public IReadOnlyList<IUndoRedoAction> Actions
        {
            get { return actions; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeUndoRedoAction"/> class with the name and the date.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <param name="date">The date of the action.</param>
        public CompositeUndoRedoAction(string name, DateTimeOffset date)
        {
            Guard.NotNullOrEmpty(name, nameof(name));

            this.date = date;
            this.name = name;
        }

        /// <summary>
        /// Adds a action to the composite action.
        /// </summary>
        /// <param name="action">The action to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="action"/> is null.</exception>
        public void Add(IUndoRedoAction action)
        {
            Guard.NotNull(action, nameof(action));

            actions.Add(action);
        }

        /// <summary>
        /// /Reverts the executed actions.
        /// </summary>
        public virtual void Undo()
        {
            foreach (IUndoRedoAction action in actions.OfType<IUndoRedoAction>().Reverse())
            {
                action.Undo();
            }
        }

        /// <summary>
        /// Executes the action agains.
        /// </summary>
        public virtual void Redo()
        {
            foreach (IUndoRedoAction action in actions)
            {
                action.Redo();
            }
        }
    }
}
