using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using Renderly.Source.Colors;

namespace Renderly.Source.BasicRenders
{
    public class BasicRenderer
    {
        private GameWindow window;
        private Vector3 clearColor; // initial color set on window creation

        public void CreateWindow(int windowWidth, int windowHeight, string windowName, Vector3 startColor)
        {
            clearColor = startColor; // set initial color

            var nativeSettings = new NativeWindowSettings()
            {
                Size = new Vector2i(windowWidth, windowHeight),
                Title = windowName
            };

            window = new GameWindow(GameWindowSettings.Default, nativeSettings);

            window.Load += () =>
            {
                GL.ClearColor(clearColor.X, clearColor.Y, clearColor.Z, 1.0f);
            };

            window.RenderFrame += (FrameEventArgs e) =>
            {
                GL.ClearColor(clearColor.X, clearColor.Y, clearColor.Z, 1.0f);
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                window.SwapBuffers();
            };

            window.Run(); // start the loop (blocks here)
        }
    }
}
