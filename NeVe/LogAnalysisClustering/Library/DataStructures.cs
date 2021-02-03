using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace LogAnalysisLibrary.Algorithms
{
    public static class DataStructures
    {
        private static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static string Serialization(object target, Type type)
        {
            XmlSerializer xsSubmit = new XmlSerializer(type);

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, target);
                    return sww.ToString(); // Your XML
                }
            }
        }

        public static object Deserialization(string data, Type type)
        {
            XmlSerializer serializer = new XmlSerializer(type);

            using (TextReader reader = new StringReader(data))
            {
                return serializer.Deserialize(reader);
            }
        }
    }   
}
