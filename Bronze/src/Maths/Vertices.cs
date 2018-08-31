using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Bronze.Maths
{
    public class Vertices : IReadOnlyList<Vector2>
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

        public int Count => VertexData.Count;

        public Vertices(IEnumerable<Vector2> vertexData, DataType vertexDataType)
        {
            VertexData = vertexData.ToList();
            VertexDataType = vertexDataType;
        }

        public Vertices ApplyFunction(Func<Vector2, Vector2> function) => new Vertices(from vertex in this select function(vertex), VertexDataType);
        
        public IEnumerator<Vector2> GetEnumerator() => VertexData.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public Vector2 this[int index] => VertexData[index];
    }
}