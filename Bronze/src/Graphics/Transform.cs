using System.Collections.Generic;
using System.Linq;
using Bronze.Math;

namespace Bronze.Graphics
{
    public class Transform
    {
        public Vector2 LocalOrigin { get; set; }

        public Vector2 Translation { get; set; }

        public float Rotation { get; set; }

        public Vector2 ScaleFactor { get; set; } = new Vector2(1, 1);

        public Vector2 ShearFactor { get; set; }

        public bool IsIdentity => TransformationMatrix == Matrix3.Identity;

        public Matrix3 TransformationMatrix
        {
            get
            {
                var originShift = Maths.CreateTranslationMatrix(-LocalOrigin);
                var scale = Maths.CreateScaleMatrix(ScaleFactor);
                var shear = Maths.CreateShearMatrix(ShearFactor);
                var rotation = Maths.CreateRotationMatrix(Rotation);
                var translation = Maths.CreateTranslationMatrix(Translation);

                return originShift.Inverse * translation * rotation * shear * scale * originShift;
            }
        }

        public Transform Inverse => new Transform
        {
            Translation = -Translation,
            Rotation = -Rotation,
            ScaleFactor = Maths.Pow(ScaleFactor, -1),
            ShearFactor = -ShearFactor
        };

        public Vector2 ApplyTransform(Vector2 point) => ApplyTransform(new[] {point})[0];

        public List<Vector2> ApplyTransform(IEnumerable<Vector2> points)
        {
            var affinePoints = (from point in points select new Vector3(point.X, point.Y, 1)).ToList();
            var transformationMatrix = TransformationMatrix;

            var transformedPoints = (from affinePoint in affinePoints select transformationMatrix * affinePoint).ToList();
            return (from transformedPoint in transformedPoints select new Vector2(transformedPoint.X, transformedPoint.Y)).ToList();
        }

        public void Translate(Vector2 translation) => Translation += translation;

        public void Rotate(float angle) => Rotation += angle;

        public void HorizontalShear(float factor) => ShearFactor = new Vector2(ShearFactor.X + factor, ShearFactor.Y);

        public void VerticalShear(float factor) => ShearFactor = new Vector2(ShearFactor.X, ShearFactor.Y + factor);

        public void Shear(Vector2 factor) => ShearFactor += factor;

        public void HorizontalScale(float factor) => ScaleFactor = new Vector2(ScaleFactor.X * factor, ScaleFactor.Y);

        public void VerticalScale(float factor) => ScaleFactor = new Vector2(ScaleFactor.X, ScaleFactor.Y * factor);

        public void Scale(float factor) => ScaleFactor *= factor;

        public void Scale(Vector2 factor) => ScaleFactor = ScaleFactor.ComponentWiseMultiply(factor);
    }
}