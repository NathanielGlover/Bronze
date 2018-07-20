using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Bronze.Math
{
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Matrix3x4D : IEquatable<Matrix3x4D>
    {
        public static readonly Matrix3x4D Zero = new Matrix3x4D();

        public double[,] Values { get; }

        public double[] SingleIndexedValues => new[]
        {
            Values[0, 0], Values[0, 1], Values[0, 2], Values[0, 3],
            Values[1, 0], Values[1, 1], Values[1, 2], Values[1, 3],
            Values[2, 0], Values[2, 1], Values[2, 2], Values[2, 3]
        };

        public static int NumRows => 3;

        public static int NumColumns => 4;

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

        public Vector3D Col2
        {
            get => new Vector3D(Values[0, 2], Values[1, 2], Values[2, 2]);
            private set
            {
                Values[0, 2] = value.X;
                Values[1, 2] = value.Y;
                Values[2, 2] = value.Z;
            }
        }

        public Vector3D Col3
        {
            get => new Vector3D(Values[0, 3], Values[1, 3], Values[2, 3]);
            private set
            {
                Values[0, 3] = value.X;
                Values[1, 3] = value.Y;
                Values[2, 3] = value.Z;
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

        public Vector4D Row2
        {
            get => new Vector4D(Values[2, 0], Values[2, 1], Values[2, 2], Values[2, 3]);
        }

        public Vector3D[] Columns => new[] {Col0, Col1, Col2, Col3};

        public Vector4D[] Rows => new[] {Row0, Row1, Row2};

        public Matrix3x4D() => Values = new double[NumRows, NumColumns];

        public Matrix3x4D
        (
            double e00, double e01, double e02, double e03,
            double e10, double e11, double e12, double e13,
            double e20, double e21, double e22, double e23
        ) : this()
        {
            Col0 = new Vector3D(e00, e10, e20);
            Col1 = new Vector3D(e01, e11, e21);
            Col2 = new Vector3D(e02, e12, e22);
            Col3 = new Vector3D(e03, e13, e23);
        }

        public Matrix3x4D(Vector3D col0, Vector3D col1, Vector3D col2, Vector3D col3) : this()
        {
            Col0 = col0;
            Col1 = col1;
            Col2 = col2;
            Col3 = col3;
        }

        public Matrix3x4D(IReadOnlyList<double> matrix) : this()
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

            Values[2, 0] = matrix[8];
            Values[2, 1] = matrix[9];
            Values[2, 2] = matrix[10];
            Values[2, 3] = matrix[11];
        }

        public Matrix3x4D(double[,] matrix) : this()
        {
            if(matrix.GetLength(0) != NumRows)
                throw new ArgumentException($"Matrix4 holds {NumRows} rows, not {matrix.GetLength(0)}");

            if(matrix.GetLength(1) != NumColumns)
                throw new ArgumentException($"Matrix4 holds {NumColumns} columns, not {matrix.GetLength(1)}");

            Values = matrix;
        }

        public Matrix4x3D Transpose => new Matrix4x3D(Row0, Row1, Row2);

        public Matrix3x4D Negate() => new Matrix3x4D {Col0 = -Col0, Col1 = -Col1, Col2 = -Col2, Col3 = -Col3};

        public Matrix3x4D Add(Matrix3x4D mat) => new Matrix3x4D
            {Col0 = Col0 + mat.Col0, Col1 = Col1 + mat.Col1, Col2 = Col2 + mat.Col2, Col3 = Col3 + mat.Col3};

        public Matrix3x4D Subtract(Matrix3x4D mat) => Add(mat.Negate());

        public Vector3D Multiply(Vector4D vec) => new Vector3D(Row0.Dot(vec), Row1.Dot(vec), Row2.Dot(vec));

        public Matrix3x4D Multiply(double scalar) => new Matrix3x4D
            {Col0 = Col0 * scalar, Col1 = Col1 * scalar, Col2 = Col2 * scalar, Col3 = Col3 * scalar};

        public Matrix3x2D Multiply(Matrix4x2D mat) => new Matrix3x2D
        (
            Row0.Dot(mat.Col0), Row0.Dot(mat.Col1),
            Row1.Dot(mat.Col0), Row1.Dot(mat.Col1),
            Row2.Dot(mat.Col0), Row2.Dot(mat.Col1)
        );

        public Matrix3D Multiply(Matrix4x3D mat) => new Matrix3D
        (
            Row0.Dot(mat.Col0), Row0.Dot(mat.Col1), Row0.Dot(mat.Col2),
            Row1.Dot(mat.Col0), Row1.Dot(mat.Col1), Row1.Dot(mat.Col2),
            Row2.Dot(mat.Col0), Row2.Dot(mat.Col1), Row2.Dot(mat.Col2)
        );

        public Matrix3x4D Multiply(Matrix4D mat) => new Matrix3x4D
        (
            Row0.Dot(mat.Col0), Row0.Dot(mat.Col1), Row0.Dot(mat.Col2), Row0.Dot(mat.Col3),
            Row1.Dot(mat.Col0), Row1.Dot(mat.Col1), Row1.Dot(mat.Col2), Row1.Dot(mat.Col3),
            Row2.Dot(mat.Col0), Row2.Dot(mat.Col1), Row2.Dot(mat.Col2), Row2.Dot(mat.Col3)
        );

        public static Matrix3x4D operator -(Matrix3x4D mat) => mat.Negate();

        public static Matrix3x4D operator +(Matrix3x4D left, Matrix3x4D right) => left.Add(right);

        public static Matrix3x4D operator -(Matrix3x4D left, Matrix3x4D right) => left.Subtract(right);

        public static Vector3D operator *(Matrix3x4D left, Vector4D right) => left.Multiply(right);

        public static Matrix3x4D operator *(Matrix3x4D left, double right) => left.Multiply(right);

        public static Matrix3x4D operator *(double left, Matrix3x4D right) => right.Multiply(left);

        public static Matrix3x2D operator *(Matrix3x4D left, Matrix4x2D right) => left.Multiply(right);

        public static Matrix3D operator *(Matrix3x4D left, Matrix4x3D right) => left.Multiply(right);

        public static Matrix3x4D operator *(Matrix3x4D left, Matrix4D right) => left.Multiply(right);

        public static explicit operator Matrix3x4(Matrix3x4D mat) =>
            new Matrix3x4((Vector3) mat.Col0, (Vector3) mat.Col1, (Vector3) mat.Col2, (Vector3) mat.Col3);

        public bool Equals(Matrix3x4D mat) => Col0 == mat.Col0 && Col1 == mat.Col1 && Col2 == mat.Col2 && Col3 == mat.Col3;

        public static bool operator ==(Matrix3x4D left, Matrix3x4D right) => left.Equals(right);

        public static bool operator !=(Matrix3x4D left, Matrix3x4D right) => !(left == right);

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj)) return false;
            if(ReferenceEquals(this, obj)) return true;

            return obj.GetType() == GetType() && Equals(obj as Matrix3x4D);
        }

        public override string ToString() =>
            $"|{Maths.Trim(Values[0, 0])} {Maths.Trim(Values[0, 1])} {Maths.Trim(Values[0, 2])} {Maths.Trim(Values[0, 3])} |\n" +
            $"|{Maths.Trim(Values[1, 0])} {Maths.Trim(Values[1, 1])} {Maths.Trim(Values[1, 2])} {Maths.Trim(Values[1, 3])} |\n" +
            $"|{Maths.Trim(Values[2, 0])} {Maths.Trim(Values[2, 1])} {Maths.Trim(Values[2, 2])} {Maths.Trim(Values[2, 3])} |\n";

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode() => Values.GetHashCode();
    }
}