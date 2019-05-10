using System;
using System.Collections.Generic;
using Bronze.Graphics;

namespace Bronze.Maths
{
    public class Circle : ParametricShape
    {
        public override float Area => Math.Pi * Math.Pow(Radius, 2);

        public sealed override float Perimeter => Circumference;
        
        public override IEnumerable<uint> GetElementIndices() => new uint[0];
        
        public override DrawType GetPreferredDrawType() => DrawType.TriangleFan;

        public float Radius { get; }

        public float Circumference => 2 * Math.Pi * Radius;
        
        public override Func<float, Vector2> ParametricFunction => t => Radius * new Vector2(Math.Cos(t), Math.Sin(t));
        
        public Circle(float radius) => Radius = radius;
    }
}