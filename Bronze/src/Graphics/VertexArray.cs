using System;
using System.Linq;
using Bronze.Maths;
using OpenGL;
using Math = System.Math;

namespace Bronze.Graphics
{
    public class VertexArray : GraphicsResource
    {
        internal readonly uint Handle;

        private PrimitiveType Type { get; }

        private int Count { get; }

        public VertexArray(uint handle, int count, Vertices.DataType primitiveType)
        {
            Handle = handle;
            Count = count;
            switch(primitiveType)
            {
                case Vertices.DataType.Points:
                    Type = PrimitiveType.Points;
                    break;
                case Vertices.DataType.Lines:
                    Type = PrimitiveType.Lines;
                    break;
                case Vertices.DataType.LineStrip:
                    Type = PrimitiveType.LineStrip;
                    break;
                case Vertices.DataType.LineLoop:
                    Type = PrimitiveType.LineLoop;
                    break;
                case Vertices.DataType.Triangles:
                    Type = PrimitiveType.Triangles;
                    break;
                case Vertices.DataType.TriangleStrip:
                    Type = PrimitiveType.TriangleStrip;
                    break;
                case Vertices.DataType.TriangleFan:
                    Type = PrimitiveType.TriangleFan;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public sealed override void Bind() => Gl.BindVertexArray(Handle);

        public sealed override void Unbind() => Gl.BindVertexArray(0);

        public void Draw()
        {
            Bind();
            Gl.DrawArrays(Type, 0, Count);
            Unbind();
        }

        protected override void ReleaseUnmanagedResources()
        {
            Gl.DeleteVertexArrays(Handle);
        }
    }
}