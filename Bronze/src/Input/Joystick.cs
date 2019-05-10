using System;
using System.Collections.Generic;
using System.Linq;
using Bronze.Graphics;
using glfw3;

namespace Bronze.Input
{
    public class Joystick
    {
        [Flags]
        public enum HatState
        {
            Centered = Glfw.HatCentered,
            Up = Glfw.HatUp,
            RightUp = Glfw.HatRightUp,
            Right = Glfw.HatRight,
            RightDown = Glfw.HatRightDown,
            Down = Glfw.HatDown,
            LeftDown = Glfw.HatLeftDown,
            Left = Glfw.HatLeft,
            LeftUp = Glfw.HatLeftUp
        }

        private static List<Joystick> PrivateJoysticks { get; } = new List<Joystick>(MaxConnectedJoysticks);

        public static IReadOnlyList<Joystick> Joysticks => PrivateJoysticks;

        public const int MaxConnectedJoysticks = 16;

        static Joystick()
        {
            ContextManager.EnsureDefaultContext();

            for(int i = 0; i < MaxConnectedJoysticks; i++)
            {
                PrivateJoysticks.Add(new Joystick(0));
            }

            Glfw.SetJoystickCallback((joy, connectionEvent) => Connected?.Invoke(Joysticks[joy], connectionEvent == Glfw.Connected));
        }

        internal Joystick(int handle) => Id = handle;

        public int Id { get; }

        public static event Action<Joystick, bool> Connected;

        public bool IsConnected => Glfw.JoystickPresent(Id) == Glfw.True;

        public string Name => Glfw.GetJoystickName(Id);

        public float[] Axes =>
            IsConnected ? Glfw.GetJoystickAxes(Id, out int _) : throw new NullReferenceException($"Joystick {Id + 1} is not connected.");

        public bool[] Buttons
        {
            get
            {
                var buttons = IsConnected
                    ? Glfw.GetJoystickButtons(Id, out int _)
                    : throw new NullReferenceException($"Joystick {Id + 1} is not connected.");
                
                return (from button in buttons select button == 1).ToArray();
            }
        }

        public HatState[] Hats
        {
            get
            {
                var hats = IsConnected ? Glfw.GetJoystickHats(Id, out int _) : throw new NullReferenceException($"Joystick {Id + 1} is not connected.");
                return (from hat in hats select (HatState) hat).ToArray();
            }
        }
    }
}