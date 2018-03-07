using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arkanoid
{
    class Collision
    {

        public static void checkForCollision(Ball[] balls, Object2D[] objects)
        {
            foreach (Ball ball in balls)
            {
                foreach (Object2D object2D in objects)
                {
                    if (ball.boundingBox.x < object2D.boundingBox.x + object2D.boundingBox.width &&
                    ball.boundingBox.x + ball.boundingBox.width > object2D.boundingBox.x &&
                    ball.boundingBox.y < object2D.boundingBox.y + object2D.boundingBox.height &&
                    ball.boundingBox.height + ball.boundingBox.y > object2D.boundingBox.y)
                    {
                        if (ReferenceEquals(object2D.GetType(), typeof(GameOverBound)))
                        {
                            ball.loose();
                            break;
                        }
                        if (ReferenceEquals(object2D.GetType(), typeof(Platform)))
                        {
                            ball.bounce((ball.X - (((Platform)object2D).X + ((Platform)object2D).width / 2)) / 2);
                            break;
                        }
                        if (ReferenceEquals(object2D.GetType(), typeof(Block)))
                        {
                            ((Block)object2D).destroy();
                        }
                        float ball_bottom = ball.boundingBox.y + ball.boundingBox.height;
                        float object2D_bottom = object2D.boundingBox.y + object2D.boundingBox.height;
                        float ball_right = ball.boundingBox.x + ball.boundingBox.width;
                        float object2D_right = object2D.boundingBox.x + object2D.boundingBox.width;

                        float b_collision = object2D_bottom - ball.boundingBox.y;
                        float t_collision = ball_bottom - object2D.boundingBox.y;
                        float l_collision = ball_right - object2D.boundingBox.x;
                        float r_collision = object2D_right - ball.boundingBox.x;

                        if (t_collision < b_collision && t_collision < l_collision && t_collision < r_collision)
                        {
                            ball.bounce(new Vector2(0, -1));
                            break;
                        }
                        if (b_collision < t_collision && b_collision < l_collision && b_collision < r_collision)
                        {
                            ball.bounce(new Vector2(0, 1));
                            break;
                        }
                        if (l_collision < r_collision && l_collision < t_collision && l_collision < b_collision)
                        {
                            ball.bounce(new Vector2(1, 0));
                            break;
                        }
                        if (r_collision < l_collision && r_collision < t_collision && r_collision < b_collision)
                        {
                            ball.bounce(new Vector2(-1, 0));
                            break;
                        }
                        
                    }
                }
            }
        }
    }
}
