using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arkanoid
{
    class Platform:Object2D
    {
        public float height { get; }
        public float width { get; set; }

        public Platform(float x, float y, float height, float width) : base(x, y, new BoundingBox(x, y, width, height))
        {
            this.height = height;
            this.width = width;
        }
        public void moveLeft(float distance)
        {
            X -= distance;
        }
        public void moveRight(float distance)
        {
            X += distance;
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
