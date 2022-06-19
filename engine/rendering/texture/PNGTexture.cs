using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Numerics;

using GEngine.OpenGL;
using System.Drawing.Imaging;

namespace GEngine.engine.rendering.texture
{
    class PNGTexture
    {
        public uint textureId { get; set; }

        public Bitmap texture { get; set; }

        public Vector2 textureSize { get; set; }
        public PNGTexture(Bitmap texture)
        {
            this.texture = texture;
            this.textureSize = new Vector2(texture.Width, texture.Height);
        }


        public float[] calculateCoordinates(Vector2 singleTextureSize,int offset)
        {
            float sUnit = 1 / textureSize.X;
            float tUnit = 1 / textureSize.Y;

            //row size in textures
            int rowSize = (int)(textureSize.X / singleTextureSize.X);

            int offsetInRow = offset % rowSize;
            int rowOffset = (offset / rowSize) + 1;

            float texturePosBottomLeftX = sUnit * offsetInRow * singleTextureSize.X;
            float texturePosBottomLeftY = tUnit * rowOffset * singleTextureSize.Y;

            float texturePosBottomRightX = texturePosBottomLeftX + (singleTextureSize.X * sUnit);
            float texturePosBottomRightY = texturePosBottomLeftY;

            float texturePosTopRightX = texturePosBottomLeftX + (singleTextureSize.X * sUnit);
            float texturePosTopRightY = texturePosBottomLeftY + (singleTextureSize.Y * tUnit);

            float texturePosTopLeftX = texturePosBottomLeftX;
            float texturePosTopLeftY = texturePosBottomLeftY + (singleTextureSize.Y * tUnit);

            float[] coordinates = new float[]
            {
                texturePosBottomLeftX,
                texturePosBottomLeftY,
                texturePosBottomRightX,
                texturePosBottomRightY,
                texturePosTopLeftX,
                texturePosTopLeftY,
                texturePosTopRightX,
                texturePosTopRightY
            };

            return coordinates;
        }


        public void Create()
        {
            textureId = GL.glGenTexture();
            GL.glBindTexture(GL.GL_TEXTURE_2D, textureId);


            BitmapData data = texture.LockBits(new Rectangle(0, 0, texture.Width, texture.Height), ImageLockMode.ReadOnly ,PixelFormat.Format32bppArgb);

            GL.glTexImage2D(GL.GL_TEXTURE_2D, 0, GL.GL_RGBA, texture.Width, texture.Height, 0, GL.GL_BGRA, GL.GL_UNSIGNED_BYTE,data.Scan0);
            GL.glGenerateMipmap(GL.GL_TEXTURE_2D);

            GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_WRAP_S, GL.GL_REPEAT);
            GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_WRAP_T, GL.GL_REPEAT);
            GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MIN_FILTER, GL.GL_NEAREST_MIPMAP_LINEAR);
            GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MAG_FILTER, GL.GL_NEAREST_MIPMAP_LINEAR);

            GL.glBindTexture(GL.GL_TEXTURE_2D, 0);
        }

        public void Use()
        {
            GL.glBindTexture(GL.GL_TEXTURE_2D, textureId);
        }

        public void Unbind()
        {
            GL.glBindTexture(GL.GL_TEXTURE_2D, 0);
        }
    }
}
