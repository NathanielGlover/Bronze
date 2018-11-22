using System;
using System.Collections.Generic;
using System.Linq;
using Bronze.Graphics;

// ReSharper disable InconsistentNaming

namespace Bronze.Maths
{
    public class Triangle : Polygon
    {
        public static Triangle FromAngles(float perimeter, float angleAB, float angleBC, float angleAC)
        {
            if(!Math.EqualsWithTolerance(angleAB + angleBC + angleAC, Math.Pi))
                throw new ArgumentException("Triangle angles must sum to Math.Pi radians.");

            float k = perimeter / (Math.Sin(angleAB) + Math.Sin(angleBC) + Math.Sin(angleAC));
            float a = k * Math.Sin(angleBC);
            float b = k * Math.Sin(angleAC);
            float c = k * Math.Sin(angleAB);
            return new Triangle(a, b, c);
        }

        public override float Area { get; }

        public sealed override float Perimeter => SideA + SideB + SideC;

        public float SideA { get; }

        public float SideB { get; }

        public float SideC { get; }

        public float AngleAB { get; }

        public float AngleBC { get; }

        public float AngleAC { get; }

        public sealed override List<float> SideLengths => new List<float> {SideA, SideB, SideC};

        public sealed override List<float> ExteriorAngles => new List<float> {Math.Pi - AngleAB, Math.Pi - AngleBC, Math.Pi - AngleAC};

        public Triangle(float sideA, float sideB, float sideC)
        {
            if(sideA <= 0 || sideB <= 0 || sideC <= 0)
                throw new ArgumentException("Triangle sides must be positive.");

            if(!(sideA + sideB > sideC && sideB + sideC > sideA && sideA + sideC > sideB))
                throw new ArgumentException("Triangle sides must obey triangle inequality (a + b > c).");

            SideA = sideA;
            SideB = sideB;
            SideC = sideC;

            var sortedSideLengths = SideLengths.ToArray();
            Array.Sort(sortedSideLengths);
            float c = sortedSideLengths[0];
            float b = sortedSideLengths[1];
            float a = sortedSideLengths[2];

            //Numerically stable alternative to Heron's formula (requires side lengths to be sorted)
            Area = Math.Sqrt((a + (b + c)) * (c - (a - b)) * (c + (a - b)) * (a + (b - c))) / 4;

            a = SideA;
            b = SideB;
            c = SideC;

            AngleAB = Math.Acos((c * c - a * a - b * b) / (-2 * a * b));
            AngleBC = Math.Acos((a * a - b * b - c * c) / (-2 * b * c));
            AngleAC = Math.Acos((b * b - a * a - c * c) / (-2 * a * c));
        }

        public override Vertices GenerateFromInitial(Vector2 initialVertex, float initialExteriorAngle = 0,
            WindingOrder windingOrder = WindingOrder.CounterClockwise)
        {
            var vertices = base.GenerateFromInitial(initialVertex, initialExteriorAngle, windingOrder);
            return new Vertices(vertices.ToList(), Vertices.DataType.Triangles);
        }

        public override Vertices GenerateAroundCentroid(Vector2 centroid, float vertexAlignmentRay = 0, bool alignAroundRay = true,
            WindingOrder windingOrder = WindingOrder.CounterClockwise)
        {
            var vertices = base.GenerateAroundCentroid(centroid, vertexAlignmentRay, alignAroundRay, windingOrder);
            return new Vertices(vertices.ToList(), Vertices.DataType.Triangles);
        }

        public override int NumVertices => 3;
    }
}