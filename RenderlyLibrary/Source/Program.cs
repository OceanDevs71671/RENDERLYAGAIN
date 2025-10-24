using Renderly.Source.BasicRenders;
using Renderly.Source.Colors;

namespace Renderly.Source.Program
{
    public class ProgramClass
    {
        public string WindowName = "Window Name";

        public BasicRenderer Renderer = new BasicRenderer();
        public ColorsClass Colors = new ColorsClass();


        public static void Main(string[] args)
        {
            ProgramClass program = new ProgramClass();
            program.RunRenderly();
        }


        //Write your code in here, DONT touch the Render Loop unless you understand the code.
        public void RunRenderly()
        {
            Renderer.CreateWindow(800, 800, WindowName, ColorsClass.Red);
        }

    }
}