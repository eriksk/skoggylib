using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.Physics.Rendering;

namespace se.skoggy.utils.Physics
{
    public class PhysicsWorld
    {
        private readonly World _world;
        private DebugViewMonoGame _debug;
        private DebugViewCamera2D _cam;

        public PhysicsWorld(Vector2 gravity)
        {
            _world = new World(gravity);
        }

        public void LoadDebug(ContentManager content, GraphicsDevice device, string debugFontName)
        {
            _debug = new DebugViewMonoGame(_world);
            _debug.LoadContent(device, content, debugFontName);
            _cam = new DebugViewCamera2D(device);
        }

        public World World
        {
            get { return _world; }
        }

        public DebugViewCamera2D Cam
        {
            get { return _cam; }
        }

        public void Update(float dt)
        {
            _world.Step((1f / 30f) * (dt / 16f));

            if (_cam == null)
                return;

            _cam.Update(dt);
        }

        public void DrawDebug()
        {
            if(_debug == null)
                throw new Exception("Call LoadDebug first");

            _debug.RenderDebugData(_cam.SimProjection, _cam.SimView);
        }
    }
}
