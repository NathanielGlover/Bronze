using Bronze.Maths;

namespace Bronze.Graphics
{
    public struct Color
    {
        public static Color Zero = new Color(0, 0, 0, 0);
        public static Color White = new Color(1, 1, 1, 1);
        public static Color Black = new Color(0, 0, 0, 1);

        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public byte A { get; set; }

        public float Red => (float) R / 255;
        public float Green => (float) G / 255;
        public float Blue => (float) B / 255;
        public float Alpha => (float) A / 255;

        public byte[] Values => new[] {R, G, B, A};

        public Vector4 ToVector() => new Vector4((float) R / 255, (float) G / 255, (float) B / 255, (float) A / 255);

        public Color(double r, double g, double b, double a)
        {
            R = (byte) (r * 255);
            G = (byte) (g * 255);
            B = (byte) (b * 255);
            A = (byte) (a * 255);
        }

        public Color(float r, float g, float b, float a) : this((double) r, g, b, a) { }

        public static Color operator +(Color left, Color right) => new Color(left.R + right.R, left.G + right.G, left.B + right.B, left.A + right.A);

        public static Color operator -(Color left, Color right) => new Color(left.R - right.R, left.G - right.G, left.B - right.B, left.A - right.A);

        public static Color operator *(Color left, Color right) => new Color(left.R * right.R, left.G * right.G, left.B * right.B, left.A * right.A);
    }
}