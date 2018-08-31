using System;
using System.Diagnostics.CodeAnalysis;
using static Bronze.Maths.Math;

namespace Bronze.Maths
{
    public struct ComplexD : IEquatable<ComplexD>
    {
        public double Re { get; set; }

        public double Im { get; set; }

        public ComplexD Conjugate => new ComplexD(Re, -Im);

        public ComplexD(double real, double imaginary)
        {
            Re = real;
            Im = imaginary;
        }

        public double Magnitude
        {
            get => Sqrt(Re * Re + Im * Im);

            set
            {
                double phase = Phase;
                Re = value * Cos(phase);
                Im = value * Sin(phase);
            }
        }

        public double Phase
        {
            get => Atan2(Im, Re);

            set
            {
                double magnitude = Magnitude;
                Re = magnitude * Cos(value);
                Im = magnitude * Sin(value);
            }
        }

        public ComplexD Negate() => new ComplexD(-Re, -Im);

        public ComplexD Add(ComplexD c) => new ComplexD(Re + c.Re, Im + c.Im);

        public ComplexD Add(double scalar) => new ComplexD(Re + scalar, Im);

        public ComplexD Subtract(ComplexD c) => new ComplexD(Re - c.Re, Im - c.Im);

        public ComplexD Subtract(double scalar) => new ComplexD(Re - scalar, Im);

        public ComplexD Multiply(ComplexD c) => new ComplexD(Re * c.Re - Im * c.Im, Re * c.Im + Im * c.Re);

        public ComplexD Multiply(double scalar) => new ComplexD(Re * scalar, Im * scalar);

        public ComplexD Divide(ComplexD c) => this * c.Conjugate / (c.Re * c.Re + c.Im * c.Im);

        public ComplexD Divide(double scalar) => new ComplexD(Re / scalar, Im / scalar);

        public bool Equals(ComplexD c) => EqualsWithTolerance(Re, c.Re) && EqualsWithTolerance(Im, c.Im);

        public Vector2D ToVector() => new Vector2D(Re, Im);

        public static ComplexD operator -(ComplexD c) => c.Negate();

        public static ComplexD operator +(ComplexD left, ComplexD right) => left.Add(right);

        public static ComplexD operator -(ComplexD left, ComplexD right) => left.Subtract(right);

        public static ComplexD operator +(ComplexD left, double right) => left.Add(right);

        public static ComplexD operator -(ComplexD left, double right) => left.Subtract(right);

        public static ComplexD operator +(double left, ComplexD right) => right.Add(left);

        public static ComplexD operator -(double left, ComplexD right) => -right.Subtract(left);

        public static ComplexD operator *(ComplexD left, ComplexD right) => left.Multiply(right);

        public static ComplexD operator *(ComplexD left, double right) => left.Multiply(right);

        public static ComplexD operator *(double left, ComplexD right) => right.Multiply(left);

        public static ComplexD operator /(ComplexD left, ComplexD right) => left.Divide(right);

        public static ComplexD operator /(ComplexD left, double right) => left.Divide(right);

        public static ComplexD operator /(double left, ComplexD right) => new ComplexD(left, 0).Divide(right);

        public static bool operator ==(ComplexD left, ComplexD right) => left.Equals(right);

        public static bool operator !=(ComplexD left, ComplexD right) => !(left == right);

        public static explicit operator Vector2(ComplexD c) => (Vector2) c.ToVector();

        public static explicit operator Vector2D(ComplexD c) => c.ToVector();

        public static explicit operator Vector2I(ComplexD c) => (Vector2I) c.ToVector();

        public static explicit operator Complex(ComplexD c) => new Complex((float) c.Re, (float) c.Im);

        public static implicit operator ComplexD(double scalar) => new ComplexD(scalar, 0);

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
            return obj is ComplexD complex && Equals(complex);
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