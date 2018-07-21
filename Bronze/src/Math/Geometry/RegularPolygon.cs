using System.Collections.Generic;
using System.Linq;

namespace Bronze.Math
{
    public class RegularPolygon : Polygon
    {
        public static RegularPolygon FromRadius(int numVertices, float radius)
        {
            float interiorAngle = Maths.Pi * (numVertices - 2) / numVertices;
            float sideLength = 2 * radius * Maths.Cos(interiorAngle / 2);
            return new RegularPolygon(numVertices, sideLength);
        }

        public static RegularPolygon FromApothem(int numVertices, float apothem)
        {
            float interiorAngle = Maths.Pi * (numVertices - 2) / numVertices;
            float sideLength = 2 * apothem / Maths.Tan(interiorAngle / 2);
            return new RegularPolygon(numVertices, sideLength);
        }

        public static RegularPolygon FromArea(int numVertices, float area)
        {
            float interiorAngle = Maths.Pi * (numVertices - 2) / numVertices;
            float sideLength = Maths.Sqrt(4 * area / (numVertices * Maths.Tan(interiorAngle / 2)));
            return new RegularPolygon(numVertices, sideLength);
        }

        public RegularPolygon(int numVertices, float sideLength)
        {
            NumVertices = numVertices;
            SideLength = sideLength;
            Area = numVertices * Maths.Pow(sideLength, 2) * Maths.Tan(InteriorAngle / 2) / 4;
        }

        public override int NumVertices { get; }

        public override List<float> SideLengths => Enumerable.Repeat(SideLength, NumVertices).ToList();

        public override List<float> ExteriorAngles => Enumerable.Repeat(ExteriorAngle, NumVertices).ToList();

        public float SideLength { get; }

        public float InteriorAngle => Maths.Pi * (NumVertices - 2) / NumVertices;

        public float ExteriorAngle => Maths.Pi - InteriorAngle;

        public float Radius => SideLength / (2 * Maths.Cos(InteriorAngle / 2));

        public float Apothem => SideLength * Maths.Tan(InteriorAngle / 2) / 2;

        public override float Area { get; }

        public override float Perimeter => NumVertices * SideLength;
    }
}