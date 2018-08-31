using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Bronze.Maths
{
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Matrix4x3D : IEquatable<Matrix4x3D>
    {
        public static readonly Matrix4x3D Zero = new Matrix4x3D();

        public double[,] Values { get; }

        public double[] SingleIndexedValues => new[]
        {
            Values[0, 0], Values[0, 1], Values[0, 2],
            Values[1, 0], Values[1, 1], Values[1, 2],
            Values[2, 0], Values[2, 1], Values[2, 2],
            Values[3, 0], Values[3, 1], Values[3, 2]
        };

        public static int NumRows => 4;

        public static int NumColumns => 3;

        public static int NumElements => NumRows * NumColumns;

        public Vector4D Col0
        {
            get => new Vector4D(Values[0, 0], Values[1, 0], Values[2, 0], Values[3, 0]);
            private set
            {
                Values[0, 0] = value.X;
                Values[1, 0] = value.Y;
                Values[2, 0] = value.Z;
                Values[3, 0] = value.W;
            }
        }

        public Vector4D Col1
        {
            get => new Vector4D(Values[0, 1], Values[1, 1], Values[2, 1], Values[3, 1]);
            private set
            {
                Values[0, 1] = value.X;
                Values[1, 1] = value.Y;
                Values[2, 1] = value.Z;
                Values[3, 1] = value.W;
            }
        }

        public Vector4D Col2
        {
            get => new Vector4D(Values[0, 2], Values[1, 2], Values[2, 2], Values[3, 2]);
            private set
            {
                Values[0, 2] = value.X;
                Values[1, 2] = value.Y;
                Values[2, 2] = value.Z;
                Values[3, 2] = value.W;
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

        public Vector3D Row2
        {
            get => new Vector3D(Values[2, 0], Values[2, 1], Values[2, 2]);
        }

        public Vector3D Row3
        {
            get => new Vector3D(Values[3, 0], Values[3, 1], Values[3, 2]);
        }

        public Vector4D[] Columns => new[] {Col0, Col1, Col2};

        public Vector3D[] Rows => new[] {Row0, Row1, Row2, Row3};

        public Matrix4x3D() => Values = new double[NumRows, NumColumns];

        public Matrix4x3D
        (
            double e00, double e01, double e02,
            double e10, double e11, double e12,
            double e20, double e21, double e22,
            double e30, double e31, double e32
        ) : this()
        {
            Col0 = new Vector4D(e00, e10, e20, e30);
            Col1 = new Vector4D(e01, e11, e21, e31);
            Col2 = new Vector4D(e02, e12, e22, e32);
        }

        public Matrix4x3D(Vector4D col0, Vector4D col1, Vector4D col2) : this()
        {
            Col0 = col0;
            Col1 = col1;
            Col2 = col2;
        }

        public Matrix4x3D(IReadOnlyList<double> matrix) : this()
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

        public Matrix4x3D(double[,] matrix) : this()
        {
            if(matrix.GetLength(0) != NumRows)
                throw new ArgumentException($"Matrix4 holds {NumRows} rows, not {matrix.GetLength(0)}");

            if(matrix.GetLength(1) != NumColumns)
                throw new ArgumentException($"Matrix4 holds {NumColumns} columns, not {matrix.GetLength(1)}");

            Values = matrix;
        }

        public Matrix3x4D Transpose => new Matrix3x4D(Row0, Row1, Row2, Row3);

        public Matrix4x3D Negate() => new Matrix4x3D {Col0 = -Col0, Col1 = -Col1, Col2 = -Col2};

        public Matrix4x3D Add(Matrix4x3D mat) => new Matrix4x3D {Col0 = Col0 + mat.Col0, Col1 = Col1 + mat.Col1, Col2 = Col2 + mat.Col2};

        public Matrix4x3D Subtract(Matrix4x3D mat) => Add(mat.Negate());

        public Vector4D Multiply(Vector3D vec) => new Vector4D(Row0.Dot(vec), Row1.Dot(vec), Row2.Dot(vec), Row3.Dot(vec));

        public Matrix4x3D Multiply(double scalar) => new Matrix4x3D {Col0 = Col0 * scalar, Col1 = Col1 * scalar, Col2 = Col2 * scalar};

        public Matrix4x2D Multiply(Matrix3x2D mat) => new Matrix4x2D
        (
            Row0.Dot(mat.Col0), Row0.Dot(mat.Col1),
            Row1.Dot(mat.Col0), Row1.Dot(mat.Col1),
            Row2.Dot(mat.Col0), Row2.Dot(mat.Col1),
            Row3.Dot(mat.Col0), Row3.Dot(mat.Col1)
        );

        public Matrix4x3D Multiply(Matrix3D mat) => new Matrix4x3D
        (
            Row0.Dot(mat.Col0), Row0.Dot(mat.Col1), Row0.Dot(mat.Col2),
            Row1.Dot(mat.Col0), Row1.Dot(mat.Col1), Row1.Dot(mat.Col2),
            Row2.Dot(mat.Col0), Row2.Dot(mat.Col1), Row2.Dot(mat.Col2),
            Row3.Dot(mat.Col0), Row3.Dot(mat.Col1), Row3.Dot(mat.Col2)
        );

        public Matrix4D Multiply(Matrix3x4D mat) => new Matrix4D
        (
            Row0.Dot(mat.Col0), Row0.Dot(mat.Col1), Row0.Dot(mat.Col2), Row0.Dot(mat.Col3),
            Row1.Dot(mat.Col0), Row1.Dot(mat.Col1), Row1.Dot(mat.Col2), Row1.Dot(mat.Col3),
            Row2.Dot(mat.Col0), Row2.Dot(mat.Col1), Row2.Dot(mat.Col2), Row2.Dot(mat.Col3),
            Row3.Dot(mat.Col0), Row3.Dot(mat.Col1), Row3.Dot(mat.Col2), Row3.Dot(mat.Col3)
        );

        public static Matrix4x3D operator -(Matrix4x3D mat) => mat.Negate();

        public static Matrix4x3D operator +(Matrix4x3D left, Matrix4x3D right) => left.Add(right);

        public static Matrix4x3D operator -(Matrix4x3D left, Matrix4x3D right) => left.Subtract(right);

        public static Vector4D operator *(Matrix4x3D left, Vector3D right) => left.Multiply(right);

        public static Matrix4x3D operator *(Matrix4x3D left, double right) => left.Multiply(right);

        public static Matrix4x3D operator *(double left, Matrix4x3D right) => right.Multiply(left);

        public static Matrix4x2D operator *(Matrix4x3D left, Matrix3x2D right) => left.Multiply(right);

        public static Matrix4x3D operator *(Matrix4x3D left, Matrix3D right) => left.Multiply(right);

        public static Matrix4D operator *(Matrix4x3D left, Matrix3x4D right) => left.Multiply(right);

        public static explicit operator Matrix4x3(Matrix4x3D mat) => new Matrix4x3((Vector4) mat.Col0, (Vector4) mat.Col1, (Vector4) mat.Col2);

        public bool Equals(Matrix4x3D mat) => Col0 == mat.Col0 && Col1 == mat.Col1 && Col2 == mat.Col2;

        public static bool operator ==(Matrix4x3D left, Matrix4x3D right) => left.Equals(right);

        public static bool operator !=(Matrix4x3D left, Matrix4x3D right) => !(left == right);

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj)) return false;
            if(ReferenceEquals(this, obj)) return true;

            return obj.GetType() == GetType() && Equals(obj as Matrix4x3D);
        }

        public override string ToString() =>
            $"|{Math.Trim(Values[0, 0])} {Math.Trim(Values[0, 1])} {Math.Trim(Values[0, 2])} |\n" +
            $"|{Math.Trim(Values[1, 0])} {Math.Trim(Values[1, 1])} {Math.Trim(Values[1, 2])} |\n" +
            $"|{Math.Trim(Values[2, 0])} {Math.Trim(Values[2, 1])} {Math.Trim(Values[2, 2])} |\n" +
            $"|{Math.Trim(Values[3, 0])} {Math.Trim(Values[3, 1])} {Math.Trim(Values[3, 2])} |\n";

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode() => Values.GetHashCode();
    }
}