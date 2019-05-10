using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Bronze.Maths
{
    public class Vertices : IEnumerable<Vector3>
    {
        public IReadOnlyList<Vector3> VertexData { get; }

        public int Count => VertexData.Count;

        public Vector3 Centroid => 1f / Count * this.Aggregate((result, element) => result + element);

        public Vertices(IEnumerable<Vector3> vertexData) => VertexData = vertexData.ToList();

        public Vertices(IEnumerable<Vector2> vertexData) => VertexData = (from v in vertexData select (Vector3) v).ToList();

        public Vertices ApplyFunction(Func<Vector3, Vector3> function) => new Vertices(from vertex in this select function(vertex));
        
        public Vertices ApplyFunction(Func<Vector2, Vector2> function) => new Vertices(from vertex in this select function((Vector2) vertex));

        public IEnumerator<Vector3> GetEnumerator() => VertexData.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public Vector3 this[int index] => VertexData[index];
    }
}