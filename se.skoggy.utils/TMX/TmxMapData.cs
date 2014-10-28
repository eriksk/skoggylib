using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.TMX
{
    public class TmxMapData
    {
        public string version;
        public int width, height;
        public int tilewidth, tileheight;
        public string orientation;
        public TmxMapLayer[] layers;
        public TmxTileSet[] tilesets;
        public Dictionary<string, string> properties;

        public TmxMapData()
        {
            properties = new Dictionary<string, string>();
        }
    }
}
