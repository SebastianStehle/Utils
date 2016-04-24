// ==========================================================================
// Rect2.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System;
using System.Collections.Generic;
using System.Numerics;

// ReSharper disable ImpureMethodCallOnReadonlyValueField

namespace GP.Utils.Mathematics
{
    /// <summary>
    /// Defines an axis aligned rectangle.
    /// </summary>
    public struct Rect2 : IEquatable<Rect2>
    {
        /// <summary>
        /// An zero rectangle.
        /// </summary>
        public static readonly Rect2 Zero = new Rect2(0, 0, 0, 0);
        /// <summary>
        /// An empty rectangle.
        /// </summary>
        public static readonly Rect2 Empty = new Rect2(float.PositiveInfinity, float.PositiveInfinity, float.NegativeInfinity, float.NegativeInfinity);
        /// <summary>
        /// An rectangle with infinite size.
        /// </summary>
        public static readonly Rect2 Infinite = new Rect2(float.NegativeInfinity, float.NegativeInfinity, float.PositiveInfinity, float.PositiveInfinity);

        private readonly Vector2 position;
        private readonly Vector2 size;

        /// <summary>
        /// Gets the position of the rectangle.
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
        }

        /// <summary>
        /// Gets the size of the rectangle.
        /// </summary>
        public Vector2 Size
        {
            get { return size; }
        }

        /// <summary>
        /// Gets the center of the rectangle.
        /// </summary>
        public Vector2 Center
        {
            get { return new Vector2(CenterX, CenterY); }
        }

        /// <summary>
        /// Gets the area of the rectangle.
        /// </summary>
        public float Area
        {
            get { return size.X * size.Y; }
        }

        /// <summary>
        /// Gets the x value of the position of the rectangle.
        /// </summary>
        public float X
        {
            get { return position.X; }
        }

        /// <summary>
        /// Gets the y value of the position of the rectangle.
        /// </summary>
        public float Y
        {
            get { return position.Y; }
        }

        /// <summary>
        /// Gets the left value of the rectangle.
        /// </summary>
        public float Left
        {
            get { return position.X; }
        }

        /// <summary>
        /// Gets the top value of the rectangle.
        /// </summary>
        public float Top
        {
            get { return position.Y; }
        }

        /// <summary>
        /// Gets the right value of the rectangle.
        /// </summary>
        public float Right
        {
            get { return position.X + size.X; }
        }

        /// <summary>
        /// Gets the bottom value of the rectangle.
        /// </summary>
        public float Bottom
        {
            get { return position.Y + size.Y; }
        }

        /// <summary>
        /// Gets the width of the rectangle.
        /// </summary>
        public float Width
        {
            get { return size.X; }
        }

        /// <summary>
        /// Gets the height of the rectangle.
        /// </summary>
        public float Height
        {
            get { return size.Y; }
        }

        /// <summary>
        /// Gets the center of the x value of the rectangle.
        /// </summary>
        public float CenterX
        {
            get { return position.X + (0.5f * size.X); }
        }

        /// <summary>
        /// Gets the center of the y value of the rectangle.
        /// </summary>
        public float CenterY
        {
            get { return position.Y + (0.5f * size.Y); }
        }

        /// <summary>
        /// Gets a value indicating if the rectangle is empty (width or height less than zero).
        /// </summary>
        public bool IsEmpty
        {
            get { return size.X < 0 || size.Y < 0; }
        }

