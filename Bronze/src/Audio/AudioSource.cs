using Bronze.Math;
using OpenTK.Audio.OpenAL;

namespace Bronze.Audio
{
    public class AudioSource
    {
        private readonly int source;

        private Vector3 position, velocity, direction;
        
        public Sound Sound { get; }

        public AudioSource(Sound sound)
        {
            Sound = sound;
            source = AL.GenSource();
            AL.BindBufferToSource(source, sound.Buffer);
        }

        public Vector3 Position
        {
            get => position;

            set
            {
                position = value;
                AL.Source(source, ALSource3f.Position, value.X, value.Y, value.Z);
            }
        }

        public Vector3 Velocity
        {
            get => velocity;

            set
            {
                velocity = value;
                AL.Source(source, ALSource3f.Velocity, value.X, value.Y, value.Z);
            }
        }

        public Vector3 Direction
        {
            get => direction;

            set
            {
                direction = value;
                AL.Source(source, ALSource3f.Direction, value.X, value.Y, value.Z);
            }
        }

        public void Play()
        {
            AL.SourcePlay(source);
        }
    }
}