using Xunit;
using Bronze.Math;
using static Bronze.Math.Maths;

namespace UnitTests
{
    public class MathTests
    {
        private readonly Complex testComplex1 = 3 + 4 * I;
        private readonly ComplexD testComplexD1 = 3 + 4 * II;

        private readonly Complex testComplex2 = 2 + 5 * I;
        private readonly ComplexD testComplexD2 = 2 + 5 * II;

        [Fact]
        public void Abs()
        {
            Assert.True(EqualsWithTolerance(Maths.Abs(testComplex2), 5.3851648071345f));
            Assert.True(EqualsWithTolerance(Maths.Abs(testComplexD2), 5.3851648071345));
            Assert.True(EqualsWithTolerance(Maths.Abs(testComplex2), (float) Maths.Abs(testComplexD2)));
        }

        [Fact]
        public void Acos()
        {
            Assert.True(Maths.Acos(testComplex1) == 0.93681246115571f - 2.30550903124347f * I);
            Assert.True(Maths.Acos(testComplexD1) == 0.93681246115571 - 2.30550903124347 * II);
            Assert.True(Maths.Acos(testComplex1) == (Complex) Maths.Acos(testComplexD1));
        }

        [Fact]
        public void Asin()
        {
            Assert.True(Maths.Asin(testComplex1) == 0.63398386563917f + 2.30550903124347f * I);
            Assert.True(Maths.Asin(testComplexD1) == 0.63398386563917 + 2.30550903124347 * II);
            Assert.True(Maths.Asin(testComplex1) == (Complex) Maths.Asin(testComplexD1));
        }

        [Fact]
        public void Atan()
        {
            Assert.True(Maths.Atan(testComplex1) == 1.44830699523146f + 0.15899719167999f * I);
            Assert.True(Maths.Atan(testComplexD1) == 1.44830699523146 + 0.15899719167999 * II);
            Assert.True(Maths.Atan(testComplex1) == (Complex) Maths.Atan(testComplexD1));
        }

        [Fact]
        public void Cos()
        {
            Assert.True(Maths.Cos(testComplex1) == -27.0349456030742f - 3.85115333481177f * I);
            Assert.True(Maths.Cos(testComplexD1) == -27.0349456030742 - 3.85115333481177 * II);
            Assert.True(Maths.Cos(testComplex1) == (Complex) Maths.Cos(testComplexD1));
        }

        [Fact]
        public void Cosh()
        {
            Assert.True(Maths.Cosh(testComplex1) == -6.58066304055115f - 7.58155274274654f * I);
            Assert.True(Maths.Cosh(testComplexD1) == -6.58066304055115 - 7.58155274274654 * II);
            Assert.True(Maths.Cosh(testComplex1) == (Complex) Maths.Cosh(testComplexD1));
        }

        [Fact]
        public void Exp()
        {
            Assert.True(Maths.Exp(testComplex1) == -13.1287830814621f - 15.2007844630679f * I);
            Assert.True(Maths.Exp(testComplexD1) == -13.1287830814621 - 15.2007844630679 * II);
            Assert.True(Maths.Exp(testComplex1) == (Complex) Maths.Exp(testComplexD1));
        }

        [Fact]
        public void Log()
        {
            Assert.True(Maths.Log(testComplex1) == 1.60943791243410f + 0.92729521800161f * I);
            Assert.True(Maths.Log(testComplexD1) == 1.60943791243410 + 0.92729521800161 * II);
            Assert.True(Maths.Log(testComplex1) == (Complex) Maths.Log(testComplexD1));

            Assert.True(Maths.Log(testComplex1, testComplex2) == 0.89698045988556f - 0.08337349347910f * I);
            Assert.True(Maths.Log(testComplexD1, testComplexD2) == 0.89698045988556 - 0.08337349347910 * II);
            Assert.True(Maths.Log(testComplex1, testComplex2) == (Complex) Maths.Log(testComplexD1, testComplexD2));
        }

        [Fact]
        public void Pow()
        {
            Assert.True(Maths.Pow(testComplex1, testComplex2) == -0.21524869035410f - 0.11124186924370f * I);
            Assert.True(Maths.Pow(testComplexD1, testComplexD2) == -0.21524869035410 - 0.11124186924370 * II);
            Assert.True(Maths.Pow(testComplex1, testComplex2) == (Complex) Maths.Pow(testComplexD1, testComplexD2));
        }

        [Fact]
        public void Sigm()
        {
            Assert.True(Maths.Sigm(testComplex1) == 1.03207219958826f - 0.04019550765508f * I);
            Assert.True(Maths.Sigm(testComplexD1) == 1.03207219958826 - 0.04019550765508 * II);
            Assert.True(Maths.Sigm(testComplex1) == (Complex) Maths.Sigm(testComplexD1));
        }

        [Fact]
        public void Sin()
        {
            Assert.True(Maths.Sin(testComplex1) == 3.85373803791937f - 27.0168132580039f * I);
            Assert.True(Maths.Sin(testComplexD1) == 3.85373803791937 - 27.0168132580039 * II);
            Assert.True(Maths.Sin(testComplex1) == (Complex) Maths.Sin(testComplexD1));
        }

        [Fact]
        public void Sinh()
        {
            Assert.True(Maths.Sinh(testComplex1) == -6.5481200409110f - 7.61923172032141f * I);
            Assert.True(Maths.Sinh(testComplexD1) == -6.5481200409110 - 7.61923172032141 * II);
            Assert.True(Maths.Sinh(testComplex1) == (Complex) Maths.Sinh(testComplexD1));
        }

        [Fact]
        public void Sqrt()
        {
            Assert.True(Maths.Sqrt(testComplex1) == 2f + I);
            Assert.True(Maths.Sqrt(testComplexD1) == 2 + II);
            Assert.True(Maths.Sqrt(testComplex1) == (Complex) Maths.Sqrt(testComplexD1));
        }

        [Fact]
        public void Tan()
        {
            Assert.True(Maths.Tan(testComplex1) == -0.0001873462046f + 0.99935598738147f * I);
            Assert.True(Maths.Tan(testComplexD1) == -0.0001873462046 + 0.99935598738147 * II);
            Assert.True(Maths.Tan(testComplex1) == (Complex) Maths.Tan(testComplexD1));
        }

        [Fact]
        public void Tanh()
        {
            Assert.True(Maths.Tanh(testComplex1) == 1.00070953606723f + 0.00490825806749f * I);
            Assert.True(Maths.Tanh(testComplexD1) == 1.00070953606723 + 0.00490825806749 * II);
            Assert.True(Maths.Tanh(testComplex1) == (Complex) Maths.Tanh(testComplexD1));
        }
    }
}