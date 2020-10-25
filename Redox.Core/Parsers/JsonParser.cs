

using System;
using System.IO;
using Newtonsoft.Json;

namespace Redox.Core.Parsers
{
    /// <summary>
    /// Parser for Json
    /// </summary>
    public static class JsonParser
    {
        /// <summary>
        /// Serializes an object to Json string format.
        /// </summary>
        /// <param name="ob">The object you want to serialize to json.</param>
        /// <param name="indent">Should the json string me indented?</param>
        /// <returns>The string in json format.</returns>
        public static string ToJson(object ob, bool indent = true)
        {
            try
            {
                if (ob == null)
                    return string.Empty;
                string json = JsonConvert.SerializeObject(ob, indent ? Formatting.Indented : Formatting.None);
                return json;
            }
            catch (JsonException e)
            {
                RedoxMod.GetMod().Logger.Error("[JsonParser] An error happened while trying to parse \"{0}\". Error: {1}", nameof(ob), e.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// Serializes an object to Json string format and saves it into a file.
        /// <para>When the file already exists it will be overwritten.</para>
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="ob">The object you want to serialize to json.</param>
        public static void ToFile(string path, object ob)
        {
            try
            {
                string json = ToJson(ob);
                File.WriteAllText(path, json);
            }
            catch (Exception e)
            {
                RedoxMod.GetMod().Logger.Error("[JsonParser] An error happened at \"ToFile\" \"{0}\". Error: {1}", nameof(ob), e.Message);
            }
        }
        public static T FromJson<T>(string json)
        {
            try
            {
                return (T)JsonConvert.DeserializeObject(json);
            }
            catch (Exception e)
            {
                RedoxMod.GetMod().Logger.Error("[JsonParser] An error happened at \"FromJson\". Error: {0} ", e.Message);
                return default;
            }
        }

        public static T FromFile<T>(string path)
        {
            return FromJson<T>(File.ReadAllText(path));
        }
    }
}