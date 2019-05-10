using System;

namespace Bronze.Input
{
    public class DualJoyCon
    {
        public JoyCon Left { get; private set; }
        public JoyCon Right { get; private set; }

        public DualJoyCon()
        {
            Left = JoyCon.Null;
            Right = JoyCon.Null;
        }

        public DualJoyCon(JoyCon joyCon1, JoyCon joyCon2)
        {
            SetJoyCons(joyCon1, joyCon2);
        }

        public void SetJoyCons(JoyCon joyCon1, JoyCon joyCon2)
        {
            switch(joyCon1.Type)
            {
                case JoyCon.Side.Left:
                    Left = joyCon1;
                    Right = joyCon2;
                    break;
                case JoyCon.Side.Right:
                    Left = joyCon2;
                    Right = joyCon1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool IsConnected => Left.IsConnected && Right.IsConnected;

        public bool A => Right.B;
        public bool B => Right.Y;
        public bool X => Right.A;
        public bool Y => Right.X;

        public bool DpadUp => Left.Y;
        public bool DpadRight => Left.X;
        public bool DpadDown => Left.A;
        public bool DpadLeft => Left.B;

        public bool LeftBumper => Left.Bumper;
        public bool RightBumper => Right.Bumper;

        public bool LeftTrigger => Left.Trigger;
        public bool RightTrigger => Right.Trigger;

        public bool LeftStickButton => Left.StickButton;
        public bool RightStickButton => Right.StickButton;

        public Joystick.HatState LeftStick => Left.Stick switch
            {
            Joystick.HatState.Centered => Joystick.HatState.Centered,
            Joystick.HatState.Up => Joystick.HatState.Right,
            Joystick.HatState.RightUp => Joystick.HatState.RightDown,
            Joystick.HatState.Right => Joystick.HatState.Down,
            Joystick.HatState.RightDown => Joystick.HatState.LeftDown,
            Joystick.HatState.Down => Joystick.HatState.Left,
            Joystick.HatState.LeftDown => Joystick.HatState.LeftUp,
            Joystick.HatState.Left => Joystick.HatState.Up,
            Joystick.HatState.LeftUp => Joystick.HatState.RightUp,
            };

        public Joystick.HatState RightStick => Right.Stick switch
            {
            Joystick.HatState.Centered => Joystick.HatState.Centered,
            Joystick.HatState.Up => Joystick.HatState.Left,
            Joystick.HatState.RightUp => Joystick.HatState.LeftUp,
            Joystick.HatState.Right => Joystick.HatState.Up,
            Joystick.HatState.RightDown => Joystick.HatState.RightUp,
            Joystick.HatState.Down => Joystick.HatState.Right,
            Joystick.HatState.LeftDown => Joystick.HatState.RightDown,
            Joystick.HatState.Left => Joystick.HatState.Down,
            Joystick.HatState.LeftUp => Joystick.HatState.LeftDown,
            };

        public bool Minus => Left.MenuButton;
        public bool Plus => Right.MenuButton;

        public bool Capture => Left.FunctionButton;
        public bool Home => Right.FunctionButton;
    }
}