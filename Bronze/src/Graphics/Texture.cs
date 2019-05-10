using Bronze.Maths;
using OpenGL;

namespace Bronze.Graphics
{
    public class Texture : GraphicsResource
    {
        public static readonly Texture Blank = new Texture(0, Vector2I.Zero);
        
        internal Texture(uint handle, Vector2I size) : base(handle, tex => Gl.BindTexture(TextureTarget.Texture2d, tex), Gl.DeleteTextures)
        {
            Size = size;
            ContextManager.ContextChange += OnContextChange;
        }

        public Vector2I Size { get; }

        private void OnContextChange(ContextInfo contextInfo)
        {
            Bind();
            Gl.GenerateMipmap(TextureTarget.Texture2d);
        }
    }
}