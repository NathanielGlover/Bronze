using Bronze.Math;
using OpenTK.Audio.OpenAL;

namespace Bronze.Audio
{
    public static class Listener
    {
        static Listener()
        {
            AudioContextManager.EnsureContext();
        }

        public static Vector3 Position
        {
            get
            {
                AL.GetListener(ALListener3f.Position, out float x, out float y, out float z);
                return new Vector3(x, y, z);
            }

            set => AL.Listener(ALListener3f.Position, value.X, value.Y, value.Z);
        }

        public static Vector3 Velocity
        {
            get
            {
                AL.GetListener(ALListener3f.Velocity, out float x, out float y, out float z);
                return new Vector3(x, y, z);
            }

            set => AL.Listener(ALListener3f.Velocity, value.X, value.Y, value.Z);
        }

        public static float MasterVolume
        {
            get
            {
                AL.GetListener(ALListenerf.Gain, out float volume);
                return volume;
            }
            
            set => AL.Listener(ALListenerf.Gain, value);
        }
    }
}