using System;
using System.Diagnostics.CodeAnalysis;
using static Bronze.Math.Math;

namespace Bronze.Math
{
    public struct Vector2I : IEquatable<Vector2I>
    {
        public static readonly Vector2I Zero = new Vector2I();

        public static readonly Vector2I BasisX = new Vector2I {X = 1};

        public static readonly Vector2I BasisY = new Vector2I {Y = 1};

        public static int Size => 2;

        public int X { get; set; }

        public int Y { get; set; }

        public int[] Values => new[] {X, Y};

        public double Magnitude => Sqrt(X * X + Y * Y);

        public double Direction => Atan2(Y, X);

        public Vector2I(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Vector2I ComponentWiseMultiply(Vector2I vec) => new Vector2I(X * vec.X, Y * vec.Y);

        public int Dot(Vector2I vec) => X * vec.X + Y * vec.Y;

        public Vector2I Negate() => new Vector2I(-X, -Y);

        public Vector2I Add(Vector2I vec) => new Vector2I(X + vec.X, Y + vec.Y);

        public Vector2I Subtract(Vector2I vec) => new Vector2I(X - vec.X, Y - vec.Y);

        public Vector2I Multiply(int scalar) => new Vector2I(X * scalar, Y * scalar);

        public bool Equals(Vector2I vec) => EqualsWithTolerance(X, vec.X) && EqualsWithTolerance(Y, vec.Y);

        public Complex ToComplex() => new Complex(X, Y);

        public static Vector2I operator -(Vector2I vec) => vec.Negate();

        public static Vector2I operator +(Vector2I left, Vector2I right) => left.Add(right);

        public static Vector2I operator -(Vector2I left, Vector2I right) => left.Subtract(right);

        public static Vector2I operator *(Vector2I left, int right) => left.Multiply(right);

        public static Vector2I operator *(int left, Vector2I right) => right.Multiply(left);

        public static bool operator ==(Vector2I left, Vector2I right) => left.Equals(right);

        public static bool operator !=(Vector2I left, Vector2I right) => !(left == right);

        public static explicit operator Complex(Vector2I vec) => vec.ToComplex();

        public static explicit operator ComplexD(Vector2I vec) => vec.ToComplex();

        public static implicit operator Vector2(Vector2I vec) => new Vector2(vec.X, vec.Y);

        public static implicit operator Vector2D(Vector2I vec) => new Vector2D(vec.X, vec.Y);

        public override string ToString() => $"({Trim(X)}, {Trim(Y)})";

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj)) return false;
            return obj is Vector2I vec && Equals(vec);
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