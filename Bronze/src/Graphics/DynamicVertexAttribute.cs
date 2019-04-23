using System;
using System.Collections.Generic;
using System.Linq;
using Bronze.Maths;
using OpenGL;
using Math = Bronze.Maths.Math;

namespace Bronze.Graphics
{
    public class DynamicVertexAttribute<T>
        where T : struct
    {
        private readonly int byteOffset;
        private readonly int size;

        internal readonly uint Handle;
        public int Count { get; }

        internal DynamicVertexAttribute(uint handle, int size, int byteOffset, int count)
        {
            this.byteOffset = byteOffset;
            this.size = size;
            Handle = handle;
            Count = count;
        }

        public void Update(IEnumerable<T> newData, int offset)
        {
            var array = newData.ToArray();
            offset = System.Math.Clamp(offset, 0, array.Length);
            int dataSize = System.Math.Min(array.Length - offset, Count) * size;
            Gl.BindBuffer(BufferTarget.ArrayBuffer, Handle);
            Gl.BufferSubData(BufferTarget.ArrayBuffer, new IntPtr(byteOffset + offset * size), (uint) dataSize, array);
            Gl.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
    }
}