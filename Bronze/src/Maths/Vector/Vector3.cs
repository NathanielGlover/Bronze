using System;
using System.Diagnostics.CodeAnalysis;
using static Bronze.Maths.Math;

namespace Bronze.Maths
{
    public struct Vector3 : IEquatable<Vector3>
    {
        public static readonly Vector3 Zero = new Vector3();

        public static readonly Vector3 BasisX = new Vector3 {X = 1};

        public static readonly Vector3 BasisY = new Vector3 {Y = 1};

        public static readonly Vector3 BasisZ = new Vector3 {Z = 1};

        public static int Size => 3;

        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public float[] Values => new[] {X, Y, Z};

        public float Magnitude => Sqrt(X * X + Y * Y + Z * Z);

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3 Normalize()
        {
            float magnitude = Magnitude;
            return new Vector3(X / magnitude, Y / magnitude, Z / magnitude);
        }

        public Vector3 ComponentWiseMultiply(Vector3 vec) => new Vector3(X * vec.X, Y * vec.Y, Z * vec.Z);

        public float Dot(Vector3 vec) => X * vec.X + Y * vec.Y + Z * vec.Z;

        public Vector3 Cross(Vector3 vec) => new Vector3(Y * vec.Z - Z * vec.Y, Z * vec.X - X * vec.Z, X * vec.Y - Y * vec.X);

        public Vector3 Negate() => new Vector3(-X, -Y, -Z);

        public Vector3 Add(Vector3 vec) => new Vector3(X + vec.X, Y + vec.Y, Z + vec.Z);

        public Vector3 Subtract(Vector3 vec) => new Vector3(X - vec.X, Y - vec.Y, Z - vec.Z);

        public Vector3 Multiply(float scalar) => new Vector3(X * scalar, Y * scalar, Z * scalar);

        public bool Equals(Vector3 vec) => EqualsWithTolerance(X, vec.X) && EqualsWithTolerance(Y, vec.Y) && EqualsWithTolerance(Z, vec.Z);

        public static Vector3 operator -(Vector3 vec) => vec.Negate();

        public static Vector3 operator +(Vector3 left, Vector3 right) => left.Add(right);

        public static Vector3 operator -(Vector3 left, Vector3 right) => left.Subtract(right);

        public static Vector3 operator *(Vector3 left, float right) => left.Multiply(right);

        public static Vector3 operator *(float left, Vector3 right) => right.Multiply(left);

        public static bool operator ==(Vector3 left, Vector3 right) => left.Equals(right);

        public static bool operator !=(Vector3 left, Vector3 right) => !(left == right);

        public static implicit operator Vector3D(Vector3 vec) => new Vector3D(vec.X, vec.Y, vec.Z);

        public static explicit operator Vector3I(Vector3 vec) => new Vector3I((int) vec.X, (int) vec.Y, (int) vec.Z);
        
        public static explicit operator Vector2(Vector3 vec) => new Vector2(vec.X, vec.Y);

        public override string ToString() => $"({Trim(X)}, {Trim(Y)}, {Trim(Z)})";

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj)) return false;
            return obj is Vector3 vec && Equals(vec);
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