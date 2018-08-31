using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Bronze.Maths
{
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Matrix2D : IEquatable<Matrix2D>
    {
        public static readonly Matrix2D Zero = new Matrix2D();

        public static readonly Matrix2D Identity = new Matrix2D(1);

        public double[,] Values { get; }

        public double[] SingleIndexedValues => new[]
        {
            Values[0, 0], Values[0, 1],
            Values[1, 0], Values[1, 1]
        };

        public static int NumRows => 2;

        public static int NumColumns => 2;

        public static int NumElements => NumRows * NumColumns;

        public Vector2D Col0
        {
            get => new Vector2D(Values[0, 0], Values[1, 0]);
            private set
            {
                Values[0, 0] = value.X;
                Values[1, 0] = value.Y;
            }
        }

        public Vector2D Col1
        {
            get => new Vector2D(Values[0, 1], Values[1, 1]);
            private set
            {
                Values[0, 1] = value.X;
                Values[1, 1] = value.Y;
            }
        }

        public Vector2D Row0
        {
            get => new Vector2D(Values[0, 0], Values[0, 1]);
        }

        public Vector2D Row1
        {
            get => new Vector2D(Values[1, 0], Values[1, 1]);
        }

        public Vector2D[] Columns => new[] {Col0, Col1};

        public Vector2D[] Rows => new[] {Row0, Row1};

        public Matrix2D() => Values = new double[NumRows, NumColumns];

        public Matrix2D(double diagonalValue) : this() => Values[0, 0] = Values[1, 1] = diagonalValue;

        public Matrix2D
        (
            double e00, double e01,
            double e10, double e11
        ) : this()
        {
            Col0 = new Vector2D(e00, e10);
            Col1 = new Vector2D(e01, e11);
        }

        public Matrix2D(Vector2D col0, Vector2D col1) : this()
        {
            Col0 = col0;
            Col1 = col1;
        }

        public Matrix2D(IReadOnlyList<double> matrix) : this()
        {
            if(matrix.Count != NumElements) throw new ArgumentException($"Matrix4 holds {NumElements} values, not {matrix.Count}");

            Values[0, 0] = matrix[0];
            Values[0, 1] = matrix[1];

            Values[1, 0] = matrix[2];
            Values[1, 1] = matrix[3];
        }

        public Matrix2D(double[,] matrix) : this()
        {
            if(matrix.GetLength(0) != NumRows)
                throw new ArgumentException($"Matrix4 holds {NumRows} rows, not {matrix.GetLength(0)}");

            if(matrix.GetLength(1) != NumColumns)
                throw new ArgumentException($"Matrix4 holds {NumColumns} columns, not {matrix.GetLength(1)}");

            Values = matrix;
        }

        public Matrix2D Inverse
        {
            get
            {
                double det = Determinant;
                return IsSingular
                    ? throw new DivideByZeroException("Singular matrices cannot have inverses")
                    : new Matrix2D(Values[1, 1], -Values[0, 1], -Values[1, 0], Values[0, 0]) * (1.0f / det);
            }
        }

        public double Determinant => Values[0, 0] * Values[1, 1] - Values[0, 1] * Values[1, 0];

        public bool IsSingular => Math.EqualsWithTolerance(Determinant, 0);

        public double Trace => Values[0, 0] + Values[1, 1];

        public Matrix2D Transpose => new Matrix2D(Row0, Row1);

        public Matrix2D Negate() => new Matrix2D {Col0 = -Col0, Col1 = -Col1};

        public Matrix2D Add(Matrix2D mat) => new Matrix2D {Col0 = Col0 + mat.Col0, Col1 = Col1 + mat.Col1};

        public Matrix2D Subtract(Matrix2D mat) => Add(mat.Negate());

        public Vector2D Multiply(Vector2D vec) => new Vector2D(Row0.Dot(vec), Row1.Dot(vec));

        public Matrix2D Multiply(double scalar) => new Matrix2D {Col0 = Col0 * scalar, Col1 = Col1 * scalar};

        public Matrix2D Multiply(Matrix2D mat) => new Matrix2D
        (
            Row0.Dot(mat.Col0), Row0.Dot(mat.Col1),
            Row1.Dot(mat.Col0), Row1.Dot(mat.Col1)
        );

        public Matrix2x3D Multiply(Matrix2x3D mat) => new Matrix2x3D
        (
            Row0.Dot(mat.Col0), Row0.Dot(mat.Col1), Row0.Dot(mat.Col2),
            Row1.Dot(mat.Col0), Row1.Dot(mat.Col1), Row1.Dot(mat.Col2)
        );

        public Matrix2x4D Multiply(Matrix2x4D mat) => new Matrix2x4D
        (
            Row0.Dot(mat.Col0), Row0.Dot(mat.Col1), Row0.Dot(mat.Col2), Row0.Dot(mat.Col3),
            Row1.Dot(mat.Col0), Row1.Dot(mat.Col1), Row1.Dot(mat.Col2), Row1.Dot(mat.Col3)
        );

        public static Matrix2D operator -(Matrix2D mat) => mat.Negate();

        public static Matrix2D operator +(Matrix2D left, Matrix2D right) => left.Add(right);

        public static Matrix2D operator -(Matrix2D left, Matrix2D right) => left.Subtract(right);

        public static Vector2D operator *(Matrix2D left, Vector2D right) => left.Multiply(right);

        public static Matrix2D operator *(Matrix2D left, double right) => left.Multiply(right);

        public static Matrix2D operator *(double left, Matrix2D right) => right.Multiply(left);

        public static Matrix2D operator *(Matrix2D left, Matrix2D right) => left.Multiply(right);

        public static Matrix2x3D operator *(Matrix2D left, Matrix2x3D right) => left.Multiply(right);

        public static Matrix2x4D operator *(Matrix2D left, Matrix2x4D right) => left.Multiply(right);

        public static explicit operator Matrix2(Matrix2D mat) => new Matrix2((Vector2) mat.Col0, (Vector2) mat.Col1);

        public bool Equals(Matrix2D mat) => Col0 == mat.Col0 && Col1 == mat.Col1;

        public static bool operator ==(Matrix2D left, Matrix2D right) => left.Equals(right);

        public static bool operator !=(Matrix2D left, Matrix2D right) => !(left == right);

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj)) return false;
            if(ReferenceEquals(this, obj)) return true;

            return obj.GetType() == GetType() && Equals(obj as Matrix2D);
        }

        public override string ToString() =>
            $"|{Math.Trim(Values[0, 0])} {Math.Trim(Values[0, 1])} |\n" +
            $"|{Math.Trim(Values[1, 0])} {Math.Trim(Values[1, 1])} |\n";

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode() => Values.GetHashCode();
    }
}