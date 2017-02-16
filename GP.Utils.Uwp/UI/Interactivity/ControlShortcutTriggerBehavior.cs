// ==========================================================================
// ControlShortcutTriggerBehavior.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// ReSharper disable InvertIf

namespace GP.Utils.UI.Interactivity
{
    /// <summary>
    /// Handles the shortcuts for the control.
    /// </summary>
    public sealed class ControlShortcutTriggerBehavior : ShortcutTriggerBehaviorBase
    {
        /// <summary>
        /// Defines the <see cref="ControlShortcutTriggerBehavior"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty HandleKeyDownProperty =
            DependencyPropertyManager.Register<ControlShortcutTriggerBehavior, bool>(nameof(HandleKeyDown), false);
        /// <summary>
        /// Gets or sets a value indicating if the key down event must be handled.
        /// </summary>
        public bool HandleKeyDown
        {
            get { return (bool)GetValue(HandleKeyDownProperty); }
            set { SetValue(HandleKeyDownProperty, value); }
        }

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>Override this to hook up functionality to the AssociatedObject.</remarks>
        protected override void OnAttached()
        {
            UIElement element = AssociatedElement as FrameworkElement;

            if (element != null)
            {
                element.KeyDown += AssociatedElement_KeyDown;
                element.KeyUp   += AssociatedElement_KeyUp;
            }
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>Override this to unhook functionality from the AssociatedObject.</remarks>
        protected override void OnDetaching()
        {
            UIElement element = AssociatedElement as FrameworkElement;

            if (element != null)
            {
                element.KeyDown -= AssociatedElement_KeyDown;
                element.KeyUp   -= AssociatedElement_KeyUp;
            }
        }

        private void AssociatedElement_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            var key = e.Key;

            if (IsCorrectKey(key))
            {
                if (IsEnabled(AssociatedElement))
                {
                    Execute(this, null);
                }

                e.Handled = true;
            }
        }

        private void AssociatedElement_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            var key = e.Key;

            if (IsCorrectKey(key) && IsEnabled(AssociatedElement) && HandleKeyDown)
            {
                e.Handled = true;
            }
        }

        private static bool IsEnabled(DependencyObject target)
        {
            while (target != null)
            {
                var control = target as Control;

                if (control != null && !control.IsEnabled)
                {
                    return false;
                }

                var element = target as UIElement;

                if (element != null && (element.Visibility == Visibility.Collapsed || Math.Abs(element.Opacity) < float.Epsilon))
                {
                    return false;
                }

                target = VisualTreeHelper.GetParent(target);
            }

            return true;
        }
    }
}
