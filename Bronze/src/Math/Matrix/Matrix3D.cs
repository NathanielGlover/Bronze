using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Bronze.Math
{
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Matrix3D : IEquatable<Matrix3D>
    {
        public static readonly Matrix3D Zero = new Matrix3D();

        public static readonly Matrix3D Identity = new Matrix3D(1);

        public double[,] Values { get; }

        public double[] SingleIndexedValues => new[]
        {
            Values[0, 0], Values[0, 1], Values[0, 2],
            Values[1, 0], Values[1, 1], Values[1, 2],
            Values[2, 0], Values[2, 1], Values[2, 2]
        };

        public static int NumRows => 3;

        public static int NumColumns => 3;

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

        public Vector3D[] Columns => new[] {Col0, Col1, Col2};

        public Vector3D[] Rows => new[] {Row0, Row1, Row2};

        public Matrix3D() => Values = new double[NumRows, NumColumns];

        public Matrix3D(double diagonalValue) : this() => Values[0, 0] = Values[1, 1] = Values[2, 2] = diagonalValue;

        public Matrix3D
        (
            double e00, double e01, double e02,
            double e10, double e11, double e12,
            double e20, double e21, double e22
        ) : this()
        {
            Col0 = new Vector3D(e00, e10, e20);
            Col1 = new Vector3D(e01, e11, e21);
            Col2 = new Vector3D(e02, e12, e22);
        }

        public Matrix3D(Vector3D col0, Vector3D col1, Vector3D col2) : this()
        {
            Col0 = col0;
            Col1 = col1;
            Col2 = col2;
        }

        public Matrix3D(IReadOnlyList<double> matrix) : this()
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

        public Matrix3D(double[,] matrix) : this()
        {
            if(matrix.GetLength(0) != NumRows)
                throw new ArgumentException($"Matrix4 holds {NumRows} rows, not {matrix.GetLength(0)}");

            if(matrix.GetLength(1) != NumColumns)
                throw new ArgumentException($"Matrix4 holds {NumColumns} columns, not {matrix.GetLength(1)}");

            Values = matrix;
        }

        public Matrix3D Inverse
        {
            get
            {
                if(IsSingular) throw new DivideByZeroException("Singular matrices cannot have inverses");

                var minor1 = new Matrix2D(Values[1, 1], Values[1, 2], Values[2, 1], Values[2, 2]);
                var minor2 = new Matrix2D(Values[1, 0], Values[1, 2], Values[2, 0], Values[2, 2]);
                var minor3 = new Matrix2D(Values[1, 0], Values[1, 1], Values[2, 0], Values[2, 1]);

                var minor4 = new Matrix2D(Values[0, 1], Values[0, 2], Values[2, 1], Values[2, 2]);
                var minor5 = new Matrix2D(Values[0, 0], Values[0, 2], Values[2, 0], Values[2, 2]);
                var minor6 = new Matrix2D(Values[0, 0], Values[0, 1], Values[2, 0], Values[2, 1]);

                var minor7 = new Matrix2D(Values[0, 1], Values[0, 2], Values[1, 1], Values[1, 2]);
                var minor8 = new Matrix2D(Values[0, 0], Values[0, 2], Values[1, 0], Values[1, 2]);
                var minor9 = new Matrix2D(Values[0, 0], Values[0, 1], Values[1, 0], Values[1, 1]);

                var cofactors = new Matrix3D
                (
                    minor1.Determinant, -minor2.Determinant, minor3.Determinant,
                    -minor4.Determinant, minor5.Determinant, -minor6.Determinant,
                    minor7.Determinant, -minor8.Determinant, minor9.Determinant
                );

                var inverse = cofactors.Transpose * (1.0f / Determinant);

                return new Matrix3D {Col0 = inverse.Col0, Col1 = inverse.Col1, Col2 = inverse.Col2};
            }
        }

        public double Determinant
        {
            get
            {
                var mat1 = new Matrix2D(Values[1, 1], Values[1, 2], Values[2, 1], Values[2, 2]);
                var mat2 = new Matrix2D(Values[1, 0], Values[1, 2], Values[2, 0], Values[2, 2]);
                var mat3 = new Matrix2D(Values[1, 0], Values[1, 1], Values[2, 0], Values[2, 1]);

                return Values[0, 0] * mat1.Determinant +
                       -(Values[0, 1] * mat2.Determinant) +
                       Values[0, 2] * mat3.Determinant;
            }
        }

        public bool IsSingular => Math.EqualsWithTolerance(Determinant, 0);

        public double Trace => Values[0, 0] + Values[1, 1] + Values[2, 2];

        public Matrix3D Transpose => new Matrix3D(Row0, Row1, Row2);

        public Matrix3D Negate() => new Matrix3D {Col0 = -Col0, Col1 = -Col1, Col2 = -Col2};

        public Matrix3D Add(Matrix3D mat) => new Matrix3D {Col0 = Col0 + mat.Col0, Col1 = Col1 + mat.Col1, Col2 = Col2 + mat.Col2};

        public Matrix3D Subtract(Matrix3D mat) => Add(mat.Negate());

        public Vector3D Multiply(Vector3D vec) => new Vector3D(Row0.Dot(vec), Row1.Dot(vec), Row2.Dot(vec));

        public Matrix3D Multiply(double scalar) => new Matrix3D {Col0 = Col0 * scalar, Col1 = Col1 * scalar, Col2 = Col2 * scalar};

        public Matrix3x2D Multiply(Matrix3x2D mat) => new Matrix3x2D
        (
            Row0.Dot(mat.Col0), Row0.Dot(mat.Col1),
            Row1.Dot(mat.Col0), Row1.Dot(mat.Col1),
            Row2.Dot(mat.Col0), Row2.Dot(mat.Col1)
        );

        public Matrix3D Multiply(Matrix3D mat) => new Matrix3D
        (
            Row0.Dot(mat.Col0), Row0.Dot(mat.Col1), Row0.Dot(mat.Col2),
            Row1.Dot(mat.Col0), Row1.Dot(mat.Col1), Row1.Dot(mat.Col2),
            Row2.Dot(mat.Col0), Row2.Dot(mat.Col1), Row2.Dot(mat.Col2)
        );

        public Matrix3x4D Multiply(Matrix3x4D mat) => new Matrix3x4D
        (
            Row0.Dot(mat.Col0), Row0.Dot(mat.Col1), Row0.Dot(mat.Col2), Row0.Dot(mat.Col3),
            Row1.Dot(mat.Col0), Row1.Dot(mat.Col1), Row1.Dot(mat.Col2), Row1.Dot(mat.Col3),
            Row2.Dot(mat.Col0), Row2.Dot(mat.Col1), Row2.Dot(mat.Col2), Row2.Dot(mat.Col3)
        );

        public static Matrix3D operator -(Matrix3D mat) => mat.Negate();

        public static Matrix3D operator +(Matrix3D left, Matrix3D right) => left.Add(right);

        public static Matrix3D operator -(Matrix3D left, Matrix3D right) => left.Subtract(right);

        public static Vector3D operator *(Matrix3D left, Vector3D right) => left.Multiply(right);

        public static Matrix3D operator *(Matrix3D left, double right) => left.Multiply(right);

        public static Matrix3D operator *(double left, Matrix3D right) => right.Multiply(left);

        public static Matrix3x2D operator *(Matrix3D left, Matrix3x2D right) => left.Multiply(right);

        public static Matrix3D operator *(Matrix3D left, Matrix3D right) => left.Multiply(right);

        public static Matrix3x4D operator *(Matrix3D left, Matrix3x4D right) => left.Multiply(right);

        public static explicit operator Matrix3(Matrix3D mat) => new Matrix3((Vector3) mat.Col0, (Vector3) mat.Col1, (Vector3) mat.Col2);

        public bool Equals(Matrix3D mat) => Col0 == mat.Col0 && Col1 == mat.Col1 && Col2 == mat.Col2;

        public static bool operator ==(Matrix3D left, Matrix3D right) => left.Equals(right);

        public static bool operator !=(Matrix3D left, Matrix3D right) => !(left == right);

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj)) return false;
            if(ReferenceEquals(this, obj)) return true;

            return obj.GetType() == GetType() && Equals(obj as Matrix3D);
        }

        public override string ToString() =>
            $"|{Math.Trim(Values[0, 0])} {Math.Trim(Values[0, 1])} {Math.Trim(Values[0, 2])} |\n" +
            $"|{Math.Trim(Values[1, 0])} {Math.Trim(Values[1, 1])} {Math.Trim(Values[1, 2])} |\n" +
            $"|{Math.Trim(Values[2, 0])} {Math.Trim(Values[2, 1])} {Math.Trim(Values[2, 2])} |\n";

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode() => Values.GetHashCode();
    }
}