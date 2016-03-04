// ==========================================================================
// ColorPickerButton.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System;
using System.Windows.Input;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GP.Utils.UI.Controls
{
    /// <summary>
    /// Implements a color picker button.
    /// </summary>
    [TemplatePart(Name = PartSelectionButton, Type = typeof(Button))]
    [TemplatePart(Name = PartFlyout, Type = typeof(Flyout))]
    public sealed class ColorPickerButton : Control
    {
        private const string PartSelectionButton = "PART_SelectionButton";
        private const string PartColorPicker = "PART_ColorPicker";
        private const string PartFlyout = "PART_Flyout";
        private Flyout flyout;
        private Button selectionButton;
        private ColorPicker colorPicker;

        /// <summary>
        /// Raised when the selected color has changed.
        /// </summary>
        public event EventHandler SelectedColorChanged;

        /// <summary>
        /// Defines the <see cref="Command"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty CommandProperty =
            DependencyPropertyManager.Register<ColorPickerButton, ICommand>(nameof(Command), null);
        /// <summary>
        /// Gets or sets the selected color.
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="SelectedColor"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectedColorProperty =
            DependencyPropertyManager.Register<ColorPickerButton, Color>(nameof(SelectedColor), Colors.Red);
        /// <summary>
        /// Gets or sets the selected color.
        /// </summary>
        public Color SelectedColor
        {
            get { return (Color)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="ButtonStyle"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ButtonStyleProperty =
            DependencyPropertyManager.Register<ColorPickerButton, Style>(nameof(ButtonStyle), null);
        /// <summary>
        /// Gets or sets the style of the button.
        /// </summary>
        public Style ButtonStyle
        {
            get { return (Style)GetValue(ButtonStyleProperty); }
            set { SetValue(ButtonStyleProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="ButtonContent"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ButtonContentProperty =
            DependencyPropertyManager.Register<ColorPickerButton, object>(nameof(ButtonContent), null);
        /// <summary>
        /// Gets or sets the content of the button.
        /// </summary>
        public object ButtonContent
        {
            get { return GetValue(ButtonContentProperty); }
            set { SetValue(ButtonContentProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="ColorPickerStyle"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ColorPickerStyleProperty =
            DependencyPropertyManager.Register<ColorPickerButton, Style>(nameof(ColorPickerStyle), null);
        /// <summary>
        /// Gets or sets the style of the button.
        /// </summary>
        public Style ColorPickerStyle
        {
            get { return (Style)GetValue(ColorPickerStyleProperty); }
            set { SetValue(ColorPickerStyleProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="SelectionButtonContent"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectionButtonContentProperty =
            DependencyPropertyManager.Register<ColorPickerButton, object>(nameof(SelectionButtonContent), null);
        /// <summary>
        /// Gets or sets the content of the button.
        /// </summary>
        public object SelectionButtonContent
        {
            get { return GetValue(SelectionButtonContentProperty); }
            set { SetValue(SelectionButtonContentProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="SelectionButtonStyle"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectionButtonStyleProperty =
            DependencyPropertyManager.Register<ColorPickerButton, Style>(nameof(SelectionButtonStyle), null);
        /// <summary>
        /// Gets or sets the style of the selection button.
        /// </summary>
        public Style SelectionButtonStyle
        {
            get { return (Style)GetValue(SelectionButtonStyleProperty); }
            set { SetValue(SelectionButtonStyleProperty, value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorPickerButton"/> class.
        /// </summary>
        public ColorPickerButton()
        {
            DefaultStyleKey = typeof(ColorPickerButton);
        }

        /// <summary>
        /// Binds the controls when the template is applied for the color picker.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            BindSelectionButton();
            BindColorPicker();
            BindFlyout();
        }

        private void BindSelectionButton()
        {
            selectionButton = GetTemplateChild(PartSelectionButton) as Button;

            if (selectionButton != null)
            {
                selectionButton.Click += SelectionButton_Click;
            }
        }

        private void BindColorPicker()
        {
            colorPicker = GetTemplateChild(PartColorPicker) as ColorPicker;
        }

        private void BindFlyout()
        {
            flyout = GetTemplateChild(PartFlyout) as Flyout;
        }

        private void SelectionButton_Click(object sender, RoutedEventArgs e)
        {
            if (colorPicker != null)
            {
                SelectedColor = colorPicker.SelectedColor;
                SelectedColorChanged?.Invoke(this, EventArgs.Empty);

                flyout?.Hide();

                if (Command?.CanExecute(SelectedColor) == true)
                {
                    Command.Execute(SelectedColor);
                }
            }
        }
    }
}
