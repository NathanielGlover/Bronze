using OpenAL;

namespace Bronze.Audio
{
    public class Sound
    {
        internal uint Buffer { get; }

        public AudioType Type
        {
            get
            {
                Al.GetBufferi(Buffer, Al.Channels, out int channels);
                return channels == 1 ? AudioType.Positional : AudioType.Stereo;
            }
        }

        public float Duration
        {
            get
            {
                Al.GetBufferi(Buffer, Al.Size, out int sizeInBytes);
                Al.GetBufferi(Buffer, Al.Channels, out int channels);
                Al.GetBufferi(Buffer, Al.Bits, out int bits);

                int lengthInSamples = sizeInBytes * 8 / (channels * bits);

                Al.GetBufferi(Buffer, Al.Frequency, out int frequency);
                return (float) lengthInSamples / frequency;
            }
        }

        internal Sound(uint buffer) => Buffer = buffer;

        internal Sound(int format, short[] soundData, int sampleRate)
        {
            Al.GenBuffer(out uint buffer);
            Buffer = buffer;
            unsafe
            {
                fixed(short* dataPtr = soundData)
                {
                    Al.BufferData(Buffer, format, dataPtr, soundData.Length * sizeof(short), sampleRate);
                }
            }
        }
    }
}