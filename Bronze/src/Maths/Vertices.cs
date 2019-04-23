using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Bronze.Maths
{
    public class Vertices : IEnumerable<Vector2>
    {
        public IReadOnlyList<Vector2> VertexData { get; }

        public int Count => VertexData.Count;

        public Vector2 Centroid => 1f / Count * this.Aggregate((result, element) => result + element);

        public Vertices(IEnumerable<Vector2> vertexData) => VertexData = vertexData.ToList();

        public Vertices ApplyFunction(Func<Vector2, Vector2> function) => new Vertices(from vertex in this select function(vertex));

        public IEnumerator<Vector2> GetEnumerator() => VertexData.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public Vector2 this[int index] => VertexData[index];
    }
}