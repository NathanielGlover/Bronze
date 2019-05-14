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

        public Sprite(Shape shape, Texture texture)
        {
            int count = shape.GenerateAroundCentroid(Vector2.Zero).Count;
            Texture = texture;

            Model = new ModelBuilder(shape)
                .AddTextureCoordinates((i, vertex) =>
                {
                    if(count == 4)
                    {
                        var texCoords = new[]
                        {
                            new Vector2(0, 0),
                            new Vector2(0, 1),
                            new Vector2(1, 1),
                            new Vector2(1, 0)
                        };

                        return texCoords[i];
                    }

                    return (Vector2) vertex * (1f / Math.Sqrt(shape.Area)) + new Vector2(0.5f, 0.5f);
                })
                .BuildModel();
        }

        public Sprite(Model model, Texture texture)
        {
            Texture = texture;
            Model = model;
        }

        public virtual void Draw(ShaderPipeline pipeline, params Effect[] effects)
        {
            foreach(var effect in effects) effect.ApplyEffect(pipeline);
            
            pipeline.VertexShader.SetUniform(ReservedUniforms.Transform, Projection.ProjectionMatrix * View.ViewMatrix * Transform.TransformationMatrix);
            pipeline.FragmentShader.SetUniform(ReservedUniforms.ColorMultiplier, ColorMultiplier);
            pipeline.FragmentShader.SetUniform(ReservedUniforms.ColorAddition, ColorAddition);
            pipeline.FragmentShader.SetUniform(ReservedUniforms.ColorSubtraction, ColorSubtraction);
            Texture.Bind();
            Model.Draw();
            
            foreach(var effect in effects) effect.SetDefaults(pipeline);
        }
    }
}