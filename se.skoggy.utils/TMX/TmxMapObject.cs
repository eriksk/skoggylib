using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.TMX
{
    public class TmxMapObject
    {
        public int gid;
        public string name;
        public float x, y, width, height;
        public float rotation;
        public string type;
        public bool visible;
        public Dictionary<string, string> properties;
    }
}
