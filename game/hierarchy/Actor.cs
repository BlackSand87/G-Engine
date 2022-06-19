using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

using GEngine.engine.rendering;
using GEngine.OpenGL;

namespace GEngine.game.hierarchy
{
    class Actor
    {

        public Vector2 position { get; set; }
        public float rotation { get; set; }
        public Vector2 scale { get; set; }


        public bool enabled { get; set; }
        public bool visiable { get; set; }

        public Material actorMaterial { get; set; }

        uint vao;
        uint vbo;

        public Actor(Vector2 position, float rotation, Vector2 scale, Material actorMaterial)
        {
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
            this.actorMaterial = actorMaterial;
        }

        public unsafe void LoadActor(int atlasOffset = 0, bool isUsingTextureAtlas = false)
        {
            vao = GL.glGenVertexArray();
            vbo = GL.glGenBuffer();

            //Bind the buffers
            GL.glBindVertexArray(vao);
            GL.glBindBuffer(GL.GL_ARRAY_BUFFER, vbo);


            float[] vertices;

            if(isUsingTextureAtlas)
            {
                vertices = generateVertices(atlasOffset);
            }

            else
            {
                vertices = generateVertices();
            }

            fixed (float* v = &vertices[0])
            {
                GL.glBufferData(GL.GL_ARRAY_BUFFER, sizeof(float) * vertices.Length, v, GL.GL_STATIC_DRAW);
            }

            GL.glVertexAttribPointer(0, 2, GL.GL_FLOAT, false, 4 * sizeof(float), (void*)0);
            GL.glEnableVertexAttribArray(0);

            GL.glVertexAttribPointer(1, 2, GL.GL_FLOAT, false, 4 * sizeof(float), (void*)(2 * sizeof(float)));
            GL.glEnableVertexAttribArray(1);

            GL.glBindVertexArray(0);
            GL.glBindBuffer(GL.GL_ARRAY_BUFFER, 0);
        }

        public float[] generateVertices(int atlasOffset)
        {
            float[] textureCoordinates = actorMaterial.materialTexture.calculateCoordinates(scale, atlasOffset);

            float[] vertices =
            {
                -0.5f, 0.5f, textureCoordinates[4], textureCoordinates[5], // top left
                0.5f, 0.5f, textureCoordinates[6], textureCoordinates[7],// top right
                -0.5f, -0.5f, textureCoordinates[0], textureCoordinates[1], // bottom left

                0.5f, 0.5f, textureCoordinates[6], textureCoordinates[7],// top right
                0.5f, -0.5f, textureCoordinates[2], textureCoordinates [3], // bottom right
                -0.5f, -0.5f, textureCoordinates[0], textureCoordinates[1] // bottom left
            };
            return vertices;
        }

        public float[] generateVertices()
        {
            float[] vertices =
            {
                -0.5f, 0.5f, 0, 1, // top left
                0.5f, 0.5f, 1, 1,// top right
                -0.5f, -0.5f, 0, 0, // bottom left

                0.5f, 0.5f, 1, 1,// top right
                0.5f, -0.5f, 1, 0, // bottom right
                -0.5f, -0.5f, 0, 0 // bottom left
            };

            return vertices;
        }

        public void RenderActor()
        {
            Matrix4x4 translationMatrix = Matrix4x4.CreateTranslation(position.X, position.Y, 0);
            Matrix4x4 rotationMatrix = Matrix4x4.CreateRotationZ(rotation);
            Matrix4x4 scaleMatrix = Matrix4x4.CreateScale(scale.X, scale.Y, 1);

            actorMaterial.Use();

            actorMaterial.materialShader.SetMatrix4x4("model", scaleMatrix * rotationMatrix * translationMatrix);

            actorMaterial.materialShader.SetMatrix4x4("projection", TestGame.GetCameraProjectionMatrix());
            actorMaterial.materialShader.SetTexture("tex", actorMaterial.materialTexture.textureId);

            GL.glBindVertexArray(vao);
            GL.glDrawArrays(GL.GL_TRIANGLES, 0, 6);

            actorMaterial.Unbind();
        }
    }
}
