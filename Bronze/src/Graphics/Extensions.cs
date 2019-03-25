using System;
using OpenGL;

namespace Bronze.Graphics
{
    internal static class Extensions
    {
        internal static int SizeOf(this VertexAttribType type)
        {
            int size;

            switch(type)
            {
                case VertexAttribType.Byte:
                    size = sizeof(sbyte);
                    break;
                case VertexAttribType.UnsignedByte:
                    size = sizeof(byte);
                    break;
                case VertexAttribType.Short:
                    size = sizeof(short);
                    break;
                case VertexAttribType.UnsignedShort:
                    size = sizeof(ushort);
                    break;
                case VertexAttribType.Int:
                    size = sizeof(int);
                    break;
                case VertexAttribType.UnsignedInt:
                    size = sizeof(uint);
                    break;
                case VertexAttribType.Float:
                    size = sizeof(float);
                    break;
                case VertexAttribType.Double:
                    size = sizeof(double);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            return size;
        }
    }
}