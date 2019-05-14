using System;
using System.Collections.Generic;
using System.Linq;
using Bronze.Maths;

namespace Bronze.Graphics
{
    public class ModelBuilder
    {
        public ModelBuilder(IVertexGenerable vertexSource)
        {
            Vertices = vertexSource.GenerateAroundCentroid(Vector2.Zero);
            Indices = vertexSource.GetElementIndices();
            PreferredDrawType = vertexSource.GetPreferredDrawType();
            
            VertexArrayBuilder.AddVertexAttribute(ReservedAttributes.Position, Vertices);
        }

        private Vertices Vertices { get; }
        private IEnumerable<uint> Indices { get; }
        private DrawType PreferredDrawType { get; }
        
        private VertexArrayBuilder VertexArrayBuilder { get; } = new VertexArrayBuilder();

        public ModelBuilder AddTextureCoordinates(Func<int, Vector3, Vector2> generator) => AddAttribute(ReservedAttributes.TextureCoordinate, generator);
        
        public ModelBuilder AddNormals(Func<int, Vector3, Vector2> generator) => AddAttribute(ReservedAttributes.Normal, generator);
        
        public ModelBuilder AddColors(Func<int, Vector3, Color> generator) => AddAttribute(ReservedAttributes.Color, generator);

        public ModelBuilder AddAttribute(int location, Func<int, Vector3, float> generator)
        {
            var attributeData = Vertices.Select((t, i) => generator(i, (Vector2) t));
            VertexArrayBuilder.AddVertexAttribute(location, attributeData, 1);
            return this;
        }

        public ModelBuilder AddAttribute(int location, Func<int, Vector3, Vector2> generator)
        {
            var attributeData = Vertices.Select((t, i) => generator(i, (Vector2) t));
            VertexArrayBuilder.AddVertexAttribute(location, attributeData);
            return this;
        }

        public ModelBuilder AddAttribute(int location, Func<int, Vector3, Vector3> generator)
        {
            var attributeData = Vertices.Select((t, i) => generator(i, (Vector2) t));
            VertexArrayBuilder.AddVertexAttribute(location, attributeData);
            return this;
        }

        public ModelBuilder AddAttribute(int location, Func<int, Vector3, Vector4> generator)
        {
            var attributeData = Vertices.Select((t, i) => generator(i, (Vector2) t));
            VertexArrayBuilder.AddVertexAttribute(location, attributeData);
            return this;
        }

        public ModelBuilder AddAttribute(int location, Func<int, Vector3, Color> generator)
        {
            var attributeData = Vertices.Select((t, i) => generator(i, (Vector2) t));
            VertexArrayBuilder.AddVertexAttribute(location, attributeData);
            return this;
        }

        public Model BuildModel() => BuildModel(PreferredDrawType);
        public Model BuildModel(DrawType drawType) => new Model(VertexArrayBuilder.BuildVertexArray(Indices), drawType);
    }
}