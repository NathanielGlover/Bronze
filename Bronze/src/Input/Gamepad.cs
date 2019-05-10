using System;
using Bronze.Maths;
using glfw3;

namespace Bronze.Input
{
    public class Gamepad
    {
        public static event Action<Gamepad> Connected;

        static Gamepad()
        {
            Joystick.Connected += (joystick, connected) =>
            {
                if(Glfw.JoystickIsGamepad(joystick.Id) == Glfw.True)
                {
                    Connected?.Invoke(new Gamepad(joystick.Id));
                }
            };
        }

        public static bool UpdateBindingDatabase(string newBindings) => Glfw.UpdateGamepadMappings(newBindings) == Glfw.True;

        private readonly int handle;

        private Gamepad(int handle) => this.handle = handle;

        private Glfw.GamepadState GetState()
        {
            Glfw.GetGamepadState(handle, out var state);
            return state;
        }

        public string Name => Glfw.GetGamepadName(handle);

        public bool A => GetState().Buttons[Glfw.GamepadButtonA] == Glfw.True;
        public bool B => GetState().Buttons[Glfw.GamepadButtonB] == Glfw.True;
        public bool X => GetState().Buttons[Glfw.GamepadButtonX] == Glfw.True;
        public bool Y => GetState().Buttons[Glfw.GamepadButtonY] == Glfw.True;

        public bool Cross => A;
        public bool Circle => B;
        public bool Square => X;
        public bool Triangle => Y;

        public bool LeftBumper => GetState().Buttons[Glfw.GamepadButtonLeftBumper] == Glfw.True;
        public bool RightBumper => GetState().Buttons[Glfw.GamepadButtonRightBumper] == Glfw.True;
        public bool LeftStickButton => GetState().Buttons[Glfw.GamepadButtonLeftThumb] == Glfw.True;
        public bool RightStickButton => GetState().Buttons[Glfw.GamepadButtonRightThumb] == Glfw.True;

        public bool Up => GetState().Buttons[Glfw.GamepadButtonDpadUp] == Glfw.True;
        public bool Right => GetState().Buttons[Glfw.GamepadButtonDpadRight] == Glfw.True;
        public bool Down => GetState().Buttons[Glfw.GamepadButtonDpadDown] == Glfw.True;
        public bool Left => GetState().Buttons[Glfw.GamepadButtonDpadLeft] == Glfw.True;

        public bool Back => GetState().Buttons[Glfw.GamepadButtonBack] == Glfw.True;
        public bool Start => GetState().Buttons[Glfw.GamepadButtonStart] == Glfw.True;
        public bool Guide => GetState().Buttons[Glfw.GamepadButtonGuide] == Glfw.True;

        public float LeftTrigger => GetState().Axes[Glfw.GamepadAxisLeftTrigger];
        public float RightTrigger => GetState().Axes[Glfw.GamepadAxisRightTrigger];

        public Vector2 LeftStick
        {
            get
            {
                var state = GetState();
                return new Vector2(state.Axes[Glfw.GamepadAxisLeftX], state.Axes[Glfw.GamepadAxisLeftY]);
            }
        }

        public Vector2 RightStick
        {
            get
            {
                var state = GetState();
                return new Vector2(state.Axes[Glfw.GamepadAxisRightX], state.Axes[Glfw.GamepadAxisRightY]);
            }
        }
    }
}