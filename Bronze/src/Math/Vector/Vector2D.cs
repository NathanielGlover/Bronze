using System;
using System.Diagnostics.CodeAnalysis;
using static Bronze.Math.Math;

namespace Bronze.Math
{
    public struct Vector2D : IEquatable<Vector2D>
    {
        public static readonly Vector2D Zero = new Vector2D();

        public static readonly Vector2D BasisX = new Vector2D {X = 1};

        public static readonly Vector2D BasisY = new Vector2D {Y = 1};

        public static int Size => 2;

        public double X { get; set; }

        public double Y { get; set; }

        public double[] Values => new[] {X, Y};

        public double Magnitude => Sqrt(X * X + Y * Y);

        public double Direction => Atan2(Y, X);

        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Vector2D Normalize()
        {
            double magnitude = Magnitude;
            return new Vector2D(X / magnitude, Y / magnitude);
        }

        public Vector2D ComponentWiseMultiply(Vector2D vec) => new Vector2D(X * vec.X, Y * vec.Y);

        public double Dot(Vector2D vec) => X * vec.X + Y * vec.Y;

        public Vector2D Negate() => new Vector2D(-X, -Y);

        public Vector2D Add(Vector2D vec) => new Vector2D(X + vec.X, Y + vec.Y);

        public Vector2D Subtract(Vector2D vec) => new Vector2D(X - vec.X, Y - vec.Y);

        public Vector2D Multiply(double scalar) => new Vector2D(X * scalar, Y * scalar);

        public bool Equals(Vector2D vec) => EqualsWithTolerance(X, vec.X) && EqualsWithTolerance(Y, vec.Y);

        public ComplexD ToComplex() => new ComplexD(X, Y);

        public static Vector2D operator -(Vector2D vec) => vec.Negate();

        public static Vector2D operator +(Vector2D left, Vector2D right) => left.Add(right);

        public static Vector2D operator -(Vector2D left, Vector2D right) => left.Subtract(right);

        public static Vector2D operator *(Vector2D left, double right) => left.Multiply(right);

        public static Vector2D operator *(double left, Vector2D right) => right.Multiply(left);

        public static bool operator ==(Vector2D left, Vector2D right) => left.Equals(right);

        public static bool operator !=(Vector2D left, Vector2D right) => !(left == right);

        public static explicit operator Complex(Vector2D vec) => (Complex) vec.ToComplex();

        public static explicit operator ComplexD(Vector2D vec) => vec.ToComplex();

        public static explicit operator Vector2(Vector2D vec) => new Vector2((float) vec.X, (float) vec.Y);

        public static explicit operator Vector2I(Vector2D vec) => new Vector2I((int) vec.X, (int) vec.Y);

        public override string ToString() => $"({Trim(X)}, {Trim(Y)})";

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj)) return false;
            return obj is Vector2D vec && Equals(vec);
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