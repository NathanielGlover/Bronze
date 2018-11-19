using System.Collections.Generic;
using System.Linq;

namespace Bronze.Maths
{
    public class Rectangle : Polygon
    {
        public override float Area => Width * Height;

        public override float Perimeter => 2 * (Width + Height);

        public override int NumVertices => 4;
        
        public override List<float> SideLengths { get; }
        
        public override List<float> ExteriorAngles { get; }
        
        public float Width { get; }
        
        public float Height { get; }

        public Rectangle(float width, float height)
        {
            Width = width;
            Height = height;
            SideLengths = new List<float> {Height, Width, Height, Width};
            ExteriorAngles = Enumerable.Repeat(Math.Pi / 2, 4).ToList();
        }
    }
}