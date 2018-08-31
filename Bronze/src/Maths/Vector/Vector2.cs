using System;
using System.Diagnostics.CodeAnalysis;
using static Bronze.Maths.Math;

namespace Bronze.Maths
{
    public struct Vector2 : IEquatable<Vector2>
    {
        public static readonly Vector2 Zero = new Vector2();

        public static readonly Vector2 BasisX = new Vector2 {X = 1};

        public static readonly Vector2 BasisY = new Vector2 {Y = 1};

        public static int Size => 2;

        public float X { get; set; }

        public float Y { get; set; }

        public float[] Values => new[] {X, Y};

        public float Magnitude => Sqrt(X * X + Y * Y);

        public float Direction => Atan2(Y, X);

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vector2 Normalize()
        {
            float magnitude = Magnitude;
            return new Vector2(X / magnitude, Y / magnitude);
        }

        public Vector2 ComponentWiseMultiply(Vector2 vec) => new Vector2(X * vec.X, Y * vec.Y);

        public float Dot(Vector2 vec) => X * vec.X + Y * vec.Y;

        public Vector2 Negate() => new Vector2(-X, -Y);

        public Vector2 Add(Vector2 vec) => new Vector2(X + vec.X, Y + vec.Y);

        public Vector2 Subtract(Vector2 vec) => new Vector2(X - vec.X, Y - vec.Y);

        public Vector2 Multiply(float scalar) => new Vector2(X * scalar, Y * scalar);

        public bool Equals(Vector2 vec) => EqualsWithTolerance(X, vec.X) && EqualsWithTolerance(Y, vec.Y);

        public Complex ToComplex() => new Complex(X, Y);

        public static Vector2 operator -(Vector2 vec) => vec.Negate();

        public static Vector2 operator +(Vector2 left, Vector2 right) => left.Add(right);

        public static Vector2 operator -(Vector2 left, Vector2 right) => left.Subtract(right);

        public static Vector2 operator *(Vector2 left, float right) => left.Multiply(right);

        public static Vector2 operator *(float left, Vector2 right) => right.Multiply(left);

        public static bool operator ==(Vector2 left, Vector2 right) => left.Equals(right);

        public static bool operator !=(Vector2 left, Vector2 right) => !(left == right);

        public static explicit operator Complex(Vector2 vec) => vec.ToComplex();

        public static explicit operator ComplexD(Vector2 vec) => vec.ToComplex();

        public static implicit operator Vector2D(Vector2 vec) => new Vector2D(vec.X, vec.Y);

        public static explicit operator Vector2I(Vector2 vec) => new Vector2I((int) vec.X, (int) vec.Y);

        public override string ToString() => $"({Trim(X)}, {Trim(Y)})";

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj)) return false;
            return obj is Vector2 vec && Equals(vec);
        }

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                return hashCode;
            }
        }
    }
}