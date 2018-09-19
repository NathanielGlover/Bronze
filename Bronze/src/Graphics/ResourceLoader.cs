using System;
using System.IO;
using System.Text;
using OpenGL;

namespace Bronze.Graphics
{
    public static class ResourceLoader
    {
        private static string resourceDirectory;

        public static string ResourceDirectory
        {
            get => resourceDirectory;
            set
            {
                resourceDirectory = value;
                if(!(resourceDirectory.EndsWith("/") || resourceDirectory.EndsWith("\\")))
                {
                    if(Environment.OSVersion.Platform == PlatformID.Unix) //Mac and Linux
                    {
                        resourceDirectory += "/";
                    }
                    else //Windows
                    {
                        resourceDirectory += "\\";
                    }
                }

                if(!Directory.Exists(resourceDirectory))
                {
                    throw new ArgumentException($"Directory \"{resourceDirectory}\" does not exist.");
                }
            }
        }

        static ResourceLoader()
        {
            ContextManager.EnsureDefaultContext();
        }

        //TODO: Redo this system, temporary substitute for ShaderBuilder class
        public static Shader LoadShader(string vertexFile, string fragmentFile)
        {
            uint program = Gl.CreateProgram();
            uint vert = Gl.CreateShader(ShaderType.VertexShader);
            uint frag = Gl.CreateShader(ShaderType.FragmentShader);

            var vertLines = File.ReadAllLines(vertexFile);
            Gl.ShaderSource(vert, vertLines);
            Gl.AttachShader(program, vert);

            var fragLines = File.ReadAllLines(fragmentFile);
            Gl.ShaderSource(frag, fragLines);
            Gl.AttachShader(program, frag);
            
            Gl.LinkProgram(program);

            var errors = new StringBuilder();
            Gl.GetProgramInfoLog(program, int.MaxValue, out int length, errors);

            if(length > 0)
            {
                throw new Exception("Shader Compile Error: " + errors);
            }
            
            return new Shader(program);
        }
    }
}