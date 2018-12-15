// ==========================================================================
// UndoRedoManagerTests.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System;
using System.Linq;
using GP.Utils;
using Tests.Mockups;
using Xunit;

#pragma warning disable xUnit2013 // Do not use equality check to check for collection size.
// ReSharper disable once ObjectCreationAsStatement

namespace Tests.Facts
{
    public class UndoRedoManagerTests
    {
        private readonly UndoRedoManager undoRedoManager = new UndoRedoManager();

        [Fact]
        public void Constructor()
        {
            new UndoRedoManager();
        }

        [Fact]
        public void Redo_EmptyLog_DoesNothing()
        {
            undoRedoManager.Redo();
        }

        [Fact]
        public void RedoAll_EmptyLog_DoesNothing()
        {
            undoRedoManager.RedoAll();
        }

        [Fact]
        public void Redo_SingleAction_RedoInvoked()
        {
            EventHandler<StateChangedEventArgs> handler = (sender, e) =>
            {
                Assert.Equal(StateChangedReason.Redo, e.Reason);
            };

            MockupAction action = new MockupAction();

            undoRedoManager.RegisterExecutedAction(action);
            undoRedoManager.UndoAll();
            undoRedoManager.StateChanged += handler;
            undoRedoManager.Redo();
            undoRedoManager.StateChanged -= handler;

            Assert.True(action.IsRedoInvoked);
            Assert.True(undoRedoManager.CanUndo);
            Assert.False(undoRedoManager.CanRedo);
        }

        [Fact]
        public void Redo_MultipleItems_RedoInvoked_CanRedo()
        {
            MockupAction action1 = new MockupAction();
            MockupAction action2 = new MockupAction();

            undoRedoManager.RegisterExecutedAction(action1);
            undoRedoManager.RegisterExecutedAction(action2);
            undoRedoManager.UndoAll();
            undoRedoManager.RedoAll();

            Assert.True(action1.IsRedoInvoked);
            Assert.True(action2.IsRedoInvoked);

            Assert.True(undoRedoManager.CanUndo);
            Assert.False(undoRedoManager.CanRedo);
        }

        [Fact]
        public void Undo_EmtyLog_DoesNothing()
        {
            undoRedoManager.Undo();
        }

        [Fact]
        public void UndoTo_Empty_DoesNothing()
        {
            undoRedoManager.UndoTo(123);
        }

        [Fact]
        public void UndoTo_ActionRemaining()
        {
            MockupAction action = new MockupAction();

            undoRedoManager.RegisterExecutedAction(action);
            undoRedoManager.RegisterExecutedAction(action);
            undoRedoManager.RegisterExecutedAction(action);
            undoRedoManager.RegisterExecutedAction(action);

            undoRedoManager.UndoTo(2);

            Assert.Equal(2, undoRedoManager.History.Count());
            Assert.Equal(2, undoRedoManager.HistoryCount);
        }

        [Fact]
        public void UndoAll_EmtyLog_DoesNothing()
        {
            undoRedoManager.UndoAll();
        }

        [Fact]
        public void Undo_SingleAction_UndoInvoked()
        {
            EventHandler<StateChangedEventArgs> handler = (sender, e) =>
            {
                Assert.Equal(StateChangedReason.Undo, e.Reason);
            };

            MockupAction action = new MockupAction();

            undoRedoManager.RegisterExecutedAction(action);
            undoRedoManager.StateChanged += handler;
            undoRedoManager.Undo();
            undoRedoManager.StateChanged -= handler;

            Assert.True(action.IsUndoInvoked);

            Assert.True(undoRedoManager.CanRedo);
            Assert.False(undoRedoManager.CanUndo);
        }

        [Fact]
        public void Undo_MultipleActions_UndoInvoked_CanUndo()
        {
            MockupAction action1 = new MockupAction();
            MockupAction action2 = new MockupAction();

            undoRedoManager.RegisterExecutedAction(action1);
            undoRedoManager.RegisterExecutedAction(action2);
            undoRedoManager.UndoAll();

            Assert.True(action1.IsUndoInvoked);
            Assert.True(action2.IsUndoInvoked);
            Assert.True(undoRedoManager.CanRedo);
            Assert.False(undoRedoManager.CanUndo);
        }

        [Fact]
        public void RegisterExecutedAction_ActionIsNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => undoRedoManager.RegisterExecutedAction(null));
        }

        [Fact]
        public void RegisterExecutedAction_CanUndo()
        {
            EventHandler<StateChangedEventArgs> handler = (sender, e) =>
            {
                Assert.Equal(StateChangedReason.Register, e.Reason);
            };

            MockupAction action1 = new MockupAction();
            MockupAction action2 = new MockupAction();

            undoRedoManager.StateChanged += handler;
            undoRedoManager.RegisterExecutedAction(action1);
            undoRedoManager.StateChanged -= handler;

            Assert.Equal(1, undoRedoManager.History.Count());
            Assert.Equal(1, undoRedoManager.HistoryCount);
            Assert.True(undoRedoManager.CanUndo);

            undoRedoManager.RegisterExecutedAction(action2);

            Assert.Equal(2, undoRedoManager.History.Count());
            Assert.Equal(2, undoRedoManager.HistoryCount);
            Assert.True(undoRedoManager.CanUndo);
        }

        [Fact]
        public void RegisterExecutedAction_WithUndoneActions_StartedFromScratch()
        {
            MockupAction oldAction1 = new MockupAction();
            MockupAction oldAction2 = new MockupAction();

            undoRedoManager.RegisterExecutedAction(oldAction1);
            undoRedoManager.RegisterExecutedAction(oldAction2);
            undoRedoManager.UndoAll();

            MockupAction action1 = new MockupAction();
            MockupAction action2 = new MockupAction();

            undoRedoManager.RegisterExecutedAction(action1);

            Assert.True(undoRedoManager.CanUndo);

            undoRedoManager.RegisterExecutedAction(action2);

            Assert.True(undoRedoManager.CanUndo);
        }

        [Fact]
        public void RegisterExecutedAction_WithUndoAction_StartedAfterFirstAction()
        {
            MockupAction oldAction1 = new MockupAction();
            MockupAction oldAction2 = new MockupAction();

            undoRedoManager.RegisterExecutedAction(oldAction1);
            undoRedoManager.RegisterExecutedAction(oldAction2);
            undoRedoManager.Undo();

            MockupAction action1 = new MockupAction();
            MockupAction action2 = new MockupAction();

            undoRedoManager.RegisterExecutedAction(action1);

            Assert.True(undoRedoManager.CanUndo);

            undoRedoManager.RegisterExecutedAction(action2);

            Assert.True(undoRedoManager.CanUndo);
        }
    }
}