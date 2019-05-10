namespace Bronze.Graphics
{
    public interface IColorizeable
    {
        Color ColorMultiplier { get; set; }
        
        Color ColorAddition { get; set; }
        
        Color ColorSubtraction { get; set; }
    }
}