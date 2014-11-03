using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.Graphics;
using se.skoggy.utils.Sprites;

namespace se.skoggy.utils.Animations.KeyFrameAnimations
{
    public class FramePart
    {
        public Vector2 Position;
        public Vector2 Scale;
        public Vector2 Origin;
        public float Rotation;
        public bool Flipped { get; set; }
        public string TextureName { get; set; }

        public FramePart()
        {
            Scale = Vector2.Zero;
            Origin = new Vector2(0.5f, 0.5f);
            Scale = Vector2.One;
        }

        public Matrix GetLocalTransform(Rectangle source, bool flip)
        {
            // Transform = -Origin * Scale * Rotation * Translation
            return// Matrix.CreateTranslation(-source.Width * Origin.X, -source.Height * Origin.Y, 0f) *
                   Matrix.CreateScale(Scale.X, Scale.Y, 1f) *
                //Matrix.CreateRotationZ(Rotation) *
                   Matrix.CreateTranslation(Position.X * (flip ? -1f : 1f), Position.Y, 0f);
        }

        public static void DecomposeMatrix(ref Matrix matrix, out Vector2 position, out float rotation, out Vector2 scale)
        {
            Vector3 position3, scale3;
            Quaternion rotationQ;
            matrix.Decompose(out scale3, out rotationQ, out position3);
            Vector2 direction = Vector2.Transform(Vector2.UnitX, rotationQ);
            rotation = (float)Math.Atan2(direction.Y, direction.X);
            position = new Vector2(position3.X, position3.Y);
            scale = new Vector2(scale3.X, scale3.Y);
        }

        public FramePart Clone()
        {
            return new FramePart
            {
                Position = Position,
                Scale = Scale,
                Origin = Origin,
                Rotation = Rotation,
                Flipped = Flipped,
                TextureName = TextureName
            };
        }

        public void Draw(SpriteBatch spriteBatch, DynamicTextureAtlasManager atlas, Color color, bool flipped = false)
        {
            var dynamicTexture = atlas.GetTexture(TextureName);
            if (dynamicTexture == null)
                return;

            var source = dynamicTexture.Source;
            Vector2 position = Position;
            float rotation = Rotation;

            if (flipped)
            {
                rotation *= -1f;
                position.X *= -1f;
            }

            var originInPixels = new Vector2 {X = source.Width*Origin.X, Y = source.Height*Origin.Y};

            var flip = Flipped ? (!flipped) : (flipped);

            spriteBatch.Draw(
                dynamicTexture.Texture,
                position, 
                source,
                color,
                rotation,
                originInPixels,
                Scale,
                flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                0f);
        }


        public void Draw(SpriteBatch spriteBatch, DynamicTextureAtlasManager atlas, Vector2 position, Vector2 scale, Color color, bool flipped = false)
        {
            var dynamicTexture = atlas.GetTexture(TextureName);
            if (dynamicTexture == null)
                return;

            var source = dynamicTexture.Source;

            var parent = (Matrix.Identity * Matrix.CreateScale(scale.X, scale.Y, 1f) * Matrix.CreateTranslation(position.X, position.Y, 0f));

            Matrix globalTransform = GetLocalTransform(source, flipped) * parent;

            Vector2 lposition, lscale;
            float lrotation;
            DecomposeMatrix(ref globalTransform, out lposition, out lrotation, out lscale);

            float rot = Rotation;

            if (flipped)
            {
                //lposition.X *= -1f;
                rot *= -1f;
            }

            var originInPixels = new Vector2();
            originInPixels.X = source.Width * Origin.X;
            originInPixels.Y = source.Height * Origin.Y;

            var flip = this.Flipped ? (!flipped) : (flipped);


            // IT works properly now. We just have to "rotate" the scaling, so that it scales rotated properly

            spriteBatch.Draw(
                dynamicTexture.Texture,
                lposition,
                source,
                Color.White,
                rot,
                originInPixels,
                Scale * scale,
                flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                0.0f);

            /*
            
            Vector2 scaleFactor = Scale * scale;

            var originInPixels = new Vector2();
            originInPixels.X = source.Width * Origin.X;
            originInPixels.Y = source.Height * Origin.Y;

            var flip = this.Flipped ? (!flipped) : (flipped);

            Vector2 pos = Position ;

            float rot = Rotation;

            if (flipped)
            {
                pos.X *= -1f;
                rot *= -1f;
            }
            

            spriteBatch.Draw(
                dynamicTexture.Texture,
                position + ScaleTargetPosition(pos, Vector2.Zero, scaleFactor), 
                source,
                color,
                rot,
                originInPixels,
                scaleFactor,
                flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                0f);
             * */
        }

        public void DrawDebugPoint(SpriteBatch spriteBatch, DynamicTextureAtlasManager atlas, DrawableRectangle point, Vector2 position, Vector2 scale)
        {

            var dynamicTexture = atlas.GetTexture(TextureName);
            if (dynamicTexture == null)
                return;

            var source = dynamicTexture.Source;

            var parent = (Matrix.Identity * Matrix.CreateScale(scale.X, scale.Y, 1f) * Matrix.CreateTranslation(position.X, position.Y, 0f));

            Matrix globalTransform = Matrix.CreateTranslation(Position.X, Position.Y, 0f) * parent;

            Vector2 lposition, lscale;
            float lrotation;
            DecomposeMatrix(ref globalTransform, out lposition, out lrotation, out lscale);


            point.X = (int)lposition.X - point.Width / 2;
            point.Y = (int)lposition.Y - point.Height / 2;

            point.Draw(spriteBatch);
        }

        private Vector2 ScaleTargetPosition(Vector2 target, Vector2 point, Vector2 scaleMag)
        {
            Vector2 scalePosition = Vector2.Transform(target - point, Matrix.CreateScale(scaleMag.X, scaleMag.Y, 1f));
            return scalePosition + point;
        }
    }
}
