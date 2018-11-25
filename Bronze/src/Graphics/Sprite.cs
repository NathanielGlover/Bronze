using Bronze.Maths;

namespace Bronze.Graphics
{
    public class Sprite : IDrawable, ITransformable
    {
        public Model Model { get; }
        public Texture Texture { get; set; }
        protected VertexArray VertexArray { get; }

        public Sprite(Model model, Texture texture)
        {
            Model = model;
            Texture = texture;
            VertexArray = new VertexArray(model.Vertices);
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