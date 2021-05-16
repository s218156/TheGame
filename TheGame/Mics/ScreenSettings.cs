using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace TheGame.Mics
{
    public class ScreenSettings
    {
        public string path = "screenConfig.xml";
        public int height { get; set; }
        public int width { get; set; }
        public bool isFullScreen { get; set; }


        

        public void Save()
        {
            using (var stream=new FileStream(path, FileMode.Create))
            {
                var XML = new XmlSerializer(typeof(ScreenSettings));
                XML.Serialize(stream, this);
            }
        }

        public ScreenSettings Load()
        {
            using (var stream = new FileStream(path, FileMode.Open))
            {
                var XML = new XmlSerializer(typeof(ScreenSettings));
                return (ScreenSettings)XML.Deserialize(stream);
            }
        }

        public bool ConfigFileExist()
        {
            return File.Exists(path);
        }


    }
}
