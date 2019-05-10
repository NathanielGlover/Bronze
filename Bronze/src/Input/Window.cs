using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Bronze.Graphics;
using Bronze.Maths;
using glfw3;
using OpenGL;

/*TODO: Use GLFW 3.3 features
 Monitor work area
 Monitor content scale
 Monitor user pointer
 Frame autosaving (macOS only)
 Automatic graphics switching (macOS only)
 Window opacity
 Window content scale (?)
 Request attention to window/application
 Dynamic changing of some of the window attributes
 Maximize callback
 Content scale callback
 Clipboard functions don't need window parameter anymore
 All the gamepad/joystick stuff
 All the new window hints
 */

namespace Bronze.Input
{
    [Flags]
    public enum WindowFlags
    {
        Resizable = 1,
        Focused = 2,
        Bordered = 4,
        Visible = 8,
        Maximized = 16,
        Floating = 32,
        Default = Resizable | Focused | Bordered | Visible
    }

    public class Window
    {
        internal static Window WindowFromHandle(IntPtr handle) => (Window) GCHandle.FromIntPtr(Glfw.GetWindowUserPointer(handle)).Target;
        
        public static void PollEvents() => Glfw.PollEvents();

        public static void WaitEvents() => Glfw.WaitEvents();

        public static void PostEmptyEvent() => Glfw.PostEmptyEvent();

        internal readonly IntPtr Handle;

        private string title;

        private bool vSync;

        public event Action<Window> Closed;

        public event Action<Window, bool> Focused;

        public event Action<Window, Vector2I> Moved;

        public event Action<Window, Vector2I> Resized;

        public event Action<Window, bool> Minimized;

        public event Action<Window, List<string>> FilesDropped;

        public Window(int width, int height, string title, WindowFlags flags = WindowFlags.Default) : this(new Vector2I(width, height), title, flags) { }

        public Window(Vector2I size, string title, WindowFlags flags = WindowFlags.Default)
        {
            this.title = title;
            ContextManager.EnsureDefaultContext();
            Glfw.DefaultWindowHints();

            int resizable = flags.HasFlag(WindowFlags.Resizable) ? Glfw.True : Glfw.False;
            int focused = flags.HasFlag(WindowFlags.Focused) ? Glfw.True : Glfw.False;
            int bordered = flags.HasFlag(WindowFlags.Bordered) ? Glfw.True : Glfw.False;
            int visible = flags.HasFlag(WindowFlags.Visible) ? Glfw.True : Glfw.False;
            int maximized = flags.HasFlag(WindowFlags.Maximized) ? Glfw.True : Glfw.False;
            int floating = flags.HasFlag(WindowFlags.Floating) ? Glfw.True : Glfw.False;

            Glfw.WindowHint(Glfw.Resizable, resizable);
            Glfw.WindowHint(Glfw.Focused, focused);
            Glfw.WindowHint(Glfw.Decorated, bordered);
            Glfw.WindowHint(Glfw.Visible, visible);
            Glfw.WindowHint(Glfw.Maximized, maximized);
            Glfw.WindowHint(Glfw.Floating, floating);

            Handle = ContextManager.CreateContext(size, title);
            if(Handle == IntPtr.Zero)
            {
                throw new Exception("Window failed to create. Your computer is probably broken. Try replacing it.");
            }

            ContextManager.SetActiveContext(Handle);

            VSync = Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX;

            Center();

            var windowHandle = GCHandle.Alloc(this, GCHandleType.Weak);
            windowHandle.Target = this;
            Glfw.SetWindowUserPointer(Handle, GCHandle.ToIntPtr(windowHandle));

            Glfw.SetWindowCloseCallback(Handle, ptr => WindowFromHandle(ptr).Closed?.Invoke(WindowFromHandle(ptr)));
            Glfw.SetWindowFocusCallback(Handle, (ptr, gainedFocus) => WindowFromHandle(ptr).Focused?.Invoke(WindowFromHandle(ptr), gainedFocus == 1));
            Glfw.SetWindowPosCallback(Handle, (ptr, x, y) => WindowFromHandle(ptr).Moved?.Invoke(WindowFromHandle(ptr), new Vector2I(x, y)));
            Glfw.SetWindowSizeCallback(Handle, (ptr, width, height) =>
                WindowFromHandle(ptr).Resized?.Invoke(WindowFromHandle(ptr), new Vector2I(width, height)));
            Glfw.SetWindowIconifyCallback(Handle, (ptr, minimized) => WindowFromHandle(ptr).Minimized?.Invoke(WindowFromHandle(ptr), minimized == 1));
            Glfw.SetDropCallback(Handle, (ptr, count, paths) =>
                WindowFromHandle(ptr).FilesDropped?.Invoke(WindowFromHandle(ptr), new List<string>(paths)));

            //Set OpenGL state
            Gl.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            Gl.Enable(EnableCap.Blend);
            Gl.Enable(EnableCap.Multisample);
            Gl.Enable(EnableCap.DepthTest);
        }

