using Bronze.Maths;
using OpenGL;

namespace Bronze.Graphics
{
    public class Texture : GraphicsResource
    {
        internal readonly uint Handle;

        public Vector2 Size { get; }

        public Texture(Vector2 size, uint handle)
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

        public override void Bind() => Gl.BindTexture(TextureTarget.Texture2d, Handle);

        public override void Unbind() => Gl.BindTexture(TextureTarget.Texture2d, 0);

        protected override void ReleaseUnmanagedResources()
        {
            Gl.DeleteTextures(Handle);
        }

        ~Texture() {
            ReleaseUnmanagedResources();
        }
    }
}