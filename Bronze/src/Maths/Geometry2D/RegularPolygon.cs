using System.Collections.Generic;
using System.Linq;
using Bronze.Graphics;

namespace Bronze.Maths
{
    public class RegularPolygon : Polygon
    {
        public static RegularPolygon FromRadius(int numVertices, float radius)
        {
            float interiorAngle = Math.Pi * (numVertices - 2) / numVertices;
            float sideLength = 2 * radius * Math.Cos(interiorAngle / 2);
            return new RegularPolygon(numVertices, sideLength);
        }

        public static RegularPolygon FromApothem(int numVertices, float apothem)
        {
            float interiorAngle = Math.Pi * (numVertices - 2) / numVertices;
            float sideLength = 2 * apothem / Math.Tan(interiorAngle / 2);
            return new RegularPolygon(numVertices, sideLength);
        }

        public static RegularPolygon FromArea(int numVertices, float area)
        {
            float interiorAngle = Math.Pi * (numVertices - 2) / numVertices;
            float sideLength = Math.Sqrt(4 * area / (numVertices * Math.Tan(interiorAngle / 2)));
            return new RegularPolygon(numVertices, sideLength);
        }

        public RegularPolygon(int numVertices, float sideLength)
        {
            NumVertices = numVertices;
            SideLength = sideLength;
            Area = numVertices * Math.Pow(sideLength, 2) * Math.Tan(InteriorAngle / 2) / 4;
        }

        public override int NumVertices { get; }

        public override List<float> SideLengths => Enumerable.Repeat(SideLength, NumVertices).ToList();

        public override List<float> ExteriorAngles => Enumerable.Repeat(ExteriorAngle, NumVertices).ToList();

        public float SideLength { get; }

        public float InteriorAngle => Math.Pi * (NumVertices - 2) / NumVertices;

        public float ExteriorAngle => Math.Pi - InteriorAngle;

        public float Radius => SideLength / (2 * Math.Cos(InteriorAngle / 2));

        public float Apothem => SideLength * Math.Tan(InteriorAngle / 2) / 2;

        public override float Area { get; }

        public override float Perimeter => NumVertices * SideLength;
        
        public override IEnumerable<uint> GetElementIndices() => new uint[0];
        
        public override DrawType GetPreferredDrawType() => DrawType.TriangleFan;
    }
}