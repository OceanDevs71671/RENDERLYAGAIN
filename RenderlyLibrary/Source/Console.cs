using Renderly.Source.Graphics.Renderer;

namespace Renderly.Source.Console
{
    public class ConsoleClass
    {
        public BasicRenderer Renderer = new BasicRenderer();

        public void DrawConsole()
        {
            Renderer.SetWindowColor(Colors.ColorsClass.Black);

            Renderer.CreateWindow(800, 800, "Console");
        }
    }
}
