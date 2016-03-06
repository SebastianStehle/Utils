// ==========================================================================
// CompositeUndoRedoAction.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System;
using GP.Utils;
using Tests.Mockups;
using Xunit;

// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ConvertToConstant.Local

namespace Tests.Facts
{
    public class CompositeUndoRedoActionTest
    {
        [Fact]
        public void Constructor_ValidParameters()
        {
            var name = "MyName";
            var date = DateTimeOffset.UtcNow.AddDays(13);

            CompositeUndoRedoAction composite = new CompositeUndoRedoAction(name, date);

            Assert.Equal(name, composite.Name);
            Assert.Equal(date, composite.Date);
            Assert.Equal(0, composite.Actions.Count);
        }

        [Fact]
        public void Constructor_NameIsNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new CompositeUndoRedoAction(null, DateTimeOffset.Now));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Constructor_NameIsEmpty_ThrowsException(string name)
        {
            Assert.Throws<ArgumentException>(() => new CompositeUndoRedoAction(name, DateTimeOffset.Now));
        }

        [Fact]
        public void AddAction_ActionsIsNull_ThrowsException()
        {
            CompositeUndoRedoAction composite = new CompositeUndoRedoAction("MyName", DateTimeOffset.Now);

            Assert.Throws<ArgumentNullException>(() => composite.Add(null));
        }

        [Fact]
        public void ManyActions_Undo()
        {
            MockupAction action1 = new MockupAction();
            MockupAction action2 = new MockupAction();

            CompositeUndoRedoAction composite = new CompositeUndoRedoAction("MyName", DateTimeOffset.Now);

            composite.Add(action1);
            composite.Add(action2);

            Assert.Equal(2, composite.Actions.Count);

            composite.Undo();

            Assert.True(action1.IsUndoInvoked);
            Assert.True(action2.IsUndoInvoked);

            composite.Redo();

            Assert.True(action1.IsRedoInvoked);
            Assert.True(action2.IsRedoInvoked);
        }
    }
}
