using System.Linq;

namespace Bronze.Maths
{
    public class Transform
    {
        public static Transform Identity => new Transform();

        public Vector3 LocalOrigin { get; set; }

        public Vector3 Translation { get; set; }

        public float Rotation { get; set; }
        
        public Vector3 RotationAxis { get; set; } = new Vector3(0, 0, -1);

        public Vector3 ScaleFactor { get; set; } = new Vector3(1, 1, 1);

        public Vector3 ShearFactor { get; set; }

        public bool IsIdentity => TransformationMatrix == Matrix4.Identity;

        public virtual Matrix4 TransformationMatrix
        {
            get
            {
                if(LocalOrigin == Vector3.Zero)
                {
                    var scale = Math.CreateScaleMatrix(ScaleFactor);
                    var rotation = Math.CreateRotationMatrix(Rotation, RotationAxis);
                    var translation = Math.CreateTranslationMatrix(Translation);
                    
                    return translation * rotation * scale;
                }
                else
                {
                    
                    var originShift = Math.CreateTranslationMatrix(-LocalOrigin);
                    var scale = Math.CreateScaleMatrix(ScaleFactor);
                    var rotation = Math.CreateRotationMatrix(Rotation, RotationAxis);
                    var translation = Math.CreateTranslationMatrix(Translation);

                    return originShift.Inverse * translation * rotation * scale * originShift;
                }
            }
        }

        public virtual Transform Inverse => new Transform
        {
            Translation = -Translation,
            Rotation = -Rotation,
            ScaleFactor = new Vector3(1f / ScaleFactor.X, 1f / ScaleFactor.Y, 1f / ScaleFactor.Z),
            ShearFactor = -ShearFactor
        };

        public Vector3 ApplyTransform(Vector3 point) => ApplyTransform(new Vertices(new[] {point}))[0];

        public Vertices ApplyTransform(Vertices vertices)
        {
            var affinePoints = (from point in vertices select new Vector4(point.X, point.Y, 0, 1)).ToList();
            var transformationMatrix = TransformationMatrix;

            var newPoints = (from affinePoint in affinePoints select transformationMatrix * affinePoint).ToList();
            return new Vertices(from newPoint in newPoints select new Vector3(newPoint.X, newPoint.Y, newPoint.Z));
        }

        public void Translate(Vector3 translation) => Translation += translation;

        public void Rotate(float angle) => Rotation += angle;
    }
}