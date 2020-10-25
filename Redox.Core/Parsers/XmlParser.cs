using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Redox.Core.Parsers
{
    public static class XmlParser
    {
        public static string ToXml(object ob)
        {
            if (ob == null) return string.Empty;
            try
            {
                using (StringWriter writer = new StringWriter())
                {
                    XmlSerializer serializer = new XmlSerializer(ob.GetType());
                    serializer.Serialize(writer, ob);
                    return writer.ToString();
                }
                
            }
            catch (Exception e)
            {
                RedoxMod.GetMod().Logger.Error("[XmlParser] An error happended while trying to serialize \"{0}\" to xml. Error: {1}", nameof(ob), e.Message);
                return string.Empty;
            }
        }

        public static T FromXml<T>(string xml)
        {
            try
            {
                using (StringReader reader = new StringReader(xml))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    return (T)serializer.Deserialize(reader);
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return default;
            }
        }
    }
}