using System;
using System.Runtime.InteropServices;
using Bronze.Math;
using Bronze.UserInterface;
using glfw3;
using Khronos;
using OpenGL;
using KhronosApi = Khronos.KhronosApi;

namespace Bronze.Core
{
    public static class ContextManager
    {
        internal static IntPtr DefaultContext { get; }

        public static ContextInfo DefaultContextInfo => new ContextInfo(DefaultContext);

        internal static IntPtr ActiveContext => Glfw.GetCurrentContext();

        public static event Action<ContextInfo> ContextChange;

        static ContextManager()
        {
            Gl.Initialize();
            Glfw.Init();
            Glfw.DefaultWindowHints();

            Glfw.WindowHint(Glfw.ContextVersionMajor, 4);
            Glfw.WindowHint(Glfw.ContextVersionMinor, 1);
            Glfw.WindowHint(Glfw.OpenglProfile, Glfw.OpenglCoreProfile);
            Glfw.WindowHint(Glfw.OpenglForwardCompat, Glfw.True);
            Glfw.WindowHint(Glfw.Doublebuffer, Glfw.True);

            Glfw.WindowHint(Glfw.Visible, Glfw.False);

            DefaultContext = Glfw.CreateWindow(1, 1, "", IntPtr.Zero, IntPtr.Zero);
            if(DefaultContext == IntPtr.Zero)
            {
                throw new CoreException("Default OpenGL context failed to initialize.");
            }

            SetActiveContext(DefaultContext);

            Glx.IsRequired = true;
            Gl.BindAPI(new KhronosVersion(4, 1, "gl"), new Gl.Extensions());
        }

        internal static IntPtr CreateContext(Vector2I size, string title)
        {
            Glfw.WindowHint(Glfw.ContextVersionMajor, 4);
            Glfw.WindowHint(Glfw.ContextVersionMinor, 1);
            Glfw.WindowHint(Glfw.OpenglProfile, Glfw.OpenglCoreProfile);
            Glfw.WindowHint(Glfw.OpenglForwardCompat, Glfw.True);
            Glfw.WindowHint(Glfw.Doublebuffer, Glfw.True);

            var context = Glfw.CreateWindow(size.X, size.Y, title, IntPtr.Zero, DefaultContext);
            if(context == IntPtr.Zero) throw new CoreException($"Context \"{title}\" failed to initialize.");

            RunInSeperateContext(() =>
            {
                Glx.IsRequired = true;
                Gl.BindAPI(new KhronosVersion(4, 1, "gl"), new Gl.Extensions());
            }, context);

            return context;
        }

        internal static Window WindowFromHandle(IntPtr handle) => (Window) GCHandle.FromIntPtr(Glfw.GetWindowUserPointer(handle)).Target;

        internal static void EnsureDefaultContext()
        {
            if(DefaultContext == IntPtr.Zero)
                throw new NullReferenceException("Default OpenGL context was prematurely destroyed. Please don't do that.");
        }

        internal static void RunInSeperateContext(Action task, IntPtr context)
        {
            var currentContext = ActiveContext;
            SetActiveContext(context);
            task();
            SetActiveContext(currentContext);
        }

        internal static void RunInDefaultContext(Action task) => RunInSeperateContext(task, DefaultContext);

        internal static void SetActiveContext(IntPtr context)
        {
            if(IsActive(context)) return;
            Glfw.MakeContextCurrent(context);
            ContextChange?.Invoke(new ContextInfo(context));
        }

        internal static bool IsActive(IntPtr context) => context == ActiveContext;
    }
}