using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace se.skoggy.utils.Gleed.Model
{
    public class GleedMap
    {
        public Layer[] layers;
        public string name;
    }

    public class Layer
    {
        public string name;
        public Editor[] items;
        public LayerProperties properties;
    }

    public class Editor
    {
        public string name;
        public TextureItemProperties properties;
    }

    public class TextureItemProperties
    {
        public bool flipHorizontally;
        public bool flipVertically;
        public string id;
        public bool isTemplate;
        public string name;
        public Vector2 origin;
        public Vector2 position;
        public float rotation;
        public Vector2 scale;
        public string texturePathRelativeToContentRoot;
        public Color color;
        public bool visible;
        public string assetName;
        public Vector2[] nodes;
        public string type;
    }

    public class LayerProperties
    {
        public CustomProperty[] customProperties;
        public string id;
        public string name;
        public Vector2 position;
        public Vector2 scrollSpeed;
        public bool visible;
    }

    public class CustomProperty
    {
        public string name;
        public string value;
        public string description;
        public string type;
    }

    public class LevelProperties
    {
        public Vector2 CameraPosition;
        public string ContentRootFolder;
        // CustomProperties
        public string Id;
        public string Name;
        public Vector2 Position;
        public string Version;
        public bool Visible;
    }
}