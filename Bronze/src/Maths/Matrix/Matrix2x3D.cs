using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Bronze.Maths
{
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Matrix2x3D : IEquatable<Matrix2x3D>
    {
        public static readonly Matrix2x3D Zero = new Matrix2x3D();

        public double[,] Values { get; }

        public double[] SingleIndexedValues => new[]
        {
            Values[0, 0], Values[0, 1], Values[0, 2],
            Values[1, 0], Values[1, 1], Values[1, 2]
        };

        public static int NumRows => 2;

        public static int NumColumns => 3;

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

        public Vector2D Col2
        {
            get => new Vector2D(Values[0, 2], Values[1, 2]);
            private set
            {
                Values[0, 2] = value.X;
                Values[1, 2] = value.Y;
            }
        }

        public Vector3D Row0
        {
            get => new Vector3D(Values[0, 0], Values[0, 1], Values[0, 2]);
        }

        public Vector3D Row1
        {
            get => new Vector3D(Values[1, 0], Values[1, 1], Values[1, 2]);
        }

        public Vector2D[] Columns => new[] {Col0, Col1, Col2};

        public Vector3D[] Rows => new[] {Row0, Row1};

        public Matrix2x3D() => Values = new double[NumRows, NumColumns];

        public Matrix2x3D
        (
            double e00, double e01, double e02,
            double e10, double e11, double e12
        ) : this()
        {
            Col0 = new Vector2D(e00, e10);
            Col1 = new Vector2D(e01, e11);
            Col2 = new Vector2D(e02, e12);
        }

        public Matrix2x3D(Vector2D col0, Vector2D col1, Vector2D col2) : this()
        {
            Col0 = col0;
            Col1 = col1;
            Col2 = col2;
        }

        public Matrix2x3D(IReadOnlyList<double> matrix) : this()
        {
            if(matrix.Count != NumElements) throw new ArgumentException($"Matrix4 holds {NumElements} values, not {matrix.Count}");

            Values[0, 0] = matrix[0];
            Values[0, 1] = matrix[1];
            Values[0, 2] = matrix[2];

            Values[1, 0] = matrix[3];
            Values[1, 1] = matrix[4];
            Values[1, 2] = matrix[5];
        }

        public Matrix2x3D(double[,] matrix) : this()
        {
            if(matrix.GetLength(0) != NumRows)
                throw new ArgumentException($"Matrix4 holds {NumRows} rows, not {matrix.GetLength(0)}");

            if(matrix.GetLength(1) != NumColumns)
                throw new ArgumentException($"Matrix4 holds {NumColumns} columns, not {matrix.GetLength(1)}");

            Values = matrix;
        }

        public Matrix3x2D Transpose => new Matrix3x2D(Row0, Row1);

        public Matrix2x3D Negate() => new Matrix2x3D {Col0 = -Col0, Col1 = -Col1, Col2 = -Col2};

        public Matrix2x3D Add(Matrix2x3D mat) => new Matrix2x3D {Col0 = Col0 + mat.Col0, Col1 = Col1 + mat.Col1, Col2 = Col2 + mat.Col2};

        public Matrix2x3D Subtract(Matrix2x3D mat) => Add(mat.Negate());

        public Vector2D Multiply(Vector3D vec) => new Vector2D(Row0.Dot(vec), Row1.Dot(vec));

        public Matrix2x3D Multiply(double scalar) => new Matrix2x3D {Col0 = Col0 * scalar, Col1 = Col1 * scalar, Col2 = Col2 * scalar};

        public Matrix2D Multiply(Matrix3x2D mat) => new Matrix2D
        (
            Row0.Dot(mat.Col0), Row0.Dot(mat.Col1),
            Row1.Dot(mat.Col0), Row1.Dot(mat.Col1)
        );

        public Matrix2x3D Multiply(Matrix3D mat) => new Matrix2x3D
        (
            Row0.Dot(mat.Col0), Row0.Dot(mat.Col1), Row0.Dot(mat.Col2),
            Row1.Dot(mat.Col0), Row1.Dot(mat.Col1), Row1.Dot(mat.Col2)
        );

        public Matrix2x4D Multiply(Matrix3x4D mat) => new Matrix2x4D
        (
            Row0.Dot(mat.Col0), Row0.Dot(mat.Col1), Row0.Dot(mat.Col2), Row0.Dot(mat.Col3),
            Row1.Dot(mat.Col0), Row1.Dot(mat.Col1), Row1.Dot(mat.Col2), Row1.Dot(mat.Col3)
        );

        public static Matrix2x3D operator -(Matrix2x3D mat) => mat.Negate();

        public static Matrix2x3D operator +(Matrix2x3D left, Matrix2x3D right) => left.Add(right);

        public static Matrix2x3D operator -(Matrix2x3D left, Matrix2x3D right) => left.Subtract(right);

        public static Vector2D operator *(Matrix2x3D left, Vector3D right) => left.Multiply(right);

        public static Matrix2x3D operator *(Matrix2x3D left, double right) => left.Multiply(right);

        public static Matrix2x3D operator *(double left, Matrix2x3D right) => right.Multiply(left);

        public static Matrix2D operator *(Matrix2x3D left, Matrix3x2D right) => left.Multiply(right);

        public static Matrix2x3D operator *(Matrix2x3D left, Matrix3D right) => left.Multiply(right);

        public static Matrix2x4D operator *(Matrix2x3D left, Matrix3x4D right) => left.Multiply(right);

        public static explicit operator Matrix2x3(Matrix2x3D mat) => new Matrix2x3((Vector2) mat.Col0, (Vector2) mat.Col1, (Vector2) mat.Col2);

        public bool Equals(Matrix2x3D mat) => Col0 == mat.Col0 && Col1 == mat.Col1 && Col2 == mat.Col2;

        public static bool operator ==(Matrix2x3D left, Matrix2x3D right) => left.Equals(right);

        public static bool operator !=(Matrix2x3D left, Matrix2x3D right) => !(left == right);

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj)) return false;
            if(ReferenceEquals(this, obj)) return true;

            return obj.GetType() == GetType() && Equals(obj as Matrix2x3D);
        }

        public override string ToString() =>
            $"|{Math.Trim(Values[0, 0])} {Math.Trim(Values[0, 1])} {Math.Trim(Values[0, 2])} |\n" +
            $"|{Math.Trim(Values[1, 0])} {Math.Trim(Values[1, 1])} {Math.Trim(Values[1, 2])} |\n";

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode() => Values.GetHashCode();
    }
}