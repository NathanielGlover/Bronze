using Bronze.Maths;
using Bronze.UserInterface;

namespace Bronze.Graphics
{
    public class Renderer
    {
        public Window RenderTarget { get; set; }

        public Renderer(Window renderTarget)
        {
            RenderTarget = renderTarget;
        }

        public void Render(IDrawable drawable)
        {
            ContextManager.RunInSeperateContext(() =>
            {
                drawable.InitialRenderEffect.Bind();
                //TODO: Change to calculate preferred projection and view
                drawable.InitialRenderEffect.View = Matrix3.Identity;
                drawable.InitialRenderEffect.Projection = Matrix3.Identity;
            
                drawable.Draw();
                drawable.InitialRenderEffect.Unbind();
            }, RenderTarget.Handle);
        }
    }
}