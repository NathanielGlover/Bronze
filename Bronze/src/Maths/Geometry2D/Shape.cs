using System.Collections.Generic;
using Bronze.Graphics;

namespace Bronze.Maths
{
    public abstract class Shape : IVertexGenerable
    {
        public abstract float Area { get; }

        public abstract float Perimeter { get; }

        public abstract Vertices GenerateFromInitial(Vector2 initialVertex, float initialExteriorAngle = 0,
            WindingOrder windingOrder = WindingOrder.CounterClockwise);

        public abstract Vertices GenerateAroundCentroid(Vector2 centroid, float vertexAlignmentRay = 0,
            bool alignAroundRay = true, WindingOrder windingOrder = WindingOrder.CounterClockwise);

        public abstract IEnumerable<uint> GetElementIndices();

        public abstract DrawType GetPreferredDrawType();
    }
}