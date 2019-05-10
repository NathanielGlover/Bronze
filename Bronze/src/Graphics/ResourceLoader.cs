using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Bronze.Maths;
using OpenGL;
using PixelFormat = System.Drawing.Imaging.PixelFormat;
using Rectangle = System.Drawing.Rectangle;

namespace Bronze.Graphics
{
    public static class ResourceLoader
    {
        private static string resourceDirectory;

        static ResourceLoader()
        {
            ContextManager.EnsureDefaultContext();
        }

        public static string ResourceDirectory
        {
            get => resourceDirectory;
            set
            {
                resourceDirectory = value;
                if(!(resourceDirectory.EndsWith("/") || resourceDirectory.EndsWith("\\")))
                {
                    if(Environment.OSVersion.Platform == PlatformID.Unix) //Mac and Linux
                        resourceDirectory += "/";
                    else //Windows
                        resourceDirectory += "\\";
                }

                if(!Directory.Exists(resourceDirectory)) throw new ArgumentException($"Directory \"{resourceDirectory}\" does not exist.");
            }
        }

        public static Texture LoadTexture(string path)
        {
            if(!File.Exists(path)) path = ResourceDirectory + path;

            var image = Image.FromFile(path);
            
            uint handle = Gl.GenTexture();
            Gl.ActiveTexture(TextureUnit.Texture0);
            Gl.BindTexture(TextureTarget.Texture2d, handle);

            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureWrapS, TextureWrapMode.Repeat);
            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureWrapT, TextureWrapMode.Repeat);

            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureSwizzleR, Gl.BLUE);
            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureSwizzleG, Gl.GREEN);
            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureSwizzleB, Gl.RED);
            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureSwizzleA, Gl.ALPHA);

            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMinFilter, TextureMinFilter.LinearMipmapLinear);
            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMagFilter, TextureMagFilter.Linear);

            var bitmap = new Bitmap(image);
            var data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            Gl.TexImage2D(TextureTarget.Texture2d, 0, InternalFormat.Rgba, image.Width, image.Height, 0, OpenGL.PixelFormat.Rgba,
                PixelType.UnsignedByte,
                data.Scan0);
            Gl.GenerateMipmap(TextureTarget.Texture2d);

            return new Texture(handle, new Vector2I(image.Width, image.Height));
        }
    }
}