using Bronze.Maths;

namespace Bronze.Graphics
{
    public class Sprite : IDrawable, ITransformable
    {
        public FullRenderEffect InitialRenderEffect { get; set; } = new StandardRenderEffect();
        
        public Model Model { get; }
        
        protected VertexArray VertexArray { get; }

        public Sprite(Model model)
        {
            Model = model;
            VertexArray = new VertexArray(model.Vertices);
        }
        
        public void Draw()
        {
            InitialRenderEffect.Transform = Transform.TransformationMatrix;
            VertexArray.Draw();
        }

        public Transform Transform { get; set; } = new Transform();
    }
}