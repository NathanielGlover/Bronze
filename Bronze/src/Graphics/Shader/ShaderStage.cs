using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using OpenGL;

namespace Bronze.Graphics
{
    public class ShaderStage : IDisposable
    {
        public enum StageType
        {
            Vertex = ShaderType.VertexShader,
            Geometry = ShaderType.GeometryShader,
            Fragment = ShaderType.FragmentShader
        }

        private ShaderStage(uint handle, StageType type)
        {
            Handle = handle;
            Type = type;
        }

        internal uint Handle { get; }

        public StageType Type { get; }

        public void Dispose() => Gl.DeleteShader(Handle);

        public static ShaderStage FromFile(string path, StageType type)
        {
            if(!File.Exists(path)) throw new IOException($"Could not find file: {path}");

            var lines = File.ReadAllLines(path);
            return FromSource(lines, type);
        }

        public static ShaderStage FromSource(string code, StageType type) => FromSource(code.Split("\n"), type);

        public static ShaderStage FromSource(IEnumerable<string> codeLines, StageType type)
        {
            uint handle = Gl.CreateShader((ShaderType) type);
            var sourceCodeLines = codeLines.ToArray();

            for(int i = 0; i < sourceCodeLines.Length; i++) sourceCodeLines[i] += sourceCodeLines[i].EndsWith("\n") ? "" : "\n";

            Gl.ShaderSource(handle, sourceCodeLines);
            Gl.CompileShader(handle);

            Gl.GetShader(handle, ShaderParameterName.CompileStatus, out int compiled);

            if(compiled != Gl.TRUE)
            {
                Gl.GetShader(handle, ShaderParameterName.InfoLogLength, out int logLength);
                var errorLog = new StringBuilder(logLength);
                Gl.GetShaderInfoLog(handle, logLength, out int _, errorLog);
                throw new Exception("Shader Stage Compile Errors: \n" + errorLog);
            }

            return new ShaderStage(handle, type);
        }
    }
}