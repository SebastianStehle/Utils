// ==========================================================================
// Objects.cs
// Jupiter Presenter App
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// ReSharper disable LoopCanBeConvertedToQuery

namespace GP.Utils
{
    /// <summary>
    /// Objects helper.
    /// </summary>
    public static class Objects
    {
        /// <summary>
        /// Compares two objects for equals.
        /// </summary>
        /// <typeparam name="T">The type of the objects.</typeparam>
        /// <param name="lhs">The left instance.</param>
        /// <param name="rhs">The right instance.</param>
        /// <param name="selector">The selector for the values.</param>
        /// <returns>
        /// The result of the equality comparison.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals<T>(T lhs, T rhs, Func<T, IEnumerable<object>> selector)
        {
            if (ReferenceEquals(lhs, rhs))
            {
                return true;
            }

            if (lhs == null || rhs == null)
            {
                return false;
            }

            using (IEnumerator<object> iterator1 = selector(lhs).GetEnumerator())
            {
                using (IEnumerator<object> iterator2 = selector(rhs).GetEnumerator())
                {
                    while (true)
                    {
                        bool moved1 = iterator1.MoveNext();
                        bool moved2 = iterator2.MoveNext();

                        if (moved1 != moved2)
                        {
                            return false;
                        }

                        if (moved1)
                        {
                            if (!Equals(iterator1.Current, iterator2.Current))
                            {
                                return false;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Gets the hashcode of the specified values.
        /// </summary>
        /// <param name="values">The values to get the hashcode for.</param>
        /// <returns>
        /// The calculated hashcode.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetHashCode(IEnumerable<object> values)
        {
            int hashCode = 17;

            foreach (object value in values)
            {
                if (value != null)
                {
                    hashCode = (hashCode * 23) + value.GetHashCode();
                }
            }

            return hashCode;
        }

        /// <summary>
        /// Gets the hashcode of the specified values.
        /// </summary>
        /// <param name="values">The values to get the hashcode for.</param>
        /// <returns>
        /// The calculated hashcode.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetHashCode(params object[] values)
        {
            int hashCode = 17;

            foreach (object value in values)
            {
                if (value != null)
                {
                    hashCode = (hashCode * 23) + value.GetHashCode();
                }
            }

            return hashCode;
        }
    }
}
