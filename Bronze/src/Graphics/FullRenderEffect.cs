using Bronze.Maths;

namespace Bronze.Graphics
{
    public abstract class FullRenderEffect : Effect
    {
        public FullRenderEffect(Shader shader) : base(shader)
        {
            transformLoc = Shader.GetUniformLocation("transform");
            viewLoc = Shader.GetUniformLocation("view");
            projLoc = Shader.GetUniformLocation("projection");
        }

        private readonly int transformLoc;
        private Matrix3 transform = Matrix3.Identity;

        private readonly int viewLoc;
        private Matrix3 view = Matrix3.Identity;

        private readonly int projLoc;
        private Matrix3 projection = Matrix3.Identity;

        internal Matrix3 Transform
        {
            get => transform;
            set
            {
                transform = value;
                Shader.SetUniform(transformLoc, transform);
            }
        }

        internal Matrix3 View
        {
            get => view;
            set
            {
                view = value;
                Shader.SetUniform(viewLoc, view);
            }
        }

        internal Matrix3 Projection
        {
            get => projection;
            set
            {
                projection = value;
                Shader.SetUniform(projLoc, projection);
            }
        }
    }
}