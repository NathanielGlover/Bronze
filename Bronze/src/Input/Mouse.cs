using System;
using Bronze.Maths;
using glfw3;

namespace Bronze.Input
{
    public enum MouseAction
    {
        Click = Glfw.Press,
        Release = Glfw.Release
    }

    public enum MouseButton
    {
        Left = Glfw.MouseButtonLeft,
        Right = Glfw.MouseButtonRight,
        Middle = Glfw.MouseButtonMiddle,
        Button4 = Glfw.MouseButton4,
        Button5 = Glfw.MouseButton5,
        Button6 = Glfw.MouseButton6,
        Button7 = Glfw.MouseButton7,
        Button8 = Glfw.MouseButton8
    }
    
    public class Mouse
    {
        public enum CursorMode
        {
            Normal = Glfw.CursorNormal,
            Disabled = Glfw.CursorDisabled,
            Hidden = Glfw.CursorHidden
        }

        public Window Window { get; }

        public event Action<Window, Vector2> Move;

        public event Action<Window, bool> Enter;

        public event Action<Window, MouseButton, MouseAction, ModKey> Click;

        public event Action<Window, Vector2> Scroll;

        public Vector2 CursorPosition
        {
            get
            {
                Glfw.GetCursorPos(Window.Handle, out double x, out double y);
                return new Vector2((float) x, (float) y);
            }

            set => Glfw.SetCursorPos(Window.Handle, value.X, value.Y);
        }

        public CursorMode Mode
        {
            get => (CursorMode) Glfw.GetInputMode(Window.Handle, Glfw.Cursor);
            set => Glfw.SetInputMode(Window.Handle, Glfw.Cursor, (int) value);
        }

        public Cursor Cursor
        {
            set => Glfw.SetCursor(Window.Handle, value.Handle);
        }

        internal Mouse(Window window)
        {
            Window = window;
            Glfw.SetCursorPosCallback(window.Handle, (sender, x, y) => Move?.Invoke(Window.WindowFromHandle(sender), new Vector2((float) x, (float) y)));
            Glfw.SetCursorEnterCallback(window.Handle, (sender, entered) => Enter?.Invoke(Window.WindowFromHandle(sender), entered == Glfw.True));
            Glfw.SetMouseButtonCallback(window.Handle, (sender, button, action, mods) =>
            {
                Click?.Invoke(Window.WindowFromHandle(sender), (MouseButton) button, (MouseAction) action, (ModKey) mods);
            });
            Glfw.SetScrollCallback(window.Handle, (sender, x, y) => Scroll?.Invoke(Window.WindowFromHandle(sender), new Vector2((float) x, (float) y)));
        }
    }
}