using Bronze.Maths;

namespace Bronze.Graphics
{
    public class Sprite : IDrawable, ITransformable
    {
        public Texture Texture { get; set; }
        protected VertexArray VertexArray { get; }

        public Sprite(Texture texture, VertexArray vertexArray)
        {
            Texture = texture;
            VertexArray = vertexArray;
        }

        public void Draw(FullRenderEffect renderEffect)
        {
            renderEffect.Model = Transform.TransformationMatrix;
            Texture.Bind();
            VertexArray.Draw();
        }

        public Transform Transform { get; set; } = new Transform();
    }
}