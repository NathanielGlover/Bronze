using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Bronze.Math
{
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Matrix3x2 : IEquatable<Matrix3x2>
    {
        public static readonly Matrix3x2 Zero = new Matrix3x2();

        public float[,] Values { get; }

        public float[] SingleIndexedValues => new[]
        {
            Values[0, 0], Values[0, 1],
            Values[1, 0], Values[1, 1],
            Values[2, 0], Values[2, 1]
        };

        public static int NumRows => 3;

        public static int NumColumns => 2;

        public static int NumElements => NumRows * NumColumns;

        public Vector3 Col0
        {
            get => new Vector3(Values[0, 0], Values[1, 0], Values[2, 0]);
            private set
            {
                Values[0, 0] = value.X;
                Values[1, 0] = value.Y;
                Values[2, 0] = value.Z;
            }
        }

        public Vector3 Col1
        {
            get => new Vector3(Values[0, 1], Values[1, 1], Values[2, 1]);
            private set
            {
                Values[0, 1] = value.X;
                Values[1, 1] = value.Y;
                Values[2, 1] = value.Z;
            }
        }

        public Vector2 Row0
        {
            get => new Vector2(Values[0, 0], Values[0, 1]);
        }

        public Vector2 Row1
        {
            get => new Vector2(Values[1, 0], Values[1, 1]);
        }

        public Vector2 Row2
        {
            get => new Vector2(Values[2, 0], Values[2, 1]);
        }

        public Vector3[] Columns => new[] {Col0, Col1};

        public Vector2[] Rows => new[] {Row0, Row1, Row2};

        public Matrix3x2() => Values = new float[NumRows, NumColumns];

        public Matrix3x2
        (
            float e00, float e01,
            float e10, float e11,
            float e20, float e21
        ) : this()
        {
            Col0 = new Vector3(e00, e10, e20);
            Col1 = new Vector3(e01, e11, e21);
        }

        public Matrix3x2(Vector3 col0, Vector3 col1) : this()
        {
            Col0 = col0;
            Col1 = col1;
        }

        public Matrix3x2(IReadOnlyList<float> matrix) : this()
        {
            if(matrix.Count != NumElements) throw new ArgumentException($"Matrix4 holds {NumElements} values, not {matrix.Count}");

            Values[0, 0] = matrix[0];
            Values[0, 1] = matrix[1];

            Values[1, 0] = matrix[2];
            Values[1, 1] = matrix[3];

            Values[2, 0] = matrix[4];
            Values[2, 1] = matrix[5];
        }

        public Matrix3x2(float[,] matrix) : this()
        {
            if(matrix.GetLength(0) != NumRows)
                throw new ArgumentException($"Matrix4 holds {NumRows} rows, not {matrix.GetLength(0)}");

            if(matrix.GetLength(1) != NumColumns)
                throw new ArgumentException($"Matrix4 holds {NumColumns} columns, not {matrix.GetLength(1)}");

            Values = matrix;
        }

        public Matrix2x3 Transpose => new Matrix2x3(Row0, Row1, Row2);

        public Matrix3x2 Negate() => new Matrix3x2 {Col0 = -Col0, Col1 = -Col1};

        public Matrix3x2 Add(Matrix3x2 mat) => new Matrix3x2 {Col0 = Col0 + mat.Col0, Col1 = Col1 + mat.Col1};

        public Matrix3x2 Subtract(Matrix3x2 mat) => Add(mat.Negate());

        public Vector3 Multiply(Vector2 vec) => new Vector3(Row0.Dot(vec), Row1.Dot(vec), Row2.Dot(vec));

        public Matrix3x2 Multiply(float scalar) => new Matrix3x2 {Col0 = Col0 * scalar, Col1 = Col1 * scalar};

        public Matrix3x2 Multiply(Matrix2 mat) => new Matrix3x2
        (
            Row0.Dot(mat.Col0), Row0.Dot(mat.Col1),
            Row1.Dot(mat.Col0), Row1.Dot(mat.Col1),
            Row2.Dot(mat.Col0), Row2.Dot(mat.Col1)
        );

        public Matrix3 Multiply(Matrix2x3 mat) => new Matrix3
        (
            Row0.Dot(mat.Col0), Row0.Dot(mat.Col1), Row0.Dot(mat.Col2),
            Row1.Dot(mat.Col0), Row1.Dot(mat.Col1), Row1.Dot(mat.Col2),
            Row2.Dot(mat.Col0), Row2.Dot(mat.Col1), Row2.Dot(mat.Col2)
        );

        public Matrix3x4 Multiply(Matrix2x4 mat) => new Matrix3x4
        (
            Row0.Dot(mat.Col0), Row0.Dot(mat.Col1), Row0.Dot(mat.Col2), Row0.Dot(mat.Col3),
            Row1.Dot(mat.Col0), Row1.Dot(mat.Col1), Row1.Dot(mat.Col2), Row1.Dot(mat.Col3),
            Row2.Dot(mat.Col0), Row2.Dot(mat.Col1), Row2.Dot(mat.Col2), Row2.Dot(mat.Col3)
        );

        public static Matrix3x2 operator -(Matrix3x2 mat) => mat.Negate();

        public static Matrix3x2 operator +(Matrix3x2 left, Matrix3x2 right) => left.Add(right);

        public static Matrix3x2 operator -(Matrix3x2 left, Matrix3x2 right) => left.Subtract(right);

        public static Vector3 operator *(Matrix3x2 left, Vector2 right) => left.Multiply(right);

        public static Matrix3x2 operator *(Matrix3x2 left, float right) => left.Multiply(right);

        public static Matrix3x2 operator *(float left, Matrix3x2 right) => right.Multiply(left);

        public static Matrix3x2 operator *(Matrix3x2 left, Matrix2 right) => left.Multiply(right);

        public static Matrix3 operator *(Matrix3x2 left, Matrix2x3 right) => left.Multiply(right);

        public static Matrix3x4 operator *(Matrix3x2 left, Matrix2x4 right) => left.Multiply(right);

        public static implicit operator Matrix3x2D(Matrix3x2 mat) => new Matrix3x2D(mat.Col0, mat.Col1);

        public bool Equals(Matrix3x2 mat) => Col0 == mat.Col0 && Col1 == mat.Col1;

        public static bool operator ==(Matrix3x2 left, Matrix3x2 right) => left.Equals(right);

        public static bool operator !=(Matrix3x2 left, Matrix3x2 right) => !(left == right);

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj)) return false;
            if(ReferenceEquals(this, obj)) return true;

            return obj.GetType() == GetType() && Equals(obj as Matrix3x2);
        }

        public override string ToString() =>
            $"|{Maths.Trim(Values[0, 0])} {Maths.Trim(Values[0, 1])} |\n" +
            $"|{Maths.Trim(Values[1, 0])} {Maths.Trim(Values[1, 1])} |\n" +
            $"|{Maths.Trim(Values[2, 0])} {Maths.Trim(Values[2, 1])} |\n";

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode() => Values.GetHashCode();
    }
}