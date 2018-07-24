namespace Bronze.Math
{
    #region Temporary Laziness Fix

    public static class DefaultITransformableInterfaceImplementationExtensions
    {
        public static void Translate(this ITransformable transformable, Vector2 translation) => transformable.Transform.Translate(translation);

        public static void Rotate(this ITransformable transformable, float angle) => transformable.Transform.Rotate(angle);

        public static void HorizontalShear(this ITransformable transformable, float factor) => transformable.Transform.HorizontalShear(factor);

        public static void VerticalShear(this ITransformable transformable, float factor) => transformable.Transform.VerticalShear(factor);

        public static void Shear(this ITransformable transformable, Vector2 factor) => transformable.Transform.Shear(factor);

        public static void HorizontalScale(this ITransformable transformable, float factor) => transformable.Transform.HorizontalScale(factor);

        public static void VerticalScale(this ITransformable transformable, float factor) => transformable.Transform.VerticalScale(factor);

        public static void Scale(this ITransformable transformable, float factor) => transformable.Transform.Scale(factor);

        public static void Scale(this ITransformable transformable, Vector2 factor) => transformable.Transform.Scale(factor);
    }

    #endregion

    public interface ITransformable //TODO: When C# 8 is released, add default implementations and get rid of the nasty thing above
    {
        Transform Transform { get; set; }
    }
}