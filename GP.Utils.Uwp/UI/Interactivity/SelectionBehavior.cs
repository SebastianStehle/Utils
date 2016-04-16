// ==========================================================================
// SelectionBehavior.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System.Globalization;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;

// ReSharper disable SuggestBaseTypeForParameter

namespace GP.Utils.UI.Interactivity
{
    /// <summary>
    /// Invokes a command with the selected index when the selection changes.
    /// </summary>
    public sealed class SelectionBehavior : Behavior<Selector>
    {
        private bool isUpdatingValue;

        /// <summary>
        /// Defines the <see cref="Converter"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ConverterProperty =
            DependencyPropertyManager.Register<SelectionBehavior, IValueConverter>(nameof(Converter), null);
        /// <summary>
        /// Gets or sets the converter that converts the selected item.
        /// </summary>
        public IValueConverter Converter
        {
            get { return (IValueConverter)GetValue(ConverterProperty); }
            set { SetValue(ConverterProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="SelectedItemCommand"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectedItemCommandProperty =
            DependencyPropertyManager.Register<SelectionBehavior, ICommand>(nameof(SelectedItemCommand), null);
        /// <summary>
        /// Gets or sets the command to invoke.
        /// </summary>
        public ICommand SelectedItemCommand
        {
            get { return (ICommand)GetValue(SelectedItemCommandProperty); }
            set { SetValue(SelectedItemCommandProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="SelectedIndexCommand"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectedIndexCommandProperty =
            DependencyPropertyManager.Register<SelectionBehavior, ICommand>(nameof(SelectedIndexCommand), null);
        /// <summary>
        /// Gets or sets the command to invoke.
        /// </summary>
        public ICommand SelectedIndexCommand
        {
            get { return (ICommand)GetValue(SelectedIndexCommandProperty); }
            set { SetValue(SelectedIndexCommandProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="SelectedItem"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyPropertyManager.RegisterAndUnset<SelectionBehavior, object>(nameof(SelectedItem), e => e.Owner.OnSelectedItemChanged());
        /// <summary>
        /// Gets or sets the selected item of the list.
        /// </summary>
        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>Override this to hook up functionality to the AssociatedObject.</remarks>
        protected override void OnAttached()
        {
            AssociatedElement.SelectionChanged += AssociatedElement_SelectionChanged;
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>Override this to unhook functionality from the AssociatedObject.</remarks>
        protected override void OnDetaching()
        {
            AssociatedElement.SelectionChanged -= AssociatedElement_SelectionChanged;
        }

        private void OnSelectedItemChanged()
        {
            UpdateSelection();
        }

        private void AssociatedElement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AssociatedElement != null && AssociatedElement.SelectedIndex >= 0 && !isUpdatingValue)
            {
                ICommand selectedItemCommand = SelectedItemCommand;

                if (selectedItemCommand != null)
                {
                    object value = AcquireSelectedValue();

                    if (selectedItemCommand.CanExecute(value))
                    {
                        selectedItemCommand.Execute(value);
                    }
                }

                ICommand selectedIndexCommand = SelectedIndexCommand;

                if (selectedIndexCommand != null)
                {
                    object index = AssociatedElement.SelectedIndex;

                    if (selectedIndexCommand.CanExecute(index))
                    {
                        selectedIndexCommand.Execute(index);
                    }
                }
            }
        }

        private void UpdateSelection()
        {
            if (AssociatedElement != null)
            {
                isUpdatingValue = true;

                try
                {
                    int? index = AssociatedElement.Items?.IndexOf(SelectedItem);

                    if (!index.HasValue)
                    {
                        index = -1;
                    }

                    AssociatedElement.SelectedIndex = index.Value;
                }
                finally
                {
                    isUpdatingValue = false;
                }
            }
        }

        private object AcquireSelectedValue()
        {
            object value = AssociatedElement.SelectedValue;

            IValueConverter converter = Converter;

            if (converter != null)
            {
                value = converter.ConvertBack(value, null, null, CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
            }

            return value;
        }
    }
}
