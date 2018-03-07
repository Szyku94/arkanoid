using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arkanoid
{
    class BoundingBox
    {
        public float x { get; set; }
        public float y { get; set; }
        public float width { get; }
        public float height { get; }

        public BoundingBox(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
        public void move(float x, float y)
        {
            this.x += x;
            this.y += y;
        }
    }
}
