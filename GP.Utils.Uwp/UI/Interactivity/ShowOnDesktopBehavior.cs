// ==========================================================================
// ShowOnDesktopBehavior.cs
// Hercules Mindmap App
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using Windows.UI.Xaml;

namespace GP.Utils.UI.Interactivity
{
    /// <summary>
    /// Shows the associated element when running on desktop device.
    /// </summary>
    public sealed class ShowOnDesktopBehavior : Behavior<UIElement>
    {
        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>Override this to hook up functionality to the AssociatedObject.</remarks>
        protected override void OnAttached()
        {
            if (PlattformDetector.IsDesktop)
            {
                AssociatedElement.Visibility = Visibility.Visible;
            }
        }
    }
}
