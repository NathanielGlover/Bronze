namespace Bronze.Graphics
{
    public abstract class Effect
    {
        public abstract void ApplyEffect(ShaderPipeline pipeline);
        
        public abstract void SetDefaults(ShaderPipeline pipeline);
    }
}