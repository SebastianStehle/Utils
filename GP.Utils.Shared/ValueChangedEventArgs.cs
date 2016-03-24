// ==========================================================================
// ValueChangedEventArgs.cs
// Jupiter Presenter App
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System;

namespace GP.Utils
{
    /// <summary>
    /// Holds the new and the old value when a value changed.
    /// </summary>
    /// <typeparam name="TOwner">The owner of the value.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public sealed class ValueChangedEventArgs<TOwner, TValue> : EventArgs where TOwner : class
    {
        private readonly TOwner owner;
        private readonly TValue newValue;
        private readonly TValue oldValue;

        /// <summary>
        /// Gets the owner of the value.
        /// </summary>
        public TOwner Owner
        {
            get { return owner; }
        }

        /// <summary>
        /// Gets the new value.
        /// </summary>
        public TValue NewValue
        {
            get { return newValue; }
        }

        /// <summary>
        /// Gets the old value.
        /// </summary>
        public TValue OldValue
        {
            get { return oldValue; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StateChangedEventArgs"/> with the reason with the owner and the new and old value.
        /// </summary>
        /// <param name="owner">The owner of the new and old value.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        public ValueChangedEventArgs(TOwner owner, TValue oldValue, TValue newValue)
        {
            Guard.NotNull(owner, nameof(owner));

            this.owner = owner;

            this.oldValue = oldValue;
            this.newValue = newValue;
        }
    }
}
