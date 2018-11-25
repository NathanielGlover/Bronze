using Bronze.Maths;
using OpenGL;

namespace Bronze.Graphics
{
    public class Texture
    {
        internal readonly uint Handle;

        public Vector2I Size { get; }

        public Texture(Vector2I size, uint handle)
        {
            Size = size;
            Handle = handle;
            ContextManager.ContextChange += OnContextChange;
        }

        private void OnContextChange(ContextInfo contextInfo)
        {
            Bind();
            Gl.GenerateMipmap(TextureTarget.Texture2d);
        }

        public void Bind() => Gl.BindTexture(TextureTarget.Texture2d, Handle);

        public void Unbind() => Gl.BindTexture(TextureTarget.Texture2d, 0);
    }
}