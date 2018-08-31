using System;

namespace Bronze.Maths
{
    public class Ellipse : ParametricShape
    {
        public override float Area => Math.Pi * MajorAxis * MinorAxis;

        public override float Perimeter
        {
            get
            {
                float a = MajorAxis;
                float b = MinorAxis;
                float h = Math.Pow(a - b, 2) / Math.Pow(a + b, 2);

                //Approximation for ellipse perimeter by Ramanujan
                float perimeter = Math.Pi * (a + b);
                perimeter *= 1 + 3 * h / (10 + Math.Sqrt(4 - 3 * h));
                return perimeter;
            }
        }
        
        public override Func<float, Vector2> ParametricFunction => t => new Vector2(MajorAxis * Math.Cos(t), MinorAxis * Math.Sin(t));

        public float MajorAxis { get; }

        public float MinorAxis { get; }

        public Ellipse(float majorAxis, float semiminorAxis)
        {
            MajorAxis = majorAxis;
            MinorAxis = semiminorAxis;
        }
    }
}