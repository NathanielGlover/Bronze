using System;
using System.Diagnostics.CodeAnalysis;
using static Bronze.Maths.Math;

namespace Bronze.Maths
{
    public struct Vector4 : IEquatable<Vector4>
    {
        public static readonly Vector4 Zero = new Vector4();

        public static readonly Vector4 BasisX = new Vector4 {X = 1};

        public static readonly Vector4 BasisY = new Vector4 {Y = 1};

        public static readonly Vector4 BasisZ = new Vector4 {Z = 1};

        public static readonly Vector4 BasisW = new Vector4 {W = 1};

        public static int Size => 4;

        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public float W { get; set; }

        public float[] Values => new[] {X, Y, Z, W};

        public float Magnitude => Sqrt(X * X + Y * Y + Z * Z + W * W);

        public Vector4(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public Vector4 Normalize()
        {
            float magnitude = Magnitude;
            return new Vector4(X / magnitude, Y / magnitude, Z / magnitude, W / magnitude);
        }

        public Vector4 ComponentWiseMultiply(Vector4 vec) => new Vector4(X * vec.X, Y * vec.Y, Z * vec.Z, W * vec.W);

        public float Dot(Vector4 vec) => X * vec.X + Y * vec.Y + Z * vec.Z + W * vec.W;

        public Vector4 Negate() => new Vector4(-X, -Y, -Z, -W);

        public Vector4 Add(Vector4 vec) => new Vector4(X + vec.X, Y + vec.Y, Z + vec.Z, W + vec.W);

        public Vector4 Subtract(Vector4 vec) => new Vector4(X - vec.X, Y - vec.Y, Z - vec.Z, W - vec.W);

        public Vector4 Multiply(float scalar) => new Vector4(X * scalar, Y * scalar, Z * scalar, W * scalar);

        public bool Equals(Vector4 vec) => EqualsWithTolerance(X, vec.X) && EqualsWithTolerance(Y, vec.Y) && EqualsWithTolerance(Z, vec.Z) &&
                                           EqualsWithTolerance(W, vec.W);

        public static Vector4 operator -(Vector4 vec) => vec.Negate();

        public static Vector4 operator +(Vector4 left, Vector4 right) => left.Add(right);

        public static Vector4 operator -(Vector4 left, Vector4 right) => left.Subtract(right);

        public static Vector4 operator *(Vector4 left, float right) => left.Multiply(right);

        public static Vector4 operator *(float left, Vector4 right) => right.Multiply(left);

        public static bool operator ==(Vector4 left, Vector4 right) => left.Equals(right);

        public static bool operator !=(Vector4 left, Vector4 right) => !(left == right);

        public static implicit operator Vector4D(Vector4 vec) => new Vector4D(vec.X, vec.Y, vec.Z, vec.W);

        public static explicit operator Vector4I(Vector4 vec) => new Vector4I((int) vec.X, (int) vec.Y, (int) vec.Z, (int) vec.W);

        public override string ToString() => $"({Trim(X)}, {Trim(Y)}, {Trim(Z)}, {Trim(W)})";

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj)) return false;
            return obj is Vector4 vec && Equals(vec);
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
                hashCode = (hashCode * 397) ^ W.GetHashCode();
                return hashCode;
            }
        }
    }
}