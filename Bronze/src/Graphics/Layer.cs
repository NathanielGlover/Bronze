using System;
using Bronze.Maths;
using OpenGL;

namespace Bronze.Graphics
{
    public class Layer : Sprite
    {
        public event Action Render;

        public Framebuffer Framebuffer { get; }

        public Layer(Vector2I size)
        {
            Framebuffer = new Framebuffer(size);
            Texture = Framebuffer.Texture;

            var texCoords = new[]
            {
                new Vector2(0, 0),
                new Vector2(0, 1),
                new Vector2(1, 1),
                new Vector2(1, 0)
            };

            var model = new ModelBuilder(0, new Rectangle((Vector2) size * (1f / size.X)))
                .AddAttribute(1, (i, vertex) => texCoords[i])
                .BuildModel();

            Model = model;
        }

        public override void Draw(ShaderPipeline shaderPipeline)
        {
            Framebuffer.RunActionWhileBound(() =>
            {
                Gl.ClearColor(0.5f, 0.5f, 0.5f, 1f);
                Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                Render?.Invoke();
            });

            base.Draw(shaderPipeline);
        }
    }
}