// ==========================================================================
// CommandEnabledBehavior.cs
// Jupiter Presenter App
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GP.Utils.UI.Interactivity
{
    /// <summary>
    /// Enables or disables an element based on a command.
    /// </summary>
    public sealed class CommandEnabledBehavior : Behavior<Control>
    {
        /// <summary>
        /// Defines the <see cref="Command"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty CommandBehavior =
            DependencyPropertyManager.Register<CommandEnabledBehavior, ICommand>(nameof(Command), null, e => e.Owner.OnCommandChanged(e.OldValue, e.NewValue));
        /// <summary>
        /// Gets or sets the command to invoke.
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandBehavior); }
            set { SetValue(CommandBehavior, value); }
        }

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>Override this to hook up functionality to the AssociatedObject.</remarks>
        protected override void OnAttached()
        {
            UpdateIsEnabled();
        }

        private void OnCommandChanged(ICommand oldCommand, ICommand newCommand)
        {
            if (oldCommand != null)
            {
                oldCommand.CanExecuteChanged -= Command_CanExecuteChanged;
            }

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

        private void UpdateIsEnabled()
        {
            if (AssociatedElement != null)
            {
                var isEnabled = false;

                if (Command != null)
                {
                    isEnabled = Command.CanExecute(null);
                }

                AssociatedElement.IsEnabled = isEnabled;
            }
        }
    }
}
