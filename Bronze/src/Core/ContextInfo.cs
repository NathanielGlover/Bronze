using System.Collections.Generic;
using glfw3;
using OpenTK.Graphics.OpenGL4;

namespace Bronze.Core
{
    public class ContextInfo
    {
        private GLFWwindow context;

        internal ContextInfo(GLFWwindow context) => this.context = context;

        public List<string> SupportedExtensions
        {
            get
            {
                var extensions = new List<string>();

                for(int i = 0; i < GL.GetInteger(GetPName.NumExtensions); i++)
                {
                    extensions.Add(GL.GetString(StringNameIndexed.Extensions, i));
                }

                return extensions;
            }
        }

        public double Version => double.Parse($"{GL.GetInteger(GetPName.MajorVersion)}.{GL.GetInteger(GetPName.MinorVersion)}");

        public string VersionString => GL.GetString(StringName.Version);

        public string Vendor => GL.GetString(StringName.Vendor);

        public string Renderer => GL.GetString(StringName.Renderer);

        public string ShadingLanguageVersion => GL.GetString(StringName.ShadingLanguageVersion);
    }
}