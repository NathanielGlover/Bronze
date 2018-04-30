using System;

namespace Bronze.Math
{
    using M = System.Math;

    public static class Math
    {
        public const float E = (float) M.E;

        public const float Pi = (float) M.PI;

        //------//
        // Real //
        //------//

        public const float Tolerance = 1e-5f;

        public static int Abs(int x) => M.Abs(x);

        public static float Abs(float x) => M.Abs(x);

        public static float Acos(float x) => (float) M.Acos(x);

        public static float Asin(float x) => (float) M.Asin(x);

        public static float Atan(float x) => (float) M.Atan(x);

        public static float Atan2(float y, float x) => (float) M.Atan2(y, x);

        public static float Ceiling(float x) => (float) M.Ceiling(x);

        public static float Cos(float x) => (float) M.Cos(x);

        public static float Cosh(float x) => (float) M.Cosh(x);

        public static float Differentiate(Func<float, float> functionOfX, float x, float dx = 1e-3f) =>
            (functionOfX(x + dx) - functionOfX(x - dx)) / (2 * dx);

        public static bool EqualsWithTolerance(float left, float right) => Abs(left - right) < Tolerance;

        public static bool EqualsWithTolerance(float left, float right, float tolerance) => Abs(left - right) < tolerance;

        public static float Exp(float x) => (float) M.Exp(x);

        public static float Floor(float x) => (float) M.Floor(x);

        public static float Integrate(Func<float, float> functionOfX, float a, float b, float dx = 1e-2f)
        {
            float integral = 0;

            for(float i = a; i <= b; i += dx)
            {
                integral += dx * (functionOfX(i) + functionOfX(i + dx)) / 2;
            }

            return integral;
        }

        public static float Log(float x) => (float) M.Log(x);

        public static float Log(float x, float b) => (float) M.Log(x, b);

        public static float Log2(float x) => Log(x, 2);

        public static float Log10(float x) => (float) M.Log10(x);

        public static float Pow(float x, float b) => (float) M.Pow(x, b);

        public static float Round(float x) => (float) M.Round(x);

        public static float Round(float x, int digits) => (float) M.Round(x, digits);

        public static float Round(float x, MidpointRounding mode) => (float) M.Round(x, mode);

        public static float Round(float x, int digits, MidpointRounding mode) => (float) M.Round(x, digits, mode);

        public static float Sigm(float x) => 1 / (1 + Exp(-x));

        public static float Sign(float x) => Abs(x) / x;

        public static float Sin(float x) => (float) M.Sin(x);

        public static float Sinh(float x) => (float) M.Sinh(x);

        public static float Sqrt(float x) => (float) M.Sqrt(x);

        public static float Tan(float x) => (float) M.Tan(x);

        public static float Tanh(float x) => (float) M.Tanh(x);

        public static float Trim(float x) => EqualsWithTolerance(x, 0) ? 0 : x;

        //-----------------------//
        // Double-Precision Real //
        //-----------------------//

        public const double ToleranceD = 1e-12;

        public static double Abs(double x) => M.Abs(x);

        public static double Acos(double x) => M.Acos(x);

        public static double Asin(double x) => M.Asin(x);

        public static double Atan(double x) => M.Atan(x);

        public static double Atan2(double y, double x) => M.Atan2(y, x);

        public static double Ceiling(double x) => M.Ceiling(x);

        public static double Cos(double x) => M.Cos(x);

        public static double Cosh(double x) => M.Cosh(x);

        public static double Differentiate(Func<double, double> functionOfX, double x, double dx = 1e-5) =>
            (functionOfX(x + dx) - functionOfX(x - dx)) / (2 * dx);

        public static bool EqualsWithTolerance(double left, double right) => Abs(left - right) < ToleranceD;

        public static bool EqualsWithTolerance(double left, double right, double tolerance) => Abs(left - right) < tolerance;

        public static double Exp(double x) => M.Exp(x);

        public static double Floor(double x) => M.Floor(x);

        public static double Integrate(Func<double, double> functionOfX, double a, double b, double dx = 1e-4)
        {
            double integral = 0;

            for(double i = a; i <= b; i += dx)
            {
                integral += dx * (functionOfX(i) + functionOfX(i + dx)) / 2;
            }

            return integral;
        }

        public static double Log(double x) => M.Log(x);

        public static double Log(double x, double b) => M.Log(x, b);

        public static double Log2(double x) => Log(x, 2);

