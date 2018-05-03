using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Bronze.Core;
using Bronze.Math;
using glfw3;
using OpenTK.Graphics.OpenGL4;

namespace Bronze.UserInterface
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
        public static Window ActiveWindow => ContextManager.WindowFromHandle(ContextManager.ActiveContext);

        public static void PollEvents() => Glfw.PollEvents();
        
        public static void WaitEvents() => Glfw.WaitEvents();
        
        public static void PostEmptyEvent() => Glfw.PostEmptyEvent();

        private readonly GLFWwindow window;
        
        private string title;
        
        private bool vSync;

        public event Action Closed;
        
        public event Action<bool> Focused;
        
        public event Action<Vector2I> Moved;
        
        public event Action<Vector2I> Resized;
        
        public event Action<bool> Minimized;
        
        public event Action<Vector2D> Scrolled;
        
        public event Action<List<string>> FilesDropped;

        public Window(Vector2I size, string title, WindowFlags flags = WindowFlags.Default)
        {
            this.title = title;
            ContextManager.EnsureDefaultContext();
            Glfw.DefaultWindowHints();

            Glfw.WindowHint((int) State.ContextVersionMajor, 4);
            Glfw.WindowHint((int) State.ContextVersionMinor, 1);
            Glfw.WindowHint((int) State.OpenglProfile, (int) State.OpenglCoreProfile);
            Glfw.WindowHint((int) State.OpenglForwardCompat, (int) State.True);
            Glfw.WindowHint((int) State.Doublebuffer, (int) State.True);

            int resizable = (int) (flags.HasFlag(WindowFlags.Resizable) ? State.True : State.False);
            int focused = (int) (flags.HasFlag(WindowFlags.Focused) ? State.True : State.False);
            int bordered = (int) (flags.HasFlag(WindowFlags.Bordered) ? State.True : State.False);
            int visible = (int) (flags.HasFlag(WindowFlags.Visible) ? State.True : State.False);
            int maximized = (int) (flags.HasFlag(WindowFlags.Maximized) ? State.True : State.False);
            int floating = (int) (flags.HasFlag(WindowFlags.Floating) ? State.True : State.False);

            Glfw.WindowHint((int) State.Resizable, resizable);
            Glfw.WindowHint((int) State.Focused, focused);
            Glfw.WindowHint((int) State.Decorated, bordered);
            Glfw.WindowHint((int) State.Visible, visible);
            Glfw.WindowHint((int) State.Maximized, maximized);
            Glfw.WindowHint((int) State.Floating, floating);

            window = ContextManager.CreateContext(size, title);

            if(window == null)
            {
                throw new Exception("Window failed to create. Your computer is probably broken. Try replacing it.");
            }

            VSync = Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX;

            Center();

            var windowHandle = GCHandle.Alloc(this, GCHandleType.Weak);
            windowHandle.Target = this;
            Glfw.SetWindowUserPointer(window, GCHandle.ToIntPtr(windowHandle));

            unsafe
            {
                Glfw.SetWindowCloseCallback(window, ptr => ContextManager.WindowFromPointer(ptr).Closed?.Invoke());
                Glfw.SetWindowFocusCallback(window, (ptr, gainedFocus) => ContextManager.WindowFromPointer(ptr).Focused?.Invoke(gainedFocus == 1));
                Glfw.SetWindowPosCallback(window, (ptr, x, y) => ContextManager.WindowFromPointer(ptr).Moved?.Invoke(new Vector2I(x, y)));
                Glfw.SetWindowSizeCallback(window, (ptr, width, height) =>
                    ContextManager.WindowFromPointer(ptr).Resized?.Invoke(new Vector2I(width, height)));
                Glfw.SetWindowIconifyCallback(window, (ptr, minimized) => ContextManager.WindowFromPointer(ptr).Minimized?.Invoke(minimized == 1));
                Glfw.SetScrollCallback(window, (ptr, x, y) => ContextManager.WindowFromPointer(ptr).Scrolled?.Invoke(new Vector2D(x, y)));
                Glfw.SetDropCallback(window, (ptr, count, paths) =>
                {
                    var pathsList = new List<string>(count);

                    for(int i = 0; i < count; i++)
                    {
                        var stringPtr = new IntPtr(paths[i]);
                        pathsList.Add(Marshal.PtrToStringAuto(stringPtr));
                    }

                    ContextManager.WindowFromPointer(ptr).FilesDropped?.Invoke(pathsList);
                });
            }
        }

        public ContextInfo ContextInfo => new ContextInfo(window);

        public bool VSync
        {
            get => vSync;

            set
            {
                vSync = value;
                ContextManager.RunInSeperateContext(() => Glfw.SwapInterval(value ? 1 : 0), window);
            }
        }

        public Vector2I FrameSize
        {
            get
            {
                int width = 0, height = 0;
                Glfw.GetFramebufferSize(window, ref width, ref height);
                return new Vector2I(width, height);
            }
        }

        public Vector2I Size
        {
            get
            {
                int width = 0, height = 0;
                Glfw.GetWindowSize(window, ref width, ref height);
                return new Vector2I(width, height);
            }

            set => Glfw.SetWindowSize(window, value.X, value.X);
        }

        public Vector2I Position
        {
            get
            {
                int x = 0, y = 0;
                Glfw.GetWindowPos(window, ref x, ref y);
                return new Vector2I(x, y);
            }

            set => Glfw.SetWindowPos(window, value.X, value.Y);
        }

        public string Title
        {
            get => title;

            set
            {
                title = value;
                Glfw.SetWindowTitle(window, value);
            }
        }

        public void Clear() => Clear(new Vector3(0.1f, 0.1f, 0.1f));

        public void Clear(Vector3 color)
        {
            ContextManager.RunInSeperateContext(() =>
            {
                GL.ClearColor(color.X, color.Y, color.Z, 1.0f);
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            }, window);
        }

        public void SwapBuffers() => Glfw.SwapBuffers(window);

        public void Activate() => SetActive(true);

        public void Deactivate() => SetActive(false);

        public void SetActive(bool active) => ContextManager.SetActiveContext(active ? window : ContextManager.ActiveContext);

        public bool IsActive => ContextManager.IsActive(window);

        public bool IsOpen => Glfw.WindowShouldClose(window) != (int) State.True;

        public bool IsMinimized => Glfw.GetWindowAttrib(window, (int) State.Iconified) == 1;

        public bool IsMaximized => Glfw.GetWindowAttrib(window, (int) State.Maximized) == 1;

        public bool IsFocused => Glfw.GetWindowAttrib(window, (int) State.Focused) == 1;

        public bool IsVisible => Glfw.GetWindowAttrib(window, (int) State.Visible) == 1;

        public void Close() => Glfw.SetWindowShouldClose(window, (int) State.True);

        public void Minimize() => Glfw.IconifyWindow(window);

        public void Maximize() => Glfw.MaximizeWindow(window);

        public void Restore() => Glfw.RestoreWindow(window);

        public void Focus() => Glfw.FocusWindow(window);

        public void Show() => Glfw.ShowWindow(window);

        public void Hide() => Glfw.HideWindow(window);

        public void SetVisible(bool visible)
        {
            if(visible)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        public void SetSizeLimits(Vector2I minSize, Vector2I maxSize) => Glfw.SetWindowSizeLimits(window, minSize.X, minSize.Y, maxSize.X, maxSize.Y);

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