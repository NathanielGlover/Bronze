using System;

namespace Bronze.Math
{
    using M = System.Math;

    public static class MathExtensions
    {
        public static float Trim(this float num) => Maths.Trim(num);

        public static float Trim(this float num, float tolerance) => Maths.Trim(num, tolerance);

        public static double Trim(this double num) => Maths.Trim(num);

        public static double Trim(this double num, double tolerance) => Maths.Trim(num, tolerance);

        public static float Magnitude(this float num) => Maths.Abs(num);

        public static double Magnitude(this double num) => Maths.Abs(num);
    }

    public static class Maths
    {
        #region Real
        
        public const float E = (float) M.E;

        public const float Pi = (float) M.PI;

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

        public static float Product(Func<int, float> series, int beginning, int end)
        {
            float product = 1;
                
            for(int i = beginning; i <= end; i++)
            {
                product *= series(i);
            }

            return product;
        }

        public static float Round(float x) => (float) M.Round(x);

        public static float Round(float x, int digits) => (float) M.Round(x, digits);

        public static float Round(float x, MidpointRounding mode) => (float) M.Round(x, mode);

        public static float Round(float x, int digits, MidpointRounding mode) => (float) M.Round(x, digits, mode);

        public static float Sigm(float x) => 1 / (1 + Exp(-x));

        public static float Sign(float x) => Abs(x) / x;

        public static float Sin(float x) => (float) M.Sin(x);

        public static float Sinh(float x) => (float) M.Sinh(x);

        public static float Sqrt(float x) => (float) M.Sqrt(x);
        
        public static float Sum(Func<int, float> series, int beginning, int end)
        {
            float sum = 0;
                
            for(int i = beginning; i <= end; i++)
            {
                sum += series(i);
            }

            return sum;
        }

        public static float Tan(float x) => (float) M.Tan(x);

        public static float Tanh(float x) => (float) M.Tanh(x);

        public static float Trim(float x) => EqualsWithTolerance(x, Round(x)) ? Round(x) : x;

        public static float Trim(float x, float tolerance) => EqualsWithTolerance(x, Round(x), tolerance) ? Round(x) : x;

        #endregion
        
        #region Double Precision Real
        
        // ReSharper disable once InconsistentNaming
        public const double EE = M.E;

        public const double PiD = M.PI;

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

        public static double Product(Func<int, double> series, int beginning, int end)
        {
            double product = 1;
                
            for(int i = beginning; i <= end; i++)
            {
                product *= series(i);
            }

            return product;
        }

        public static double Round(double x) => M.Round(x);

        public static double Round(double x, int digits) => M.Round(x, digits);

        public static double Round(double x, MidpointRounding mode) => M.Round(x, mode);

        public static double Round(double x, int digits, MidpointRounding mode) => M.Round(x, digits, mode);

        public static double Sigm(double x) => 1 / (1 + Exp(-x));

        public static double Sign(double x) => Abs(x) / x;

        public static double Sin(double x) => M.Sin(x);

        public static double Sinh(double x) => M.Sinh(x);

        public static double Sqrt(double x) => M.Sqrt(x);       

        public static double Sum(Func<int, double> series, int beginning, int end)
        {
            double sum = 0;
                
            for(int i = beginning; i <= end; i++)
            {
                sum += series(i);
            }

            return sum;
        }

        public static double Tan(double x) => M.Tan(x);

        public static double Tanh(double x) => M.Tanh(x);

        public static double Trim(double x) => EqualsWithTolerance(x, Round(x)) ? Round(x) : x;

        public static double Trim(double x, double tolerance) => EqualsWithTolerance(x, Round(x), tolerance) ? Round(x) : x;

        #endregion
        
        #region Complex

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

        public static Complex Product(Func<int, Complex> series, int beginning, int end)
        {
            Complex product = 1;
                
            for(int i = beginning; i <= end; i++)
            {
                product *= series(i);
            }

            return product;
        }

