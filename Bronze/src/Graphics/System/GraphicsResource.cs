using System;

namespace Bronze.Graphics
{
    public abstract class GraphicsResource : IDisposable
    {
        protected GraphicsResource(uint handle, Action<uint> bindFunction, Action<uint> deleteFunction)
        {
            ContextManager.EnsureDefaultContext();
            Handle = handle;
            BindFunction = bindFunction;
            DeleteFunction = deleteFunction;
        }

        protected GraphicsResource(uint handle, Action<uint> bindFunction, Action<uint[]> deleteFunction) :
            this(handle, bindFunction, resource => deleteFunction(new[] {resource})) { }

        private Action<uint> BindFunction { get; }
        private Action<uint> DeleteFunction { get; }

        protected internal uint Handle { get; }

        public virtual void Dispose()
        {
            DeleteFunction(Handle);
            GC.SuppressFinalize(this);
        }

        public virtual void Bind() => BindFunction(Handle);
        public virtual void Unbind() => BindFunction(0);

        public void RunActionWhileBound(Action action)
        {
            Bind();
            action();
            Unbind();
        }
    }
}