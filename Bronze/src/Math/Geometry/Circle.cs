using System;

namespace Bronze.Math
{
    public class Circle : ParametricShape
    {
        public override float Area => Maths.Pi * Maths.Pow(Radius, 2);

        public sealed override float Perimeter => Circumference;
        
        public float Radius { get; }

        public float Circumference => 2 * Maths.Pi * Radius;
        
        public override Func<float, Vector2> ParametricFunction => t => Radius * new Vector2(Maths.Cos(t), Maths.Sin(t));
        
        public Circle(float radius) => Radius = radius;
    }
}