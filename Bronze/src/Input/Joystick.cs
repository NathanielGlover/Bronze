using System;
using System.Collections.Generic;
using Bronze.Graphics;
using glfw3;

namespace Bronze.Input
{
    public class Joystick
    {
        private static List<Joystick> PrivateJoysticks { get; } = new List<Joystick>(MaxJoysticks);

        public static IReadOnlyList<Joystick> Joysticks => PrivateJoysticks;

        public const int MaxJoysticks = 16;

        public static Joystick Joystick1 => Joysticks[0];
        public static Joystick Joystick2 => Joysticks[1];
        public static Joystick Joystick3 => Joysticks[2];
        public static Joystick Joystick4 => Joysticks[3];
        public static Joystick Joystick5 => Joysticks[4];
        public static Joystick Joystick6 => Joysticks[5];
        public static Joystick Joystick7 => Joysticks[6];
        public static Joystick Joystick8 => Joysticks[7];
        public static Joystick Joystick9 => Joysticks[8];
        public static Joystick Joystick10 => Joysticks[9];
        public static Joystick Joystick11 => Joysticks[10];
        public static Joystick Joystick12 => Joysticks[11];
        public static Joystick Joystick13 => Joysticks[12];
        public static Joystick Joystick14 => Joysticks[13];
        public static Joystick Joystick15 => Joysticks[14];
        public static Joystick Joystick16 => Joysticks[15];

        static Joystick()
        {
            ContextManager.EnsureDefaultContext();

            for(int i = 0; i < MaxJoysticks; i++)
            {
                PrivateJoysticks.Add(new Joystick(0));
            }

            Glfw.SetJoystickCallback((joy, connectionEvent) => Joysticks[joy].Connected?.Invoke(Joysticks[joy], connectionEvent == Glfw.Connected));
        }

        internal Joystick(int handle) => Handle = handle;

        internal readonly int Handle;

        public event Action<Joystick, bool> Connected;

        public bool IsConnected => Glfw.JoystickPresent(Handle) == Glfw.True;

        public string Name => Glfw.GetJoystickName(Handle);

        public float[] Axes =>
            IsConnected ? Glfw.GetJoystickAxes(Handle, out int _) : throw new NullReferenceException($"Joystick {Handle + 1} is not connected");

        public byte[] ButtonStates =>
            IsConnected ? Glfw.GetJoystickButtons(Handle, out int _) : throw new NullReferenceException($"Joystick {Handle + 1} is not connected");
    }
}