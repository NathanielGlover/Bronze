using System.Collections.Generic;
using System.Linq;
using Bronze.Maths;
using OpenGL;

namespace Bronze.Graphics
{
    public class Model : IDrawable
    {
        public VertexArray VertexArray { get; }
        
        public DrawType DrawType { get; set; }

        internal Model(VertexArray vertexArray, DrawType drawType)
        {
            VertexArray = vertexArray;
            DrawType = drawType;
        }

        public void Draw(FullRenderEffect renderEffect) => VertexArray.Draw(DrawType);
    }
}