        public static double Log10(double x) => M.Log10(x);

        public static double Pow(double x, double b) => M.Pow(x, b);

        public static double Round(double x) => M.Round(x);

        public static double Round(double x, int digits) => M.Round(x, digits);

        public static double Round(double x, MidpointRounding mode) => M.Round(x, mode);

        public static double Round(double x, int digits, MidpointRounding mode) => M.Round(x, digits, mode);

        public static double Sigm(double x) => 1 / (1 + Exp(-x));

        public static double Sign(double x) => Abs(x) / x;

        public static double Sin(double x) => M.Sin(x);

        public static double Sinh(double x) => M.Sinh(x);

        public static double Sqrt(double x) => M.Sqrt(x);

        public static double Tan(double x) => M.Tan(x);

        public static double Tanh(double x) => M.Tanh(x);

        public static double Trim(double x) => EqualsWithTolerance(x, 0) ? 0 : x;

        //---------//
        // Complex //
        //---------//

        public static readonly Complex I = new Complex(0, 1);

        public static float Abs(Complex z) => z.Magnitude;

        public static Complex Acos(Complex z) => -I * Log(z + Sqrt(Pow(z, 2) - 1));

        public static Complex Asin(Complex z) => -I * Log(I * z + Sqrt(1 - Pow(z, 2)));

        public static Complex Atan(Complex z) => I / 2 * (Log(1 - I * z) - Log(1 + I * z));

        public static Complex Cos(Complex z) => (Exp(I * z) + Exp(-I * z)) / 2;

        public static Complex Cosh(Complex z) => (Exp(z) + Exp(-z)) / 2;

        public static Complex Exp(Complex z) => Exp(z.Re) * (Cos(z.Im) + I * Sin(z.Im));

        public static Complex Log(Complex z) => Log(z.Magnitude) + I * z.Phase;

        public static Complex Log(Complex z, Complex b) => Log(z) / Log(b);

        public static Complex Pow(Complex z, Complex b) => Exp(Log(z) * b);

        public static Complex Sigm(Complex z) => 1 / (1 + Exp(-z));

        public static Complex Sin(Complex z) => (Exp(I * z) - Exp(-I * z)) / (2 * I);

        public static Complex Sinh(Complex z) => (Exp(z) - Exp(-z)) / 2;

        public static Complex Sqrt(Complex z) => Pow(z, 0.5f);

        public static Complex Tan(Complex z) => Sin(z) / Cos(z);

        public static Complex Tanh(Complex z) => Sinh(z) / Cosh(z);

        //--------------------------//
        // Double-Precision Complex //
        //--------------------------//

        // ReSharper disable once InconsistentNaming
        public static readonly ComplexD II = new ComplexD(0, 1);

        public static double Abs(ComplexD z) => z.Magnitude;

        public static ComplexD Acos(ComplexD z) => -II * Log(z + Sqrt(Pow(z, 2) - 1));

        public static ComplexD Asin(ComplexD z) => -II * Log(II * z + Sqrt(1 - Pow(z, 2)));

        public static ComplexD Atan(ComplexD z) => II / 2 * (Log(1 - II * z) - Log(1 + II * z));

        public static ComplexD Cos(ComplexD z) => (Exp(II * z) + Exp(-II * z)) / 2;

        public static ComplexD Cosh(ComplexD z) => (Exp(z) + Exp(-z)) / 2;

        public static ComplexD Exp(ComplexD z) => M.Exp(z.Re) * (M.Cos(z.Im) + II * M.Sin(z.Im));

        public static ComplexD Log(ComplexD z) => M.Log(z.Magnitude) + II * z.Phase;

        public static ComplexD Log(ComplexD z, ComplexD b) => Log(z) / Log(b);

        public static ComplexD Pow(ComplexD z, ComplexD b) => Exp(Log(z) * b);

        public static ComplexD Sigm(ComplexD z) => 1 / (1 + Exp(-z));

        public static ComplexD Sin(ComplexD z) => (Exp(II * z) - Exp(-II * z)) / (2 * II);

        public static ComplexD Sinh(ComplexD z) => (Exp(z) - Exp(-z)) / 2;

        public static ComplexD Sqrt(ComplexD z) => Pow(z, 0.5);

        public static ComplexD Tan(ComplexD z) => Sin(z) / Cos(z);

        public static ComplexD Tanh(ComplexD z) => Sinh(z) / Cosh(z);
    }
}