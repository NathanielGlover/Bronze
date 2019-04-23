using System;
using System.Linq;
using Bronze.Maths;

namespace Bronze.Graphics
{
    public class ModelBuilder
    {
        private Vertices Vertices { get; }
        private VertexArrayBuilder VertexArrayBuilder { get; } = new VertexArrayBuilder();
        
        public ModelBuilder(int positionLocation, IVertexGenerable vertexSource)
        {
            Vertices = vertexSource.GenerateAroundCentroid(Vector2.Zero);
            VertexArrayBuilder.AddVertexAttribute(positionLocation, Vertices);
        }

        public ModelBuilder AddAttribute(int location, Func<int, Vector2, float> generator)
        {
            var attributeData = Vertices.Select((t, i) => generator(i, t));
            VertexArrayBuilder.AddVertexAttribute(location, attributeData, 1);
            return this;
        }

        public ModelBuilder AddAttribute(int location, Func<int, Vector2, Vector2> generator)
        {
            var attributeData = Vertices.Select((t, i) => generator(i, t));
            VertexArrayBuilder.AddVertexAttribute(location, attributeData);
            return this;
        }

        public ModelBuilder AddAttribute(int location, Func<int, Vector2, Vector3> generator)
        {
            var attributeData = Vertices.Select((t, i) => generator(i, t));
            VertexArrayBuilder.AddVertexAttribute(location, attributeData);
            return this;
        }

        public ModelBuilder AddAttribute(int location, Func<int, Vector2, Vector4> generator)
        {
            var attributeData = Vertices.Select((t, i) => generator(i, t));
            VertexArrayBuilder.AddVertexAttribute(location, attributeData);
            return this;
        }

        public ModelBuilder AddAttribute(int location, Func<int, Vector2, Color> generator)
        {
            var attributeData = Vertices.Select((t, i) => generator(i, t));
            VertexArrayBuilder.AddVertexAttribute(location, attributeData);
            return this;
        }

        public Model BuildModel(DrawType drawType) => new Model(VertexArrayBuilder.BuildVertexArray(), drawType);
    }
}