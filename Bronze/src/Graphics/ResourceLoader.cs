using System;
using System.IO;
using System.Linq;
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
            uint program = 0;
            
            ContextManager.RunInDefaultContext(() =>
            {
                program = Gl.CreateProgram();
                uint vert = Gl.CreateShader(ShaderType.VertexShader);
                uint frag = Gl.CreateShader(ShaderType.FragmentShader);

                var vertLines = File.ReadAllLines(vertexFile);
                for(int i = 0; i < vertLines.Length; i++)
                {
                    vertLines[i] += "\n"; //File.ReadAllLines doesn't add newline characters apparently
                }
                
                Gl.ShaderSource(vert, vertLines);
                Gl.CompileShader(vert);
                
                var errorLog = new StringBuilder();
                Gl.GetShaderInfoLog(vert, 512, out int length, errorLog);
                if(length > 0)
                {
                    throw new Exception("Vertex Shader Compile Errors: \n" + errorLog);
                }

                var fragLines = File.ReadAllLines(fragmentFile);
                for(int i = 0; i < fragLines.Length; i++)
                {
                    fragLines[i] += "\n";
                }
                
                Gl.ShaderSource(frag, fragLines);
                Gl.CompileShader(frag);
            
                Gl.GetShaderInfoLog(frag, 512, out length, errorLog);
                if(length > 0)
                {
                    throw new Exception("Fragment Shader Compile Errors: \n" + errorLog);
                }
            
                Gl.AttachShader(program, vert);
                Gl.AttachShader(program, frag);
                Gl.LinkProgram(program);

                Gl.GetProgramInfoLog(program, 512, out length, errorLog);
                if(length > 0)
                {
                    throw new Exception("Shader Compile Errors: \n" + errorLog);
                }
                
                Gl.DeleteShader(vert);
                Gl.DeleteShader(frag);
            });
            
            return new Shader(program);
        }
    }
}