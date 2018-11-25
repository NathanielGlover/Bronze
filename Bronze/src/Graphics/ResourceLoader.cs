using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using Bronze.Maths;
using glfw3;
using OpenGL;
using PixelFormat = OpenGL.PixelFormat;
using Rectangle = System.Drawing.Rectangle;

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

            if(!File.Exists(vertexFile))
            {
                vertexFile = ResourceDirectory + vertexFile;
            }

            if(!File.Exists(fragmentFile))
            {
                fragmentFile = ResourceDirectory + fragmentFile;
            }

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

        public static Texture LoadTexture(string path)
        {
            uint handle = 0;

            if(!File.Exists(path))
            {
                path = ResourceDirectory + path;
            }

            var image = Image.FromFile(path);

            ContextManager.RunInDefaultContext(() =>
            {
                handle = Gl.GenTexture();
                Gl.ActiveTexture(TextureUnit.Texture0);
                Gl.BindTexture(TextureTarget.Texture2d, handle);

                var repeat = TextureWrapMode.Repeat;
                Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureWrapS, ref repeat);
                Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureWrapT, ref repeat);

                int blue = Gl.BLUE;
                int green = Gl.GREEN;
                int red = Gl.RED;
                int alpha = Gl.ALPHA;
                Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureSwizzleR, ref blue);
                Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureSwizzleG, ref green);
                Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureSwizzleB, ref red);
                Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureSwizzleA, ref alpha);

                var linearMipmapLinear = TextureMinFilter.LinearMipmapLinear;
                var linear = TextureMagFilter.Linear;
                Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMinFilter, ref linearMipmapLinear);
                Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMagFilter, ref linear);

                var bitmap = new Bitmap(image);
                var data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly,
                    System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                Gl.TexImage2D(TextureTarget.Texture2d, 0, InternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte,
                    data.Scan0);
                Gl.GenerateMipmap(TextureTarget.Texture2d);
            });

            return new Texture(new Vector2I(image.Width, image.Height), handle);
        }
    }
}