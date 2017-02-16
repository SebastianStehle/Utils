// ==========================================================================
// IsFullscreenTrigger.cs
// Jupiter Presenter App
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace GP.Utils.UI
{
    /// <summary>
    /// A state trigger that is enabeld when the app is in fullscreen mode.
    /// </summary>
    public class IsFullScreenTrigger : StateTriggerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IsFullScreenTrigger"/> class.
        /// </summary>
        public IsFullScreenTrigger()
        {
            var view = ApplicationView.GetForCurrentView();

            SetActive(view.IsFullScreenMode);

            Window.Current.SizeChanged += CurrentWindow_SizeChanged;
        }

        private void CurrentWindow_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            var view = ApplicationView.GetForCurrentView();

            SetActive(view.IsFullScreenMode);
        }
    }
}
