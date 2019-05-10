namespace Bronze.Graphics
{
    public class Model
    {
        public Model(VertexArray vertexArray, DrawType drawType)
        {
            VertexArray = vertexArray;
            DrawType = drawType;
        }

        public VertexArray VertexArray { get; }
        public DrawType DrawType { get; set; }

        public void Draw() => VertexArray.DrawElements(DrawType);
    }
}