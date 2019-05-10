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

        public static readonly Joystick Null = new Joystick(-1);

        private static List<Joystick> PrivateJoysticks { get; } = new List<Joystick>(MaxConnectedJoysticks);

        public static IReadOnlyList<Joystick> Joysticks => PrivateJoysticks;

        public const int MaxConnectedJoysticks = 16;

        static Joystick()
        {
            ContextManager.EnsureDefaultContext();

            for(int i = 0; i < MaxConnectedJoysticks; i++)
            {
                PrivateJoysticks.Add(new Joystick(i));
            }

//            Glfw.SetJoystickCallback((joy, connectionEvent) => Connected?.Invoke(Joysticks[joy], connectionEvent == Glfw.Connected));
        }

        internal Joystick(int handle) => Id = handle;

        public int Id { get; }

        public static event Action<Joystick, bool> Connected;

        public bool IsConnected => Glfw.JoystickPresent(Id) == Glfw.True;

        public string Name => Glfw.GetJoystickName(Id);

        public string Guid => Glfw.GetJoystickGUID(Id);

        public float[] Axes
        {
            get
            {
                if(!IsConnected) return new float[6];
                return Glfw.GetJoystickAxes(Id, out int _);
            }
        }

        public bool[] Buttons
        {
            get
            {
                if(!IsConnected) return new bool[15];
                var buttons = Glfw.GetJoystickButtons(Id, out int _);
                return (from button in buttons select button == Glfw.True).ToArray();
            }
        }

        public HatState[] Hats
        {
            get
            {
                if(!IsConnected) return new HatState[4];
                var hats = Glfw.GetJoystickHats(Id, out int _);
                return (from hat in hats select (HatState) hat).ToArray();
            }
        }
    }
}