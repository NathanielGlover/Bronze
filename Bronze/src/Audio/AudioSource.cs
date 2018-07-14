using Bronze.Math;
using OpenAL;

namespace Bronze.Audio
{
    public class AudioSource
    {
        private readonly uint source;

        public Sound Sound { get; }

        public AudioSource(Sound sound)
        {
            Sound = sound;
            Al.GenSource(out source);
            Al.Sourcei(source, Al.Buffer, (int) sound.Buffer);
        }

        public bool UsesRelativePositioning
        {
            get
            {
                Al.GetSourcei(source, Al.SourceRelative, out int relative);
                return relative == 1;
            }

            set => Al.Sourcei(source, Al.SourceRelative, value ? 1 : 0);
        }

        public float VolumeMultiplier
        {
            get
            {
                Al.GetSourcef(source, Al.Gain, out float gain);
                return gain;
            }

            set => Al.Sourcef(source, Al.Gain, value);
        }

        public float PitchMultiplier
        {
            get
            {
                Al.GetSourcef(source, Al.Pitch, out float pitchMultiplier);
                return pitchMultiplier;
            }

            set => Al.Sourcef(source, Al.Pitch, value);
        }

        public bool Looping
        {
            get
            {
                Al.GetSourcei(source, Al.Looping, out int isLooping);
                return isLooping == 1;
            }

            set => Al.Sourcei(source, Al.Looping, value ? 1 : 0);
        }

        public float StartTime { get; set; }

        public Vector3 Position
        {
            get
            {
                Al.GetSource3f(source, Al.Position, out float x, out float y, out float z);
                return new Vector3(x, y, z);
            }

            set => Al.Source3f(source, Al.Position, value.X, value.Y, value.Z);
        }

        public Vector3 Velocity
        {
            get
            {
                Al.GetSource3f(source, Al.Velocity, out float x, out float y, out float z);
                return new Vector3(x, y, z);
            }

            set => Al.Source3f(source, Al.Velocity, value.X, value.Y, value.Z);
        }

        public Vector3 Direction
        {
            get
            {
                Al.GetSource3f(source, Al.Direction, out float x, out float y, out float z);
                return new Vector3(x, y, z);
            }

            set => Al.Source3f(source, Al.Direction, value.X, value.Y, value.Z);
        }

        public void Play()
        {
            Al.SourcePlay(source);
            PlaybackPosition = StartTime;
        }

        public void Pause() => Al.SourcePause(source);

        public void Stop() => Al.SourceStop(source);

        public void Rewind() => Al.SourceRewind(source);

        public float PlaybackPosition
        {
            get
            {
                Al.GetSourcef(source, Al.SecOffset, out float playbackPos);
                return playbackPos;
            }
            
            set => Al.Sourcef(source, Al.SecOffset, value);
        }

        public bool IsPlaying()
        {
            Al.GetSourcei(source, Al.SourceState, out int state);
            return state == Al.Playing;
        }

        public bool IsPaused()
        {
            Al.GetSourcei(source, Al.SourceState, out int state);
            return state == Al.Paused;
        }

        public bool IsStopped()
        {
            Al.GetSourcei(source, Al.SourceState, out int state);
            return state == Al.Stopped;
        }
    }
}