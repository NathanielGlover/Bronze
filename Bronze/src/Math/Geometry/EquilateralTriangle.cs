namespace Bronze.Math
{
    public class EquilateralTriangle : RegularPolygon
    {
        public EquilateralTriangle(float sideLength) : base(3, sideLength) { }

        public override float Area => Maths.Sqrt(3f) / 4 * SideLength * SideLength;
    }
}