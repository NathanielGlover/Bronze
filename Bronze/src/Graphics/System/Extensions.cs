using System;
using OpenGL;

namespace Bronze.Graphics
{
    internal static class Extensions
    {
        internal static int SizeOf(this VertexAttribType type) =>
            type switch
                {
                VertexAttribType.Byte => sizeof(sbyte),
                VertexAttribType.UnsignedByte => sizeof(byte),
                VertexAttribType.Short => sizeof(short),
                VertexAttribType.UnsignedShort => sizeof(ushort),
                VertexAttribType.Int => sizeof(int),
                VertexAttribType.UnsignedInt => sizeof(uint),
                VertexAttribType.Float => sizeof(float),
                VertexAttribType.Double => sizeof(double),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
                };
    }
}