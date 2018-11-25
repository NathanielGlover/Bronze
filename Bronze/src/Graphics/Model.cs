using System.Collections.Generic;
using System.Linq;
using Bronze.Maths;
using OpenGL;

namespace Bronze.Graphics
{
    public class Model
    {
        public Vertices Vertices { get; }

        public Model(IVertexGenerable vertexGenerable) : this(vertexGenerable, new Transform()) { }

        public Model(Vertices vertices) : this(vertices, new Transform()) { }

        public Model(IVertexGenerable vertexGenerable, Transform initialTransform) :
            this(vertexGenerable.GenerateAroundCentroid(Vector2.Zero), initialTransform) { }

        public Model(Vertices vertices, Transform initialTransform)
        {
            Vertices = initialTransform.ApplyTransform(vertices);

            //Area computed with shoelace formula
            float area = 0;
            var shoelaces = new List<Vector2>();
            shoelaces.AddRange(Vertices);
            shoelaces.Add(Vertices[0]);

            for(int i = 0; i < Vertices.Count; i++)
            {
                var first = shoelaces[i];
                var second = shoelaces[i + 1];
                area += first.X * second.Y - first.Y * second.X;
            }

            Area = area / 2;
        }

        public float Area { get; }
    }
}