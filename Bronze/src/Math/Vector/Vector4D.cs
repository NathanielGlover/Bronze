using System;
using System.Diagnostics.CodeAnalysis;
using static Bronze.Math.Maths;

namespace Bronze.Math
{
    public struct Vector4D : IEquatable<Vector4D>
    {
        public static readonly Vector4D Zero = new Vector4D();

        public static readonly Vector4D BasisX = new Vector4D {X = 1};

        public static readonly Vector4D BasisY = new Vector4D {Y = 1};

        public static readonly Vector4D BasisZ = new Vector4D {Z = 1};

        public static readonly Vector4D BasisW = new Vector4D {W = 1};

        public static int Size => 4;

        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }

        public double W { get; set; }

        public double[] Values => new[] {X, Y, Z, W};

        public double Magnitude => Sqrt(X * X + Y * Y + Z * Z + W * W);

        public Vector4D(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public Vector4D Normalize()
        {
            double magnitude = Magnitude;
            return new Vector4D(X / magnitude, Y / magnitude, Z / magnitude, W / magnitude);
        }

        public Vector4D ComponentWiseMultiply(Vector4D vec) => new Vector4D(X * vec.X, Y * vec.Y, Z * vec.Z, W * vec.W);

        public double Dot(Vector4D vec) => X * vec.X + Y * vec.Y + Z * vec.Z + W * vec.W;

        public Vector4D Negate() => new Vector4D(-X, -Y, -Z, -W);

        public Vector4D Add(Vector4D vec) => new Vector4D(X + vec.X, Y + vec.Y, Z + vec.Z, W + vec.W);

        public Vector4D Subtract(Vector4D vec) => new Vector4D(X - vec.X, Y - vec.Y, Z - vec.Z, W - vec.W);

        public Vector4D Multiply(double scalar) => new Vector4D(X * scalar, Y * scalar, Z * scalar, W * scalar);

        public bool Equals(Vector4D vec) => EqualsWithTolerance(X, vec.X) && EqualsWithTolerance(Y, vec.Y) && EqualsWithTolerance(Z, vec.Z) &&
                                            EqualsWithTolerance(W, vec.W);

        public static Vector4D operator -(Vector4D vec) => vec.Negate();

        public static Vector4D operator +(Vector4D left, Vector4D right) => left.Add(right);

        public static Vector4D operator -(Vector4D left, Vector4D right) => left.Subtract(right);

        public static Vector4D operator *(Vector4D left, double right) => left.Multiply(right);

        public static Vector4D operator *(double left, Vector4D right) => right.Multiply(left);

        public static bool operator ==(Vector4D left, Vector4D right) => left.Equals(right);

        public static bool operator !=(Vector4D left, Vector4D right) => !(left == right);

        public static explicit operator Vector4(Vector4D vec) => new Vector4((float) vec.X, (float) vec.Y, (float) vec.Z, (float) vec.W);

        public static explicit operator Vector4I(Vector4D vec) => new Vector4I((int) vec.X, (int) vec.Y, (int) vec.Z, (int) vec.W);

        public override string ToString() => $"({Trim(X)}, {Trim(Y)}, {Trim(Z)}, {Trim(W)})";

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj)) return false;
            return obj is Vector4D vec && Equals(vec);
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