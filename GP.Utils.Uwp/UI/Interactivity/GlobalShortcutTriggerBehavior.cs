// ==========================================================================
// GlobalShortcutTriggerBehavior.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using Windows.ApplicationModel;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Markup;

// ReSharper disable InvertIf

namespace GP.Utils.UI.Interactivity
{
    [ContentProperty(Name = nameof(Actions))]
    public sealed class GlobalShortcutTriggerBehavior : ShortcutTriggerBehaviorBase
    {
        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>Override this to hook up functionality to the AssociatedObject.</remarks>
        protected override void OnAttached()
        {
            if (DesignMode.DesignModeEnabled)
            {
                return;
            }

            var currentWindow = Window.Current.CoreWindow;

            currentWindow.KeyDown += CoreWindow_KeyDown;
            currentWindow.KeyUp   += CoreWindow_KeyUp;
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>Override this to unhook functionality from the AssociatedObject.</remarks>
        protected override void OnDetaching()
        {
            if (DesignMode.DesignModeEnabled)
            {
                return;
            }

            var currentWindow = Window.Current.CoreWindow;

            currentWindow.KeyDown -= CoreWindow_KeyDown;
            currentWindow.KeyUp   -= CoreWindow_KeyUp;
        }

        private void CoreWindow_KeyUp(CoreWindow sender, KeyEventArgs e)
        {
            var key = e.VirtualKey;

            if (IsCorrectKey(key))
            {
                Execute(this, null);

                e.Handled = true;
            }
        }

        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs e)
        {
            var key = e.VirtualKey;

            if (IsCorrectKey(key))
            {
                e.Handled = true;
            }
        }
    }
}
