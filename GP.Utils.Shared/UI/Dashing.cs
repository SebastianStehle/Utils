// ==========================================================================
// Dashing.cs
// Jupiter Presenter App
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System;
using System.Collections.Generic;
using System.Linq;

namespace GP.Utils.UI
{
    /// <summary>
    /// Represents the dashing settings of a line (e.g. border).
    /// </summary>
    public sealed class Dashing : IEquatable<Dashing>
    {
        /// <summary>
        /// No dashing.
        /// </summary>
        public static readonly Dashing None = new Dashing(new List<float>());

        private readonly IReadOnlyList<float> values;

        /// <summary>
        /// Gets the values that define the dashing.
        /// </summary>
        public IReadOnlyList<float> Values
        {
            get { return values; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Dashing"/> class with the values.
        /// </summary>
        /// <param name="values">The values that define the dashing.</param>
        public Dashing(IList<float> values)
        {
            Guard.NotNull(values, nameof(values));

            this.values = new List<float>(values);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(Dashing other)
        {
            return other != null && values.SequenceEqual(other.values);
        }

        /// <summary>
        /// Serves as the default hash function. 
        /// </summary>
        /// <returns>
        /// A hash code for the current object.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            int hashCode = 17;

            foreach (float value in values)
            {
                hashCode = (hashCode * 23) + value.GetHashCode();
            }

            return hashCode;
        }
    }
}
