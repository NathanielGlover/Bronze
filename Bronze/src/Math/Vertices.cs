using System.Collections.Generic;
using Bronze.Graphics;

namespace Bronze.Math
{
    public class Vertices
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

        public IReadOnlyList<Vector2> VertexData { get; }

        public DataType VertexDataType { get; }

        public Vertices(List<Vector2> vertexData, DataType vertexDataType)
        {
            VertexData = vertexData;
            VertexDataType = vertexDataType;
        }
    }
}