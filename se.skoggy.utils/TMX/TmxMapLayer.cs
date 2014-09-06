using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.TMX
{
    public class TmxMapLayer
    {
        public int[] data;
        public int width, height;
        public string name;
        public float opacity;
        public string type;
        public bool visible;
        public int x, y;
        public string draworder;
        public TmxMapObject[] objects;
    }
}
