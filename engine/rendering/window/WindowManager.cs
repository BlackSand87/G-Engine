using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

using GLFW;
using GEngine.OpenGL;
using System.Drawing;

namespace GEngine.engine.rendering.window
{
    static class WindowManager
    {

        public static Window window { get; set; }
        public static Vector2 windowSize { get; set; }

        public static void CreateWindow(int width = 1280, int height = 720, string title = "Title", bool focused = true, bool resizeable = false)
        {
            windowSize = new Vector2(width, height);

            //Check if GLFW initialized
            if(!Glfw.Init())
            {
                throw new System.Exception("Error Initializing GLFW");
            }

            //use OpenGL 3.3 core profile
            Glfw.WindowHint(Hint.ContextVersionMajor, 3);
            Glfw.WindowHint(Hint.ContextVersionMinor, 3);
            Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);

            //Additional Parameters
            Glfw.WindowHint(Hint.Focused, focused);

            //Initialize Window
            window = Glfw.CreateWindow(width, height, title, Monitor.None, Window.None);

            if(window == Window.None)
            {
                throw new System.Exception("Error initializing Game Window");
            }

            Rectangle screen = Glfw.PrimaryMonitor.WorkArea;

            int x = (screen.Width - width) / 2;
            int y = (screen.Height - height) / 2;

            Glfw.SetWindowPosition(window, x, y);

            Glfw.MakeContextCurrent(window);

            GL.Import(Glfw.GetProcAddress);

            GL.glViewport(0, 0, width, height);

            Glfw.SwapInterval(0); //No VSync;
        }

        public static void CloseWindow()
        {
            Glfw.Terminate();
        }
    }
}
