using System;
using System.Collections.Generic;
using System.Text;

using GEngine.engine.rendering.window;

using GLFW;

namespace GEngine.game
{
    abstract class Game
    {
        protected int InitialWidth { get; set; }
        protected int InitialHeight { get; set; }

        protected string InitialTitle { get; set; }

        protected Game(int initialWidth, int initialHeight, string initialTitle)
        {
            InitialWidth = initialWidth;
            InitialHeight = initialHeight;
            InitialTitle = initialTitle;
        }

        public void Run()
        {
            Initialize();

            WindowManager.CreateWindow(width: InitialWidth, height: InitialHeight, title: InitialTitle);

            LoadContent();

            while(!Glfw.WindowShouldClose(WindowManager.window))
            {
                Time.DeltaTime = (float)Glfw.Time - Time.TotalElapsedSeconds;
                Time.TotalElapsedSeconds = (float)Glfw.Time;

                Update();

                Glfw.PollEvents();

                Render();
            }

            WindowManager.CloseWindow();
        }

        /// <summary>
        /// Has no OpenGl set up
        /// Used for loading settings etc.
        /// Not textures. Thats what LoadContent() is for
        /// </summary>
        protected abstract void Initialize();
        protected abstract void LoadContent();

        protected abstract void Update();
        protected abstract void Render();
    }
}
