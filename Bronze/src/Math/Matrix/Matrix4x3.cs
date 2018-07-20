using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Bronze.Math
{
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Matrix4x3 : IEquatable<Matrix4x3>
    {
        public static readonly Matrix4x3 Zero = new Matrix4x3();

        public float[,] Values { get; }

        public float[] SingleIndexedValues => new[]
        {
            Values[0, 0], Values[0, 1], Values[0, 2],
            Values[1, 0], Values[1, 1], Values[1, 2],
            Values[2, 0], Values[2, 1], Values[2, 2],
            Values[3, 0], Values[3, 1], Values[3, 2]
        };

        public static int NumRows => 4;

        public static int NumColumns => 3;

        public static int NumElements => NumRows * NumColumns;

        public Vector4 Col0
        {
            get => new Vector4(Values[0, 0], Values[1, 0], Values[2, 0], Values[3, 0]);
            private set
            {
                Values[0, 0] = value.X;
                Values[1, 0] = value.Y;
                Values[2, 0] = value.Z;
                Values[3, 0] = value.W;
            }
        }

        public Vector4 Col1
        {
            get => new Vector4(Values[0, 1], Values[1, 1], Values[2, 1], Values[3, 1]);
            private set
            {
                Values[0, 1] = value.X;
                Values[1, 1] = value.Y;
                Values[2, 1] = value.Z;
                Values[3, 1] = value.W;
            }
        }

        public Vector4 Col2
        {
            get => new Vector4(Values[0, 2], Values[1, 2], Values[2, 2], Values[3, 2]);
            private set
            {
                Values[0, 2] = value.X;
                Values[1, 2] = value.Y;
                Values[2, 2] = value.Z;
                Values[3, 2] = value.W;
            }
        }

        public Vector3 Row0
        {
            get => new Vector3(Values[0, 0], Values[0, 1], Values[0, 2]);
        }

        public Vector3 Row1
        {
            get => new Vector3(Values[1, 0], Values[1, 1], Values[1, 2]);
        }

        public Vector3 Row2
        {
            get => new Vector3(Values[2, 0], Values[2, 1], Values[2, 2]);
        }

        public Vector3 Row3
        {
            get => new Vector3(Values[3, 0], Values[3, 1], Values[3, 2]);
        }

        public Vector4[] Columns => new[] {Col0, Col1, Col2};

        public Vector3[] Rows => new[] {Row0, Row1, Row2, Row3};

        public Matrix4x3() => Values = new float[NumRows, NumColumns];

        public Matrix4x3
        (
            float e00, float e01, float e02,
            float e10, float e11, float e12,
            float e20, float e21, float e22,
            float e30, float e31, float e32
        ) : this()
        {
            Col0 = new Vector4(e00, e10, e20, e30);
            Col1 = new Vector4(e01, e11, e21, e31);
            Col2 = new Vector4(e02, e12, e22, e32);
        }

        public Matrix4x3(Vector4 col0, Vector4 col1, Vector4 col2) : this()
        {
            Col0 = col0;
            Col1 = col1;
            Col2 = col2;
        }

        public Matrix4x3(IReadOnlyList<float> matrix) : this()
        {
            if(matrix.Count != NumElements) throw new ArgumentException($"Matrix4 holds {NumElements} values, not {matrix.Count}");

            Values[0, 0] = matrix[0];
            Values[0, 1] = matrix[1];
            Values[0, 2] = matrix[2];

            Values[1, 0] = matrix[3];
            Values[1, 1] = matrix[4];
            Values[1, 2] = matrix[5];

            Values[2, 0] = matrix[6];
            Values[2, 1] = matrix[7];
            Values[2, 2] = matrix[8];

            Values[3, 0] = matrix[9];
            Values[3, 1] = matrix[10];
            Values[3, 2] = matrix[11];
        }

        public Matrix4x3(float[,] matrix) : this()
        {
            if(matrix.GetLength(0) != NumRows)
                throw new ArgumentException($"Matrix4 holds {NumRows} rows, not {matrix.GetLength(0)}");

            if(matrix.GetLength(1) != NumColumns)
                throw new ArgumentException($"Matrix4 holds {NumColumns} columns, not {matrix.GetLength(1)}");

            Values = matrix;
        }

        public Matrix3x4 Transpose => new Matrix3x4(Row0, Row1, Row2, Row3);

        public Matrix4x3 Negate() => new Matrix4x3 {Col0 = -Col0, Col1 = -Col1, Col2 = -Col2};

        public Matrix4x3 Add(Matrix4x3 mat) => new Matrix4x3 {Col0 = Col0 + mat.Col0, Col1 = Col1 + mat.Col1, Col2 = Col2 + mat.Col2};

        public Matrix4x3 Subtract(Matrix4x3 mat) => Add(mat.Negate());

        public Vector4 Multiply(Vector3 vec) => new Vector4(Row0.Dot(vec), Row1.Dot(vec), Row2.Dot(vec), Row3.Dot(vec));

        public Matrix4x3 Multiply(float scalar) => new Matrix4x3 {Col0 = Col0 * scalar, Col1 = Col1 * scalar, Col2 = Col2 * scalar};

        public Matrix4x2 Multiply(Matrix3x2 mat) => new Matrix4x2
        (
            Row0.Dot(mat.Col0), Row0.Dot(mat.Col1),
            Row1.Dot(mat.Col0), Row1.Dot(mat.Col1),
            Row2.Dot(mat.Col0), Row2.Dot(mat.Col1),
            Row3.Dot(mat.Col0), Row3.Dot(mat.Col1)
        );

        public Matrix4x3 Multiply(Matrix3 mat) => new Matrix4x3
        (
            Row0.Dot(mat.Col0), Row0.Dot(mat.Col1), Row0.Dot(mat.Col2),
            Row1.Dot(mat.Col0), Row1.Dot(mat.Col1), Row1.Dot(mat.Col2),
            Row2.Dot(mat.Col0), Row2.Dot(mat.Col1), Row2.Dot(mat.Col2),
            Row3.Dot(mat.Col0), Row3.Dot(mat.Col1), Row3.Dot(mat.Col2)
        );

        public Matrix4 Multiply(Matrix3x4 mat) => new Matrix4
        (
            Row0.Dot(mat.Col0), Row0.Dot(mat.Col1), Row0.Dot(mat.Col2), Row0.Dot(mat.Col3),
            Row1.Dot(mat.Col0), Row1.Dot(mat.Col1), Row1.Dot(mat.Col2), Row1.Dot(mat.Col3),
            Row2.Dot(mat.Col0), Row2.Dot(mat.Col1), Row2.Dot(mat.Col2), Row2.Dot(mat.Col3),
            Row3.Dot(mat.Col0), Row3.Dot(mat.Col1), Row3.Dot(mat.Col2), Row3.Dot(mat.Col3)
        );

        public static Matrix4x3 operator -(Matrix4x3 mat) => mat.Negate();

        public static Matrix4x3 operator +(Matrix4x3 left, Matrix4x3 right) => left.Add(right);

        public static Matrix4x3 operator -(Matrix4x3 left, Matrix4x3 right) => left.Subtract(right);

        public static Vector4 operator *(Matrix4x3 left, Vector3 right) => left.Multiply(right);

        public static Matrix4x3 operator *(Matrix4x3 left, float right) => left.Multiply(right);

        public static Matrix4x3 operator *(float left, Matrix4x3 right) => right.Multiply(left);

        public static Matrix4x2 operator *(Matrix4x3 left, Matrix3x2 right) => left.Multiply(right);

        public static Matrix4x3 operator *(Matrix4x3 left, Matrix3 right) => left.Multiply(right);

        public static Matrix4 operator *(Matrix4x3 left, Matrix3x4 right) => left.Multiply(right);

        public static implicit operator Matrix4x3D(Matrix4x3 mat) => new Matrix4x3D(mat.Col0, mat.Col1, mat.Col2);

        public bool Equals(Matrix4x3 mat) => Col0 == mat.Col0 && Col1 == mat.Col1 && Col2 == mat.Col2;

        public static bool operator ==(Matrix4x3 left, Matrix4x3 right) => left.Equals(right);

        public static bool operator !=(Matrix4x3 left, Matrix4x3 right) => !(left == right);

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj)) return false;
            if(ReferenceEquals(this, obj)) return true;

            return obj.GetType() == GetType() && Equals(obj as Matrix4x3);
        }

        public override string ToString() =>
            $"|{Maths.Trim(Values[0, 0])} {Maths.Trim(Values[0, 1])} {Maths.Trim(Values[0, 2])} |\n" +
            $"|{Maths.Trim(Values[1, 0])} {Maths.Trim(Values[1, 1])} {Maths.Trim(Values[1, 2])} |\n" +
            $"|{Maths.Trim(Values[2, 0])} {Maths.Trim(Values[2, 1])} {Maths.Trim(Values[2, 2])} |\n" +
            $"|{Maths.Trim(Values[3, 0])} {Maths.Trim(Values[3, 1])} {Maths.Trim(Values[3, 2])} |\n";

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode() => Values.GetHashCode();
    }
}