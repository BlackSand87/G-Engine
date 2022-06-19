using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

using GEngine.game;
using GEngine.engine.rendering.window;
using GEngine.OpenGL;
using GEngine.engine.rendering.shaders;
using GEngine.engine.rendering.camera;
using GEngine.engine.rendering.texture;
using GEngine.engine.rendering;
using GEngine.game.hierarchy;

namespace GEngine
{
    class TestGame : Game
    {
        uint vao;
        uint vbo;

        Shader shader;
        PNGTexture texture;

        static Camera2D gameCamera;

        public TestGame(int initialWidth, int initialHeight, string initialTitle) : base(initialWidth, initialHeight, initialTitle)
        {
            base.InitialWidth = initialWidth;
            base.InitialHeight = initialHeight;
            base.InitialTitle = initialTitle;
        }

        protected override void Initialize()
        {
            Hierarchy.Init();
        }

        protected unsafe override void LoadContent()
        {
            //Default Shaders
            string vertexShader = @"#version 330 core
                                    layout (location = 0) in vec2 aPosition;
                                    layout (location = 1) in vec2 aTextureCoordinates;
                                    out vec2 coordinates;
    
                                    uniform mat4 projection;
                                    uniform mat4 model;

                                    void main() 
                                    {
                                        coordinates = aTextureCoordinates;
                                        gl_Position = projection * model * vec4(aPosition.xy, 0, 1.0);
                                    }";

            string fragmentShader = @"#version 330 core
                                    out vec4 FragColor;
                                    in vec2 coordinates;

                                    uniform sampler2D tex;

                                    void main() 
                                    {
                                        vec4 tex_color = texture(tex, coordinates);
                                        FragColor = tex_color;
                                    }";


            shader = new Shader(vertexShader, fragmentShader);
            shader.Load();

            //Commented out due to atlas being removed in repos
            //In Visual Studio just add a png file called 'test_atlas.png' and you're good to go
            //To actually see something use Actor <name> = new Actor(<position>, <rotation>, <scale>, <material>);
            //Then Hierarchy.loadActor(<actor>);
            //Of course replace things in the <> with the accoring variables/names

            //System.Drawing.Bitmap text = Properties.Resources.test_atlas;
            //text.RotateFlip(System.Drawing.RotateFlipType.RotateNoneFlipY);

            //texture = new PNGTexture(text);
            //texture.Create();

            gameCamera = new Camera2D(WindowManager.windowSize / 2f, 2);
        }
        
        protected override void Update()
        {
            
        }

        protected override void Render()
        {
            GL.glClearColor(0, 0, 0, 0);
            GL.glClear(GL.GL_COLOR_BUFFER_BIT);

            foreach(Actor ac in Hierarchy.loadedActors)
            {
                ac.RenderActor();
            }

            GLFW.Glfw.SwapBuffers(WindowManager.window);
        }

        public static Matrix4x4 GetCameraProjectionMatrix()
        {
            return gameCamera.getProjectionMatrix();
        }
    }
}
