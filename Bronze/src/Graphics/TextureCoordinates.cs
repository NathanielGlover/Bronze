using System.Collections;
using System.Collections.Generic;
using Bronze.Maths;

namespace Bronze.Graphics
{
    public class TextureCoordinates : IReadOnlyList<Vector2>
    {
        public IReadOnlyList<Vector2> Data { get; }

        public int Count => Data.Count;

        public TextureCoordinates(IReadOnlyList<Vector2> data) => Data = data;

        public TextureCoordinates(Vertices vertices) : this(RegularPolygon.FromApothem(vertices.Count, 0.5f)
            .GenerateAroundCentroid(new Vector2(0.5f, 0.5f)).VertexData) { }

        public IEnumerator<Vector2> GetEnumerator() => Data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public Vector2 this[int index] => Data[index];
    }
}