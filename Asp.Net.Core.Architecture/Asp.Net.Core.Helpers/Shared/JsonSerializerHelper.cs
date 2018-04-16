using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.Net.Core.Helpers.Shared
{
    /// <summary>
    /// Provides methods for Serialization and Deserialization of JSON/JavaScript Object Notation documents.
    /// </summary>
    public class JsonSerializerHelper
    {
        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject
            (
                obj,
                Formatting.Indented,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                   // TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple
                }
            );
        }

        public static T Deserialize<T>(string jsonstring) where T : class
        {
            return JsonConvert.DeserializeObject<T>
            (
                jsonstring,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                    //Converters = new List<JsonConverter> { new Int32Converter() }
                }
            );
        }
        public static bool TrySerialize(object obj, out string jsonstring)
        {
            jsonstring = null;
            try
            {
                jsonstring = Serialize(obj);
                return true;
            }
            catch (Exception)
            {
                jsonstring = null;
                return false;
            }
        }
        public static bool TryDeserialize<T>(string jsonstring, out T obj) where T : class
        {
            obj = null;

            try
            {
                obj = Deserialize<T>(jsonstring);
                return true;
            }
            catch (Exception)
            {

                obj = null;
                return false;
            }
        }
    }
}
