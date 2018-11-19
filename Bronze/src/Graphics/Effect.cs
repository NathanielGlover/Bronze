namespace Bronze.Graphics
{
    public abstract class Effect
    {
        protected Shader Shader { get; }

        protected Effect(Shader shader)
        {
            Shader = shader;
        }

        public void Bind()
        {
            Shader.Bind();
        }

        public void Unbind()
        {
            Shader.Unbind();
        }
    }
}