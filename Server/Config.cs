using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ServTK.Diagnostics;

namespace ServTK
{
    public class Config
    {
        private static Config _settings;

        public static Config Settings
        {
            get
            {
                if (_settings == null)
                {
                    try
                    {
                        _settings =
                            JsonConvert.DeserializeObject<Config>(
                                File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "config.json")));
                    }
                    catch (Exception e)
                    {
                        Logger.Warning("Couldn't access settings: {0}", e.Message);
                        _settings = new Config();
                        _settings.Serialize();
                    }
                }

                return _settings;
            }
        }

        public AuthConfig Auth = new AuthConfig();

        public Config()
        {
            
        }

        public void Serialize()
        {
            StreamWriter fs = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), "config.json"), false);
            fs.Write(JsonConvert.SerializeObject(this, Formatting.Indented));
            fs.Flush();
            fs.Close();
        }

        public class AuthConfig
        {
            public string Ip = "127.0.0.1";
            public int Port = 2000;
        }
    }

}
