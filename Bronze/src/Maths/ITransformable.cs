namespace Bronze.Maths
{
    public interface ITransformable
    {
        Transform Transform { get; set; }
        
        Projection Projection { get; set; }
        
//        View View { get; set; }
    }
}