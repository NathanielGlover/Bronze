using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Bronze.Math
{
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Matrix2x4 : IEquatable<Matrix2x4>
    {
        public static readonly Matrix2x4 Zero = new Matrix2x4();

        public float[,] Values { get; }

        public float[] SingleIndexedValues => new[]
        {
            Values[0, 0], Values[0, 1], Values[0, 2], Values[0, 3],
            Values[1, 0], Values[1, 1], Values[1, 2], Values[1, 3]
        };

        public static int NumRows => 2;

        public static int NumColumns => 4;

        public static int NumElements => NumRows * NumColumns;

        public Vector2 Col0
        {
            get => new Vector2(Values[0, 0], Values[1, 0]);
            private set
            {
                Values[0, 0] = value.X;
                Values[1, 0] = value.Y;
            }
        }

        public Vector2 Col1
        {
            get => new Vector2(Values[0, 1], Values[1, 1]);
            private set
            {
                Values[0, 1] = value.X;
                Values[1, 1] = value.Y;
            }
        }

        public Vector2 Col2
        {
            get => new Vector2(Values[0, 2], Values[1, 2]);
            private set
            {
                Values[0, 2] = value.X;
                Values[1, 2] = value.Y;
            }
        }

        public Vector2 Col3
        {
            get => new Vector2(Values[0, 3], Values[1, 3]);
            private set
            {
                Values[0, 3] = value.X;
                Values[1, 3] = value.Y;
            }
        }

        public Vector4 Row0
        {
            get => new Vector4(Values[0, 0], Values[0, 1], Values[0, 2], Values[0, 3]);
        }

        public Vector4 Row1
        {
            get => new Vector4(Values[1, 0], Values[1, 1], Values[1, 2], Values[1, 3]);
        }

        public Vector2[] Columns => new[] {Col0, Col1, Col2, Col3};

        public Vector4[] Rows => new[] {Row0, Row1};

        public Matrix2x4() => Values = new float[NumRows, NumColumns];

        public Matrix2x4
        (
            float e00, float e01, float e02, float e03,
            float e10, float e11, float e12, float e13
        ) : this()
        {
            Col0 = new Vector2(e00, e10);
            Col1 = new Vector2(e01, e11);
            Col2 = new Vector2(e02, e12);
            Col3 = new Vector2(e03, e13);
        }

        public Matrix2x4(Vector2 col0, Vector2 col1, Vector2 col2, Vector2 col3) : this()
        {
            Col0 = col0;
            Col1 = col1;
            Col2 = col2;
            Col3 = col3;
        }

        public Matrix2x4(IReadOnlyList<float> matrix) : this()
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

        public Matrix2x4(float[,] matrix) : this()
        {
            if(matrix.GetLength(0) != NumRows)
                throw new ArgumentException($"Matrix4 holds {NumRows} rows, not {matrix.GetLength(0)}");

            if(matrix.GetLength(1) != NumColumns)
                throw new ArgumentException($"Matrix4 holds {NumColumns} columns, not {matrix.GetLength(1)}");

            Values = matrix;
        }

        public Matrix4x2 Transpose => new Matrix4x2(Row0, Row1);

        public Matrix2x4 Negate() => new Matrix2x4 {Col0 = -Col0, Col1 = -Col1, Col2 = -Col2, Col3 = -Col3};

        public Matrix2x4 Add(Matrix2x4 mat) => new Matrix2x4
            {Col0 = Col0 + mat.Col0, Col1 = Col1 + mat.Col1, Col2 = Col2 + mat.Col2, Col3 = Col3 + mat.Col3};

        public Matrix2x4 Subtract(Matrix2x4 mat) => Add(mat.Negate());

        public Vector2 Multiply(Vector4 vec) => new Vector2(Row0.Dot(vec), Row1.Dot(vec));

        public Matrix2x4 Multiply(float scalar) => new Matrix2x4 {Col0 = Col0 * scalar, Col1 = Col1 * scalar, Col2 = Col2 * scalar, Col3 = Col3 * scalar};

        public Matrix2 Multiply(Matrix4x2 mat) => new Matrix2
        (
            Row0.Dot(mat.Col0), Row0.Dot(mat.Col1),
            Row1.Dot(mat.Col0), Row1.Dot(mat.Col1)
        );

        public Matrix2x3 Multiply(Matrix4x3 mat) => new Matrix2x3
        (
            Row0.Dot(mat.Col0), Row0.Dot(mat.Col1), Row0.Dot(mat.Col2),
            Row1.Dot(mat.Col0), Row1.Dot(mat.Col1), Row1.Dot(mat.Col2)
        );

        public Matrix2x4 Multiply(Matrix4 mat) => new Matrix2x4
        (
            Row0.Dot(mat.Col0), Row0.Dot(mat.Col1), Row0.Dot(mat.Col2), Row0.Dot(mat.Col3),
            Row1.Dot(mat.Col0), Row1.Dot(mat.Col1), Row1.Dot(mat.Col2), Row1.Dot(mat.Col3)
        );

        public static Matrix2x4 operator -(Matrix2x4 mat) => mat.Negate();

        public static Matrix2x4 operator +(Matrix2x4 left, Matrix2x4 right) => left.Add(right);

        public static Matrix2x4 operator -(Matrix2x4 left, Matrix2x4 right) => left.Subtract(right);

        public static Vector2 operator *(Matrix2x4 left, Vector4 right) => left.Multiply(right);

        public static Matrix2x4 operator *(Matrix2x4 left, float right) => left.Multiply(right);

        public static Matrix2x4 operator *(float left, Matrix2x4 right) => right.Multiply(left);

        public static Matrix2 operator *(Matrix2x4 left, Matrix4x2 right) => left.Multiply(right);

        public static Matrix2x3 operator *(Matrix2x4 left, Matrix4x3 right) => left.Multiply(right);

        public static Matrix2x4 operator *(Matrix2x4 left, Matrix4 right) => left.Multiply(right);

        public static implicit operator Matrix2x4D(Matrix2x4 mat) => new Matrix2x4D(mat.Col0, mat.Col1, mat.Col2, mat.Col3);

        public bool Equals(Matrix2x4 mat) => Col0 == mat.Col0 && Col1 == mat.Col1 && Col2 == mat.Col2 && Col3 == mat.Col3;

        public static bool operator ==(Matrix2x4 left, Matrix2x4 right) => left.Equals(right);

        public static bool operator !=(Matrix2x4 left, Matrix2x4 right) => !(left == right);

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj)) return false;
            if(ReferenceEquals(this, obj)) return true;

            return obj.GetType() == GetType() && Equals(obj as Matrix2x4);
        }

        public override string ToString() =>
            $"|{Math.Trim(Values[0, 0])} {Math.Trim(Values[0, 1])} {Math.Trim(Values[0, 2])} {Math.Trim(Values[0, 3])} |\n" +
            $"|{Math.Trim(Values[1, 0])} {Math.Trim(Values[1, 1])} {Math.Trim(Values[1, 2])} {Math.Trim(Values[1, 3])} |\n";

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode() => Values.GetHashCode();
    }
}