        public static Complex Sigm(Complex z) => 1 / (1 + Exp(-z));

        public static Complex Sin(Complex z) => (Exp(I * z) - Exp(-I * z)) / (2 * I);

        public static Complex Sinh(Complex z) => (Exp(z) - Exp(-z)) / 2;

        public static Complex Sqrt(Complex z) => Pow(z, 0.5f);

        public static Complex Sum(Func<int, Complex> series, int beginning, int end)
        {
            Complex sum = 0;
                
            for(int i = beginning; i <= end; i++)
            {
                sum += series(i);
            }

            return sum;
        }

        public static Complex Tan(Complex z) => Sin(z) / Cos(z);

        public static Complex Tanh(Complex z) => Sinh(z) / Cosh(z);
        
        #endregion
        
        #region Double Precision Complex

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

        public static ComplexD Product(Func<int, ComplexD> series, int beginning, int end)
        {
            ComplexD product = 1;
                
            for(int i = beginning; i <= end; i++)
            {
                product *= series(i);
            }

            return product;
        }

        public static ComplexD Sigm(ComplexD z) => 1 / (1 + Exp(-z));

        public static ComplexD Sin(ComplexD z) => (Exp(II * z) - Exp(-II * z)) / (2 * II);

        public static ComplexD Sinh(ComplexD z) => (Exp(z) - Exp(-z)) / 2;

        public static ComplexD Sqrt(ComplexD z) => Pow(z, 0.5);

        public static ComplexD Sum(Func<int, ComplexD> series, int beginning, int end)
        {
            ComplexD sum = 0;
                
            for(int i = beginning; i <= end; i++)
            {
                sum += series(i);
            }

            return sum;
        }

        public static ComplexD Tan(ComplexD z) => Sin(z) / Cos(z);

        public static ComplexD Tanh(ComplexD z) => Sinh(z) / Cosh(z);

        #endregion
        
        #region Vector

        public static Vector2 Abs(Vector2 v) => new Vector2(Abs(v.X), Abs(v.Y));

        public static Vector2 Acos(Vector2 v) => new Vector2(Acos(v.X), Acos(v.Y));

        public static Vector2 Asin(Vector2 v) => new Vector2(Asin(v.X), Asin(v.Y));

        public static Vector2 Atan(Vector2 v) => new Vector2(Atan(v.X), Atan(v.Y));

        public static Vector2 Cos(Vector2 v) => new Vector2(Cos(v.X), Cos(v.Y));

        public static Vector2 Cosh(Vector2 v) => new Vector2(Cosh(v.X), Cosh(v.Y));

        public static Vector2 Exp(Vector2 v) => new Vector2(Exp(v.X), Exp(v.Y));

        public static Vector2 Log(Vector2 v) => new Vector2(Log(v.X), Log(v.Y));

        public static Vector2 Log(Vector2 v, float b) => new Vector2(Log(v.X, b), Log(v.Y, b));

        public static Vector2 Pow(Vector2 v, float b) => new Vector2(Pow(v.X, b), Log(v.Y, b));

        public static Vector2 Sigm(Vector2 v) => new Vector2(Sigm(v.X), Sigm(v.Y));

        public static Vector2 Sin(Vector2 v) => new Vector2(Sin(v.X), Sin(v.Y));

        public static Vector2 Sinh(Vector2 v) => new Vector2(Sinh(v.X), Sinh(v.Y));

        public static Vector2 Sqrt(Vector2 v) => new Vector2(Sqrt(v.X), Sqrt(v.Y));

        public static Vector2 Tan(Vector2 v) => new Vector2(Tan(v.X), Tan(v.Y));

        public static Vector2 Tanh(Vector2 v) => new Vector2(Tanh(v.X), Tanh(v.Y));
        
        #endregion
        
        #region Double Precision Vector

        public static Vector2D Abs(Vector2D v) => new Vector2D(Abs(v.X), Abs(v.Y));

