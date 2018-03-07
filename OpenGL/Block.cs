using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arkanoid
{
    class Block : Object2D
    {
        public float width;
        public float height;
        private bool destroyed;
        public Color color;
        public Block(float x, float y, float width, float height, Color color) : base(x, y, new BoundingBox(x, y, width, height))
        {
            this.width = width;
            this.height = height;
            this.color = color;
            destroyed = false;
        }
        public bool isDestroyed()
        {
            return destroyed;
        }
        public void destroy()
        {
            destroyed = true;
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
        public PointF[] getPoints(float offsetX, float offsetY)
        {
            PointF[] points = new PointF[4];
            points[0] = new PointF(X + offsetX, Y + offsetY);
            points[1] = new PointF(X + offsetX, Y + height - 2*offsetY);
            points[2] = new PointF(X + width - 2*offsetX, Y + height - 2*offsetY);
            points[3] = new PointF(X + width - 2*offsetX, Y + offsetY);
            return points;
        }
    }
}
