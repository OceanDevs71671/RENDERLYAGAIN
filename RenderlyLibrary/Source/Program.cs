using OpenTK.Compute.OpenCL;
using OpenTK.Mathematics;
using Renderly.Source.Colors;
using Renderly.Source.Console;
using Renderly.Source.Examples.ColorsDisplay;
using Renderly.Source.Graphics.Renderer;
using System.Threading;

namespace Renderly.Source.Program
{
    public class ProgramClass
    {
        public string WindowName = "Renderly's Template Window";
        public BasicRenderer Renderer = new BasicRenderer();
        public ConsoleClass consoleClass = new ConsoleClass();
        ColorsDisplay colorsDisplay = new ColorsDisplay();

        public static void Main(string[] args)
        {
            ProgramClass program = new ProgramClass();
            //Runs the MAIN Renderly
            program.RunRenderly();

            //Runs the ColorsDisplay Example (NOT WORKING)
            //program.ColorsExample();
        }


        //MAKE SURE TO: 1. Draw all your voids. 2. Create the window. The window doesn't listen for drawing.
        public void RunRenderly()
        {
            
            //Drawing a button.
            Renderer.CreateButton(new Vector2(0.0f, 0.0f), new Vector2(0.5f, 0.2f), ColorsClass.Blue);

            //Changes the window's color.
            Renderer.SetWindowColor(ColorsClass.White);
            //Draws the window.
            Renderer.CreateWindow(1080, 1080, WindowName);

            //Draws the console (NOT WORKING)
            consoleClass.DrawConsole();
        }

        public void ColorsExample()
        {
            // 1. Set up the window color
            Renderer.SetWindowColor(ColorsClass.White);

            // 2. Create the window BEFORE drawing anything
            Renderer.CreateWindow(1080, 1080, "Renderly Color Display Example");

            // 3. THEN display the color buttons
            colorsDisplay.DisplayColors(Renderer);
        }
    }
}
