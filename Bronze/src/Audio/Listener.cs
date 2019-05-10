using Bronze.Maths;
using OpenAL;

namespace Bronze.Audio
{
    public static class Listener
    {
        static Listener()
        {
            ContextManager.EnsureContext();
        }

        public static Vector3 Position
        {
            get
            {
                Al.GetListener3f(Al.Position, out float x, out float y, out float z);
                return new Vector3(x, y, z);
            }

            set => Al.Listener3f(Al.Position, value.X, value.Y, value.Z);
        }

        public static Vector3 Velocity
        {
            get
            {
                Al.GetListener3f(Al.Velocity, out float x, out float y, out float z);
                return new Vector3(x, y, z);
            }

            set => Al.Listener3f(Al.Velocity, value.X, value.Y, value.Z);
        }

        public static float MasterVolume
        {
            get
            {
                Al.GetListenerf(Al.Gain, out float volume);
                return volume;
            }

            set => Al.Listenerf(Al.Gain, value);
        }
    }
}