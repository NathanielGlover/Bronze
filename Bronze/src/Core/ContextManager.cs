using System;
using glfw3;

namespace Bronze.Core
{
    public static class ContextManager
    {
        internal static GLFWwindow DefaultContext { get; }

        public static ContextInfo DefaultContextInfo => new ContextInfo(DefaultContext);

        internal static GLFWwindow ActiveContext => Glfw.GetCurrentContext();

        public static event Action<ContextInfo> ContextChange;

        static ContextManager()
        {
            //Get OpenTK to play nicely with GLFW.NET
            try
            {
                var nativeWindow = new OpenTK.NativeWindow
                (
                    1, 1, "",
                    OpenTK.GameWindowFlags.Default,
                    OpenTK.Graphics.GraphicsMode.Default,
                    OpenTK.DisplayDevice.Default
                );

                var context = new OpenTK.Graphics.GraphicsContext
                (
                    OpenTK.Graphics.GraphicsMode.Default,
                    nativeWindow.WindowInfo, 4, 1,
                    OpenTK.Graphics.GraphicsContextFlags.ForwardCompatible
                );

                context.MakeCurrent(nativeWindow.WindowInfo);
                context.LoadAll();

                Glfw.Init();
            }
            catch(Exception e)
            {
                throw new CoreException("Bronze failed to initialize: " + e.Message);
            }

            Glfw.DefaultWindowHints();
            
            Glfw.WindowHint((int) State.ContextVersionMajor, 4);
            Glfw.WindowHint((int) State.ContextVersionMinor, 1);
            Glfw.WindowHint((int) State.OpenglProfile, (int) State.OpenglCoreProfile);
            Glfw.WindowHint((int) State.OpenglForwardCompat, (int) State.True);
            Glfw.WindowHint((int) State.Doublebuffer, (int) State.True);
            
            Glfw.WindowHint((int) State.Visible, (int) State.False);
            
            DefaultContext = Glfw.CreateWindow(1, 1, "", null, null);
            if(DefaultContext == null)
            {
                throw new CoreException("Default OpenGL context failed to initialize.");
            }

            SetActiveContext(DefaultContext);
        }

        internal static GLFWwindow CreateContext(int width, int height, string title)
        {
            Glfw.WindowHint((int) State.ContextVersionMajor, 4);
            Glfw.WindowHint((int) State.ContextVersionMinor, 1);
            Glfw.WindowHint((int) State.OpenglProfile, (int) State.OpenglCoreProfile);
            Glfw.WindowHint((int) State.OpenglForwardCompat, (int) State.True);
            Glfw.WindowHint((int) State.Doublebuffer, (int) State.True);

            var context = Glfw.CreateWindow(width, height, title, null, DefaultContext);
            if(context == null) throw new CoreException($"Context \"{title}\" failed to initialize.");
            return context;
        }

        internal static void EnsureDefaultContext()
        {
            if(DefaultContext == null) throw new NullReferenceException("Default OpenGL context was prematurely destroyed. Please don't do that.");
        }

        internal static void RunInSeperateContext(Action task, GLFWwindow context)
        {
            var currentContext = ActiveContext;
            SetActiveContext(context);
            task();
            SetActiveContext(currentContext);
        }

        internal static void RunInDefaultContext(Action task) => RunInSeperateContext(task, DefaultContext);

        internal static void SetActiveContext(GLFWwindow context)
        {
            Glfw.MakeContextCurrent(context);
            ContextChange?.Invoke(new ContextInfo(context));
        }

        internal static bool IsActive(GLFWwindow context) => context == ActiveContext;
    }
}