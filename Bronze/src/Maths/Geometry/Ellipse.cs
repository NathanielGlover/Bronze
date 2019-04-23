using System;

namespace Bronze.Maths
{
    public class Ellipse : ParametricShape
    {
        public override float Area => Math.Pi * SemiHorizontalAxis * SemiVerticalAxis;

        public override float Perimeter
        {
            get
            {
                float a = SemiHorizontalAxis;
                float b = SemiVerticalAxis;
                float h = Math.Pow(a - b, 2) / Math.Pow(a + b, 2);

                //Approximation for ellipse perimeter by Ramanujan
                float perimeter = Math.Pi * (a + b);
                perimeter *= 1 + 3 * h / (10 + Math.Sqrt(4 - 3 * h));
                return perimeter;
            }
        }

        public override Func<float, Vector2> ParametricFunction => t => new Vector2(SemiHorizontalAxis * Math.Cos(t), SemiVerticalAxis * Math.Sin(t));

        public float SemiHorizontalAxis { get; }

        public float SemiVerticalAxis { get; }

        private Ellipse(float semiHorizontalAxis, float semiVerticalAxis, int numApproximationVertices) : base(numApproximationVertices)
        {
            SemiHorizontalAxis = semiHorizontalAxis;
            SemiVerticalAxis = semiVerticalAxis;
        }

        public static Ellipse FromAxes(float semiHorizontalAxis, float semiVerticalAxis) =>
            new Ellipse(semiHorizontalAxis, semiVerticalAxis, DefaultNumApproximationVertices);

        public static Ellipse FromAxes(float semiHorizontalAxis, float semiVerticalAxis, int numApproximationVertices) =>
            new Ellipse(semiHorizontalAxis, semiVerticalAxis, numApproximationVertices);
    }
}