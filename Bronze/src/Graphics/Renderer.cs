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

        public void Render(IDrawable drawable, FullRenderEffect renderEffect)
        {
            ContextManager.RunInSeperateContext(() =>
            {
                renderEffect.Bind();
                //TODO: Change to calculate preferred projection and view
                renderEffect.View = Matrix3.Identity;
                renderEffect.Projection = Matrix3.Identity;

                drawable.Draw(renderEffect);
                renderEffect.Unbind();
            }, RenderTarget.Handle);
        }
    }
}