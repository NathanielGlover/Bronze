using System;
using System.Collections.Generic;
using OpenGL;

namespace Bronze.Graphics
{
    public class ContextInfo
    {
        private readonly IntPtr context;

        internal ContextInfo(IntPtr context) => this.context = context;

        public List<string> SupportedExtensions
        {
            get
            {
                var extensions = new List<string>();

                ContextManager.RunInSeperateContext(() =>
                {
                    Gl.Get(GetPName.NumExtensions, out int numExtensions);
                    for(int i = 0; i < numExtensions; i++)
                    {
                        extensions.Add(Gl.GetString(StringName.Extensions, (uint) i));
                    }
                }, context);

                return extensions;
            }
        }

        public double Version
        {
            get
            {
                int major = -1, minor = -1;
                ContextManager.RunInSeperateContext(() =>
                {
                    Gl.Get(Gl.MAJOR_VERSION, out major);
                    Gl.Get(Gl.MINOR_VERSION, out minor);
                }, context);
                return double.Parse($"{major}.{minor}");
            }
        }

        public string VersionString
        {
            get
            {
                string s = "";
                ContextManager.RunInSeperateContext(() => { s = Gl.GetString(StringName.Version); }, context);
                return s;
            }
        }

        public string Vendor
        {
            get
            {
                string s = "";
                ContextManager.RunInSeperateContext(() => { s = Gl.GetString(StringName.Vendor); }, context);
                return s;
            }
        }

        public string Renderer
        {
            get
            {
                string s = "";
                ContextManager.RunInSeperateContext(() => { s = Gl.GetString(StringName.Renderer); }, context);
                return s;
            }
        }

        public string ShadingLanguageVersion
        {
            get
            {
                string s = "";
                ContextManager.RunInSeperateContext(() => { s = Gl.GetString(StringName.ShadingLanguageVersion); }, context);
                return s;
            }
        }
    }
}