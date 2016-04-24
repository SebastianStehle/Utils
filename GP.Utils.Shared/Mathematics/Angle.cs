// ==========================================================================
// Angle.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System;

namespace GP.Utils.Mathematics
{
    /// <summary>
    /// Represents an angle.
    /// </summary>
    public struct Angle : IEquatable<Angle>
    {
        private readonly float degree;
        private readonly float rad;
        private readonly float cos;
        private readonly float sin;

        /// <summary>
        /// Gets the rotation as degree.
        /// </summary>
        public float Degree
        {
            get { return degree; }
        }

        /// <summary>
        /// Gets the rotation as radian.
        /// </summary>
        public float Rad
        {
            get { return rad; }
        }

        /// <summary>
        /// Gets the rotation as cosinus.
        /// </summary>
        public float Cos
        {
            get { return cos; }
        }

        /// <summary>
        /// Gets the rotation as sinus.
        /// </summary>
        public float Sin
        {
            get { return sin; }
        }

        private Angle(float degree, float rad)
        {
            this.degree = degree;

            this.rad = rad;

            cos = (float)Math.Cos(rad);
            sin = (float)Math.Sin(rad);
        }

        /// <summary>
        /// Creates a new angle from its radian.
        /// </summary>
        /// <param name="radian">The radian.</param>
        /// <returns>
        /// The created angle.
        /// </returns>
        public static Angle CreateByRadian(float radian)
        {
            return new Angle(radian.ToDegree(), radian);
        }

        /// <summary>
        /// Creates a new angle from its degree.
        /// </summary>
        /// <param name="degree">The degree.</param>
        /// <returns>
        /// The created angle.
        /// </returns>
        public static Angle CreateByDegree(float degree)
        {
            return new Angle(degree, degree.ToRad());
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <returns>
        /// true if <paramref name="obj"/> and this instance are the same type and represent the same value; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current instance. </param><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            return obj is Angle && Equals((Angle)obj);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(Angle other)
        {
            return Math.Abs(other.degree - degree) < float.Epsilon;
        }

        /// <summary>
        /// The equality operator to compare two rotations.
        /// </summary>
        /// <param name="lhs">The left hand sided rotation.</param>
        /// <param name="rhs">The right hand sided rotation.</param>
        /// <returns>
        /// True, if the rotations are equal, or false otherwise.
        /// </returns>
        public static bool operator ==(Angle lhs, Angle rhs)
        {
            return Math.Abs(lhs.degree - rhs.degree) < float.Epsilon;
        }

        /// <summary>
        /// The equality operator to compare two rotations.
        /// </summary>
        /// <param name="lhs">The left hand sided rotation.</param>
        /// <param name="rhs">The right hand sided rotation.</param>
        /// <returns>
        /// False, if the rotations are equal, or true otherwise.
        /// </returns>
        public static bool operator !=(Angle lhs, Angle rhs)
        {
            return Math.Abs(lhs.degree - rhs.degree) > float.Epsilon;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return degree.GetHashCode();
        }

        /// <summary>
        /// Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> containing a fully qualified type name.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"{degree}°";
        }
    }
}
