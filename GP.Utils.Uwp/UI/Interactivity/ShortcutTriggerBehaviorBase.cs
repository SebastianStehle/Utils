// ==========================================================================
// ShortcutTriggerBehaviorBase.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Markup;
using Microsoft.Xaml.Interactions.Core;
using Microsoft.Xaml.Interactivity;

namespace GP.Utils.UI.Interactivity
{
    /// <summary>
    /// Base class for all shortcuts.
    /// </summary>
    [ContentProperty(Name = nameof(Actions))]
    public abstract class ShortcutTriggerBehaviorBase : Behavior<DependencyObject>
    {
        /// <summary>
        /// Defines the <see cref="Key"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty KeyProperty =
            DependencyPropertyManager.Register<ShortcutTriggerBehaviorBase, VirtualKey>(nameof(Key), VirtualKey.None);
        /// <summary>
        /// Gets or sets the key that must be pressed when the command should be invoked.
        /// </summary>
        public VirtualKey Key
        {
            get { return (VirtualKey)GetValue(KeyProperty); }
            set { SetValue(KeyProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="RequiresControlModifier"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty RequiresControlModifierProperty =
            DependencyPropertyManager.Register<ShortcutTriggerBehaviorBase, bool>(nameof(RequiresControlModifierProperty), false);
        /// <summary>
        /// Gets or sets a value indicating if the control key must be pressed.
        /// </summary>
        public bool RequiresControlModifier
        {
            get { return (bool)GetValue(RequiresControlModifierProperty); }
            set { SetValue(RequiresControlModifierProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="RequiresShiftModifier"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty RequiresShiftModifierProperty =
            DependencyPropertyManager.Register<ShortcutTriggerBehaviorBase, bool>(nameof(RequiresShiftModifier), false);
        /// <summary>
        /// Gets or sets a value indicating if the shift key must be pressed.
        /// </summary>
        public bool RequiresShiftModifier
        {
            get { return (bool)GetValue(RequiresShiftModifierProperty); }
            set { SetValue(RequiresShiftModifierProperty, value); }
        }
        /// <summary>
        /// Identifies the <seealso cref="Actions"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ActionsProperty =
            DependencyPropertyManager.Register<ShortcutTriggerBehaviorBase, ActionCollection>(nameof(Actions), null);
        /// <summary>
        /// Gets the collection of actions associated with the behavior. This is a dependency property.
        /// </summary>
        public ActionCollection Actions
        {
            get
            {
                var actionCollection = (ActionCollection)GetValue(EventTriggerBehavior.ActionsProperty);

                if (actionCollection == null)
                {
                    actionCollection = new ActionCollection();

                    SetValue(EventTriggerBehavior.ActionsProperty, actionCollection);
                }

                return actionCollection;
            }
        }

        /// <summary>
        /// Executes all actions in the <see cref="ActionCollection"/> and returns their results.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> which will be passed on to the action.</param>
        /// <param name="parameter">The value of this parameter is determined by the calling behavior.</param>
        /// <returns>Returns the results of the actions.</returns>
        protected void Execute(object sender, object parameter)
        {
            Interaction.ExecuteActions(sender, Actions, parameter);
        }

        /// <summary>
        /// Determines whether the specified key is the correct key.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>
        /// True, if the key is correct or false otherwise.
        /// </returns>
        protected bool IsCorrectKey(VirtualKey key)
        {
            return key == Key && (IsShiftKeyPressed() == RequiresShiftModifier) && (IsControlKeyPressed() == RequiresControlModifier);
        }

        private static bool IsControlKeyPressed()
        {
            var state = Window.Current.CoreWindow.GetKeyState(VirtualKey.Control);

            return state.HasFlag(CoreVirtualKeyStates.Down);
        }

        private static bool IsShiftKeyPressed()
        {
            var state = Window.Current.CoreWindow.GetKeyState(VirtualKey.Shift);

            return state.HasFlag(CoreVirtualKeyStates.Down);
        }
    }
}
