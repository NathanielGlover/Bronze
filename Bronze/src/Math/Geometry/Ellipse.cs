using System;

namespace Bronze.Math
{
    public class Ellipse : ParametricShape
    {
        public override float Area => Maths.Pi * MajorAxis * MinorAxis;

        public override float Perimeter
        {
            get
            {
                float a = MajorAxis;
                float b = MinorAxis;
                float h = Maths.Pow(a - b, 2) / Maths.Pow(a + b, 2);

                //Approximation for ellipse perimeter by Ramanujan
                float perimeter = Maths.Pi * (a + b);
                perimeter *= 1 + 3 * h / (10 + Maths.Sqrt(4 - 3 * h));
                return perimeter;
            }
        }
        
        public override Func<float, Vector2> ParametricFunction => t => new Vector2(MajorAxis * Maths.Cos(t), MinorAxis * Maths.Sin(t));

        public float MajorAxis { get; }

        public float MinorAxis { get; }

        public Ellipse(float majorAxis, float semiminorAxis)
        {
            MajorAxis = majorAxis;
            MinorAxis = semiminorAxis;
        }
    }
}