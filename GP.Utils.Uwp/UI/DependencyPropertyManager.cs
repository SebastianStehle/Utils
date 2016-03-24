// ==========================================================================
// DependencyPropertyManager.cs
// Hercules Mindmap App
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System;
using Windows.UI.Xaml;

namespace GP.Utils.UI
{
    /// <summary>
    /// Simplifies registration of dependency properties.
    /// </summary>
    public static class DependencyPropertyManager
    {
        /// <summary>
        /// Registers a dependency property with the specified name.
        /// </summary>
        /// <typeparam name="TOwner">The type of the owner class.</typeparam>
        /// <typeparam name="TProperty">The type of the property value.</typeparam>
        /// <param name="name">The name of the dependency property.</param>
        /// <returns>
        /// The dependency property.
        /// </returns>
        public static DependencyProperty Register<TOwner, TProperty>(string name) where TOwner : DependencyObject
        {
            return DependencyProperty.Register(name,
                typeof(TProperty),
                typeof(TOwner),
                new PropertyMetadata(default(TProperty)));
        }

        /// <summary>
        /// Registers a dependency property with the specified name and the default value.
        /// </summary>
        /// <typeparam name="TOwner">The type of the owner class.</typeparam>
        /// <typeparam name="TProperty">The type of the property value.</typeparam>
        /// <param name="name">The name of the dependency property.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        /// The dependency property.
        /// </returns>
        public static DependencyProperty Register<TOwner, TProperty>(string name, TProperty defaultValue) where TOwner : DependencyObject
        {
            return DependencyProperty.Register(name,
                typeof(TProperty),
                typeof(TOwner),
                new PropertyMetadata(defaultValue));
        }

        /// <summary>
        /// Registers a dependency property with the specified name and the default value.
        /// </summary>
        /// <typeparam name="TOwner">The type of the owner class.</typeparam>
        /// <typeparam name="TProperty">The type of the property value.</typeparam>
        /// <param name="name">The name of the dependency property.</param>
        /// <param name="callback">The callback that is invoked when the property has changed.</param>
        /// <returns>
        /// The dependency property.
        /// </returns>
        public static DependencyProperty RegisterAndUnset<TOwner, TProperty>(string name, Action<ValueChangedEventArgs<TOwner, TProperty>> callback) where TOwner : DependencyObject
        {
            return DependencyProperty.Register(name,
                typeof(TProperty),
                typeof(TOwner),
                new PropertyMetadata(DependencyProperty.UnsetValue,
                    (d, e) => callback(new ValueChangedEventArgs<TOwner, TProperty>((TOwner)d, (TProperty)e.OldValue, (TProperty)e.NewValue))));
        }

        /// <summary>
        /// Registers a dependency property with the specified name and the default value.
        /// </summary>
        /// <typeparam name="TOwner">The type of the owner class.</typeparam>
        /// <typeparam name="TProperty">The type of the property value.</typeparam>
        /// <param name="name">The name of the dependency property.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="callback">The callback that is invoked when the property has changed.</param>
        /// <returns>
        /// The dependency property.
        /// </returns>
        public static DependencyProperty Register<TOwner, TProperty>(string name, TProperty defaultValue, Action<ValueChangedEventArgs<TOwner, TProperty>> callback) where TOwner : DependencyObject
        {
            return DependencyProperty.Register(name,
                typeof(TProperty),
                typeof(TOwner),
                new PropertyMetadata(defaultValue,
                    (d, e) => callback(new ValueChangedEventArgs<TOwner, TProperty>((TOwner)d, (TProperty)e.OldValue, (TProperty)e.NewValue))));
        }

        /// <summary>
        /// Registers an attached dependency property with the specified name.
        /// </summary>
        /// <typeparam name="TOwner">The type of the owner class.</typeparam>
        /// <typeparam name="TProperty">The type of the property value.</typeparam>
        /// <param name="name">The name of the dependency property.</param>
        /// <returns>
        /// The dependency property.
        /// </returns>
        public static DependencyProperty RegisterAttached<TOwner, TProperty>(string name) where TOwner : DependencyObject
        {
            return DependencyProperty.RegisterAttached(name,
                typeof(TProperty),
                typeof(TOwner),
                new PropertyMetadata(default(TProperty)));
        }

        /// <summary>
        /// Registers an attached dependency property with the specified name and the default value.
        /// </summary>
        /// <typeparam name="TOwner">The type of the owner class.</typeparam>
        /// <typeparam name="TProperty">The type of the property value.</typeparam>
        /// <param name="name">The name of the dependency property.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        /// The dependency property.
        /// </returns>
        public static DependencyProperty RegisterAttached<TOwner, TProperty>(string name, TProperty defaultValue) where TOwner : DependencyObject
        {
            return DependencyProperty.RegisterAttached(name,
                typeof(TProperty),
                typeof(TOwner),
                new PropertyMetadata(defaultValue));
        }

        /// <summary>
        /// Registers an attached dependency property with the specified name and the default value.
        /// </summary>
        /// <typeparam name="TOwner">The type of the owner class.</typeparam>
        /// <typeparam name="TProperty">The type of the property value.</typeparam>
        /// <param name="name">The name of the dependency property.</param>
        /// <param name="callback">The callback that is invoked when the property has changed.</param>
        /// <returns>
        /// The dependency property.
        /// </returns>
        public static DependencyProperty RegisterAttachedAndUnset<TOwner, TProperty>(string name, Action<ValueChangedEventArgs<DependencyObject, TProperty>> callback)
        {
            return DependencyProperty.RegisterAttached(name,
                typeof(TProperty),
                typeof(TOwner),
                new PropertyMetadata(DependencyProperty.UnsetValue,
                    (d, e) => callback(new ValueChangedEventArgs<DependencyObject, TProperty>(d, (TProperty)e.OldValue, (TProperty)e.NewValue))));
        }

        /// <summary>
        /// Registers an attached dependency property with the specified name and the default value.
        /// </summary>
        /// <typeparam name="TOwner">The type of the owner class.</typeparam>
        /// <typeparam name="TProperty">The type of the property value.</typeparam>
        /// <param name="name">The name of the dependency property.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="callback">The callback that is invoked when the property has changed.</param>
        /// <returns>
        /// The dependency property.
        /// </returns>
        public static DependencyProperty RegisterAttached<TOwner, TProperty>(string name, TProperty defaultValue, Action<ValueChangedEventArgs<DependencyObject, TProperty>> callback)
        {
            return DependencyProperty.RegisterAttached(name,
                typeof(TProperty),
                typeof(TOwner),
                new PropertyMetadata(defaultValue,
                    (d, e) => callback(new ValueChangedEventArgs<DependencyObject, TProperty>(d, (TProperty)e.OldValue, (TProperty)e.NewValue))));
        }
    }
}
