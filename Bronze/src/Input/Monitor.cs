using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Bronze.Maths;
using glfw3;

namespace Bronze.Input
{
    public class Monitor
    {
        internal static Monitor MonitorFromHandle(IntPtr handle) => (Monitor) GCHandle.FromIntPtr(Glfw.GetMonitorUserPointer(handle)).Target;

        public static event Action<Monitor, bool> Connected;
        
        public static Monitor PrimaryMonitor => new Monitor(Glfw.GetPrimaryMonitor());

        public static List<Monitor> ConnectedMonitors
        {
            get
            {
                var connectedMonitors = new List<Monitor>();
                var monitorHandles = Glfw.GetMonitors(out int count);
                for(int i = 0; i < count; i++)
                {
                    connectedMonitors[i] = new Monitor(monitorHandles[i]);
                }

                return connectedMonitors;
            }
        }

        internal readonly IntPtr Handle;

        private readonly Glfw.VidMode videoMode;

        private float gamma = 1;

        static Monitor()
        {
            Glfw.SetMonitorCallback((handle, connected) => Connected?.Invoke(MonitorFromHandle(handle), connected == Glfw.Connected));
        }

        internal Monitor(IntPtr handle)
        {
            Handle = handle;
            videoMode = Glfw.GetVideoMode(handle);
            
            var monitorHandle = GCHandle.Alloc(this, GCHandleType.Weak);
            monitorHandle.Target = this;
            Glfw.SetMonitorUserPointer(Handle, GCHandle.ToIntPtr(monitorHandle));
        }

        public string Name => Glfw.GetMonitorName(Handle);

        public Vector2I Position
        {
            get
            {
                Glfw.GetMonitorPos(Handle, out int x, out int y);
                return new Vector2I(x, y);
            }
        }

        public Vector2I PhysicalSize
        {
            get
            {
                Glfw.GetMonitorPhysicalSize(Handle, out int width, out int height);
                return new Vector2I(width, height);
            }
        }

        public Vector2I Size => new Vector2I(videoMode.Width, videoMode.Height);

        public int RefreshRate => videoMode.RefreshRate;

        public float Gamma
        {
            get => gamma;

            set
            {
                gamma = value;
                Glfw.SetGamma(Handle, value);
            }
        }
    }
}