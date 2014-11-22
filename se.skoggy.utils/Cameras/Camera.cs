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
        private Rectangle _boundary;
        private bool _useBoundary;
        private float _rotation;
        private CameraShaker _shaker;

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
            ZoomSpeed = 0.01f;
            MaxZoom = 0.1f;
            MinZoom = 2f;
            _boundary = new Rectangle();
            _useBoundary = false;
            _shaker = new CameraShaker();
        }

        public void SetBoundary(Rectangle area)
        {
            _boundary = area;
            _useBoundary = true;
        }

        public float Speed { get; set; }
        public float ZoomSpeed { get; set; }
        public float MaxZoom { get; set; }
        public float MinZoom { get; set; }
        public float Scale { get { return scale; } }
        public Vector2 Offset { get; set; }
        public float OffsetRotation { get; set; }

        public float Rotation 
        {
            get { return _rotation; }
        }

        public void SetSize(int Width, int Height)
        {
            center.X = Width / 2;
            center.Y = Height / 2;
        }

        public void Shake(int duration, float mag)
        {
            _shaker.Start(duration, mag);
        }

        public Matrix GetParallaxView(Vector2 parallax)
        {
            return Matrix.CreateRotationZ(rotation) *
                   Matrix.CreateRotationZ(OffsetRotation) *
                   Matrix.CreateTranslation(new Vector3(position.X * parallax.X, position.Y * parallax.Y, 0f)) *
                   Matrix.CreateTranslation(Offset.X, Offset.Y, 0f) *
                   Matrix.CreateScale(scale) *
                   Matrix.CreateTranslation(center.X, center.Y, 0f);
        }

        public virtual Matrix View
        {
            get
            {
                return Matrix.CreateRotationZ(rotation) *
                       Matrix.CreateRotationZ(OffsetRotation) *
                       Matrix.CreateTranslation(position.X, position.Y, 0f) *
                       Matrix.CreateTranslation(Offset.X, Offset.Y, 0f) *
                       Matrix.CreateScale(scale) *
                       Matrix.CreateTranslation(center.X, center.Y, 0f);
            }
        }
        
        public virtual Matrix ViewNoScale
        {
            get
            {
                return Matrix.CreateRotationZ(rotation) *
                       Matrix.CreateTranslation(position.X, position.Y, 0f) *
                       Matrix.CreateTranslation(center.X, center.Y, 0f);
            }
        }

        public Vector2 ScreenToWorld(Vector2 screen)
        {
            return Vector2.Transform(screen, Matrix.Invert(View));
        }

        public Vector2 WorldToScreen(Vector2 screen) 
        {
            return new Vector2();
        }

        public void SetPosition(float x, float y) 
        {
            position.X = -x;
            position.Y = -y;
            Move(-x, -y);
        }

        public void SetX(float x)
        {
            position.X = -x;
            target.X = -x;
        }

        public void SetY(float y)
        {
            position.Y = -y;
            target.Y = -y;
        }

        public void Move(Vector2 position)
        {
            Move(position.X, position.Y);
        }

        public void Move(float x, float y)
        {
            target.X = -x;
            target.Y = -y;
        }

        public void MoveBy(float x, float y)
        {
            target.X += -x;
            target.Y += -y;
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

        public void ZoomTo(float zoom)
        {
            targetScale = zoom;
        }

        public virtual void Update(float dt)
        {   
            _shaker.Update(dt, this);

            position.X = movementInterpolation.Apply(position.X, target.X, Speed);
            position.Y = movementInterpolation.Apply(position.Y, target.Y, Speed);

            rotation = movementInterpolation.Apply(rotation, targetRotation, Speed);
            scale = movementInterpolation.Apply(scale, targetScale, ZoomSpeed);
            scale = MathHelper.Clamp(scale, MaxZoom, MinZoom);

            if (_useBoundary)
            {
                if (-position.X + center.X > _boundary.Right)
                    position.X = -_boundary.Right + center.X;
                if (-position.X - center.X < _boundary.Left)
                    position.X = -_boundary.Left - center.X;
                if (-position.Y + center.Y > _boundary.Bottom)
                    position.Y = -_boundary.Bottom + center.Y;
                if (-position.Y - center.Y < _boundary.Top)
                    position.Y = -_boundary.Top - center.Y;
            }
        }

        public float GetZoom()
        {
            return scale;
        }
    }
}
