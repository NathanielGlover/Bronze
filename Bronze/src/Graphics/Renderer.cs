using Bronze.Input;
using Bronze.Maths;
using OpenGL;

namespace Bronze.Graphics
{
    public class Renderer
    {
        private const string DefaultVertexSource =
            "#version 410 core\n" +
            "layout (location = 0) in vec3 position;\n" +
            "layout (location = 1) in vec2 texCoord;\n" +
            "\n" +
            "uniform mat4 br_Transform;\n" +
            "\n" +
            "out vec2 TexCoord;\n" +
            "out vec4 gl_Position;\n" +
            "\n" +
            "void main() \n" +
            "{\n" +
            "    gl_Position = br_Transform * vec4(position, 1.0f);\n" +
            "    TexCoord = texCoord;\n" +
            "}";

        private const string DefaultFragmentSource =
            "#version 410 core\n" +
            "in vec2 TexCoord;\n" +
            "\n" +
            "out vec4 FragColor;\n" +
            "\n" +
            "uniform sampler2D br_Texture0;\n" +
            "uniform vec4 br_ColorMultiplier;\n" +
            "uniform vec4 br_ColorAddition;\n" +
            "\n" +
            "void main() {\n" +
            "    FragColor = texture(br_Texture0, vec2(TexCoord.x, -TexCoord.y)) * br_ColorMultiplier + br_ColorAddition;\n" +
            "}";

        public Renderer(Window renderTarget, ShaderPipeline pipeline)
        {
            RenderTarget = renderTarget;
            Pipeline = pipeline;
        }

        public Renderer(Window renderTarget) : this(renderTarget, new ShaderBuilder()
            .AddStage(ShaderStage.FromSource(DefaultVertexSource, ShaderStage.StageType.Vertex))
            .AddStage(ShaderStage.FromSource(DefaultFragmentSource, ShaderStage.StageType.Fragment)).BuildPipeline()) { }

        private ShaderPipeline pipeline;

        public Window RenderTarget { get; }

        public ShaderPipeline Pipeline
        {
            get => pipeline;

            set
            {
                pipeline = value;
                value.Bind();
            }
        }

        public void Render(IDrawable drawable)
        {
            ContextManager.RunInSeparateContext(() =>
            {
                drawable.Draw(Pipeline);
            }, RenderTarget.Handle);
        }
    }
}