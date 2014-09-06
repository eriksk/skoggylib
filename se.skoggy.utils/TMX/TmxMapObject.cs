using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.TMX
{
    public class TmxMapObject
    {
        public string name;
        public int x, y, width, height;
        public float rotation;
        public string type;
        public bool visible;
        public Dictionary<string, string> properties;
    }
}