        ~Window() => Glfw.DestroyWindow(Handle);

        public ContextInfo ContextInfo => new ContextInfo(Handle);

        public bool VSync
        {
            get => vSync;

            set
            {
                vSync = value;
                ContextManager.RunInSeparateContext(() => Glfw.SwapInterval(value ? 1 : 0), Handle);
            }
        }

        public Vector2I FrameSize
        {
            get
            {
                Glfw.GetFramebufferSize(Handle, out int width, out int height);
                return new Vector2I(width, height);
            }
        }

        public Vector2I Size
        {
            get
            {
                Glfw.GetWindowSize(Handle, out int width, out int height);
                return new Vector2I(width, height);
            }

            set => Glfw.SetWindowSize(Handle, value.X, value.X);
        }

        public Vector2I Position
        {
            get
            {
                Glfw.GetWindowPos(Handle, out int x, out int y);
                return new Vector2I(x, y);
            }

            set => Glfw.SetWindowPos(Handle, value.X, value.Y);
        }

        public string Title
        {
            get => title;

            set
            {
                title = value;
                Glfw.SetWindowTitle(Handle, value);
            }
        }

        public Monitor Monitor => new Monitor(Glfw.GetWindowMonitor(Handle));

        public void Clear() => Clear(Color.Black);

        public void Clear(Color color)
        {
            ContextManager.RunInSeparateContext(() =>
            {
                Gl.ClearColor(color.Red, color.Green, color.Blue, color.Alpha);
                Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            }, Handle);
        }

        public void SwapBuffers() => Glfw.SwapBuffers(Handle);

        public bool IsOpen => Glfw.WindowShouldClose(Handle) != Glfw.True;

        public bool IsMinimized => Glfw.GetWindowAttrib(Handle, Glfw.Iconified) == 1;

        public bool IsMaximized => Glfw.GetWindowAttrib(Handle, Glfw.Maximized) == 1;

        public bool IsFocused => Glfw.GetWindowAttrib(Handle, Glfw.Focused) == 1;

        public void Close() => Glfw.SetWindowShouldClose(Handle, Glfw.True);

        public void Minimize() => Glfw.IconifyWindow(Handle);

        public void Maximize() => Glfw.MaximizeWindow(Handle);

        public void Restore() => Glfw.RestoreWindow(Handle);

        public void Focus() => Glfw.FocusWindow(Handle);

        public void Show() => Glfw.ShowWindow(Handle);

        public void Hide() => Glfw.HideWindow(Handle);

        public bool Visible
        {
            get => Glfw.GetWindowAttrib(Handle, Glfw.Visible) == 1;

            set
            {
                if(value)
                {
                    Show();
                }
                else
                {
                    Hide();
                }
            }
        }

        public void SetSizeLimits(Vector2I minSize, Vector2I maxSize) => Glfw.SetWindowSizeLimits(Handle, minSize.X, minSize.Y, maxSize.X, maxSize.Y);

        public void Center()
        {
            var vidmode = Glfw.GetVideoMode(Glfw.GetPrimaryMonitor());
            var windowSize = Size;
            Position = new Vector2I
            (
                (vidmode.Width - windowSize.X) / 2,
                (vidmode.Height - windowSize.Y) / 2
            );
        }
    }
}