using Bronze.Maths;

namespace Bronze.Graphics
{
    public abstract class FullRenderEffect : Effect
    {
        public FullRenderEffect(Shader shader) : base(shader) { }

        private Matrix3 transform = Matrix3.Identity;
        private Matrix3 view = Matrix3.Identity;
        private Matrix3 projection = Matrix3.Identity;

        internal Matrix3 Transform
        {
            get => transform;
            set
            {
                transform = value;
                Shader.SetUniform("transform", transform);
            }
        }

        internal Matrix3 View
        {
            get => view;
            set
            {
                view = value;
                Shader.SetUniform("view", view);
            }
        }

        internal Matrix3 Projection
        {
            get => projection;
            set
            {
                projection = value;
                Shader.SetUniform("projection", projection);
            }
        }
    }
}