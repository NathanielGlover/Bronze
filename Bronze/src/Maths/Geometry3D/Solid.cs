using System.Collections.Generic;
using Bronze.Graphics;

namespace Bronze.Maths.Geometry3D
{
    public abstract class Solid : IVertexGenerable
    {
        public abstract Vertices GenerateFromInitial(Vector2 initialVertex, float initialExteriorAngle = 0, 
            WindingOrder windingOrder = WindingOrder.CounterClockwise);

        public abstract Vertices GenerateAroundCentroid(Vector2 centroid, float vertexAlignmentRay = 0, 
            bool alignAroundRay = true, WindingOrder windingOrder = WindingOrder.CounterClockwise);

        public abstract IEnumerable<uint> GetElementIndices();

        public abstract DrawType GetPreferredDrawType();

        public abstract float Volume { get; }
        
        public abstract float SurfaceArea { get; }
    }
}