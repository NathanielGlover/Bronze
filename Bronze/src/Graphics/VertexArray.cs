using System;
using System.Collections.Generic;
using System.Linq;
using OpenGL;

namespace Bronze.Graphics
{
    public class VertexArray : GraphicsResource
    {
        private readonly uint[] bufferHandles;

        internal VertexArray(uint handle, int vertexCount, int elementCount, IEnumerable<uint> bufferHandles) :
            base(handle, Gl.BindVertexArray, Gl.DeleteVertexArrays)
        {
            this.bufferHandles = bufferHandles.ToArray();
            VertexCount = vertexCount;
            ElementCount = elementCount;
        }

        public int VertexCount { get; }
        public int ElementCount { get; }

        public void DrawArrays(DrawType type) => DrawArrays(type, VertexCount);
        public void DrawArrays(DrawType type, int vertexCount) => RunActionWhileBound(() => Gl.DrawArrays((PrimitiveType) type, 0, vertexCount));

        public void DrawElements(DrawType type) => DrawElements(type, ElementCount);
        public void DrawElements(DrawType type, int elementCount) =>
            RunActionWhileBound(() => Gl.DrawElements((PrimitiveType) type, elementCount, DrawElementsType.UnsignedInt, IntPtr.Zero));

        public override void Dispose()
        {
            base.Dispose();
            Gl.DeleteBuffers(bufferHandles);
        }
    }
}