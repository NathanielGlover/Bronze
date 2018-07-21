using System.Collections.Generic;
using Bronze.Math;

namespace Bronze.Graphics
{
    public class Vertices : ITransformable
    {
        public enum DataType
        {
            Points,
            Lines,
            LineStrip,
            LineLoop,
            Triangles,
            TriangleStrip,
            TriangleFan
        }

        private readonly List<Vector2> vertexData;

        public IReadOnlyList<Vector2> VertexData => Transform.ApplyTransform(vertexData);

        public DataType VertexDataType { get; }

        public Vertices(List<Vector2> vertexData, DataType vertexDataType)
        {
            this.vertexData = vertexData;
            VertexDataType = vertexDataType;
        }

        public Transform Transform { get; set; } = new Transform();
    }
}