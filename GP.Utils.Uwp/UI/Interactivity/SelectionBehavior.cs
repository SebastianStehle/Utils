﻿// ==========================================================================
// SelectionBehavior.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// ReSharper disable SuggestBaseTypeForParameter

namespace GP.Utils.UI.Interactivity
{
    /// <summary>
    /// Invokes a command with the selected index when the selection changes.
    /// </summary>
    public sealed class SelectionBehavior : Behavior<ListViewBase>
    {
        /// <summary>
        /// Defines the <see cref="SelectedItemCommand"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectedItemCommandProperty =
            DependencyProperty.Register(nameof(SelectedItemCommand), typeof(ICommand), typeof(SelectionBehavior), new PropertyMetadata(null, (d, e) => ((SelectionBehavior)d).OnCommandChanged(e)));

        /// <summary>
        /// Gets or sets the command to invoke.
        /// </summary>
        /// <value>The command to invoke.</value>
        public ICommand SelectedItemCommand
        {
            get { return (ICommand)GetValue(SelectedItemCommandProperty); }
            set { SetValue(SelectedItemCommandProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="SelectedIndexCommand"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectedIndexCommandProperty =
            DependencyProperty.Register(nameof(SelectedIndexCommand), typeof(ICommand), typeof(SelectionBehavior), new PropertyMetadata(null, (d, e) => ((SelectionBehavior)d).OnCommandChanged(e)));
        /// <summary>
        /// Gets or sets the command to invoke.
        /// </summary>
        /// <value>The command to invoke.</value>
        public ICommand SelectedIndexCommand
        {
            get { return (ICommand)GetValue(SelectedIndexCommandProperty); }
            set { SetValue(SelectedIndexCommandProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="SelectedItem"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(nameof(SelectedItem), typeof(object), typeof(SelectionBehavior), new PropertyMetadata(DependencyProperty.UnsetValue, (d, e) => ((SelectionBehavior)d).OnSelectedItemChanged()));

        /// <summary>
        /// Gets or sets the selected item of the list.
        /// </summary>
        /// <value>The selected item of the list.</value>
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

            UpdateIsEnabled();
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
            if (AssociatedElement == null || AssociatedElement.SelectedIndex < 0)
            {
                return;
            }

            if (SelectedItemCommand?.CanExecute(AssociatedElement.SelectedItem) == true)
            {
                SelectedItemCommand.Execute(AssociatedElement.SelectedItem);
            }

            if (SelectedIndexCommand?.CanExecute(AssociatedElement.SelectedIndex) == true)
            {
                SelectedIndexCommand.Execute(AssociatedElement.SelectedIndex);
            }
        }
        private void OnCommandChanged(DependencyPropertyChangedEventArgs d)
        {
            ICommand oldCommand = d.OldValue as ICommand;

            if (oldCommand != null)
            {
                oldCommand.CanExecuteChanged -= Command_CanExecuteChanged;
            }

            ICommand newCommand = d.NewValue as ICommand;

            if (newCommand != null)
            {
                newCommand.CanExecuteChanged += Command_CanExecuteChanged;
            }

            UpdateIsEnabled();
        }

        private void Command_CanExecuteChanged(object sender, EventArgs e)
        {
            UpdateIsEnabled();
        }

        private void UpdateSelection()
        {
            if (AssociatedElement == null)
            {
                return;
            }

            int? index = AssociatedElement.Items?.IndexOf(SelectedItem);

            if (!index.HasValue)
            {
                index = -1;
            }

            AssociatedElement.SelectedIndex = index.Value;
        }

        private void UpdateIsEnabled()
        {
            if (AssociatedElement == null)
            {
                return;
            }

            bool isEnabled = false;

            if (SelectedIndexCommand != null)
            {
                isEnabled = SelectedIndexCommand.CanExecute(AssociatedElement.SelectedIndex);
            }

            if (SelectedItemCommand != null)
            {
                isEnabled = SelectedItemCommand.CanExecute(AssociatedElement.SelectedItem);
            }

            AssociatedElement.IsEnabled = isEnabled;
        }
    }
}
