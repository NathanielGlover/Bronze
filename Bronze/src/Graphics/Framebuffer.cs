using System;
using Bronze.Maths;
using OpenGL;

namespace Bronze.Graphics
{
    public class Framebuffer : GraphicsResource
    {
        public Framebuffer(Vector2I size) :
            base(Gl.GenFramebuffer(), buffer => Gl.BindFramebuffer(FramebufferTarget.Framebuffer, buffer), Gl.DeleteFramebuffers)
        {
            Gl.BindFramebuffer(FramebufferTarget.Framebuffer, Handle);

            //Color texture
            uint colorTexture = Gl.GenTexture();
            int linear = Gl.LINEAR;
            Gl.BindTexture(TextureTarget.Texture2d, colorTexture);
            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMinFilter, ref linear);
            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMagFilter, ref linear);
            Gl.TexImage2D(TextureTarget.Texture2d, 0, InternalFormat.Rgba, size.X, size.Y, 0, PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);
            Gl.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2d, colorTexture, 0);
            
            Texture = new Texture(colorTexture, size);

            //Depth render buffer
            uint depth = Gl.GenRenderbuffer();
            Gl.BindRenderbuffer(RenderbufferTarget.Renderbuffer, depth);
            Gl.RenderbufferStorage(RenderbufferTarget.Renderbuffer, InternalFormat.DepthComponent, size.X, size.Y);
            Gl.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, RenderbufferTarget.Renderbuffer, depth);

            //Set OpenGL state
            Gl.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            Gl.Enable(EnableCap.Blend);
            Gl.Enable(EnableCap.Multisample);
            Gl.Enable(EnableCap.DepthTest);

            if(Gl.CheckFramebufferStatus(FramebufferTarget.Framebuffer) != FramebufferStatus.FramebufferComplete)
            {
                throw new Exception("Framebuffer failed to initialize.");
            }
            
            Gl.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }
        
        public Texture Texture { get; }
    }
}