using System;
using OpenTK;
using OpenTK.Audio.OpenAL;
using OpenTK.Audio;

namespace Bronze.Audio
{
    internal static class AudioContextManager
    {
        private static readonly ContextHandle AudioContext;
        
        static AudioContextManager()
        {
            var device = Alc.OpenDevice(null);
            if(device != IntPtr.Zero)
            {
                AudioContext = Alc.CreateContext(device, (int[]) null);
                Alc.MakeContextCurrent(AudioContext);
            }
            
        }
        
        public static void EnsureContext()
        {
            if(AudioContext == null) throw new NullReferenceException("OpenAL context was prematurely destroyed. Please don't do that.");
            Alc.MakeContextCurrent(AudioContext);
        }
    }
}