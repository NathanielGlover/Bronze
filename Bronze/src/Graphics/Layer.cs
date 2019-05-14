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

            var model = new ModelBuilder(new Rectangle((Vector2) size * (1f / size.X)))
                .AddAttribute(1, (i, vertex) => texCoords[i])
                .BuildModel();

            Model = model;
        }

        public override void Draw(ShaderPipeline pipeline, params Effect[] effects)
        {
            Framebuffer.RunActionWhileBound(() =>
            {
                Gl.ClearColor(0, 0, 0, 0);
                Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                Render?.Invoke();
            });

            foreach(var effect in effects) effect.ApplyEffect(pipeline);
            base.Draw(pipeline);
            foreach(var effect in effects) effect.SetDefaults(pipeline);
        }
    }
}