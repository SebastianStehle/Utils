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

// ReSharper disable LoopCanBeConvertedToQuery
// ReSharper disable ConvertIfStatementToReturnStatement

namespace GP.Utils.UI
{
    /// <summary>
    /// Represents the dashing settings of a line (e.g. border).
    /// </summary>
    public sealed class Dashing
    {
        private static readonly Dictionary<Key, Dashing> Cache = new Dictionary<Key, Dashing>();

        /// <summary>
        /// No dashing.
        /// </summary>
        public static readonly Dashing None = GetDashing();

        /// <summary>
        /// Dashing with no values.
        /// </summary>
        public static readonly Dashing Unset = new Dashing(new List<float>());

        private readonly IReadOnlyList<float> values;

        /// <summary>
        /// Gets the values that define the dashing.
        /// </summary>
        public IReadOnlyList<float> Values
        {
            get
            {
                if (values == null)
                {
                    throw new InvalidOperationException("The unset instance of the dashing class has no values.");
                }

                return values;
            }
        }

        private Dashing(IEnumerable<float> values)
        {
            if (values != null)
            {
                this.values = new List<float>(values);
            }
        }

        /// <summary>
        /// Gets the dashing instance from the values.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>
        /// The created or cached dashing values.
        /// </returns>
        public static Dashing GetDashing(IList<float> values)
        {
            Guard.NotNull(values, nameof(values));

            return Cache.GetOrAddDefault(new Key(values), () => new Dashing(values));
        }

        /// <summary>
        /// Gets the dashing instance from the values.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>
        /// The created or cached dashing values.
        /// </returns>
        public static Dashing GetDashing(params float[] values)
        {
            Guard.NotNull(values, nameof(values));

            return Cache.GetOrAddDefault(new Key(values), () => new Dashing(values));
        }

        private sealed class Key : IEquatable<Key>
        {
            private readonly IList<float> values;

            public Key(IList<float> values)
            {
                this.values = values;
            }

            public bool Equals(Key other)
            {
                return other != null && values.SequenceEqual(other.values);
            }

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
}
