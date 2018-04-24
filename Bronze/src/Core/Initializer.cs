using System;
using glfw3;

namespace Bronze.Core
{
    public static class Initializer
    {
        public static event Action Initialized;

        private static bool initialized;

        public static void Initialize()
        {
            if(!initialized)
            {
                try
                {
                    var nativeWindow = new OpenTK.NativeWindow
                    (
                        1, 1, "",
                        OpenTK.GameWindowFlags.Default,
                        OpenTK.Graphics.GraphicsMode.Default,
                        OpenTK.DisplayDevice.Default
                    );

                    var context = new OpenTK.Graphics.GraphicsContext
                    (
                        OpenTK.Graphics.GraphicsMode.Default,
                        nativeWindow.WindowInfo, 4, 1,
                        OpenTK.Graphics.GraphicsContextFlags.ForwardCompatible
                    );

                    context.MakeCurrent(nativeWindow.WindowInfo);
                    context.LoadAll();

                    Glfw.Init();
                }
                catch(Exception e)
                {
                    throw new InitializationException("Bronze failed to initialize: " + e.Message);
                }

                initialized = true;
                Initialized?.Invoke();
            }
        }
    }
}