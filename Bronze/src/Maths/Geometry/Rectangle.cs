using System.Collections.Generic;
using System.Linq;

namespace Bronze.Maths
{
    public class Rectangle : Polygon
    {
        public override float Area => Size.X * Size.Y;

        public override float Perimeter => 2 * (Size.X + Size.Y);

        public override int NumVertices => 4;
        
        public override List<float> SideLengths { get; }
        
        public override List<float> ExteriorAngles { get; }
        
        public Vector2 Size { get; }

        public Rectangle(float width, float height) : this(new Vector2(width, height))
        {
            
        }

        public Rectangle(Vector2 size)
        {
            Size = size;
            SideLengths = new List<float> {size.Y, size.X, size.Y, size.X};
            ExteriorAngles = Enumerable.Repeat(Math.Pi / 2, 4).ToList();
        }
    }
}