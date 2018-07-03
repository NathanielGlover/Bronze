using OpenTK.Audio.OpenAL;

namespace Bronze.Audio
{
    public class Sound
    {
        internal int Buffer { get; }

        private ALFormat Format { get; }

        public int SampleRate { get; }

        public bool Positional => Format == ALFormat.Mono8 || Format == ALFormat.Mono16;

        public float Duration
        {
            get
            {
                AL.GetBuffer(Buffer, ALGetBufferi.Size, out int sizeInBytes);
                AL.GetBuffer(Buffer, ALGetBufferi.Channels, out int channels);
                AL.GetBuffer(Buffer, ALGetBufferi.Bits, out int bits);

                int lengthInSamples = sizeInBytes * 8 / (channels * bits);

                AL.GetBuffer(Buffer, ALGetBufferi.Frequency, out int frequency);
                return (float) lengthInSamples / frequency;
            }
        }

        internal Sound(ALFormat format, byte[] soundData, int sampleRate)
        {
            Buffer = AL.GenBuffer();
            Format = format;
            SampleRate = sampleRate;
            AL.BufferData(Buffer, format, soundData, soundData.Length, sampleRate);
        }
    }
}