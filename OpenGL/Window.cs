using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;

namespace arkanoid
{
    class Window : GameWindow
    {
        float platform_speed;
        Platform platform;
        List<Ball> balls;
        Bound[] bounds;
        List<Object2D> objects;
        List<Block> blocks;
        public Window(int width, int height) : base(width, height, GraphicsMode.Default, "Arkanoid")
        {
            platform_speed = Convert.ToSingle(ConfigManager.read("Game", "platform_speed"));
            platform = new Platform(-Convert.ToSingle(ConfigManager.read("Game", "platform_width"))/2, -5.9F, Convert.ToSingle(ConfigManager.read("Game", "platform_height")), Convert.ToSingle(ConfigManager.read("Game", "platform_width")));
            balls = new List<Ball>();
            float ball_radius= Convert.ToSingle(ConfigManager.read("Game", "ball_radius"));
            for (int i = 0; i < Convert.ToInt32(ConfigManager.read("Game", "ball_starting_number")); i++)
            {
                balls.Add(new Ball(platform.X + platform.width / 2 - ball_radius, platform.Y + platform.height, ball_radius));
            }
            bounds = new Bound[3];
            bounds[0] = new Bound(-10F, -10F, 4.1F, 20F);
            bounds[1] = new Bound(5.9F, -10F, 20F, 20F);
            bounds[2] = new Bound(-5.9F, 5.9F, 20F, 20F);
            GameOverBound gameOverBound = new GameOverBound(-10F, -10F, 20F, 4.1F);
            objects = new List<Object2D>();
            objects.Add(gameOverBound);
            objects.Add(bounds[0]);
            objects.Add(bounds[1]);
            objects.Add(bounds[2]);
            objects.Add(platform);
            blocks = new List<Block>();
            Color[] colors = { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Purple, Color.Pink };
            int k = 0;
            for (float j = 5.0F; j > 2.2F; j -= 0.4F, k++)
            {
                if (k >= colors.Length)
                    k = 0;
                for (float i = -5.6F; i <= 5.6F - 11.2F / 20; i += 11.2F / 20)
                {
                    Block block = new Block(i, j, 0.6F, 0.4F,colors[k]);
                    blocks.Add(block);
                    objects.Add(block);
                }
            }
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            {
                base.OnUpdateFrame(e);
                var keyboard = OpenTK.Input.Keyboard.GetState();
                if (keyboard[OpenTK.Input.Key.Escape])
                {
                    this.Exit();
                }
                if (keyboard[OpenTK.Input.Key.Left])
                {
                    platform.moveLeft(platform_speed);
                    if (platform.X < -5.9F)
                        platform.X = -5.9F;
                }
                if (keyboard[OpenTK.Input.Key.Right])
                {
                    platform.moveRight(platform_speed);
                    if (platform.X + platform.width > 5.9F)
                        platform.X = 5.9F - platform.width;
                }
                if (keyboard[OpenTK.Input.Key.Space])
                {
                    foreach (Ball ball in balls)
                    {
                        ball.release();
                    }
                }
            }
        }
        void draw()
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(0.0, 0.0, 0.0);
            foreach (Bound b in bounds)
            {
                foreach (PointF p in b.getPoints())
                {
                    GL.Vertex2(p.X, p.Y);
                }
            }
            GL.Color3(1.0, 0.0, 0.0);
            foreach (PointF p in platform.getPoints())
            {
                GL.Vertex2(p.X, p.Y);
            }
            foreach (Block block in blocks)
            {
                GL.Color3(block.color);
                foreach (PointF p in block.getPoints(0.01F,0.01F))
                {
                    GL.Vertex2(p.X, p.Y);
                }
                GL.Color3(0.0, 0.0, 0.0);
                foreach (PointF p in block.getPoints())
                {
                    GL.Vertex2(p.X, p.Y);
                }
            }
            GL.End();

           
            foreach (Ball ball in balls)
            {
                GL.Begin(PrimitiveType.TriangleFan);
                GL.Color3(1.0, 1.0, 1.0);
                foreach (PointF p in ball.getPoints())
                {
                    GL.Vertex2(p.X, p.Y);
                }
                GL.End();
            }

            GL.Flush();
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(Color.CornflowerBlue);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();//Macierz jednostkowa
            GL.Translate(0, 0, -3);
            foreach (Ball ball in balls)
            {
                if (ball.isReleased())
                {
                    ball.move();
                }
                else
                {
                    ball.X = platform.X + platform.width / 2 - 0.1F;
                    ball.Y = platform.Y + platform.height;
                }
            }
            Collision.checkForCollision(balls.ToArray(),objects.ToArray());
            for (int i = balls.Count - 1; i >= 0 ; i--)
            {
                if (balls[i].isLost())
                {
                    balls.Remove(balls[i]);
                }
            }
            for (int i = blocks.Count - 1; i >= 0; i--)
                {
                if (blocks[i].isDestroyed())
                {
                    objects.Remove(blocks[i]);
                    blocks.Remove(blocks[i]);
                }
            }
            draw();
            this.SwapBuffers();
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(ClientRectangle);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Frustum(-2, 2, -2, 2, 1, 5);
            GL.Enable(EnableCap.DepthTest);
        }
    }
}
