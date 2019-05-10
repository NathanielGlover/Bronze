namespace Bronze.Maths
{
    public class EquilateralTriangle : RegularPolygon
    {
        public EquilateralTriangle(float sideLength) : base(3, sideLength) { }

        public override float Area => Math.Sqrt(3f) / 4 * SideLength * SideLength;
    }
}