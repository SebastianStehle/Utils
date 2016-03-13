// ==========================================================================
// SwallowKeyArrowDownEventsBehavior.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace GP.Utils.UI.Interactivity
{
    /// <summary>
    /// An event that swallows all key arrow events.
    /// </summary>
    public sealed class SwallowKeyArrowDownEventsBehavior : Behavior<FrameworkElement>
    {
        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>Override this to hook up functionality to the AssociatedObject.</remarks>
        protected override void OnAttached()
        {
            AssociatedElement.KeyDown += AssociatedObject_KeyDown;
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>Override this to unhook functionality from the AssociatedObject.</remarks>
        protected override void OnDetaching()
        {
            AssociatedElement.KeyDown -= AssociatedObject_KeyDown;
        }

        private static void AssociatedObject_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key >= VirtualKey.Left && e.Key <= VirtualKey.Down)
            {
                e.Handled = true;
            }
        }
    }
}
