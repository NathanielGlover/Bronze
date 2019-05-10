using System;
using System.Collections.Generic;
using System.Linq;
using OpenGL;

namespace Bronze.Graphics
{
    public class VertexBuffer<T>
        where T : struct
    {
        private readonly int byteOffset;

        internal readonly uint Handle;
        private readonly int size;

        internal VertexBuffer(uint handle, int size, int byteOffset, int count)
        {
            this.byteOffset = byteOffset;
            this.size = size;
            Handle = handle;
            Count = count;
        }

        public int Count { get; }

        public void Update(IEnumerable<T> newData, int offset)
        {
            var array = newData.ToArray();
            offset = Math.Clamp(offset, 0, array.Length);
            int dataSize = Math.Min(array.Length - offset, Count) * size;
            Gl.BindBuffer(BufferTarget.ArrayBuffer, Handle);
            Gl.BufferSubData(BufferTarget.ArrayBuffer, new IntPtr(byteOffset + offset * size), (uint) dataSize, array);
            Gl.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
    }
}