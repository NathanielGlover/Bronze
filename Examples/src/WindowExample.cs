using System;
using System.Threading;
using Bronze.UserInterface;

namespace Examples
{
    internal class WindowExample
    {
        public static void Main(string[] args)
        {
            var window = new Window(1300, 650, "Window");

            window.Closed += sender => Console.WriteLine($"Window \"{sender.Title}\" has been closed.");

            window.Focused += (sender, focused) => Console.WriteLine($"Window \"{sender.Title}\" has " + (focused ? "gained" : "lost") + " focus.");

            window.Moved += (sender, location) => Console.WriteLine($"Window \"{sender.Title}\" has moved to {location}.");

            window.Resized += (sender, size) => Console.WriteLine($"Window \"{sender.Title}\" has been resized to {size.X} by {size.Y}");

            window.Minimized += (sender, minimized) =>
                Console.WriteLine($"Window \"{sender.Title}\" has been " + (minimized ? "minimized." : "restored."));

            window.FilesDropped += (sender, paths) =>
            {
                Console.WriteLine($"Window \"{sender.Title}\" has received files:");
                foreach(string path in paths)
                {
                    Console.WriteLine(path);
                }
            };
            
            while(window.IsOpen)
            {
                window.Clear();
                window.SwapBuffers();
                
                Window.WaitEvents();
                Thread.Sleep(1);
            }
        }
    }
}