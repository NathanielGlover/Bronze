using OpenGL;

namespace Bronze.Graphics
{
    public enum DrawType
    {
        Points = PrimitiveType.Points,
        Lines = PrimitiveType.Lines,
        LineStrip = PrimitiveType.LineStrip,
        LineLoop = PrimitiveType.LineLoop,
        Triangles = PrimitiveType.Triangles,
        TriangleStrip = PrimitiveType.TriangleStrip,
        TriangleFan = PrimitiveType.TriangleFan
    }
}