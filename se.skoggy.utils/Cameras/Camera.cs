using Microsoft.Xna.Framework;
using se.skoggy.utils.Interpolations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Cameras
{
    public class Camera
    {
        public Vector2 position, center;
        protected Vector2 target;
        protected Interpolation movementInterpolation;
        protected float rotation, targetRotation;
        protected float scale, targetScale;

        public Camera(Vector2 center)
        {
            this.center = center;
            target = new Vector2();
            position = new Vector2();
            rotation = 0f;
            scale = 1f;
            targetScale = 1f;
            movementInterpolation = Interpolation.Linear;
            Speed = 0.05f;
            MaxZoom = 0.1f;
            MinZoom = 2f;
        }

        public float Speed { get; set; }
        public float MaxZoom { get; set; }
        public float MinZoom { get; set; }
        public float Scale { get { return scale; } }

        public void SetSize(int Width, int Height)
        {
            center.X = Width / 2;
            center.Y = Height / 2;
        }

        public virtual Matrix Projection 
        {
            get 
            {
                return Matrix.CreateRotationZ(rotation) * 
                       Matrix.CreateTranslation(position.X, position.Y, 0f) *
                       Matrix.CreateScale(scale) *
                       Matrix.CreateTranslation(center.X, center.Y, 0f);
            }
        }

        public Vector2 ScreenToWorld(Vector2 screen)
        {
            return Vector2.Transform(screen, Matrix.Invert(Projection));
        }

        public Vector2 WorldToScreen(Vector2 screen) 
        {
            return new Vector2();
        }

        public void SetPosition(float x, float y) 
        {
            position.X = x;
            position.Y = y;
            Move(x, y);
        }

        public void SetX(float x)
        {
            position.X = x;
            target.X = x;
        }

        public void SetY(float y)
        {
            position.Y = y;
            target.Y = y;
        }

        public void Move(float x, float y)
        {
            target.X = x;
            target.Y = y;
        }

        public void MoveBy(float x, float y)
        {
            target.X += x;
            target.Y += y;
        }

        public void SetRotation(float rotation) 
        {
            this.rotation = rotation;
            Rotate(rotation);
        }

        public void Rotate(float rotation)
        {
            this.targetRotation = rotation;
        }

        public void SetZoom(float zoom) 
        {
            this.scale = zoom;
            targetScale = zoom;
        }

        public void Zoom(float zoom)
        {
            targetScale += zoom;
        }

        public virtual void Update(float dt)
        {            
            position.X = movementInterpolation.Apply(position.X, target.X, Speed);
            position.Y = movementInterpolation.Apply(position.Y, target.Y, Speed);

            rotation = movementInterpolation.Apply(rotation, targetRotation, Speed);
            scale = movementInterpolation.Apply(scale, targetScale, Speed);
            scale = MathHelper.Clamp(scale, MaxZoom, MinZoom);
        }
    }
}
