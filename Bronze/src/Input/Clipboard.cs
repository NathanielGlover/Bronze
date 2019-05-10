using System;
using glfw3;

namespace Bronze.Input
{
    public static class Clipboard
    {
        public static string Content
        {
            get => Glfw.GetClipboardString(IntPtr.Zero);
            set => Glfw.SetClipboardString(IntPtr.Zero, value);
        }
    }
}