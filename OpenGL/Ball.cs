using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arkanoid
{
    class Ball:Object2D
    {
        public float radius { get; }
        private const int NUMBER_OF_POINTS = 24;
        private float speed;
        private bool released;
        Vector2 vector;
        private bool lost;
        private float bounceRng;
        private static Random rand;
        static Ball()
        {
            rand = new Random();
        } 
        public Ball(float x, float y, float radius) : base(x, y, new BoundingBox(x, y, 2 * radius, 2 * radius))
        {
            this.radius = radius;
            bounceRng = Convert.ToSingle(ConfigManager.read("Game", "ball_bounce_rng"));
            speed = Convert.ToSingle(ConfigManager.read("Game", "ball_speed"));
            released = false;
            vector = new Vector2(1F, 1F);
            vector = Vector2.Multiply(vector, speed / vector.Length);
            lost = false;
        }
        public PointF[] getPoints()
        {
            float tempX = X + radius;
            float tempY = Y + radius;
            float angle = 0;
            PointF[] points = new PointF[NUMBER_OF_POINTS+2];
            points[0] = new PointF(tempX, tempY);
            for (int i = 1; i <= NUMBER_OF_POINTS + 1; i++) 
            {
                points[i] = new PointF(tempX + radius * (float)Math.Cos(angle), tempY + radius * (float)Math.Sin(angle));
                angle += 2 * (float)Math.PI / NUMBER_OF_POINTS;
            }
            return points;
        }
        public void move()
        {
            X += vector.X;
            Y += vector.Y;
            boundingBox.move(vector.X, vector.Y);
        }
        public void release()
        {
            released = true;
        }
        public bool isReleased()
        {
            return released;
        }
        public void bounce(Vector2 normal)
        {
            vector = -2 * Vector2.Dot(vector, normal) * normal + vector;
            vector.X += ((float)rand.NextDouble()-0.5F)/1000* bounceRng;
            vector.Y += ((float)rand.NextDouble()-0.5F)/1000* bounceRng;
            vector = Vector2.Multiply(vector, speed / vector.Length);
        }
        public void bounce(float x)
        {
            vector.X = x;
            vector.Y = 0.5F;
            vector = Vector2.Multiply(vector, speed / vector.Length);
        }
        public bool isLost()
        {
            return lost;
        }
        public void loose()
        {
            lost = true;
        }
    }
}
