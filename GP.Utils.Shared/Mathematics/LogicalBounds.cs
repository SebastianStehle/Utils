// ==========================================================================
// LogicalBounds.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System.Numerics;

namespace GP.Utils.Mathematics
{
    public struct LogicalBounds
    {
        private readonly Vector2 position;
        private readonly Vector2 size;
        private readonly float rotationInDegrees;

        public Vector2 Position
        {
            get { return position; }
        }

        public Vector2 Size
        {
            get { return size; }
        }

        public float RotationInDegrees
        {
            get { return rotationInDegrees; }
        }

        public LogicalBounds(Vector2 position, Vector2 size, float rotationInDegrees)
        {
            this.rotationInDegrees = rotationInDegrees;

            this.position = position;

            this.size = size;
        }

        public Vector2 CalculateTopLeft()
        {
            return Position;
        }

        public Vector2 CalculateTopCenter()
        {
            Angle rot = Angle.CreateByDegree(rotationInDegrees);

            return CalculateTopCenter(ref rot);
        }

        private Vector2 CalculateTopCenter(ref Angle rot)
        {
            return new Vector2(position.X + size.X * 0.5f * rot.Cos, position.Y + size.X * rot.Sin);
        }

        public Vector2 CalculateTopRight()
        {
            Angle rot = Angle.CreateByDegree(rotationInDegrees);

            return CalculateTopRight(ref rot);
        }

        private Vector2 CalculateTopRight(ref Angle rot)
        {
            return new Vector2(position.X + size.X * rot.Cos, position.Y + size.X * rot.Sin);
        }

        public Vector2 CalculateBottomLeft()
        {
            Angle rot = Angle.CreateByDegree(rotationInDegrees);

            return CalculateBottomLeft(ref rot);
        }

        public Vector2 CalculateBottomLeft(ref Angle rot)
        {
            return new Vector2(position.Y - size.Y * rot.Sin, position.Y + size.Y * rot.Cos);
        }

        public Vector2 CalculateBottomCenter()
        {
            Angle rot = Angle.CreateByDegree(rotationInDegrees);

            return CalculateBottomCenter(ref rot);
        }

        private Vector2 CalculateBottomCenter(ref Angle rot)
        {
            return new Vector2(position.X + size.X * 0.5f * rot.Cos - size.Y * rot.Sin, position.Y + size.X * rot.Sin + size.Y * rot.Cos);
        }

        public Vector2 CalculateBottomRight()
        {
            Angle rot = Angle.CreateByDegree(rotationInDegrees);

            return CalculateBottomRight(ref rot);
        }

        private Vector2 CalculateBottomRight(ref Angle rot)
        {
            return new Vector2(position.X + size.X * rot.Cos - size.Y * rot.Cos, position.Y + size.X * rot.Sin + size.Y * rot.Cos);
        }

        public Vector2 CalculateCenter()
        {
            Angle rot = Angle.CreateByDegree(rotationInDegrees);

            return CalculateCenter(ref rot);
        }

        private Vector2 CalculateCenter(ref Angle rot)
        {
            return new Vector2(position.X + size.X * 0.5f * rot.Cos - size.Y * 0.5f * rot.Sin, position.Y + size.Y * 0.5f * rot.Cos);
        }

        public Vector2 CalculateCenterLeft()
        {
            Angle rot = Angle.CreateByDegree(rotationInDegrees);

            return CalculateCenterLeft(ref rot);
        }

        private Vector2 CalculateCenterLeft(ref Angle rot)
        {
            return new Vector2(position.X - size.Y * 0.5f * rot.Sin, position.Y + size.Y * 0.5f * rot.Cos);
        }

        public Vector2 CalculateCenterRight()
        {
            Angle rot = Angle.CreateByDegree(rotationInDegrees);

            return CalculateCenterRight(ref rot);
        }

        private Vector2 CalculateCenterRight(ref Angle rot)
        {
            return new Vector2(position.X + size.X * rot.Cos - size.Y * 0.5f * rot.Sin, position.Y + size.X * rot.Sin + size.Y * 0.5f * rot.Cos);
        }
    }
}
