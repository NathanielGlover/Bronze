using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Bronze.Maths
{
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Matrix3 : IEquatable<Matrix3>
    {
        public static readonly Matrix3 Zero = new Matrix3();

        public static readonly Matrix3 Identity = new Matrix3(1);

        public float[,] Values { get; }

        public float[] SingleIndexedValues => new[]
        {
            Values[0, 0], Values[0, 1], Values[0, 2],
            Values[1, 0], Values[1, 1], Values[1, 2],
            Values[2, 0], Values[2, 1], Values[2, 2]
        };

        public static int NumRows => 3;

        public static int NumColumns => 3;

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

        public Vector3 Col2
        {
            get => new Vector3(Values[0, 2], Values[1, 2], Values[2, 2]);
            private set
            {
                Values[0, 2] = value.X;
                Values[1, 2] = value.Y;
                Values[2, 2] = value.Z;
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

        public Vector3[] Columns => new[] {Col0, Col1, Col2};

        public Vector3[] Rows => new[] {Row0, Row1, Row2};

        public Matrix3() => Values = new float[NumRows, NumColumns];

        public Matrix3(float diagonalValue) : this() => Values[0, 0] = Values[1, 1] = Values[2, 2] = diagonalValue;

        public Matrix3
        (
            float e00, float e01, float e02,
            float e10, float e11, float e12,
            float e20, float e21, float e22
        ) : this()
        {
            Col0 = new Vector3(e00, e10, e20);
            Col1 = new Vector3(e01, e11, e21);
            Col2 = new Vector3(e02, e12, e22);
        }

        public Matrix3(Vector3 col0, Vector3 col1, Vector3 col2) : this()
        {
            Col0 = col0;
            Col1 = col1;
            Col2 = col2;
        }

        public Matrix3(IReadOnlyList<float> matrix) : this()
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
        }

        public Matrix3(float[,] matrix) : this()
        {
            if(matrix.GetLength(0) != NumRows)
                throw new ArgumentException($"Matrix4 holds {NumRows} rows, not {matrix.GetLength(0)}");

            if(matrix.GetLength(1) != NumColumns)
                throw new ArgumentException($"Matrix4 holds {NumColumns} columns, not {matrix.GetLength(1)}");

            Values = matrix;
        }

        public Matrix3 Inverse
        {
            get
            {
                if(IsSingular) throw new DivideByZeroException("Singular matrices cannot have inverses");

                var minor1 = new Matrix2(Values[1, 1], Values[1, 2], Values[2, 1], Values[2, 2]);
                var minor2 = new Matrix2(Values[1, 0], Values[1, 2], Values[2, 0], Values[2, 2]);
                var minor3 = new Matrix2(Values[1, 0], Values[1, 1], Values[2, 0], Values[2, 1]);

                var minor4 = new Matrix2(Values[0, 1], Values[0, 2], Values[2, 1], Values[2, 2]);
                var minor5 = new Matrix2(Values[0, 0], Values[0, 2], Values[2, 0], Values[2, 2]);
                var minor6 = new Matrix2(Values[0, 0], Values[0, 1], Values[2, 0], Values[2, 1]);

                var minor7 = new Matrix2(Values[0, 1], Values[0, 2], Values[1, 1], Values[1, 2]);
                var minor8 = new Matrix2(Values[0, 0], Values[0, 2], Values[1, 0], Values[1, 2]);
                var minor9 = new Matrix2(Values[0, 0], Values[0, 1], Values[1, 0], Values[1, 1]);

                var cofactors = new Matrix3
                (
                    minor1.Determinant, -minor2.Determinant, minor3.Determinant,
                    -minor4.Determinant, minor5.Determinant, -minor6.Determinant,
                    minor7.Determinant, -minor8.Determinant, minor9.Determinant
                );

                var inverse = cofactors.Transpose * (1.0f / Determinant);

                return new Matrix3 {Col0 = inverse.Col0, Col1 = inverse.Col1, Col2 = inverse.Col2};
            }
        }

        public float Determinant
        {
            get
            {
                var mat1 = new Matrix2(Values[1, 1], Values[1, 2], Values[2, 1], Values[2, 2]);
                var mat2 = new Matrix2(Values[1, 0], Values[1, 2], Values[2, 0], Values[2, 2]);
                var mat3 = new Matrix2(Values[1, 0], Values[1, 1], Values[2, 0], Values[2, 1]);

                return Values[0, 0] * mat1.Determinant +
                       -(Values[0, 1] * mat2.Determinant) +
                       Values[0, 2] * mat3.Determinant;
            }
        }

        public bool IsSingular => Math.EqualsWithTolerance(Determinant, 0);

        public float Trace => Values[0, 0] + Values[1, 1] + Values[2, 2];

        public Matrix3 Transpose => new Matrix3(Row0, Row1, Row2);

        public Matrix3 Negate() => new Matrix3 {Col0 = -Col0, Col1 = -Col1, Col2 = -Col2};

        public Matrix3 Add(Matrix3 mat) => new Matrix3 {Col0 = Col0 + mat.Col0, Col1 = Col1 + mat.Col1, Col2 = Col2 + mat.Col2};

        public Matrix3 Subtract(Matrix3 mat) => Add(mat.Negate());

        public Vector3 Multiply(Vector3 vec) => new Vector3(Row0.Dot(vec), Row1.Dot(vec), Row2.Dot(vec));

        public Matrix3 Multiply(float scalar) => new Matrix3 {Col0 = Col0 * scalar, Col1 = Col1 * scalar, Col2 = Col2 * scalar};

        public Matrix3x2 Multiply(Matrix3x2 mat) => new Matrix3x2
        (
            Row0.Dot(mat.Col0), Row0.Dot(mat.Col1),
            Row1.Dot(mat.Col0), Row1.Dot(mat.Col1),
            Row2.Dot(mat.Col0), Row2.Dot(mat.Col1)
        );

        public Matrix3 Multiply(Matrix3 mat) => new Matrix3
        (
            Row0.Dot(mat.Col0), Row0.Dot(mat.Col1), Row0.Dot(mat.Col2),
            Row1.Dot(mat.Col0), Row1.Dot(mat.Col1), Row1.Dot(mat.Col2),
            Row2.Dot(mat.Col0), Row2.Dot(mat.Col1), Row2.Dot(mat.Col2)
        );

        public Matrix3x4 Multiply(Matrix3x4 mat) => new Matrix3x4
        (
            Row0.Dot(mat.Col0), Row0.Dot(mat.Col1), Row0.Dot(mat.Col2), Row0.Dot(mat.Col3),
            Row1.Dot(mat.Col0), Row1.Dot(mat.Col1), Row1.Dot(mat.Col2), Row1.Dot(mat.Col3),
            Row2.Dot(mat.Col0), Row2.Dot(mat.Col1), Row2.Dot(mat.Col2), Row2.Dot(mat.Col3)
        );

        public static Matrix3 operator -(Matrix3 mat) => mat.Negate();

        public static Matrix3 operator +(Matrix3 left, Matrix3 right) => left.Add(right);

        public static Matrix3 operator -(Matrix3 left, Matrix3 right) => left.Subtract(right);

        public static Vector3 operator *(Matrix3 left, Vector3 right) => left.Multiply(right);

        public static Matrix3 operator *(Matrix3 left, float right) => left.Multiply(right);

        public static Matrix3 operator *(float left, Matrix3 right) => right.Multiply(left);

        public static Matrix3x2 operator *(Matrix3 left, Matrix3x2 right) => left.Multiply(right);

        public static Matrix3 operator *(Matrix3 left, Matrix3 right) => left.Multiply(right);

        public static Matrix3x4 operator *(Matrix3 left, Matrix3x4 right) => left.Multiply(right);

        public static implicit operator Matrix3D(Matrix3 mat) => new Matrix3D(mat.Col0, mat.Col1, mat.Col2);

        public bool Equals(Matrix3 mat) => Col0 == mat.Col0 && Col1 == mat.Col1 && Col2 == mat.Col2;

        public static bool operator ==(Matrix3 left, Matrix3 right) => left.Equals(right);

        public static bool operator !=(Matrix3 left, Matrix3 right) => !(left == right);

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj)) return false;
            if(ReferenceEquals(this, obj)) return true;

            return obj.GetType() == GetType() && Equals(obj as Matrix3);
        }

        public override string ToString() =>
            $"|{Math.Trim(Values[0, 0])} {Math.Trim(Values[0, 1])} {Math.Trim(Values[0, 2])} |\n" +
            $"|{Math.Trim(Values[1, 0])} {Math.Trim(Values[1, 1])} {Math.Trim(Values[1, 2])} |\n" +
            $"|{Math.Trim(Values[2, 0])} {Math.Trim(Values[2, 1])} {Math.Trim(Values[2, 2])} |\n";

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode() => Values.GetHashCode();
    }
}