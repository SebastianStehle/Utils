// ==========================================================================
// Binder.cs
// Jupiter Presenter App
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using Windows.UI.Xaml;

namespace GP.Utils.UI
{
    /// <summary>
    /// Simplifies bindings.
    /// </summary>
    public sealed class Binder
    {
        /// <summary>
        /// Defines the IsVisible attached dependency property.
        /// </summary>
        public static readonly DependencyProperty IsVisibleProperty =
            DependencyPropertyManager.RegisterAttached<Binder, bool>("IsVisible", true, OnIsVisibleChanged);
        /// <summary>
        /// Gets the visible of the element as boolean.
        /// </summary>
        /// <param name="element">The element to get the visibility for.</param>
        /// <returns>The visibility as boolean.</returns>
        public static bool GetIsVisible(UIElement element)
        {
            return (bool)element.GetValue(IsVisibleProperty);
        }

        /// <summary>
        /// Sets the visible of the element as boolean.
        /// </summary>
        /// <param name="element">The element to get the visibility for.</param>
        /// <param name="value">The visibility as boolean.</param>
        public static void SetIsVisible(UIElement element, bool value)
        {
            element.SetValue(IsVisibleProperty, value);
        }

        private static void OnIsVisibleChanged(ValueChangedEventArgs<DependencyObject, bool> e)
        {
            ((UIElement)e.Owner).Visibility = e.NewValue ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
