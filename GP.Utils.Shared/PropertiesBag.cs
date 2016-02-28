// ==========================================================================
// PropertiesBag.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System;
using System.Collections.Generic;

namespace GP.Utils
{
    /// <summary>
    /// A bag of properties as list of key-value tuples with intelligent casting.
    /// </summary>
    public sealed class PropertiesBag
    {
        private readonly Dictionary<string, PropertyValue> internalDictionary = new Dictionary<string, PropertyValue>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Gets the number of properties.
        /// </summary>
        public int Count
        {
            get { return internalDictionary.Count; }
        }

        /// <summary>
        /// Gets all properties.
        /// </summary>
        public IReadOnlyDictionary<string, PropertyValue> Properties
        {
            get { return internalDictionary; }
        }

        /// <summary>
        /// Gets all property names.
        /// </summary>
        public IEnumerable<string> PropertyNames
        {
            get { return internalDictionary.Keys; }
        }

        /// <summary>
        /// Gets the property with the specified key.
        /// </summary>
        /// <param name="propertyName">The name of the property. Not case sensitive. Cannot be null or empty.</param>
        /// <exception cref="ArgumentNullException"><paramref name="propertyName"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="propertyName"/> is empty or contains only whitespaces.</exception>
        /// <exception cref="KeyNotFoundException">A property with this name does not exist.</exception>
        /// <returns>
        /// The property with the specified key.
        /// </returns>
        public PropertyValue this[string propertyName]
        {
            get
            {
                Guard.NotNullOrEmpty(propertyName, nameof(propertyName));

                return internalDictionary[propertyName];
            }
        }

        /// <summary>
        /// Determines whether a property with the specified name exists.
        /// </summary>
        /// <param name="propertyName">The name of the property. Not case sensitive. Cannot be null or empty.</param>
        /// <exception cref="ArgumentNullException"><paramref name="propertyName"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="propertyName"/> is empty or contains only whitespaces.</exception>
        /// <returns>
        /// True, if the property exists; false otherwise.
        /// </returns>
        public bool Contains(string propertyName)
        {
            Guard.NotNullOrEmpty(propertyName, nameof(propertyName));

            return internalDictionary.ContainsKey(propertyName);
        }

        /// <summary>
        /// Add or override the property with the specified name.
        /// </summary>
        /// <param name="propertyName">The name of the property. Not case sensitive. Cannot be null or empty.</param>
        /// <exception cref="ArgumentNullException"><paramref name="propertyName"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="propertyName"/> is empty or contains only whitespaces.</exception>
        /// <param name="value">The new property value.</param>
        public void Set(string propertyName, object value)
        {
            Guard.NotNullOrEmpty(propertyName, nameof(propertyName));

            internalDictionary[propertyName] = new PropertyValue(value);
        }

        /// <summary>
        /// Removes the property with the specified name from the <see cref="PropertiesBag"/>.
        /// </summary>
        /// <param name="propertyName">The name of the property. Not case sensitive. Cannot be null or empty.</param>
        /// <exception cref="ArgumentNullException"><paramref name="propertyName"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="propertyName"/> is empty or contains only whitespaces.</exception>
        /// <returns>
        /// True, if the property has been removed, false otherwise.
        /// </returns>
        public bool Remove(string propertyName)
        {
            Guard.NotNullOrEmpty(propertyName, nameof(propertyName));

            return internalDictionary.Remove(propertyName);
        }

        /// <summary>
        /// Renames the property with the specified name, if it exists.
        /// </summary>
        /// <param name="oldPropertyName">The old name of the property. Not case sensitive. Cannot be null or empty.</param>
        /// <param name="newPropertyName">The new name of the property. Not case sensitive. Cannot be null or empty.</param>
        /// <exception cref="ArgumentNullException"><paramref name="oldPropertyName"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="newPropertyName"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="oldPropertyName"/> is empty or contains only whitespaces.</exception>
        /// <exception cref="ArgumentException"><paramref name="newPropertyName"/> is empty or contains only whitespaces.</exception>
        /// <exception cref="ArgumentException"><paramref name="newPropertyName"/> already exists in the <see cref="PropertiesBag"/>.</exception>
        /// <returns>
        /// True, if the property has been renamed, false otherwise.
        /// </returns>
        public bool Rename(string oldPropertyName, string newPropertyName)
        {
            Guard.NotNullOrEmpty(oldPropertyName, nameof(oldPropertyName));
            Guard.NotNullOrEmpty(newPropertyName, nameof(newPropertyName));

            if (internalDictionary.ContainsKey(newPropertyName))
            {
                throw new ArgumentException($"An property with the key '{newPropertyName}' already exists.", newPropertyName);
            }

            if (string.Equals(oldPropertyName, newPropertyName, StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException($"The property names '{newPropertyName}' are equal.", newPropertyName);
            }

            PropertyValue property;

            if (internalDictionary.TryGetValue(oldPropertyName, out property))
            {
                internalDictionary[newPropertyName] = property;
                internalDictionary.Remove(oldPropertyName);

                return true;
            }

            return false;
        }
    }
}
