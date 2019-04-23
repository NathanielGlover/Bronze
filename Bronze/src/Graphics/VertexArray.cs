using System;
using System.Collections.Generic;
using System.Linq;
using Bronze.Maths;
using OpenGL;
using Math = System.Math;

namespace Bronze.Graphics
{
    public class VertexArray : GraphicsResource
    {
        private readonly uint[] bufferHandles;
        internal readonly uint Handle;
        
        public int Count { get; }

        internal VertexArray(uint handle, int count, IReadOnlyList<uint> bufferHandles)
        {
            this.bufferHandles = bufferHandles.ToArray();
            Handle = handle;
            Count = count;
        }

        public sealed override void Bind() => Gl.BindVertexArray(Handle);

        public sealed override void Unbind() => Gl.BindVertexArray(0);

        public void Draw(DrawType type)
        {
            Bind();
            Gl.DrawArrays((PrimitiveType) type, 0, Count);
            Unbind();
        }

        protected override void ReleaseUnmanagedResources()
        {
            Gl.DeleteVertexArrays(Handle);
            Gl.DeleteBuffers(bufferHandles);
        }
    }
}