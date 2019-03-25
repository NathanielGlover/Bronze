using Bronze.Maths;

namespace Bronze.Graphics
{
    public abstract class FullRenderEffect : Effect
    {
        protected FullRenderEffect(Shader shader) : base(shader) { }

        private Matrix3 model = Matrix3.Identity;
        private Matrix3 view = Matrix3.Identity;
        private Matrix3 projection = Matrix3.Identity;

        private void UpdateTransform()
        {
            Shader.SetUniform("transform", projection * view * model);
        }

        internal Matrix3 Model
        {
            get => model;
            set
            {
                model = value;
                UpdateTransform();
            }
        }

        internal Matrix3 View
        {
            get => view;
            set
            {
                view = value;
                UpdateTransform();
            }
        }

        internal Matrix3 Projection
        {
            get => projection;
            set
            {
                projection = value;
                UpdateTransform();
            }
        }
    }
}