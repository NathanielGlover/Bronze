namespace Bronze.Maths
{
    public class Projection
    {
        public static readonly Projection None = new Projection(Matrix4.Identity);
        
        public Matrix4 ProjectionMatrix { get; }

        protected Projection(Matrix4 projectionMatrix) => ProjectionMatrix = projectionMatrix;
    }
}