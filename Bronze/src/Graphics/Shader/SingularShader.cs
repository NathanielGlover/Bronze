namespace Bronze.Graphics
{
    public class SingularShader : Shader
    {
        internal SingularShader(uint handle) : base(handle) { }

        internal static SingularShader FromShader(Shader shader) => new SingularShader(shader.Handle);

        public SingularShader(ShaderStage stage) : base(new ShaderBuilder().AddStage(stage).BuildShader().Handle) { }
    }
}