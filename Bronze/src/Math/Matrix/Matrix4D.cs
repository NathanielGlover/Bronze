using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Bronze.Math
{
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Matrix4D : IEquatable<Matrix4D>
    {
        public static readonly Matrix4D Zero = new Matrix4D();

        public static readonly Matrix4D Identity = new Matrix4D(1);

        public double[,] Values { get; }

        public double[] SingleIndexedValues => new[]
        {
            Values[0, 0], Values[0, 1], Values[0, 2], Values[0, 3],
            Values[1, 0], Values[1, 1], Values[1, 2], Values[1, 3],
            Values[2, 0], Values[2, 1], Values[2, 2], Values[2, 3],
            Values[3, 0], Values[3, 1], Values[3, 2], Values[3, 3]
        };

        public static int NumRows => 4;

        public static int NumColumns => 4;

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

        public Vector4D Col3
        {
            get => new Vector4D(Values[0, 3], Values[1, 3], Values[2, 3], Values[3, 3]);
            private set
            {
                Values[0, 3] = value.X;
                Values[1, 3] = value.Y;
                Values[2, 3] = value.Z;
                Values[3, 3] = value.W;
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

        public Vector4D Row3
        {
            get => new Vector4D(Values[3, 0], Values[3, 1], Values[3, 2], Values[3, 3]);
        }

        public Vector4D[] Columns => new[] {Col0, Col1, Col2, Col3};

        public Vector4D[] Rows => new[] {Row0, Row1, Row2, Row3};

        public Matrix4D() => Values = new double[NumRows, NumColumns];

        public Matrix4D(double diagonalValue) : this() => Values[0, 0] = Values[1, 1] = Values[2, 2] = Values[3, 3] = diagonalValue;

        public Matrix4D
        (
            double e00, double e01, double e02, double e03,
            double e10, double e11, double e12, double e13,
            double e20, double e21, double e22, double e23,
            double e30, double e31, double e32, double e33
        ) : this()
        {
            Col0 = new Vector4D(e00, e10, e20, e30);
            Col1 = new Vector4D(e01, e11, e21, e31);
            Col2 = new Vector4D(e02, e12, e22, e32);
            Col3 = new Vector4D(e03, e13, e23, e33);
        }

        public Matrix4D(Vector4D col0, Vector4D col1, Vector4D col2, Vector4D col3) : this()
        {
            Col0 = col0;
            Col1 = col1;
            Col2 = col2;
            Col3 = col3;
        }

        public Matrix4D(IReadOnlyList<double> matrix) : this()
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

            Values[3, 0] = matrix[12];
            Values[3, 1] = matrix[13];
            Values[3, 2] = matrix[14];
            Values[3, 3] = matrix[15];
        }

        public Matrix4D(double[,] matrix) : this()
        {
            if(matrix.GetLength(0) != NumRows)
                throw new ArgumentException($"Matrix4 holds {NumRows} rows, not {matrix.GetLength(0)}");

            if(matrix.GetLength(1) != NumColumns)
                throw new ArgumentException($"Matrix4 holds {NumColumns} columns, not {matrix.GetLength(1)}");

            Values = matrix;
        }

        public Matrix4D Inverse
        {
            get
            {
                if(IsSingular) throw new DivideByZeroException("Singular matrices cannot have inverses");

                var minor1 = new Matrix3D
                (
                    Values[1, 1], Values[1, 2], Values[1, 3],
                    Values[2, 1], Values[2, 2], Values[2, 3],
                    Values[3, 1], Values[3, 2], Values[3, 3]
                );
                var minor2 = new Matrix3D
                (
                    Values[1, 0], Values[1, 2], Values[1, 3],
                    Values[2, 0], Values[2, 2], Values[2, 3],
                    Values[3, 0], Values[3, 2], Values[3, 3]
                );
                var minor3 = new Matrix3D
                (
                    Values[1, 0], Values[1, 1], Values[1, 3],
                    Values[2, 0], Values[2, 1], Values[2, 3],
                    Values[3, 0], Values[3, 1], Values[3, 3]
                );
                var minor4 = new Matrix3D
                (
                    Values[1, 0], Values[1, 1], Values[1, 2],
                    Values[2, 0], Values[2, 1], Values[2, 2],
                    Values[3, 0], Values[3, 1], Values[3, 2]
                );
                var minor5 = new Matrix3D
                (
                    Values[0, 1], Values[0, 2], Values[0, 3],
                    Values[2, 1], Values[2, 2], Values[2, 3],
                    Values[3, 1], Values[3, 2], Values[3, 3]
                );
                var minor6 = new Matrix3D
                (
                    Values[0, 0], Values[0, 2], Values[0, 3],
                    Values[2, 0], Values[2, 2], Values[2, 3],
                    Values[3, 0], Values[3, 2], Values[3, 3]
                );
                var minor7 = new Matrix3D
                (
                    Values[0, 0], Values[0, 1], Values[0, 3],
                    Values[2, 0], Values[2, 1], Values[2, 3],
                    Values[3, 0], Values[3, 1], Values[3, 3]
                );
                var minor8 = new Matrix3D
                (
                    Values[0, 0], Values[0, 1], Values[0, 2],
                    Values[2, 0], Values[2, 1], Values[2, 2],
                    Values[3, 0], Values[3, 1], Values[3, 2]
                );
                var minor9 = new Matrix3D
                (
                    Values[0, 1], Values[0, 2], Values[0, 3],
                    Values[1, 1], Values[1, 2], Values[1, 3],
                    Values[3, 1], Values[3, 2], Values[3, 3]
                );
                var minor10 = new Matrix3D
                (
                    Values[0, 0], Values[0, 2], Values[0, 3],
                    Values[1, 0], Values[1, 2], Values[1, 3],
                    Values[3, 0], Values[3, 2], Values[3, 3]
                );
                var minor11 = new Matrix3D
                (
                    Values[0, 0], Values[0, 1], Values[0, 3],
                    Values[1, 0], Values[1, 1], Values[1, 3],
                    Values[3, 0], Values[3, 1], Values[3, 3]
                );
                var minor12 = new Matrix3D
                (
                    Values[0, 0], Values[0, 1], Values[0, 2],
                    Values[1, 0], Values[1, 1], Values[1, 2],
                    Values[3, 0], Values[3, 1], Values[3, 2]
                );
                var minor13 = new Matrix3D
                (
                    Values[0, 1], Values[0, 2], Values[0, 3],
                    Values[1, 1], Values[1, 2], Values[1, 3],
                    Values[2, 1], Values[2, 2], Values[2, 3]
                );
                var minor14 = new Matrix3D
                (
                    Values[0, 0], Values[0, 2], Values[0, 3],
                    Values[1, 0], Values[1, 2], Values[1, 3],
                    Values[2, 0], Values[2, 2], Values[2, 3]
                );
                var minor15 = new Matrix3D
                (
                    Values[0, 0], Values[0, 1], Values[0, 3],
                    Values[1, 0], Values[1, 1], Values[1, 3],
                    Values[2, 0], Values[2, 1], Values[2, 3]
                );
                var minor16 = new Matrix3D
                (
                    Values[0, 0], Values[0, 1], Values[0, 2],
                    Values[1, 0], Values[1, 1], Values[1, 2],
                    Values[2, 0], Values[2, 1], Values[2, 2]
                );

                var cofactors = new Matrix4D
                (
                    minor1.Determinant, -minor2.Determinant, minor3.Determinant, -minor4.Determinant,
                    -minor5.Determinant, minor6.Determinant, -minor7.Determinant, minor8.Determinant,
                    minor9.Determinant, -minor10.Determinant, minor11.Determinant, -minor12.Determinant,
                    -minor13.Determinant, minor14.Determinant, -minor15.Determinant, minor16.Determinant
                );

                var inverse = 1.0f / Determinant * cofactors.Transpose;

                return new Matrix4D {Col0 = inverse.Col0, Col1 = inverse.Col1, Col2 = inverse.Col2, Col3 = inverse.Col3};
            }
        }

        public double Determinant
        {
            get
            {
                var mat1 = new Matrix3D
                (
                    Values[1, 1], Values[1, 2], Values[1, 3],
                    Values[2, 1], Values[2, 2], Values[2, 3],
                    Values[3, 1], Values[3, 2], Values[3, 3]
                );
                var mat2 = new Matrix3D
                (
                    Values[1, 0], Values[1, 2], Values[1, 3],
                    Values[2, 0], Values[2, 2], Values[2, 3],
                    Values[3, 0], Values[3, 2], Values[3, 3]
                );
                var mat3 = new Matrix3D
                (
                    Values[1, 0], Values[1, 1], Values[1, 3],
                    Values[2, 0], Values[2, 1], Values[2, 3],
                    Values[3, 0], Values[3, 1], Values[3, 3]
                );
                var mat4 = new Matrix3D
                (
                    Values[1, 0], Values[1, 1], Values[1, 2],
                    Values[2, 0], Values[2, 1], Values[2, 2],
                    Values[3, 0], Values[3, 1], Values[3, 2]
                );

                return Values[0, 0] * mat1.Determinant +
                       -(Values[0, 1] * mat2.Determinant) +
                       Values[0, 2] * mat3.Determinant +
                       -(Values[0, 3] * mat4.Determinant);
            }
        }

        public bool IsSingular => Math.EqualsWithTolerance(Determinant, 0);

        public double Trace => Values[0, 0] + Values[1, 1] + Values[2, 2] + Values[3, 3];

        public Matrix4D Transpose => new Matrix4D(Row0, Row1, Row2, Row3);

        public Matrix4D Negate() => new Matrix4D {Col0 = -Col0, Col1 = -Col1, Col2 = -Col2, Col3 = -Col3};

        public Matrix4D Add(Matrix4D mat) =>
            new Matrix4D {Col0 = Col0 + mat.Col0, Col1 = Col1 + mat.Col1, Col2 = Col2 + mat.Col2, Col3 = Col3 + mat.Col3};

        public Matrix4D Subtract(Matrix4D mat) => Add(mat.Negate());

        public Vector4D Multiply(Vector4D vec) => new Vector4D(Row0.Dot(vec), Row1.Dot(vec), Row2.Dot(vec), Row3.Dot(vec));

        public Matrix4D Multiply(double scalar) => new Matrix4D {Col0 = Col0 * scalar, Col1 = Col1 * scalar, Col2 = Col2 * scalar, Col3 = Col3 * scalar};

        public Matrix4x2D Multiply(Matrix4x2D mat) => new Matrix4x2D
        (
            Row0.Dot(mat.Col0), Row0.Dot(mat.Col1),
            Row1.Dot(mat.Col0), Row1.Dot(mat.Col1),
            Row2.Dot(mat.Col0), Row2.Dot(mat.Col1),
            Row3.Dot(mat.Col0), Row3.Dot(mat.Col1)
        );

        public Matrix4x3D Multiply(Matrix4x3D mat) => new Matrix4x3D
        (
            Row0.Dot(mat.Col0), Row0.Dot(mat.Col1), Row0.Dot(mat.Col2),
            Row1.Dot(mat.Col0), Row1.Dot(mat.Col1), Row1.Dot(mat.Col2),
            Row2.Dot(mat.Col0), Row2.Dot(mat.Col1), Row2.Dot(mat.Col2),
            Row3.Dot(mat.Col0), Row3.Dot(mat.Col1), Row3.Dot(mat.Col2)
        );

        public Matrix4D Multiply(Matrix4D mat) => new Matrix4D
        (
            Row0.Dot(mat.Col0), Row0.Dot(mat.Col1), Row0.Dot(mat.Col2), Row0.Dot(mat.Col3),
            Row1.Dot(mat.Col0), Row1.Dot(mat.Col1), Row1.Dot(mat.Col2), Row1.Dot(mat.Col3),
            Row2.Dot(mat.Col0), Row2.Dot(mat.Col1), Row2.Dot(mat.Col2), Row2.Dot(mat.Col3),
            Row3.Dot(mat.Col0), Row3.Dot(mat.Col1), Row3.Dot(mat.Col2), Row3.Dot(mat.Col3)
        );

        public static Matrix4D operator -(Matrix4D mat) => mat.Negate();

        public static Matrix4D operator +(Matrix4D left, Matrix4D right) => left.Add(right);

        public static Matrix4D operator -(Matrix4D left, Matrix4D right) => left.Subtract(right);

        public static Vector4D operator *(Matrix4D left, Vector4D right) => left.Multiply(right);

        public static Matrix4D operator *(Matrix4D left, double right) => left.Multiply(right);

        public static Matrix4D operator *(double left, Matrix4D right) => right.Multiply(left);

        public static Matrix4x2D operator *(Matrix4D left, Matrix4x2D right) => left.Multiply(right);

        public static Matrix4x3D operator *(Matrix4D left, Matrix4x3D right) => left.Multiply(right);

        public static Matrix4D operator *(Matrix4D left, Matrix4D right) => left.Multiply(right);

        public static explicit operator Matrix4(Matrix4D mat) =>
            new Matrix4((Vector4) mat.Col0, (Vector4) mat.Col1, (Vector4) mat.Col2, (Vector4) mat.Col3);

        public bool Equals(Matrix4D mat) => Col0 == mat.Col0 && Col1 == mat.Col1 && Col2 == mat.Col2 && Col3 == mat.Col3;

        public static bool operator ==(Matrix4D left, Matrix4D right) => left.Equals(right);

        public static bool operator !=(Matrix4D left, Matrix4D right) => !(left == right);

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj)) return false;
            if(ReferenceEquals(this, obj)) return true;

            return obj.GetType() == GetType() && Equals(obj as Matrix4D);
        }

        public override string ToString() =>
            $"|{Math.Trim(Values[0, 0])} {Math.Trim(Values[0, 1])} {Math.Trim(Values[0, 2])} {Math.Trim(Values[0, 3])} |\n" +
            $"|{Math.Trim(Values[1, 0])} {Math.Trim(Values[1, 1])} {Math.Trim(Values[1, 2])} {Math.Trim(Values[1, 3])} |\n" +
            $"|{Math.Trim(Values[2, 0])} {Math.Trim(Values[2, 1])} {Math.Trim(Values[2, 2])} {Math.Trim(Values[2, 3])} |\n" +
            $"|{Math.Trim(Values[3, 0])} {Math.Trim(Values[3, 1])} {Math.Trim(Values[3, 2])} {Math.Trim(Values[3, 3])} |\n";

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode() => Values.GetHashCode();
    }
}