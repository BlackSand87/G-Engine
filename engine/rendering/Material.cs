using System;
using System.Collections.Generic;
using System.Text;

using GEngine.engine.rendering.shaders;
using GEngine.engine.rendering.texture;

using GEngine.OpenGL;

namespace GEngine.engine.rendering
{
    class Material
    {
        public Shader materialShader { get; set; }
        public PNGTexture materialTexture { get; set; }

        public Material(Shader materialShader, PNGTexture materialTexture)
        {
            this.materialShader = materialShader;
            this.materialTexture = materialTexture;
            
        }

        public void Use()
        {
            materialShader.Use();
            materialTexture.Use();
        }

        public void Unbind()
        {
            GL.glUseProgram(0);
            materialTexture.Unbind();
        }

        public Material NULL = null;
    }
}
