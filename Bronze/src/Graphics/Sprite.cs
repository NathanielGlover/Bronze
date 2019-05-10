using Bronze.Maths;

namespace Bronze.Graphics
{
    public class Sprite : IDrawable, ITransformable, IColorizeable
    {
        public Color ColorMultiplier { get; set; } = Color.White;
        public Color ColorAddition { get; set; } = Color.Zero;
        public Color ColorSubtraction { get; set; } = Color.Zero;
        public Transform Transform { get; set; } = Transform.Identity;
        public Projection Projection { get; set; } = Projection.None;
        public Viewpoint View { get; set; } = Viewpoint.Default;

        public Texture Texture { get; set; }
        protected Model Model { get; set; }

        internal Sprite() { }

        public Sprite(Model model, Texture texture)
        {
            Texture = texture;
            Model = model;
        }

        public virtual void Draw(ShaderPipeline pipeline)
        {
            Texture.Bind();

            pipeline.VertexShader.SetUniform(ReservedUniforms.Transform, Projection.ProjectionMatrix * View.ViewMatrix * Transform.TransformationMatrix);
            pipeline.FragmentShader.SetUniform(ReservedUniforms.ColorMultiplier, ColorMultiplier);
            pipeline.FragmentShader.SetUniform(ReservedUniforms.ColorAddition, ColorAddition);
            pipeline.FragmentShader.SetUniform(ReservedUniforms.ColorSubtraction, ColorSubtraction);

            Model.Draw();
        }
    }
}