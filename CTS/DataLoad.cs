using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CTS
{
    public static class DataLoad
    {
        public static void Save<T>(T data, string path)
        {
            StreamWriter sw = new StreamWriter(path);

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(sw, data);

            sw.Close();
        }

        public static T Load<T>(string path) where T : new()
        {
            if (!File.Exists(path)) return new T();
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StreamReader sw = new StreamReader(path);

            T list = (T)serializer.Deserialize(sw);
            sw.Close();
            return list;
        }
    }
}
