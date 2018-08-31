using System.Collections.Generic;
using System.Linq;

namespace Bronze.Maths
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
                var originShift = Math.CreateTranslationMatrix(-LocalOrigin);
                var scale = Math.CreateScaleMatrix(ScaleFactor);
                var shear = Math.CreateShearMatrix(ShearFactor);
                var rotation = Math.CreateRotationMatrix(Rotation);
                var translation = Math.CreateTranslationMatrix(Translation);

                return originShift.Inverse * translation * rotation * shear * scale * originShift;
            }
        }

        public Transform Inverse => new Transform
        {
            Translation = -Translation,
            Rotation = -Rotation,
            ScaleFactor = Math.Pow(ScaleFactor, -1),
            ShearFactor = -ShearFactor
        };

        public Vector2 ApplyTransform(Vector2 point) => ApplyTransform(new Vertices(new List<Vector2> {point}, Vertices.DataType.Points)).VertexData[0];

        public Vertices ApplyTransform(Vertices vertices)
        {
            var affinePoints = (from point in vertices.VertexData select new Vector3(point.X, point.Y, 1)).ToList();
            var transformationMatrix = TransformationMatrix;

            var transformedPoints = (from affinePoint in affinePoints select transformationMatrix * affinePoint).ToList();
            return new Vertices((from transformedPoint in transformedPoints select new Vector2(transformedPoint.X, transformedPoint.Y)).ToList(),
                vertices.VertexDataType);
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