using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.TMX
{
    public class TmxTileSet
    {
        public int firstgid;
        public string image;
        public int imagewidth, imageheight;
        public int margin;
        public string name;
        public int spacing;
        public int tilewidth, tileheight;
        public Dictionary<string, Tile> tiles;
        public Dictionary<string, string> properties;
    }

    public class Tile
    {
        public string image;
    }
}
