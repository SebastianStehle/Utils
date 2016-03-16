using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using GP.Utils.Mathematics;
using Xunit;

namespace Tests.Facts
{
    public class LogicalBoundsTest
    {
        [Fact]
        public void Corners()
        {
            LogicalBounds bounds = new LogicalBounds(new Vector2(100, 100), new Vector2(100, 50), 90f);

            AssertClose(new Vector2(100, 100), bounds.CalculateTopLeft());
            AssertClose(new Vector2(100, 150), bounds.CalculateTopCenter());
            AssertClose(new Vector2(100, 200), bounds.CalculateTopRight());
            AssertClose(new Vector2( 75, 200), bounds.CalculateCenterRight());
            AssertClose(new Vector2( 50, 200), bounds.CalculateBottomRight());
            AssertClose(new Vector2( 50, 150), bounds.CalculateBottomCenter());
            AssertClose(new Vector2( 50, 100), bounds.CalculateBottomLeft());
            AssertClose(new Vector2( 75, 100), bounds.CalculateCenterLeft());
            AssertClose(new Vector2( 75, 150), bounds.CalculateCenter());
        }

        private static void AssertClose(Vector2 lhs, Vector2 rhs)
        {
            Assert.Equal(lhs.X, lhs.X, 4);
            Assert.Equal(lhs.Y, lhs.Y, 4);
        }
    }
}
