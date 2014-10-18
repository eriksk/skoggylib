using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using se.skoggy.utils.Gleed.Model;

namespace se.skoggy.utils.Gleed
{
    internal class GleedMapJsonConverter : JsonConverter
    {
        private readonly Type[] _types = new Type[]
        {
            typeof(Vector2),
            typeof(Color)
        };

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            string value = reader.Value.ToString();
            if (objectType == typeof (Vector2))
            {
                return new Vector2(
                    float.Parse(value.Split(',')[0].Trim(), CultureInfo.InvariantCulture),
                    float.Parse(value.Split(',')[1].Trim(), CultureInfo.InvariantCulture));
            }
            else if (objectType == typeof(Color))
            {
                return new Color(
                    byte.Parse(value.Split(',')[0].Trim(), CultureInfo.InvariantCulture),
                    byte.Parse(value.Split(',')[1].Trim(), CultureInfo.InvariantCulture),
                    byte.Parse(value.Split(',')[2].Trim(), CultureInfo.InvariantCulture),
                    byte.Parse(value.Split(',')[3].Trim(), CultureInfo.InvariantCulture));
            }

            return serializer.Deserialize(reader);
        }

        public override bool CanConvert(Type objectType)
        {
            return _types.Contains(objectType);
        }
    }

    public class GleedMapLoader
    {
        public GleedMap Load(string fileName)
        {
            string json = File.ReadAllText(fileName);
            var map = JsonConvert.DeserializeObject<GleedMap>(json, new JsonConverter[]
            {
                new GleedMapJsonConverter()
            });
            return map;
        }
    }
}
