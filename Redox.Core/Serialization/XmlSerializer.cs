using System;
using System.IO;
using Serializer = System.Xml.Serialization.XmlSerializer;

namespace Redox.Core.Serialization
{
    public static class XmlSerializer
    {
        public static string ToXml(object ob)
        {
            if (ob == null) return string.Empty;
            try
            {
                using (StringWriter writer = new StringWriter())
                {
                    Serializer serializer = new Serializer(ob.GetType());
                    serializer.Serialize(writer, ob);
                    return writer.ToString();
                }
                
            }
            catch (Exception e)
            {
                RedoxMod.GetMod().TempLogger.Error("[XmlParser] An error happened while trying to serialize \"{0}\" to xml. Error: {1}", nameof(ob), e.Message);
                return string.Empty;
            }
        }

        public static T FromXml<T>(string xml)
        {
            try
            {
                using (StringReader reader = new StringReader(xml))
                {
                    Serializer serializer = new Serializer(typeof(T));
                    return (T)serializer.Deserialize(reader);
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return default;
            }
        }

        public static void ToFile(string filePath, object ob)
        {
            try
            {
                if (ob == null) return;
                string xml = ToXml(ob);
                File.WriteAllText(filePath, xml);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static T FromFile<T>(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    using StreamReader reader = File.OpenText(filePath);
                    Serializer serializer = new Serializer(typeof(T));
                    return (T)serializer.Deserialize(reader);
                }

                return default;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
    }
}