using System;
using System.Diagnostics.CodeAnalysis;
using static Bronze.Math.Math;

namespace Bronze.Math
{
    public struct Complex : IEquatable<Complex>
    {
        public float Re { get; set; }

        public float Im { get; set; }

        public Complex Conjugate => new Complex(Re, -Im);

        public Complex(float real, float imaginary)
        {
            Re = real;
            Im = imaginary;
        }

        public float Magnitude
        {
            get => Sqrt(Re * Re + Im * Im);

            set
            {
                float phase = Phase;
                Re = value * Cos(phase);
                Im = value * Sin(phase);
            }
        }

        public float Phase
        {
            get => Atan2(Im, Re);

            set
            {
                float magnitude = Magnitude;
                Re = magnitude * Cos(value);
                Im = magnitude * Sin(value);
            }
        }

        public Complex Negate() => new Complex(-Re, -Im);

        public Complex Add(Complex c) => new Complex(Re + c.Re, Im + c.Im);

        public Complex Add(float scalar) => new Complex(Re + scalar, Im);

        public Complex Subtract(Complex c) => new Complex(Re - c.Re, Im - c.Im);

        public Complex Subtract(float scalar) => new Complex(Re - scalar, Im);

        public Complex Multiply(Complex c) => new Complex(Re * c.Re - Im * c.Im, Re * c.Im + Im * c.Re);

        public Complex Multiply(float scalar) => new Complex(Re * scalar, Im * scalar);

        public Complex Divide(Complex c) => this * c.Conjugate / (c.Re * c.Re + c.Im * c.Im);

        public Complex Divide(float scalar) => new Complex(Re / scalar, Im / scalar);

        public bool Equals(Complex c) => EqualsWithTolerance(Re, c.Re) && EqualsWithTolerance(Im, c.Im);

        public Vector2 ToVector() => new Vector2(Re, Im);

        public static Complex operator -(Complex c) => c.Negate();

        public static Complex operator +(Complex left, Complex right) => left.Add(right);

        public static Complex operator -(Complex left, Complex right) => left.Subtract(right);

        public static Complex operator +(Complex left, float right) => left.Add(right);

        public static Complex operator -(Complex left, float right) => left.Subtract(right);

        public static Complex operator +(float left, Complex right) => right.Add(left);

        public static Complex operator -(float left, Complex right) => -right.Subtract(left);

        public static Complex operator *(Complex left, Complex right) => left.Multiply(right);

        public static Complex operator *(Complex left, float right) => left.Multiply(right);

        public static Complex operator *(float left, Complex right) => right.Multiply(left);

        public static Complex operator /(Complex left, Complex right) => left.Divide(right);

        public static Complex operator /(Complex left, float right) => left.Divide(right);

        public static Complex operator /(float left, Complex right) => new Complex(left, 0).Divide(right);

        public static bool operator ==(Complex left, Complex right) => left.Equals(right);

        public static bool operator !=(Complex left, Complex right) => !(left == right);

        public static explicit operator Vector2(Complex c) => c.ToVector();

        public static explicit operator Vector2D(Complex c) => c.ToVector();

        public static explicit operator Vector2I(Complex c) => (Vector2I) c.ToVector();

        public static implicit operator ComplexD(Complex c) => new ComplexD(c.Re, c.Im);

        public static implicit operator Complex(float scalar) => new Complex(scalar, 0);

        public override string ToString()
        {
            string real = $"{Trim(Re)}";
            if(EqualsWithTolerance(Im, 0)) return real;

            string imaginary = EqualsWithTolerance(Im, 1) ? "i" : $"{Abs(Trim(Im))}i";

            return real + (Sign(Im) >= 0 ? " + " : " - ") + imaginary;
        }

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj)) return false;
            return obj is Complex complex && Equals(complex);
        }

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            unchecked
            {
                return (Re.GetHashCode() * 397) ^ Im.GetHashCode();
            }
        }
    }
}