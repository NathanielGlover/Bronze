namespace Bronze.Graphics
{
    public interface IDrawable
    {
        FullRenderEffect InitialRenderEffect { get; set; }
        
        void Draw();
    }
}