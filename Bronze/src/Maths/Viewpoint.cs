namespace Bronze.Maths
{
    public class Viewpoint
    {
        public static Viewpoint Default => new Viewpoint(Vector3.Zero, -Vector3.BasisZ, Vector3.BasisY);
        
        public Vector3 Position { get; set; }
        
        public Vector3 Target { get; set; }
        
        public Vector3 Up { get; set; }

        public Vector3 Direction => (Position - Target).Normalize();

        public Vector3 Right => Up.Cross(Direction);
        
        public Matrix4 ViewMatrix => new Matrix4
            (
                Right.X, Right.Y, Right.Z, 0,
                Up.X, Up.Y, Up.Z, 0,
                Direction.X, Direction.Y, Direction.Z, 0,
                0, 0, 0, 1
            ) * Math.CreateTranslationMatrix(-Position);

        public Viewpoint(Vector3 position, Vector3 target, Vector3 up)
        {
            Position = position;
            Target = target;
            Up = up;
        }
    }
}