﻿using System;
using OpenAL;

namespace Bronze.Audio
{
    internal static class AudioContextManager
    {
        private static readonly IntPtr AudioContext;

        static AudioContextManager()
        {
            var device = Alc.OpenDevice(null);
            if(device != IntPtr.Zero)
            {
                AudioContext = Alc.CreateContext(device, null);
                Alc.MakeContextCurrent(AudioContext);
            }
        }

        public static void EnsureContext()
        {
            if(AudioContext == IntPtr.Zero) throw new NullReferenceException("OpenAL context was prematurely destroyed. Please don't do that.");
            Alc.MakeContextCurrent(AudioContext);
        }
    }
}