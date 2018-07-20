using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Bronze.Math
{
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Matrix2x4D : IEquatable<Matrix2x4D>
    {
        public static readonly Matrix2x4D Zero = new Matrix2x4D();

        public double[,] Values { get; }

        public double[] SingleIndexedValues => new[]
        {
            Values[0, 0], Values[0, 1], Values[0, 2], Values[0, 3],
            Values[1, 0], Values[1, 1], Values[1, 2], Values[1, 3]
        };

        public static int NumRows => 2;

        public static int NumColumns => 4;

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

        public Vector2D Col3
        {
            get => new Vector2D(Values[0, 3], Values[1, 3]);
            private set
            {
                Values[0, 3] = value.X;
                Values[1, 3] = value.Y;
            }
        }

        public Vector4D Row0
        {
            get => new Vector4D(Values[0, 0], Values[0, 1], Values[0, 2], Values[0, 3]);
        }

        public Vector4D Row1
        {
            get => new Vector4D(Values[1, 0], Values[1, 1], Values[1, 2], Values[1, 3]);
        }

        public Vector2D[] Columns => new[] {Col0, Col1, Col2, Col3};

        public Vector4D[] Rows => new[] {Row0, Row1};

        public Matrix2x4D() => Values = new double[NumRows, NumColumns];

        public Matrix2x4D
        (
            double e00, double e01, double e02, double e03,
            double e10, double e11, double e12, double e13
        ) : this()
        {
            Col0 = new Vector2D(e00, e10);
            Col1 = new Vector2D(e01, e11);
            Col2 = new Vector2D(e02, e12);
            Col3 = new Vector2D(e03, e13);
        }

        public Matrix2x4D(Vector2D col0, Vector2D col1, Vector2D col2, Vector2D col3) : this()
        {
            Col0 = col0;
            Col1 = col1;
            Col2 = col2;
            Col3 = col3;
        }

        public Matrix2x4D(IReadOnlyList<double> matrix) : this()
        {
            if(matrix.Count != NumElements) throw new ArgumentException($"Matrix4 holds {NumElements} values, not {matrix.Count}");

            Values[0, 0] = matrix[0];
            Values[0, 1] = matrix[1];
            Values[0, 2] = matrix[2];
            Values[0, 3] = matrix[3];

            Values[1, 0] = matrix[4];
            Values[1, 1] = matrix[5];
            Values[1, 2] = matrix[6];
            Values[1, 3] = matrix[7];
        }

        public Matrix2x4D(double[,] matrix) : this()
        {
            if(matrix.GetLength(0) != NumRows)
                throw new ArgumentException($"Matrix4 holds {NumRows} rows, not {matrix.GetLength(0)}");

            if(matrix.GetLength(1) != NumColumns)
                throw new ArgumentException($"Matrix4 holds {NumColumns} columns, not {matrix.GetLength(1)}");

            Values = matrix;
        }

        public Matrix4x2D Transpose => new Matrix4x2D(Row0, Row1);

        public Matrix2x4D Negate() => new Matrix2x4D {Col0 = -Col0, Col1 = -Col1, Col2 = -Col2, Col3 = -Col3};

        public Matrix2x4D Add(Matrix2x4D mat) => new Matrix2x4D
            {Col0 = Col0 + mat.Col0, Col1 = Col1 + mat.Col1, Col2 = Col2 + mat.Col2, Col3 = Col3 + mat.Col3};

        public Matrix2x4D Subtract(Matrix2x4D mat) => Add(mat.Negate());

        public Vector2D Multiply(Vector4D vec) => new Vector2D(Row0.Dot(vec), Row1.Dot(vec));

        public Matrix2x4D Multiply(double scalar) => new Matrix2x4D
            {Col0 = Col0 * scalar, Col1 = Col1 * scalar, Col2 = Col2 * scalar, Col3 = Col3 * scalar};

        public Matrix2D Multiply(Matrix4x2D mat) => new Matrix2D
        (
            Row0.Dot(mat.Col0), Row0.Dot(mat.Col1),
            Row1.Dot(mat.Col0), Row1.Dot(mat.Col1)
        );

        public Matrix2x3D Multiply(Matrix4x3D mat) => new Matrix2x3D
        (
            Row0.Dot(mat.Col0), Row0.Dot(mat.Col1), Row0.Dot(mat.Col2),
            Row1.Dot(mat.Col0), Row1.Dot(mat.Col1), Row1.Dot(mat.Col2)
        );

        public Matrix2x4D Multiply(Matrix4D mat) => new Matrix2x4D
        (
            Row0.Dot(mat.Col0), Row0.Dot(mat.Col1), Row0.Dot(mat.Col2), Row0.Dot(mat.Col3),
            Row1.Dot(mat.Col0), Row1.Dot(mat.Col1), Row1.Dot(mat.Col2), Row1.Dot(mat.Col3)
        );

        public static Matrix2x4D operator -(Matrix2x4D mat) => mat.Negate();

        public static Matrix2x4D operator +(Matrix2x4D left, Matrix2x4D right) => left.Add(right);

        public static Matrix2x4D operator -(Matrix2x4D left, Matrix2x4D right) => left.Subtract(right);

        public static Vector2D operator *(Matrix2x4D left, Vector4D right) => left.Multiply(right);

        public static Matrix2x4D operator *(Matrix2x4D left, double right) => left.Multiply(right);

        public static Matrix2x4D operator *(double left, Matrix2x4D right) => right.Multiply(left);

        public static Matrix2D operator *(Matrix2x4D left, Matrix4x2D right) => left.Multiply(right);

        public static Matrix2x3D operator *(Matrix2x4D left, Matrix4x3D right) => left.Multiply(right);

        public static Matrix2x4D operator *(Matrix2x4D left, Matrix4D right) => left.Multiply(right);

        public static explicit operator Matrix2x4(Matrix2x4D mat) =>
            new Matrix2x4((Vector2) mat.Col0, (Vector2) mat.Col1, (Vector2) mat.Col2, (Vector2) mat.Col3);

        public bool Equals(Matrix2x4D mat) => Col0 == mat.Col0 && Col1 == mat.Col1 && Col2 == mat.Col2 && Col3 == mat.Col3;

        public static bool operator ==(Matrix2x4D left, Matrix2x4D right) => left.Equals(right);

        public static bool operator !=(Matrix2x4D left, Matrix2x4D right) => !(left == right);

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj)) return false;
            if(ReferenceEquals(this, obj)) return true;

            return obj.GetType() == GetType() && Equals(obj as Matrix2x4D);
        }

        public override string ToString() =>
            $"|{Maths.Trim(Values[0, 0])} {Maths.Trim(Values[0, 1])} {Maths.Trim(Values[0, 2])} {Maths.Trim(Values[0, 3])} |\n" +
            $"|{Maths.Trim(Values[1, 0])} {Maths.Trim(Values[1, 1])} {Maths.Trim(Values[1, 2])} {Maths.Trim(Values[1, 3])} |\n";

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode() => Values.GetHashCode();
    }
}