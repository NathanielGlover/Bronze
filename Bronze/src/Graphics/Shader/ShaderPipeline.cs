using OpenGL;

namespace Bronze.Graphics
{
    public class ShaderPipeline : GraphicsResource
    {
        private SingularShader vertexShader;
        private SingularShader geometryShader;
        private SingularShader fragmentShader;

        public ShaderPipeline(SingularShader vertexShader, SingularShader geometryShader, SingularShader fragmentShader)
            : base(Gl.GenProgramPipeline(), Gl.BindProgramPipeline, Gl.DeleteProgramPipelines)
        {
            VertexShader = vertexShader;
            GeometryShader = geometryShader;
            FragmentShader = fragmentShader;
        }

        public ShaderPipeline(SingularShader vertexShader, SingularShader fragmentShader) : this(vertexShader, vertexShader, fragmentShader) { }

        public SingularShader VertexShader
        {
            get => vertexShader;

            set
            {
                Gl.UseProgramStage(Handle, Gl.VERTEX_SHADER_BIT, 0);
                Gl.UseProgramStage(Handle, Gl.VERTEX_SHADER_BIT, value.Handle);
                vertexShader = value;
            }
        }

        public SingularShader GeometryShader
        {
            get => geometryShader;

            set
            {
                Gl.UseProgramStage(Handle, Gl.GEOMETRY_SHADER_BIT, 0);
                Gl.UseProgramStage(Handle, Gl.GEOMETRY_SHADER_BIT, value.Handle);
                geometryShader = value;
            }
        }

        public SingularShader FragmentShader
        {
            get => fragmentShader;

            set
            {
                Gl.UseProgramStage(Handle, Gl.FRAGMENT_SHADER_BIT, 0);
                Gl.UseProgramStage(Handle, Gl.FRAGMENT_SHADER_BIT, value.Handle);
                fragmentShader = value;
            }
        }

        public override void Bind()
        {
            Gl.UseProgram(0);
            Gl.BindProgramPipeline(Handle);
        }
    }
}