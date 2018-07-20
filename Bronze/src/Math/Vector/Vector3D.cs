using System;
using System.Diagnostics.CodeAnalysis;
using static Bronze.Math.Maths;

namespace Bronze.Math
{
    public struct Vector3D : IEquatable<Vector3D>
    {
        public static readonly Vector3D Zero = new Vector3D();

        public static readonly Vector3D BasisX = new Vector3D {X = 1};

        public static readonly Vector3D BasisY = new Vector3D {Y = 1};

        public static readonly Vector3D BasisZ = new Vector3D {Z = 1};

        public static int Size => 3;

        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }

        public double[] Values => new[] {X, Y, Z};

        public double Magnitude => Sqrt(X * X + Y * Y + Z * Z);

        public Vector3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3D Normalize()
        {
            double magnitude = Magnitude;
            return new Vector3D(X / magnitude, Y / magnitude, Z / magnitude);
        }

        public Vector3D ComponentWiseMultiply(Vector3D vec) => new Vector3D(X * vec.X, Y * vec.Y, Z * vec.Z);

        public double Dot(Vector3D vec) => X * vec.X + Y * vec.Y + Z * vec.Z;

        public Vector3D Cross(Vector3D vec) => new Vector3D(Y * vec.Z - Z * vec.Y, Z * vec.X - X * vec.Z, X * vec.Y - Y * vec.X);

        public Vector3D Negate() => new Vector3D(-X, -Y, -Z);

        public Vector3D Add(Vector3D vec) => new Vector3D(X + vec.X, Y + vec.Y, Z + vec.Z);

        public Vector3D Subtract(Vector3D vec) => new Vector3D(X - vec.X, Y - vec.Y, Z - vec.Z);

        public Vector3D Multiply(double scalar) => new Vector3D(X * scalar, Y * scalar, Z * scalar);

        public bool Equals(Vector3D vec) => EqualsWithTolerance(X, vec.X) && EqualsWithTolerance(Y, vec.Y) && EqualsWithTolerance(Z, vec.Z);

        public static Vector3D operator -(Vector3D vec) => vec.Negate();

        public static Vector3D operator +(Vector3D left, Vector3D right) => left.Add(right);

        public static Vector3D operator -(Vector3D left, Vector3D right) => left.Subtract(right);

        public static Vector3D operator *(Vector3D left, double right) => left.Multiply(right);

        public static Vector3D operator *(double left, Vector3D right) => right.Multiply(left);

        public static bool operator ==(Vector3D left, Vector3D right) => left.Equals(right);

        public static bool operator !=(Vector3D left, Vector3D right) => !(left == right);

        public static explicit operator Vector3(Vector3D vec) => new Vector3((float) vec.X, (float) vec.Y, (float) vec.Z);

        public static explicit operator Vector3I(Vector3D vec) => new Vector3I((int) vec.X, (int) vec.Y, (int) vec.Z);

        public override string ToString() => $"({Trim(X)}, {Trim(Y)}, {Trim(Z)})";

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj)) return false;
            return obj is Vector3D vec && Equals(vec);
        }

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
                return hashCode;
            }
        }
    }
}