using OpenTK.Mathematics;
using Renderly.Source.Colors;
using Renderly.Source.Graphics.Renderer;
using System.Collections.Generic;

namespace Renderly.Source.Examples.ColorsDisplay
{
    public class ColorsDisplay
    {
        public void DisplayColors(BasicRenderer renderer)
        {
            // List of all color names and values from ColorsClass
            var colors = new Dictionary<string, Vector3>()
            {
                { "Red", ColorsClass.Red },
                { "Green", ColorsClass.Green },
                { "Blue", ColorsClass.Blue },
                { "White", ColorsClass.White },
                { "Black", ColorsClass.Black },
                { "Yellow", ColorsClass.Yellow },
                { "Cyan", ColorsClass.Cyan },
                { "Magenta", ColorsClass.Magenta },
                { "Orange", ColorsClass.Orange },
                { "Gray", ColorsClass.Gray }
            };

            // Draw a button for each color in a grid layout
            float startX = -0.9f;
            float startY = 0.8f;
            float buttonWidth = 0.35f;
            float buttonHeight = 0.15f;
            float x = startX;
            float y = startY;

            int count = 0;
            foreach (var kvp in colors)
            {
                // Draw each color as a button
                renderer.CreateButton(new Vector2(x, y), new Vector2(buttonWidth, buttonHeight), kvp.Value);

                // Move to next position
                x += buttonWidth + 0.05f;
                count++;

                // Move down to next row after 3 buttons
                if (count % 3 == 0)
                {
                    x = startX;
                    y -= buttonHeight + 0.05f;
                }
            }
        }
    }
}
