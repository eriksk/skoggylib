using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace se.skoggy.utils.Sprites
{
    public class DynamicTextureAtlasLoader
    {
        public DynamicTextureAtlasData Load(string path)
        {
            var json = File.ReadAllText(path);
            var atlasData = JsonConvert.DeserializeObject<DynamicTextureAtlasData>(json, new JsonConverter[]{ new DynamicTextureAtlasDataJsonConverter() });
            atlasData.Name = Path.GetFileNameWithoutExtension(path);
            return atlasData;
        }
    }

    internal class LazyRectangle
    {
        public int x, y, w, h;

        public Rectangle ToRect()
        {
            return new Rectangle(x,y,w,h);
        }
    }

    public class DynamicTextureAtlasDataJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (objectType == typeof (Rectangle))
            {
                return serializer.Deserialize<LazyRectangle>(reader).ToRect();
            }

            return serializer.Deserialize(reader);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (Rectangle);
        }
    }

    public class DynamicTextureAtlasData
    {
        public DynamicTextureAtlasDataFrame[] frames;
        public DynamicTextureAtlasDataMeta meta;

        [JsonIgnore]
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class DynamicTextureAtlasDataMeta
    {
        public string image;
    }

    public class DynamicTextureAtlasDataFrame
    {
        public string filename;
        public Rectangle frame;

    }
}
