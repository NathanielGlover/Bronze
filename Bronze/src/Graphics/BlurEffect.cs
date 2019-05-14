using System;
using Bronze.Graphics;
using Bronze.Maths;

namespace Bronze
{
    public class BlurEffect : Effect
    {
        public float Intensity { get; set; } = 1f / 1000;

        public override void ApplyEffect(ShaderPipeline pipeline)
        {
            pipeline.FragmentShader.SetUniform(ReservedUniforms.ConvolutionReach, Intensity);
            pipeline.FragmentShader.SetUniform(ReservedUniforms.Convolution, (new Matrix3
            (
                1, 2, 1,
                2, 4, 2,
                1, 2, 1
            ) * (1f / 16)).SingleIndexedValues);
        }

        public override void SetDefaults(ShaderPipeline pipeline)
        {
            pipeline.FragmentShader.SetUniform(ReservedUniforms.ConvolutionReach, 0);
            pipeline.FragmentShader.SetUniform(ReservedUniforms.Convolution, new float[] {0, 0, 0, 0, 1, 0, 0, 0, 0});
        }
    }
}