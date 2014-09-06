using Microsoft.Xna.Framework.Content;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.TMX
{
    public class TmxMapDataLoader
    {
        private string mapDirectoryName;
        private ContentManager content;

        public TmxMapDataLoader(ContentManager content, string mapDirectoryName)
	    {
            this.content = content;
            this.mapDirectoryName = mapDirectoryName;
	    }

        public TmxMapData Load(string name) 
        {
            var jsonContent = File.ReadAllText(string.Format("{0}/{1}/{2}.json", content.RootDirectory, mapDirectoryName, name));
            return JsonConvert.DeserializeObject<TmxMapData>(jsonContent);
        }
    }
}
