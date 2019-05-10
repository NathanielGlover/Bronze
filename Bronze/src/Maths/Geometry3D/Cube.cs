using System.Collections.Generic;
using Bronze.Graphics;

namespace Bronze.Maths.Geometry3D
{
    public class Cube : Solid
    {
        public float EdgeLength { get; }

        public Cube(float edgeLength)
        {
            EdgeLength = edgeLength;
        }

        public override Vertices GenerateFromInitial(Vector2 initialVertex, float initialExteriorAngle = 0,
            WindingOrder windingOrder = WindingOrder.CounterClockwise)
        {
            return GenerateAroundCentroid(Vector2.Zero);
        }

        public override Vertices GenerateAroundCentroid(Vector2 centroid, float vertexAlignmentRay = 0,
            bool alignAroundRay = true, WindingOrder windingOrder = WindingOrder.CounterClockwise)
        {
            float e = EdgeLength / 2;
            return new Vertices(new[]
            {
                // front
                new Vector3(-e, -e,  e),
                new Vector3(e, -e,  e),
                new Vector3(e,  e,  e),
                new Vector3(-e,  e,  e),
                // back
                new Vector3(-e, -e,  -e),
                new Vector3(e, -e,  -e),
                new Vector3(e,  e,  -e), 
                new Vector3(-e,  e,  -e)
            });
        }

        public override IEnumerable<uint> GetElementIndices() => new uint[]
        {
            // front
            0, 1, 2,
            2, 3, 0,
            // right
            1, 5, 6,
            6, 2, 1,
            // back
            7, 6, 5,
            5, 4, 7,
            // left
            4, 0, 3,
            3, 7, 4,
            // bottom
            4, 5, 1,
            1, 0, 4,
            // top
            3, 2, 6,
            6, 7, 3
        };

        public override DrawType GetPreferredDrawType() => DrawType.Triangles;

        public override float Volume => Math.Pow(EdgeLength, 3);

        public override float SurfaceArea => 6 * Math.Pow(EdgeLength, 2);
    }
}