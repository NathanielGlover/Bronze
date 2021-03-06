using System.Collections.Generic;
using Bronze.Graphics;

namespace Bronze.Maths
{
    public enum WindingOrder
    {
        Clockwise,
        CounterClockwise
    }

    public interface IVertexGenerable
    {
        Vertices GenerateFromInitial(Vector2 initialVertex, float initialExteriorAngle = 0, WindingOrder windingOrder = WindingOrder.CounterClockwise);

        Vertices GenerateAroundCentroid(Vector2 centroid, float vertexAlignmentRay = 0, bool alignAroundRay = true,
            WindingOrder windingOrder = WindingOrder.CounterClockwise);

        IEnumerable<uint> GetElementIndices();

        DrawType GetPreferredDrawType();
    }
}