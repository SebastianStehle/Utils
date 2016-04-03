// ==========================================================================
// ExpandableContentControl.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace GP.Utils.UI.Controls
{
    /// <summary>
    /// An expandable content control.
    /// </summary>
    [TemplateVisualState(GroupName = "ExpandedStates", Name = StateExpanded)]
    [TemplateVisualState(GroupName = "ExpandedStates", Name = StateCollapsed)]
    [TemplatePart(Name = PartButton, Type = typeof(ButtonBase))]
    public sealed class ExpandableContentControl : ContentControl
    {
        private const string StateExpanded = "Expanded";
        private const string StateCollapsed = "Collapsed";
        private const string PartButton = "PART_Button";

        private ButtonBase button;

        /// <summary>
        /// Defines the <see cref="Header"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderProperty =
            DependencyPropertyManager.Register<ExpandableContentControl, Style>(nameof(Header), null);
        /// <summary>
        /// Gets or sets the header content.
        /// </summary>
        public object Header
        {
            get { return GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="IsExpanded"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsExpandedProperty =
            DependencyPropertyManager.Register<ExpandableContentControl, bool>(nameof(IsExpanded), true, e => e.Owner.OnIsExpandedChanged());
        /// <summary>
        /// Gets or sets a value indicating if the content control is expanded.
        /// </summary>
        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorPickerButton"/> class.
        /// </summary>
        public ExpandableContentControl()
        {
            DefaultStyleKey = typeof(ExpandableContentControl);
        }

        /// <summary>
        /// Binds the controls when the template is applied for the color picker.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            BindButton();

            UpdateStates();
        }

        private void BindButton()
        {
            if (button != null)
            {
                button.Click -= Button_Click;
            }

            button = GetTemplateChild(PartButton) as ButtonBase;

            if (button != null)
            {
                button.Click += Button_Click;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IsExpanded = !IsExpanded;
        }

        private void OnIsExpandedChanged()
        {
            UpdateStates();
        }

        private void UpdateStates()
        {
            VisualStateManager.GoToState(this, IsExpanded ? StateExpanded : StateCollapsed, true);
        }
    }
}
