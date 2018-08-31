using System;
using System.Diagnostics.CodeAnalysis;
using static Bronze.Maths.Math;

namespace Bronze.Maths
{
    public struct Vector3I : IEquatable<Vector3I>
    {
        public static readonly Vector3I Zero = new Vector3I();

        public static readonly Vector3I BasisX = new Vector3I {X = 1};

        public static readonly Vector3I BasisY = new Vector3I {Y = 1};

        public static readonly Vector3I BasisZ = new Vector3I {Z = 1};

        public static int Size => 3;

        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }

        public int[] Values => new[] {X, Y, Z};

        public double Magnitude => Sqrt(X * X + Y * Y + Z * Z);

        public Vector3I(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3I ComponentWiseMultiply(Vector3I vec) => new Vector3I(X * vec.X, Y * vec.Y, Z * vec.Z);

        public int Dot(Vector3I vec) => X * vec.X + Y * vec.Y + Z * vec.Z;

        public Vector3I Cross(Vector3I vec) => new Vector3I(Y * vec.Z - Z * vec.Y, Z * vec.X - X * vec.Z, X * vec.Y - Y * vec.X);

        public Vector3I Negate() => new Vector3I(-X, -Y, -Z);

        public Vector3I Add(Vector3I vec) => new Vector3I(X + vec.X, Y + vec.Y, Z + vec.Z);

        public Vector3I Subtract(Vector3I vec) => new Vector3I(X - vec.X, Y - vec.Y, Z - vec.Z);

        public Vector3I Multiply(int scalar) => new Vector3I(X * scalar, Y * scalar, Z * scalar);

        public bool Equals(Vector3I vec) => EqualsWithTolerance(X, vec.X) && EqualsWithTolerance(Y, vec.Y) && EqualsWithTolerance(Z, vec.Z);

        public static Vector3I operator -(Vector3I vec) => vec.Negate();

        public static Vector3I operator +(Vector3I left, Vector3I right) => left.Add(right);

        public static Vector3I operator -(Vector3I left, Vector3I right) => left.Subtract(right);

        public static Vector3I operator *(Vector3I left, int right) => left.Multiply(right);

        public static Vector3I operator *(int left, Vector3I right) => right.Multiply(left);

        public static bool operator ==(Vector3I left, Vector3I right) => left.Equals(right);

        public static bool operator !=(Vector3I left, Vector3I right) => !(left == right);

        public static implicit operator Vector3(Vector3I vec) => new Vector3(vec.X, vec.Y, vec.Z);

        public static implicit operator Vector3D(Vector3I vec) => new Vector3D(vec.X, vec.Y, vec.Z);

        public override string ToString() => $"({Trim(X)}, {Trim(Y)}, {Trim(Z)})";

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj)) return false;
            return obj is Vector3I vec && Equals(vec);
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