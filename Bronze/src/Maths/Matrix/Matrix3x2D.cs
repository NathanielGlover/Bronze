using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Bronze.Maths
{
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Matrix3x2D : IEquatable<Matrix3x2D>
    {
        public static readonly Matrix3x2D Zero = new Matrix3x2D();

        public double[,] Values { get; }

        public double[] SingleIndexedValues => new[]
        {
            Values[0, 0], Values[0, 1],
            Values[1, 0], Values[1, 1],
            Values[2, 0], Values[2, 1]
        };

        public static int NumRows => 3;

        public static int NumColumns => 2;

        public static int NumElements => NumRows * NumColumns;

        public Vector3D Col0
        {
            get => new Vector3D(Values[0, 0], Values[1, 0], Values[2, 0]);
            private set
            {
                Values[0, 0] = value.X;
                Values[1, 0] = value.Y;
                Values[2, 0] = value.Z;
            }
        }

        public Vector3D Col1
        {
            get => new Vector3D(Values[0, 1], Values[1, 1], Values[2, 1]);
            private set
            {
                Values[0, 1] = value.X;
                Values[1, 1] = value.Y;
                Values[2, 1] = value.Z;
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

        public Vector2D Row2
        {
            get => new Vector2D(Values[2, 0], Values[2, 1]);
        }

        public Vector3D[] Columns => new[] {Col0, Col1};

        public Vector2D[] Rows => new[] {Row0, Row1, Row2};

        public Matrix3x2D() => Values = new double[NumRows, NumColumns];

        public Matrix3x2D
        (
            double e00, double e01,
            double e10, double e11,
            double e20, double e21
        ) : this()
        {
            Col0 = new Vector3D(e00, e10, e20);
            Col1 = new Vector3D(e01, e11, e21);
        }

        public Matrix3x2D(Vector3D col0, Vector3D col1) : this()
        {
            Col0 = col0;
            Col1 = col1;
        }

        public Matrix3x2D(IReadOnlyList<double> matrix) : this()
        {
            if(matrix.Count != NumElements) throw new ArgumentException($"Matrix4 holds {NumElements} values, not {matrix.Count}");

            Values[0, 0] = matrix[0];
            Values[0, 1] = matrix[1];

            Values[1, 0] = matrix[2];
            Values[1, 1] = matrix[3];

            Values[2, 0] = matrix[4];
            Values[2, 1] = matrix[5];
        }

        public Matrix3x2D(double[,] matrix) : this()
        {
            if(matrix.GetLength(0) != NumRows)
                throw new ArgumentException($"Matrix4 holds {NumRows} rows, not {matrix.GetLength(0)}");

            if(matrix.GetLength(1) != NumColumns)
                throw new ArgumentException($"Matrix4 holds {NumColumns} columns, not {matrix.GetLength(1)}");

            Values = matrix;
        }

        public Matrix2x3D Transpose => new Matrix2x3D(Row0, Row1, Row2);

        public Matrix3x2D Negate() => new Matrix3x2D {Col0 = -Col0, Col1 = -Col1};

        public Matrix3x2D Add(Matrix3x2D mat) => new Matrix3x2D {Col0 = Col0 + mat.Col0, Col1 = Col1 + mat.Col1};

        public Matrix3x2D Subtract(Matrix3x2D mat) => Add(mat.Negate());

        public Vector3D Multiply(Vector2D vec) => new Vector3D(Row0.Dot(vec), Row1.Dot(vec), Row2.Dot(vec));

        public Matrix3x2D Multiply(double scalar) => new Matrix3x2D {Col0 = Col0 * scalar, Col1 = Col1 * scalar};

        public Matrix3x2D Multiply(Matrix2D mat) => new Matrix3x2D
        (
            Row0.Dot(mat.Col0), Row0.Dot(mat.Col1),
            Row1.Dot(mat.Col0), Row1.Dot(mat.Col1),
            Row2.Dot(mat.Col0), Row2.Dot(mat.Col1)
        );

        public Matrix3D Multiply(Matrix2x3D mat) => new Matrix3D
        (
            Row0.Dot(mat.Col0), Row0.Dot(mat.Col1), Row0.Dot(mat.Col2),
            Row1.Dot(mat.Col0), Row1.Dot(mat.Col1), Row1.Dot(mat.Col2),
            Row2.Dot(mat.Col0), Row2.Dot(mat.Col1), Row2.Dot(mat.Col2)
        );

        public Matrix3x4D Multiply(Matrix2x4D mat) => new Matrix3x4D
        (
            Row0.Dot(mat.Col0), Row0.Dot(mat.Col1), Row0.Dot(mat.Col2), Row0.Dot(mat.Col3),
            Row1.Dot(mat.Col0), Row1.Dot(mat.Col1), Row1.Dot(mat.Col2), Row1.Dot(mat.Col3),
            Row2.Dot(mat.Col0), Row2.Dot(mat.Col1), Row2.Dot(mat.Col2), Row2.Dot(mat.Col3)
        );

        public static Matrix3x2D operator -(Matrix3x2D mat) => mat.Negate();

        public static Matrix3x2D operator +(Matrix3x2D left, Matrix3x2D right) => left.Add(right);

        public static Matrix3x2D operator -(Matrix3x2D left, Matrix3x2D right) => left.Subtract(right);

        public static Vector3D operator *(Matrix3x2D left, Vector2D right) => left.Multiply(right);

        public static Matrix3x2D operator *(Matrix3x2D left, double right) => left.Multiply(right);

        public static Matrix3x2D operator *(double left, Matrix3x2D right) => right.Multiply(left);

        public static Matrix3x2D operator *(Matrix3x2D left, Matrix2D right) => left.Multiply(right);

        public static Matrix3D operator *(Matrix3x2D left, Matrix2x3D right) => left.Multiply(right);

        public static Matrix3x4D operator *(Matrix3x2D left, Matrix2x4D right) => left.Multiply(right);

        public static explicit operator Matrix3x2(Matrix3x2D mat) => new Matrix3x2((Vector3) mat.Col0, (Vector3) mat.Col1);

        public bool Equals(Matrix3x2D mat) => Col0 == mat.Col0 && Col1 == mat.Col1;

        public static bool operator ==(Matrix3x2D left, Matrix3x2D right) => left.Equals(right);

        public static bool operator !=(Matrix3x2D left, Matrix3x2D right) => !(left == right);

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj)) return false;
            if(ReferenceEquals(this, obj)) return true;

            return obj.GetType() == GetType() && Equals(obj as Matrix3x2D);
        }

        public override string ToString() =>
            $"|{Math.Trim(Values[0, 0])} {Math.Trim(Values[0, 1])} |\n" +
            $"|{Math.Trim(Values[1, 0])} {Math.Trim(Values[1, 1])} |\n" +
            $"|{Math.Trim(Values[2, 0])} {Math.Trim(Values[2, 1])} |\n";

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode() => Values.GetHashCode();
    }
}