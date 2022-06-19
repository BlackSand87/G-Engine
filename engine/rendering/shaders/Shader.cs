using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

using GEngine.OpenGL;

namespace GEngine.engine.rendering.shaders
{
    class Shader
    {
        string vertexCode;
        string fragmentCode;
        public uint ProgramID { get; set; }
        public Shader(string vertexCode, string fragmentCode)
        {
            this.vertexCode = vertexCode;
            this.fragmentCode = fragmentCode;
        }

        public void Load()
        {
            uint vs = GL.glCreateShader(GL.GL_VERTEX_SHADER);
            GL.glShaderSource(vs, vertexCode);
            GL.glCompileShader(vs);

            int[] status = GL.glGetShaderiv(vs, GL.GL_COMPILE_STATUS, 1);

            if(status[0] == 0)
            {
                string error = GL.glGetShaderInfoLog(vs);
                Console.WriteLine("Error Compiling Vertex Shader: " + error);
            }

            uint fs = GL.glCreateShader(GL.GL_FRAGMENT_SHADER);
            GL.glShaderSource(fs, fragmentCode);
            GL.glCompileShader(fs);

            status = GL.glGetShaderiv(fs, GL.GL_COMPILE_STATUS, 1);

            if (status[0] == 0)
            {
                string error = GL.glGetShaderInfoLog(fs);
                Console.WriteLine("Error Compiling Fragment Shader: " + error);
            }

            ProgramID = GL.glCreateProgram();
            GL.glAttachShader(ProgramID, vs);
            GL.glAttachShader(ProgramID, fs);
            GL.glLinkProgram(ProgramID);

            GL.glDetachShader(ProgramID, vs);
            GL.glDetachShader(ProgramID, fs);
            GL.glDeleteShader(vs);
            GL.glDeleteShader(fs);
        }

        public void Use()
        {
            GL.glUseProgram(ProgramID);
        }

        public void SetMatrix4x4(string uniformName, Matrix4x4 value)
        {
            int location = GL.glGetUniformLocation(ProgramID, uniformName);
            GL.glUniformMatrix4fv(location, 1, false, GetMatrix4x4Values(value));
        }

        private float[] GetMatrix4x4Values(Matrix4x4 m)
        {
            return new float[]
            {
                m.M11, m.M12, m.M13, m.M14,
                m.M21, m.M22, m.M23, m.M24,
                m.M31, m.M32, m.M33, m.M34,
                m.M41, m.M42, m.M43, m.M44
            };
        }

        public void SetTexture(string uniformName, uint value)
        {
            int loc = GL.glGetUniformLocation(ProgramID, uniformName);
            GL.glUniform1ui(loc, value);
        }

    }
}
