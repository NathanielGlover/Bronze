using System;
using System.Diagnostics.CodeAnalysis;
using static Bronze.Maths.Math;

namespace Bronze.Maths
{
    public struct Vector4I : IEquatable<Vector4I>
    {
        public static readonly Vector4I Zero = new Vector4I();

        public static readonly Vector4I BasisX = new Vector4I {X = 1};

        public static readonly Vector4I BasisY = new Vector4I {Y = 1};

        public static readonly Vector4I BasisZ = new Vector4I {Z = 1};

        public static readonly Vector4I BasisW = new Vector4I {W = 1};

        public static int Size => 4;

        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }

        public int W { get; set; }

        public int[] Values => new[] {X, Y, Z, W};

        public double Magnitude => Sqrt(X * X + Y * Y + Z * Z + W * W);

        public Vector4I(int x, int y, int z, int w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public Vector4I ComponentWiseMultiply(Vector4I vec) => new Vector4I(X * vec.X, Y * vec.Y, Z * vec.Z, W * vec.W);

        public int Dot(Vector4I vec) => X * vec.X + Y * vec.Y + Z * vec.Z + W * vec.W;

        public Vector4I Negate() => new Vector4I(-X, -Y, -Z, -W);

        public Vector4I Add(Vector4I vec) => new Vector4I(X + vec.X, Y + vec.Y, Z + vec.Z, W + vec.W);

        public Vector4I Subtract(Vector4I vec) => new Vector4I(X - vec.X, Y - vec.Y, Z - vec.Z, W - vec.W);

        public Vector4I Multiply(int scalar) => new Vector4I(X * scalar, Y * scalar, Z * scalar, W * scalar);

        public bool Equals(Vector4I vec) => EqualsWithTolerance(X, vec.X) && EqualsWithTolerance(Y, vec.Y) && EqualsWithTolerance(Z, vec.Z) &&
                                            EqualsWithTolerance(W, vec.W);

        public static Vector4I operator -(Vector4I vec) => vec.Negate();

        public static Vector4I operator +(Vector4I left, Vector4I right) => left.Add(right);

        public static Vector4I operator -(Vector4I left, Vector4I right) => left.Subtract(right);

        public static Vector4I operator *(Vector4I left, int right) => left.Multiply(right);

        public static Vector4I operator *(int left, Vector4I right) => right.Multiply(left);

        public static bool operator ==(Vector4I left, Vector4I right) => left.Equals(right);

        public static bool operator !=(Vector4I left, Vector4I right) => !(left == right);

        public static implicit operator Vector4(Vector4I vec) => new Vector4(vec.X, vec.Y, vec.Z, vec.W);

        public static implicit operator Vector4D(Vector4I vec) => new Vector4D(vec.X, vec.Y, vec.Z, vec.W);

        public override string ToString() => $"({Trim(X)}, {Trim(Y)}, {Trim(Z)}, {Trim(W)})";

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj)) return false;
            return obj is Vector4I vec && Equals(vec);
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