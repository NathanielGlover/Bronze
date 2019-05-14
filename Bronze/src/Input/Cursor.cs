using System;
using glfw3;

namespace Bronze.Input
{
    public class Cursor
    {
        public static readonly Cursor Standard = new Cursor(Glfw.CreateStandardCursor(Glfw.ArrowCursor));
        
        public static readonly Cursor TextBeam = new Cursor(Glfw.CreateStandardCursor(Glfw.IbeamCursor));
        
        public static readonly Cursor Crosshair = new Cursor(Glfw.CreateStandardCursor(Glfw.CrosshairCursor));
        
        public static readonly Cursor Hand = new Cursor(Glfw.CreateStandardCursor(Glfw.HandCursor));
        
        public static readonly Cursor HorizontalResize = new Cursor(Glfw.CreateStandardCursor(Glfw.HresizeCursor));
        
        public static readonly Cursor VerticalResize = new Cursor(Glfw.CreateStandardCursor(Glfw.VresizeCursor));
        
        public static readonly Cursor Invisible = new Cursor(IntPtr.Zero);

        internal IntPtr Handle { get; }

        internal Cursor(IntPtr handle) => Handle = handle;
    }
}