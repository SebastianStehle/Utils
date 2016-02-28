﻿// ==========================================================================
// MockupAction.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using GP.Utils;

namespace Tests.Mockups
{
    internal class MockupAction : IUndoRedoAction
    {
        public bool IsUndoInvoked { get; private set; }
        public bool IsRedoInvoked { get; private set; }
        public bool IsCleanupInvoked { get; private set; }

        public void Undo()
        {
            IsUndoInvoked = true;
        }

        public void Redo()
        {
            IsRedoInvoked = true;
        }

        public void Cleanup()
        {
            IsCleanupInvoked = true;
        }
    }
}
