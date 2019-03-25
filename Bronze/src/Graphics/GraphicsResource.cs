using System;

namespace Bronze.Graphics
{
    public abstract class GraphicsResource : IDisposable
    {
        public abstract void Bind();

        public abstract void Unbind();

        protected abstract void ReleaseUnmanagedResources();

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~GraphicsResource() {
            ReleaseUnmanagedResources();
        }

        public void RunActionWhileBound(Action action)
        {
            Bind();
            action();
            Unbind();
        }
    }
}