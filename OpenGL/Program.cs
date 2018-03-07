using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arkanoid
{
    class Program
    {
        static void Main(string[] args)
        {
            int fps = Convert.ToInt32(ConfigManager.read("Graphics", "FPS"));
            int width= Convert.ToInt32(ConfigManager.read("Graphics", "width"));
            int height= Convert.ToInt32(ConfigManager.read("Graphics", "height"));
            using (Window win = new Window(width, height))
            {
                win.Run(fps,fps);
            }
        }
    }
}
