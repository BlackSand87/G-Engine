using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using GEngine.engine.rendering.window;

namespace GEngine.engine.rendering.camera
{
    class Camera2D
    {
        public Vector2 FocusPosition { get; set; }
        public float Zoom { get; set; }

        public Camera2D(Vector2 focusPosition, float zoom)
        {
            FocusPosition = focusPosition;
            Zoom = zoom;
        }

        public Matrix4x4 getProjectionMatrix()
        {
            float left = FocusPosition.X - WindowManager.windowSize.X / 2;
            float right = FocusPosition.X + WindowManager.windowSize.X / 2;
            float top = FocusPosition.Y - WindowManager.windowSize.Y / 2;
            float bottom = FocusPosition.X + WindowManager.windowSize.Y / 2;

            Matrix4x4 orthoMatrix = Matrix4x4.CreateOrthographicOffCenter(left, right, top, bottom, 0.01f, 100f);
            Matrix4x4 zoomMatrix = Matrix4x4.CreateScale(Zoom);

            return orthoMatrix * zoomMatrix;
        }
    }
}
