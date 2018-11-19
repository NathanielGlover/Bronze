using System;
using System.Linq;
using Bronze.Maths;
using OpenGL;

namespace Bronze.Graphics
{
    public class VertexArray
    {
        internal readonly uint Handle;

        private PrimitiveType Type { get; }
        
        private int Count { get; }

        public VertexArray(Vertices data)
        {
            switch(data.VertexDataType)
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
                    Type = PrimitiveType.Triangles;
                    break;
                case Vertices.DataType.TriangleFan:
                    Type = PrimitiveType.TriangleFan;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Count = data.Count;
            
            Handle = Gl.GenVertexArray();
            uint bufferHandle = Gl.GenBuffer();
            
            float[] floatData = new float[data.Count * 2];
            for(int i = 0; i < data.Count; i++)
            {
                floatData[2 * i] = data[i].X;
                floatData[2 * i + 1] = data[i].Y;
            }
            
            Bind();
            Gl.BindBuffer(BufferTarget.ArrayBuffer, bufferHandle);
            Gl.BufferData(BufferTarget.ArrayBuffer, (uint) (sizeof(float) * floatData.Length), floatData, BufferUsage.StaticDraw);
            
            Gl.VertexAttribPointer(0, Vector2.Size, VertexAttribType.Float, false, 2 * sizeof(float), IntPtr.Zero);
            Gl.EnableVertexAttribArray(0);
            Unbind();
        }

        public void Bind() => Gl.BindVertexArray(Handle);

        public void Unbind() => Gl.BindVertexArray(0);

        public void Draw()
        {
            Bind();
            Gl.DrawArrays(Type, 0, Count);
            Unbind();
        }
    }
}