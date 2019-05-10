// ReSharper disable InconsistentNaming

using System;

namespace Bronze.Input
{
    public class JoyCon
    {
        public enum Side
        {
            Left,
            Right
        }

        public static readonly JoyCon Null = new JoyCon();

        public Joystick Joystick { get; }

        public Side Type { get; }

        private JoyCon() => Joystick = Joystick.Null;

        public static bool IsJoyCon(Joystick joystick) =>
            joystick.Guid switch
                {
                "030000007e0500000720000001000000" => true,
                "030000007e0500000620000001000000" => true,
                _ => false
                };

        public JoyCon(Joystick joystick)
        {
            Joystick = joystick;

            Type = joystick.Guid switch
                {
                "030000007e0500000720000001000000" => Side.Right,
                "030000007e0500000620000001000000" => Side.Left,
                _ => throw new Exception("Joystick is not a Nintendo Joy-Con.")
                };
        }

        public bool IsConnected => Joystick.IsConnected;

        public bool A => Joystick.Buttons[1];
        public bool B => Joystick.Buttons[0];
        public bool X => Joystick.Buttons[3];
        public bool Y => Joystick.Buttons[2];

        public bool SL => Joystick.Buttons[4];
        public bool SR => Joystick.Buttons[5];

        public bool Bumper => Joystick.Buttons[14];
        public bool Trigger => Joystick.Buttons[15];

        public bool StickButton
        {
            get
            {
                var buttons = Joystick.Buttons;
                return buttons[10] || buttons[11];
            }
        }

        public Joystick.HatState Stick => Joystick.Hats[0];

        public bool MenuButton
        {
            get
            {
                var buttons = Joystick.Buttons;
                return buttons[8] || buttons[9];
            }
        }

        public bool FunctionButton
        {
            get
            {
                var buttons = Joystick.Buttons;
                return buttons[12] || buttons[13];
            }
        }
    }
}