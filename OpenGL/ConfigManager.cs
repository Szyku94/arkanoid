using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arkanoid
{
    class ConfigManager
    {
        static IniData defaultData;
        static IniData data;
        static ConfigManager()
        {
            defaultData = new IniData();
            defaultData["Graphics"]["width"] = "800";
            defaultData["Graphics"]["height"] = "600";
            defaultData["Graphics"]["FPS"] = "60";
            defaultData["Game"]["platform_height"] = "0,3";
            defaultData["Game"]["platform_width"] = "2";
            defaultData["Game"]["platform_speed"] = "0,1";
            defaultData["Game"]["ball_radius"] = "0,1";
            defaultData["Game"]["ball_speed"] = "0,1";
            defaultData["Game"]["ball_bounce_rng"] = "1";
            defaultData["Game"]["ball_starting_number"] = "1";
            
        }
        private static void save()
        {
            var parser = new FileIniDataParser();
            IniData dataTmp = (IniData)defaultData.Clone();
            dataTmp.Merge(data);
            parser.WriteFile("config.ini", data);
        }
        public static string read(String section,String name)
        {
            var parser = new FileIniDataParser();
            try
            {
                if (data is null)
                {
                    data = parser.ReadFile("config.ini");
                }
            }
            catch
            {
                parser.WriteFile("config.ini", defaultData);
                data = parser.ReadFile("config.ini");
            }
            if(data[section][name] is null)
            {
                data[section][name] = defaultData[section][name];
                save();
            }
            return data[section][name];
        }
    }
}
