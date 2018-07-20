using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Bronze.Math
{
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Matrix2 : IEquatable<Matrix2>
    {
        public static readonly Matrix2 Zero = new Matrix2();

        public static readonly Matrix2 Identity = new Matrix2(1);

        public float[,] Values { get; }

        public float[] SingleIndexedValues => new[]
        {
            Values[0, 0], Values[0, 1],
            Values[1, 0], Values[1, 1]
        };

        public static int NumRows => 2;

        public static int NumColumns => 2;

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

        public Vector2 Row0
        {
            get => new Vector2(Values[0, 0], Values[0, 1]);
        }

        public Vector2 Row1
        {
            get => new Vector2(Values[1, 0], Values[1, 1]);
        }

        public Vector2[] Columns => new[] {Col0, Col1};

        public Vector2[] Rows => new[] {Row0, Row1};

        public Matrix2() => Values = new float[NumRows, NumColumns];

        public Matrix2(float diagonalValue) : this() => Values[0, 0] = Values[1, 1] = diagonalValue;

        public Matrix2
        (
            float e00, float e01,
            float e10, float e11
        ) : this()
        {
            Col0 = new Vector2(e00, e10);
            Col1 = new Vector2(e01, e11);
        }

        public Matrix2(Vector2 col0, Vector2 col1) : this()
        {
            Col0 = col0;
            Col1 = col1;
        }

        public Matrix2(IReadOnlyList<float> matrix) : this()
        {
            if(matrix.Count != NumElements) throw new ArgumentException($"Matrix4 holds {NumElements} values, not {matrix.Count}");

            Values[0, 0] = matrix[0];
            Values[0, 1] = matrix[1];

            Values[1, 0] = matrix[2];
            Values[1, 1] = matrix[3];
        }

        public Matrix2(float[,] matrix) : this()
        {
            if(matrix.GetLength(0) != NumRows)
                throw new ArgumentException($"Matrix4 holds {NumRows} rows, not {matrix.GetLength(0)}");

            if(matrix.GetLength(1) != NumColumns)
                throw new ArgumentException($"Matrix4 holds {NumColumns} columns, not {matrix.GetLength(1)}");

            Values = matrix;
        }

        public Matrix2 Inverse
        {
            get
            {
                float det = Determinant;
                return IsSingular
                    ? throw new DivideByZeroException("Singular matrices cannot have inverses")
                    : new Matrix2(Values[1, 1], -Values[0, 1], -Values[1, 0], Values[0, 0]) * (1.0f / det);
            }
        }

        public float Determinant => Values[0, 0] * Values[1, 1] - Values[0, 1] * Values[1, 0];

        public bool IsSingular => Maths.EqualsWithTolerance(Determinant, 0);

        public float Trace => Values[0, 0] + Values[1, 1];

        public Matrix2 Transpose => new Matrix2(Row0, Row1);

        public Matrix2 Negate() => new Matrix2 {Col0 = -Col0, Col1 = -Col1};

        public Matrix2 Add(Matrix2 mat) => new Matrix2 {Col0 = Col0 + mat.Col0, Col1 = Col1 + mat.Col1};

        public Matrix2 Subtract(Matrix2 mat) => Add(mat.Negate());

        public Vector2 Multiply(Vector2 vec) => new Vector2(Row0.Dot(vec), Row1.Dot(vec));

        public Matrix2 Multiply(float scalar) => new Matrix2 {Col0 = Col0 * scalar, Col1 = Col1 * scalar};

        public Matrix2 Multiply(Matrix2 mat) => new Matrix2
        (
            Row0.Dot(mat.Col0), Row0.Dot(mat.Col1),
            Row1.Dot(mat.Col0), Row1.Dot(mat.Col1)
        );

        public Matrix2x3 Multiply(Matrix2x3 mat) => new Matrix2x3
        (
            Row0.Dot(mat.Col0), Row0.Dot(mat.Col1), Row0.Dot(mat.Col2),
            Row1.Dot(mat.Col0), Row1.Dot(mat.Col1), Row1.Dot(mat.Col2)
        );

        public Matrix2x4 Multiply(Matrix2x4 mat) => new Matrix2x4
        (
            Row0.Dot(mat.Col0), Row0.Dot(mat.Col1), Row0.Dot(mat.Col2), Row0.Dot(mat.Col3),
            Row1.Dot(mat.Col0), Row1.Dot(mat.Col1), Row1.Dot(mat.Col2), Row1.Dot(mat.Col3)
        );

        public static Matrix2 operator -(Matrix2 mat) => mat.Negate();

        public static Matrix2 operator +(Matrix2 left, Matrix2 right) => left.Add(right);

        public static Matrix2 operator -(Matrix2 left, Matrix2 right) => left.Subtract(right);

        public static Vector2 operator *(Matrix2 left, Vector2 right) => left.Multiply(right);

        public static Matrix2 operator *(Matrix2 left, float right) => left.Multiply(right);

        public static Matrix2 operator *(float left, Matrix2 right) => right.Multiply(left);

        public static Matrix2 operator *(Matrix2 left, Matrix2 right) => left.Multiply(right);

        public static Matrix2x3 operator *(Matrix2 left, Matrix2x3 right) => left.Multiply(right);

        public static Matrix2x4 operator *(Matrix2 left, Matrix2x4 right) => left.Multiply(right);

        public static implicit operator Matrix2D(Matrix2 mat) => new Matrix2D(mat.Col0, mat.Col1);

        public bool Equals(Matrix2 mat) => Col0 == mat.Col0 && Col1 == mat.Col1;

        public static bool operator ==(Matrix2 left, Matrix2 right) => left.Equals(right);

        public static bool operator !=(Matrix2 left, Matrix2 right) => !(left == right);

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj)) return false;
            if(ReferenceEquals(this, obj)) return true;

            return obj.GetType() == GetType() && Equals(obj as Matrix2);
        }

        public override string ToString() =>
            $"|{Maths.Trim(Values[0, 0])} {Maths.Trim(Values[0, 1])} |\n" +
            $"|{Maths.Trim(Values[1, 0])} {Maths.Trim(Values[1, 1])} |\n";

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode() => Values.GetHashCode();
    }
}