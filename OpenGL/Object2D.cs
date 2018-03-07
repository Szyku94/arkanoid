using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arkanoid
{
    abstract class Object2D
    {
        private float x;
        private float y;
        public float X { get { return x; } set { x = value; boundingBox.x = x; } }
        public float Y { get { return y; } set { y = value; boundingBox.y = y; } }
        public BoundingBox boundingBox { get; set; }

        public Object2D(float x, float y, BoundingBox boundingBox)
        {
            this.x = x;
            this.y = y;
            this.boundingBox = boundingBox;
        }
    }
}
