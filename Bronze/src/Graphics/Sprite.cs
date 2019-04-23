using Bronze.Maths;

namespace Bronze.Graphics
{
    public class Sprite : IDrawable, ITransformable
    {
        public Texture Texture { get; set; }
        protected Model Model { get; }

        public Sprite(Texture texture, Model model)
        {
            Texture = texture;
            Model = model;
        }

        public void Draw(FullRenderEffect renderEffect)
        {
            renderEffect.Model = Transform.TransformationMatrix;
            Texture.Bind();
            Model.Draw(renderEffect);
        }

        public Transform Transform { get; set; } = new Transform();
    }
}