        public static Vector2D Acos(Vector2D v) => new Vector2D(Acos(v.X), Acos(v.Y));

        public static Vector2D Asin(Vector2D v) => new Vector2D(Asin(v.X), Asin(v.Y));

        public static Vector2D Atan(Vector2D v) => new Vector2D(Atan(v.X), Atan(v.Y));

        public static Vector2D Cos(Vector2D v) => new Vector2D(Cos(v.X), Cos(v.Y));

        public static Vector2D Cosh(Vector2D v) => new Vector2D(Cosh(v.X), Cosh(v.Y));

        public static Vector2D Exp(Vector2D v) => new Vector2D(Exp(v.X), Exp(v.Y));

        public static Vector2D Log(Vector2D v) => new Vector2D(Log(v.X), Log(v.Y));

        public static Vector2D Log(Vector2D v, float b) => new Vector2D(Log(v.X, b), Log(v.Y, b));

        public static Vector2D Pow(Vector2D v, float b) => new Vector2D(Pow(v.X, b), Log(v.Y, b));

        public static Vector2D Sigm(Vector2D v) => new Vector2D(Sigm(v.X), Sigm(v.Y));

        public static Vector2D Sin(Vector2D v) => new Vector2D(Sin(v.X), Sin(v.Y));

        public static Vector2D Sinh(Vector2D v) => new Vector2D(Sinh(v.X), Sinh(v.Y));

        public static Vector2D Sqrt(Vector2D v) => new Vector2D(Sqrt(v.X), Sqrt(v.Y));

        public static Vector2D Tan(Vector2D v) => new Vector2D(Tan(v.X), Tan(v.Y));

        public static Vector2D Tanh(Vector2D v) => new Vector2D(Tanh(v.X), Tanh(v.Y));
        
        #endregion

        #region Matrix

        public static Matrix3 CreateTranslationMatrix(Vector2 translation) => new Matrix3
        (
            1, 0, translation.X,
            0, 1, translation.Y,
            0, 0, 1
        );

        public static Matrix3 CreateRotationMatrix(float rotation)
        {
            float cos = Cos(rotation);
            float sin = Sin(rotation);

            return new Matrix3
            (
                cos, -sin, 0,
                sin, cos, 0,
                0, 0, 1
            );
        }

        public static Matrix3 CreateScaleMatrix(float scaleFactor) => scaleFactor * Matrix3.Identity;

        public static Matrix3 CreateScaleMatrix(Vector2 scaleFactor) => new Matrix3
        (
            scaleFactor.X, 0, 0,
            0, scaleFactor.Y, 0,
            0, 0, 1
        );

        public static Matrix3 CreateShearMatrix(Vector2 shearFactor) => new Matrix3
        (
            1, shearFactor.X, 0,
            shearFactor.Y, 1, 0,
            0, 0, 1
        );
        
        #endregion
        
        #region Double Precision Matrix

        public static Matrix3D CreateTranslationMatrix(Vector2D translation) => new Matrix3D
        (
            1, 0, translation.X,
            0, 1, translation.Y,
            0, 0, 1
        );

        public static Matrix3D CreateRotationMatrix(double rotation)
        {
            double cos = Cos(rotation);
            double sin = Sin(rotation);

            return new Matrix3D
            (
                cos, -sin, 0,
                sin, cos, 0,
                0, 0, 1
            );
        }

        public static Matrix3D CreateScaleMatrix(double scaleFactor) => scaleFactor * Matrix3D.Identity;

        public static Matrix3D CreateScaleMatrix(Vector2D scaleFactor) => new Matrix3D
        (
            scaleFactor.X, 0, 0,
            0, scaleFactor.Y, 0,
            0, 0, 1
        );

        public static Matrix3D CreateShearMatrix(Vector2D shearFactor) => new Matrix3D
        (
            1, shearFactor.X, 0,
            shearFactor.Y, 1, 0,
            0, 0, 1
        );
        
        #endregion
    }
}