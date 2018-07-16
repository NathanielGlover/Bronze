using System;
using System.Threading;
using Bronze.Math;
using Bronze.UserInterface;

namespace Examples
{
    internal static class JoystickExample
    {
        public static void _Main(string[] args)
        {
            var win = new Window(100, 100, "Joystick Input Window", WindowFlags.Default ^ WindowFlags.Visible);
            var mainStick = Joystick.Joystick1;
            mainStick.Connected += (sender, isConnected) => Console.WriteLine("Joystick 1 has been " + (isConnected ? "connected" : "disconnected")); 

            while(win.IsOpen)
            {
                win.Clear();
                win.SwapBuffers();
                
                if(mainStick.IsConnected)
                {
                    //WARNING: Elements of the "Axes" and "ButtonStates" arrays may mean different things for different joysticks
                    var leftStick = new Vector2(mainStick.Axes[0], mainStick.Axes[1]);
                    var rightStick = new Vector2(mainStick.Axes[2], mainStick.Axes[3]);

                    bool buttonA = mainStick.ButtonStates[0] == 1;
                    bool buttonB = mainStick.ButtonStates[1] == 1;

                    Console.WriteLine($"Left Joystick Value: {leftStick}");
                    Console.WriteLine($"Right Joystick Value: {rightStick}");
                    Console.WriteLine($"Button A Pressed: {buttonA}");
                    Console.WriteLine($"Button B Pressed: {buttonB}");
                }
                
                Window.PollEvents();
                Thread.Sleep(1);
            }
        }
    }
}