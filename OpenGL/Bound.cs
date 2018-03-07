using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arkanoid
{
    class Bound:Object2D
    {
        public float width;
        public float height;
        public Bound(float x, float y, float width, float height) : base(x, y, new BoundingBox(x, y, width, height))
        {
            this.width = width;
            this.height = height;
        }
        public PointF[] getPoints()
        {
            PointF[] points = new PointF[4];
            points[0] = new PointF(X, Y);
            points[1] = new PointF(X, Y + height);
            points[2] = new PointF(X + width, Y + height);
            points[3] = new PointF(X + width, Y);
            return points;
        }
    }
}
