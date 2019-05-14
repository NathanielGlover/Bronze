namespace Bronze.Graphics
{
    public interface IDrawable
    {
        void Draw(ShaderPipeline shaderPipeline, params Effect[] effects);
    }
}