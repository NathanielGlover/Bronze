namespace Bronze.Maths
{
    public class Square : RegularPolygon
    {
        public Square(float sideLength) : base(4, sideLength) { }

        public override float Area => SideLength * SideLength;
    }
}