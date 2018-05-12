using System;
using Bronze.Math;
using OpenTK.Audio.OpenAL;

namespace Bronze.Audio
{
    public class AudioSource
    {
        private readonly int source;

        private Vector3 position, velocity, direction;
        private float startTime;

        public Sound Sound { get; }

        public AudioSource(Sound sound)
        {
            Sound = sound;
            source = AL.GenSource();
            AL.BindBufferToSource(source, sound.Buffer);
            AL.Source(source, ALSourceb.SourceRelative, true);
            AL.Listener(ALListener3f.Position, 0, 0, 0);
        }

        public float VolumeMultiplier
        {
            get
            {
                AL.GetSource(source, ALSourcef.Gain, out float gain);
                return gain;
            }

            set => AL.Source(source, ALSourcef.Gain, value);
        }

        public float PitchMultiplier
        {
            get
            {
                AL.GetSource(source, ALSourcef.Pitch, out float pitchMultiplier);
                return pitchMultiplier;
            }

            set => AL.Source(source, ALSourcef.Pitch, value);
        }

        public bool Looping
        {
            get
            {
                AL.GetSource(source, ALSourceb.Looping, out bool isLooping);
                return isLooping;
            }

            set => AL.Source(source, ALSourceb.Looping, value);
        }

        public float StartTime
        {
            get => startTime;

            set
            {
                startTime = value;
                AL.Source(source, ALSourcef.SecOffset, value);
            }
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
            StartTime = StartTime; //OpenAL not working right here, start time is only applied if set while playing
        }

        public void Pause() => AL.SourcePause(source);

        public void Stop() => AL.SourceStop(source);

        public void Rewind() => AL.SourceRewind(source);

        public bool IsPlaying() => AL.GetSourceState(source) == ALSourceState.Playing;

        public bool IsPaused() => AL.GetSourceState(source) == ALSourceState.Paused;

        public bool IsStopped() => AL.GetSourceState(source) == ALSourceState.Stopped || AL.GetSourceState(source) == ALSourceState.Initial;
    }
}