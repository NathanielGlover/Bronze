using Bronze.Math;
using NUnit.Framework;
using static Bronze.Math.Math;

namespace UnitTests
{
    [TestFixture]
    public class MathTests
    {
        private readonly Complex testComplex1 = 3 + 4 * I;
        private readonly ComplexD testComplexD1 = 3 + 4 * II;

        private readonly Complex testComplex2 = 2 + 5 * I;
        private readonly ComplexD testComplexD2 = 2 + 5 * II;

        [Test]
        public void Abs()
        {
            Assert.True(EqualsWithTolerance(Math.Abs(testComplex2), 5.3851648071345f));
            Assert.True(EqualsWithTolerance(Math.Abs(testComplexD2), 5.3851648071345));
            Assert.True(EqualsWithTolerance(Math.Abs(testComplex2), (float) Math.Abs(testComplexD2)));
        }

        [Test]
        public void Acos()
        {
            Assert.True(Math.Acos(testComplex1) == 0.93681246115571f - 2.30550903124347f * I);
            Assert.True(Math.Acos(testComplexD1) == 0.93681246115571 - 2.30550903124347 * II);
            Assert.True(Math.Acos(testComplex1) == (Complex) Math.Acos(testComplexD1));
        }

        [Test]
        public void Asin()
        {
            Assert.True(Math.Asin(testComplex1) == 0.63398386563917f + 2.30550903124347f * I);
            Assert.True(Math.Asin(testComplexD1) == 0.63398386563917 + 2.30550903124347 * II);
            Assert.True(Math.Asin(testComplex1) == (Complex) Math.Asin(testComplexD1));
        }

        [Test]
        public void Atan()
        {
            Assert.True(Math.Atan(testComplex1) == 1.44830699523146f + 0.15899719167999f * I);
            Assert.True(Math.Atan(testComplexD1) == 1.44830699523146 + 0.15899719167999 * II);
            Assert.True(Math.Atan(testComplex1) == (Complex) Math.Atan(testComplexD1));
        }

        [Test]
        public void Cos()
        {
            Assert.True(Math.Cos(testComplex1) == -27.0349456030742f - 3.85115333481177f * I);
            Assert.True(Math.Cos(testComplexD1) == -27.0349456030742 - 3.85115333481177 * II);
            Assert.True(Math.Cos(testComplex1) == (Complex) Math.Cos(testComplexD1));
        }

        [Test]
        public void Cosh()
        {
            Assert.True(Math.Cosh(testComplex1) == -6.58066304055115f - 7.58155274274654f * I);
            Assert.True(Math.Cosh(testComplexD1) == -6.58066304055115 - 7.58155274274654 * II);
            Assert.True(Math.Cosh(testComplex1) == (Complex) Math.Cosh(testComplexD1));
        }

        [Test]
        public void Exp()
        {
            Assert.True(Math.Exp(testComplex1) == -13.1287830814621f - 15.2007844630679f * I);
            Assert.True(Math.Exp(testComplexD1) == -13.1287830814621 - 15.2007844630679 * II);
            Assert.True(Math.Exp(testComplex1) == (Complex) Math.Exp(testComplexD1));
        }

        [Test]
        public void Log()
        {
            Assert.True(Math.Log(testComplex1) == 1.60943791243410f + 0.92729521800161f * I);
            Assert.True(Math.Log(testComplexD1) == 1.60943791243410 + 0.92729521800161 * II);
            Assert.True(Math.Log(testComplex1) == (Complex) Math.Log(testComplexD1));

            Assert.True(Math.Log(testComplex1, testComplex2) == 0.89698045988556f - 0.08337349347910f * I);
            Assert.True(Math.Log(testComplexD1, testComplexD2) == 0.89698045988556 - 0.08337349347910 * II);
            Assert.True(Math.Log(testComplex1, testComplex2) == (Complex) Math.Log(testComplexD1, testComplexD2));
        }

        [Test]
        public void Pow()
        {
            Assert.True(Math.Pow(testComplex1, testComplex2) == -0.21524869035410f - 0.11124186924370f * I);
            Assert.True(Math.Pow(testComplexD1, testComplexD2) == -0.21524869035410 - 0.11124186924370 * II);
            Assert.True(Math.Pow(testComplex1, testComplex2) == (Complex) Math.Pow(testComplexD1, testComplexD2));
        }

        [Test]
        public void Sigm()
        {
            Assert.True(Math.Sigm(testComplex1) == 1.03207219958826f - 0.04019550765508f * I);
            Assert.True(Math.Sigm(testComplexD1) == 1.03207219958826 - 0.04019550765508 * II);
            Assert.True(Math.Sigm(testComplex1) == (Complex) Math.Sigm(testComplexD1));
        }

        [Test]
        public void Sin()
        {
            Assert.True(Math.Sin(testComplex1) == 3.85373803791937f - 27.0168132580039f * I);
            Assert.True(Math.Sin(testComplexD1) == 3.85373803791937 - 27.0168132580039 * II);
            Assert.True(Math.Sin(testComplex1) == (Complex) Math.Sin(testComplexD1));
        }

        [Test]
        public void Sinh()
        {
            Assert.True(Math.Sinh(testComplex1) == -6.5481200409110f - 7.61923172032141f * I);
            Assert.True(Math.Sinh(testComplexD1) == -6.5481200409110 - 7.61923172032141 * II);
            Assert.True(Math.Sinh(testComplex1) == (Complex) Math.Sinh(testComplexD1));
        }

        [Test]
        public void Sqrt()
        {
            Assert.True(Math.Sqrt(testComplex1) == 2f + I);
            Assert.True(Math.Sqrt(testComplexD1) == 2 + II);
            Assert.True(Math.Sqrt(testComplex1) == (Complex) Math.Sqrt(testComplexD1));
        }

        [Test]
        public void Tan()
        {
            Assert.True(Math.Tan(testComplex1) == -0.0001873462046f + 0.99935598738147f * I);
            Assert.True(Math.Tan(testComplexD1) == -0.0001873462046 + 0.99935598738147 * II);
            Assert.True(Math.Tan(testComplex1) == (Complex) Math.Tan(testComplexD1));
        }

        [Test]
        public void Tanh()
        {
            Assert.True(Math.Tanh(testComplex1) == 1.00070953606723f + 0.00490825806749f * I);
            Assert.True(Math.Tanh(testComplexD1) == 1.00070953606723 + 0.00490825806749 * II);
            Assert.True(Math.Tanh(testComplex1) == (Complex) Math.Tanh(testComplexD1));
        }
    }
}