        /// <summary>
        /// Gets a value indicating if the rectangle is infinite (width or height is infinite).
        /// </summary>
        public bool IsInfinite
        {
            get { return float.IsPositiveInfinity(size.X) || float.IsPositiveInfinity(size.Y); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rect2"/> struct with the position and the size.
        /// </summary>
        /// <param name="position">The position of the rectangle.</param>
        /// <param name="size">The size of the rectangle.</param>
        public Rect2(Vector2 position, Vector2 size)
        {
            this.position = position;

            this.size = size;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rect2"/> struct with the position and the size.
        /// </summary>
        /// <param name="x">The x value of the position.</param>
        /// <param name="y">The y value of the position.</param>
        /// <param name="w">The width of the rectangle.</param>
        /// <param name="h">The height of the rectangle.</param>
        public Rect2(float x, float y, float w, float h)
        {
            position = new Vector2(x, y);

            size = new Vector2(w, h);
        }

        /// <summary>
        /// Creates a new rectangle from two positions.
        /// </summary>
        /// <param name="positions">The positions.</param>
        /// <returns>
        /// The created rectangle.
        /// </returns>
        public static Rect2 Create(params Vector2[] positions)
        {
            return Create((IEnumerable<Vector2>)positions);
        }

        /// <summary>
        /// Creates a new rectangle from two positions.
        /// </summary>
        /// <param name="positions">The positions.</param>
        /// <returns>
        /// The created rectangle.
        /// </returns>
        public static Rect2 Create(IEnumerable<Vector2> positions)
        {
            if (positions == null)
            {
                return Infinite;
            }

            float minX = float.MaxValue;
            float minY = float.MaxValue;
            float maxX = float.MinValue;
            float maxY = float.MinValue;

            foreach (Vector2 p in positions)
            {
                minX = Math.Min(minX, p.X);
                minY = Math.Min(minY, p.Y);
                maxX = Math.Max(maxX, p.X);
                maxY = Math.Max(maxY, p.Y);
            }

            return new Rect2(minX, minY, Math.Max(0, maxX - minX), Math.Max(0, maxY - minY));
        }

        /// <summary>
        /// Creates a new rectangle from two positions.
        /// </summary>
        /// <param name="rects">The positions.</param>
        /// <returns>
        /// The created rectangle.
        /// </returns>
        public static Rect2 Create(params Rect2[] rects)
        {
            return Create((IEnumerable<Rect2>)rects);
        }

        /// <summary>
        /// Creates a new rectangle from two positions.
        /// </summary>
        /// <param name="rects">The positions.</param>
        /// <returns>
        /// The created rectangle.
        /// </returns>
        public static Rect2 Create(IEnumerable<Rect2> rects)
        {
            if (rects == null)
            {
                return Infinite;
            }

            float minX = float.MaxValue;
            float minY = float.MaxValue;
            float maxX = float.MinValue;
            float maxY = float.MinValue;

            foreach (Rect2 r in rects)
            {
                minX = Math.Min(minX, r.Left);
                minY = Math.Min(minY, r.Top);
                maxX = Math.Max(maxX, r.Right);
                maxY = Math.Max(maxY, r.Bottom);
            }

            return new Rect2(minX, minY, Math.Max(0, maxX - minX), Math.Max(0, maxY - minY));
        }

        /// <summary>
        /// Rotates the bounding box using the specified angle.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="w">The width.</param>
        /// <param name="h">The height.</param>
        /// <param name="angle">The angle that is used to rorate the bounding box.</param>
        /// <returns>The rotated bounding box.</returns>
        public static Rect2 Rotated(Vector2 position, float w, float h, float angle)
        {
            float x = position.X;
            float y = position.Y;

            if (Math.Abs(angle) < float.Epsilon)
            {
                return new Rect2(position, new Vector2(w, h));
            }

            Vector2 center = new Vector2(x + (w * 0.5f), y + (h * 0.5f));

            float radian = angle.ToRad();

            double cos = Math.Cos(radian);
            double sin = Math.Sin(radian);

            Vector2 lt = MathHelper.RotatedVector2(x + 0, y + 0, center, cos, sin);
            Vector2 rt = MathHelper.RotatedVector2(x + w, y + 0, center, cos, sin);
            Vector2 rb = MathHelper.RotatedVector2(x + w, y + h, center, cos, sin);
            Vector2 lb = MathHelper.RotatedVector2(x + 0, y + h, center, cos, sin);

            return Create(lb, lt, rb, rt);
        }

        /// <summary>
        /// Inflates the rectangle by the specified vector and creates a new rectangle.
        /// </summary>
        /// <param name="v">The vector that contains the two values.</param>
        /// <returns>
        /// The inflated rectangle.
        /// </returns>
        public Rect2 Inflate(Vector2 v)
        {
            return Inflate(v.X, v.Y);
        }

        /// <summary>
        /// Inflates the rectangle by the specified values and creates a new rectangle.
        /// </summary>
        /// <param name="w">The x value to inflate by.</param>
        /// <param name="h">The y value to inflate by.</param>
        /// <returns>
        /// The inflated rectangle.
        /// </returns>
        public Rect2 Inflate(float w, float h)
        {
            return new Rect2(position.X - w, position.Y - h, size.X + (2 * w), size.Y + (2 * h));
        }

        /// <summary>
        /// Deflates the rectangle by the specified vector and creates a new rectangle.
        /// </summary>
        /// <param name="v">The vector that contains the two values.</param>
        /// <returns>
        /// The deflated rectangle.
        /// </returns>
        public Rect2 Deflate(Vector2 v)
        {
            return Deflate(v.X, v.Y);
        }

        /// <summary>
        /// Deflates the rectangle by the specified values and creates a new rectangle.
        /// </summary>
        /// <param name="w">The x value to deflate by.</param>
        /// <param name="h">The y value to deflate by.</param>
        /// <returns>
        /// The deflated rectangle.
        /// </returns>
        public Rect2 Deflate(float w, float h)
        {
            return new Rect2(position.X + w, position.Y + h, size.X - (2 * w), size.Y - (2 * h));
        }

        /// <summary>
        /// Inflates the specified rectangle by the specified vector and creates a new rectangle.
        /// </summary>
        /// <param name="rect">The rectangle to inflate.</param>
        /// <param name="v">The vector that contains the two values.</param>
        /// <returns>
        /// The inflated rectangle.
        /// </returns>
        public static Rect2 Inflate(Rect2 rect, Vector2 v)
        {
            return rect.Inflate(v.X, v.Y);
        }

        /// <summary>
        /// Inflates the specified rectangle by the specified values and creates a new rectangle.
        /// </summary>
        /// <param name="rect">The rectangle to inflate.</param>
        /// <param name="w">The x value to inflate by.</param>
        /// <param name="h">The y value to inflate by.</param>
        /// <returns>
        /// The inflated rectangle.
        /// </returns>
        public static Rect2 Inflate(Rect2 rect, float w, float h)
        {
            return rect.Inflate(w, h);
        }

        /// <summary>
        /// Deflates the specified rectangle by the specified vector and creates a new rectangle.
        /// </summary>
        /// <param name="rect">The rectangle to deflate.</param>
        /// <param name="v">The vector that contains the two values.</param>
        /// <returns>
        /// The deflated rectangle.
        /// </returns>
        public static Rect2 Deflate(Rect2 rect, Vector2 v)
        {
            return rect.Deflate(v.X, v.Y);
        }

        /// <summary>
        /// Deflates the specified rectangle by the specified values and creates a new rectangle.
        /// </summary>
        /// <param name="rect">The rectangle to deflate.</param>
        /// <param name="w">The x value to deflate by.</param>
        /// <param name="h">The y value to deflate by.</param>
        /// <returns>
        /// The deflated rectangle.
        /// </returns>
        public static Rect2 Deflate(Rect2 rect, float w, float h)
        {
            return rect.Deflate(w, h);
        }

        /// <summary>
        /// Determines whether the rectangle contains the other rect.
        /// </summary>
        /// <param name="r">The rect.</param>
        /// <returns>
        /// True, if the vector contains the vector; false otherwise.
        /// </returns>
        public bool Contains(Rect2 r)
        {
            return r.Left >= Left && r.Right <= Right && r.Top >= Top && r.Bottom <= Bottom;
        }

        /// <summary>
        /// Determines whether the rectangle contains the vector.
        /// </summary>
        /// <param name="v">The vector.</param>
        /// <returns>
        /// True, if the vector contains the vector; false otherwise.
        /// </returns>
        public bool Contains(Vector2 v)
        {
            return Contains(v.X, v.Y);
        }

        /// <summary>
        /// Determines whether the rectangle contains the x and y value.
        /// </summary>
        /// <param name="x">The x value.</param>
        /// <param name="y">The y value.</param>
        /// <returns>
        /// True, if the vector contains the x and y value; false otherwise.
        /// </returns>
        public bool Contains(double x, double y)
        {
            return x >= position.X && x - size.X <= position.X && y >= position.Y && y - size.Y <= position.Y;
        }

        /// <summary>
        /// Calculates the intersecting vector between the current vector and the specified vector.
        /// </summary>
        /// <param name="rect">The other vector.</param>
        /// <returns>
        /// The intersection vector, if there is an intersection; an empty vector otherwise.
        /// </returns>
        public Rect2 Intersect(Rect2 rect)
        {
            if (!IntersectsWith(rect))
            {
                return Empty;
            }

            float minX = Math.Max(X, rect.X);
            float minY = Math.Max(Y, rect.Y);

            float w = Math.Min(position.X + size.X, rect.Position.X + rect.Size.X) - minX;
            float h = Math.Min(position.Y + size.Y, rect.Position.Y + rect.Size.Y) - minY;

            return new Rect2(minX, minY, Math.Max(w, 0.0f), Math.Max(h, 0.0f));
        }

        /// <summary>
        /// Determines whether there is  an intersection of the current rectangle with the other rectangle.
        /// </summary>
        /// <param name="rect">The other rectangle.</param>
        /// <returns>
        /// True, if there is an intersection, false otherwise.
        /// </returns>
        public bool IntersectsWith(Rect2 rect)
        {
            return !IsEmpty && !rect.IsEmpty && ((rect.Left <= Right && rect.Right >= Left && rect.Top <= Bottom && rect.Bottom >= Top) || IsInfinite || rect.IsInfinite);
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
            return obj is Rect2 && Equals((Rect2)obj);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(Rect2 other)
        {
            return other.position.Equals(position) && other.size.Equals(size);
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
            return size.GetHashCode() ^ position.GetHashCode();
        }

        /// <summary>
        /// The equality operator to compare two rectangles.
        /// </summary>
        /// <param name="lhs">The left hand sided rectangle.</param>
        /// <param name="rhs">The right hand sided rectangle.</param>
        /// <returns>
        /// True, if the rectangles are equal, or false otherwise.
        /// </returns>
        public static bool operator ==(Rect2 lhs, Rect2 rhs)
        {
            return lhs.position.Equals(rhs.position) && lhs.size.Equals(rhs.size);
        }

        /// <summary>
        /// The inequality operator to compare two rectangles.
        /// </summary>
        /// <param name="lhs">The left hand sided rectangle.</param>
        /// <param name="rhs">The right hand sided rectangle.</param>
        /// <returns>
        /// False, if the rectangles are equal, or true otherwise.
        /// </returns>
        public static bool operator !=(Rect2 lhs, Rect2 rhs)
        {
            return !lhs.position.Equals(rhs.position) || !lhs.size.Equals(rhs.size);
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
            return $"X: {position.X}, Y: {position.Y}, W: {size.X}, H: {size.Y}";
        }

        /// <summary>
        /// Returns all four corners of the rectangle.
        /// </summary>
        /// <returns>
        /// The four corners.
        /// </returns>
        public IEnumerable<Vector2> ToCorners()
        {
            yield return new Vector2(Left, Top);
            yield return new Vector2(Right, Top);
            yield return new Vector2(Right, Bottom);
            yield return new Vector2(Left, Bottom);
        }
    }
}
