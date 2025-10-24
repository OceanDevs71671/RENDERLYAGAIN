using OpenTK.Mathematics;

namespace Renderly.Source.Colors
{
    public class ColorsClass
    {
        // Basic colors
        public static readonly Vector3 Red = new Vector3(1f, 0f, 0f);
        public static readonly Vector3 Green = new Vector3(0f, 1f, 0f);
        public static readonly Vector3 Blue = new Vector3(0f, 0f, 1f);
        public static readonly Vector3 White = new Vector3(1f, 1f, 1f);
        public static readonly Vector3 Black = new Vector3(0f, 0f, 0f);
        public static readonly Vector3 Yellow = new Vector3(1f, 1f, 0f);
        public static readonly Vector3 Cyan = new Vector3(0f, 1f, 1f);
        public static readonly Vector3 Magenta = new Vector3(1f, 0f, 1f);
        public static readonly Vector3 Orange = new Vector3(1f, 0.5f, 0f);
        public static readonly Vector3 Gray = new Vector3(0.5f, 0.5f, 0.5f);

        // Example of RGBA color if you need transparency
        public static readonly Vector4 TransparentRed = new Vector4(1f, 0f, 0f, 0.5f);
        public static readonly Vector4 TransparentBlue = new Vector4(0f, 0f, 1f, 0.5f);
    }
}
