namespace Bronze.Graphics
{
    public struct Color
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public byte A { get; set; }

        public byte[] Values => new[] {R, G, B, A};

        public Color(byte r, byte g, byte b, byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public Color(float r, float g, float b, float a)
        {
            R = (byte) (r * 255);
            G = (byte) (g * 255);
            B = (byte) (b * 255);
            A = (byte) (a * 255);
        }
    }
}