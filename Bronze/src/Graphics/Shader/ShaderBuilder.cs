using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenGL;

namespace Bronze.Graphics
{
    public class ShaderBuilder
    {
        private readonly List<ShaderStage> shaderStages = new List<ShaderStage>();

        public void ClearCurrentBuild()
        {
            foreach(var stage in shaderStages) stage.Dispose();

            shaderStages.Clear();
        }

        public ShaderBuilder AddStage(ShaderStage stage)
        {
            shaderStages.Add(stage);
            return this;
        }

        public ShaderBuilder AddStages(IEnumerable<ShaderStage> stages)
        {
            shaderStages.AddRange(stages);
            return this;
        }

        public Shader BuildShader()
        {
            uint programHandle = Gl.CreateProgram();
            Gl.ProgramParameter(programHandle, ProgramParameterPName.ProgramSeparable, Gl.TRUE);

            foreach(var stage in shaderStages) Gl.AttachShader(programHandle, stage.Handle);

            Gl.LinkProgram(programHandle);

            Gl.GetProgram(programHandle, ProgramProperty.LinkStatus, out int linked);
            if(linked != Gl.TRUE)
            {
                Gl.GetProgram(programHandle, ProgramProperty.InfoLogLength, out int logLength);
                var errorLog = new StringBuilder(logLength);
                Gl.GetProgramInfoLog(programHandle, logLength, out int _, errorLog);
                throw new Exception("Shader Linking Errors: \n" + errorLog);
            }

            foreach(var stage in shaderStages) Gl.DetachShader(programHandle, stage.Handle);

            ClearCurrentBuild();
            return new Shader(programHandle);
        }

        public ShaderPipeline BuildPipeline()
        {
            var vertexStages = from stage in shaderStages where stage.Type == ShaderStage.StageType.Vertex select stage;
            var geometryStages = from stage in shaderStages where stage.Type == ShaderStage.StageType.Geometry select stage;
            var fragmentStages = from stage in shaderStages where stage.Type == ShaderStage.StageType.Fragment select stage;

            SingularShader CreateProgram(IEnumerable<ShaderStage> programStages) =>
                SingularShader.FromShader(new ShaderBuilder().AddStages(programStages).BuildShader());

            var vertexShader = CreateProgram(vertexStages);
            var geometryShader = CreateProgram(geometryStages);
            var fragmentShader = CreateProgram(fragmentStages);

            ClearCurrentBuild();
            return new ShaderPipeline(vertexShader, geometryShader, fragmentShader);
        }
    }
}