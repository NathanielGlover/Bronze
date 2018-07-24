namespace Bronze.Math
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
    }
}