using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.Physics.Events;
using se.skoggy.utils.Physics.Rendering;

namespace se.skoggy.utils.Physics
{
    public class PhysicsWorld
    {
        private readonly World _world;
        private DebugViewMonoGame _debug;
        private DebugViewCamera2D _cam;

        private readonly PhysicsEvents _events;

        public PhysicsWorld(Vector2 gravity)
        {
            _world = new World(gravity);
            _events = new PhysicsEvents();
        }

        public void LoadDebug(ContentManager content, GraphicsDevice device, string debugFontName)
        {
            _debug = new DebugViewMonoGame(_world);
            _debug.LoadContent(device, content, debugFontName);
            _cam = new DebugViewCamera2D(device);
        }

        public PhysicsEvents Events 
        {
            get { return _events; }
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
            if(dt > 0f)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position">In sim units</param>
        /// <param name="force">Impulse force</param>
        /// <param name="reach">In sim units</param>
        public void CreateExplosionForce(Vector2 position, float force, float reach)
        {
            foreach (var body in _world.BodyList)
            {
                float distance = Vector2.Distance(body.Position, position);
                if(distance > reach) continue;

                float angle = (float)Math.Atan2(body.Position.Y - position.Y, body.Position.X - position.X);
                body.ApplyLinearImpulse(new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * force, position);
                if (body.UserData != null)
                {
                    Events.InvokeOnBodyHitByExplosion(body, force, reach, distance);
                }
            }
        }
    }
}
