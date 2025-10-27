using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using System.Collections.Generic;
using Renderly.Source.Colors;

namespace Renderly.Source.Graphics.Renderer
{
    public class BasicRenderer
    {
        private GameWindow window;
        private Vector3 clearColor = ColorsClass.Black;
        private readonly object colorLock = new object();

        // For buttons
        private List<Button> buttons = new List<Button>();

        // OpenGL resources
        private int vao, vbo;
        private Shader shader;

        private struct Button
        {
            public Vector2 Position; // Center position, range -1..1
            public Vector2 Size;     // Width and height
            public Vector3 Color;    // RGB color
        }

        public void CreateWindow(int windowWidth, int windowHeight, string windowName)
        {

            var nativeSettings = new NativeWindowSettings()
            {
                Size = new Vector2i(windowWidth, windowHeight),
                Title = windowName
            };

            window = new GameWindow(GameWindowSettings.Default, nativeSettings);

            window.Load += () =>
            {
                GL.ClearColor(clearColor.X, clearColor.Y, clearColor.Z, 1.0f);
                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

                // Initialize shader
                shader = new Shader(VertexShaderSource, FragmentShaderSource);

                // Setup a unit square VAO/VBO
                float[] vertices = new float[]
                {
                    // Two triangles for a square
                    -0.5f, -0.5f,
                     0.5f, -0.5f,
                     0.5f,  0.5f,

                    -0.5f, -0.5f,
                     0.5f,  0.5f,
                    -0.5f,  0.5f
                };

                vao = GL.GenVertexArray();
                vbo = GL.GenBuffer();

                GL.BindVertexArray(vao);
                GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
                GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

                GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
                GL.EnableVertexAttribArray(0);
                GL.BindVertexArray(0);
            };

            window.RenderFrame += (FrameEventArgs e) =>
            {
                // Thread-safe clear color
                Vector3 color;
                lock (colorLock)
                    color = clearColor;

                GL.ClearColor(color.X, color.Y, color.Z, 1.0f);
                GL.Clear(ClearBufferMask.ColorBufferBit);

                // Draw buttons
                foreach (var btn in buttons)
                {
                    DrawButton(btn);
                }

                window.SwapBuffers();
            };

            window.Run();
        }

        public void SetWindowColor(Vector3 newColor)
        {
                clearColor = newColor;
        }

        public void CreateButton(Vector2 position, Vector2 size, Vector3 color)
        {
            buttons.Add(new Button
            {
                Position = position,
                Size = size,
                Color = color
            });
        }

        private void DrawButton(Button btn)
        {
            shader.Use();
            shader.SetVector2("uPosition", btn.Position);
            shader.SetVector2("uSize", btn.Size);
            shader.SetVector3("uColor", btn.Color);

            GL.BindVertexArray(vao);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
            GL.BindVertexArray(0);
        }

        // Simple shader sources
        private const string VertexShaderSource = @"
#version 330 core
layout(location = 0) in vec2 aPosition;

uniform vec2 uPosition;
uniform vec2 uSize;

void main()
{
    vec2 pos = aPosition * uSize + uPosition;
    gl_Position = vec4(pos, 0.0, 1.0);
}";

        private const string FragmentShaderSource = @"
#version 330 core
uniform vec3 uColor;
out vec4 FragColor;

void main()
{
    FragColor = vec4(uColor, 1.0);
}";
    }

    // Minimal shader helper class
    public class Shader
    {
        public int Handle;

        public Shader(string vertexSource, string fragmentSource)
        {
            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertexSource);
            GL.CompileShader(vertexShader);
            CheckShader(vertexShader);

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragmentSource);
            GL.CompileShader(fragmentShader);
            CheckShader(fragmentShader);

            Handle = GL.CreateProgram();
            GL.AttachShader(Handle, vertexShader);
            GL.AttachShader(Handle, fragmentShader);
            GL.LinkProgram(Handle);
            GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out int success);
            if (success == 0)
            {
                string info = GL.GetProgramInfoLog(Handle);
                throw new System.Exception($"Program linking failed: {info}");
            }

            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
        }

        public void Use() => GL.UseProgram(Handle);

        public void SetVector2(string name, Vector2 value)
        {
            int location = GL.GetUniformLocation(Handle, name);
            GL.Uniform2(location, value);
        }

        public void SetVector3(string name, Vector3 value)
        {
            int location = GL.GetUniformLocation(Handle, name);
            GL.Uniform3(location, value);
        }

        private void CheckShader(int shader)
        {
            GL.GetShader(shader, ShaderParameter.CompileStatus, out int success);
            if (success == 0)
            {
                string info = GL.GetShaderInfoLog(shader);
                throw new System.Exception($"Shader compilation failed: {info}");
            }
        }
    }
}
