using OpenAL;

namespace Bronze.Audio
{
    public class Sound
    {
        internal uint Buffer { get; }

        private int Format { get; }

        public int SampleRate { get; }

        public bool Positional => Format == Al.FormatMono8 || Format == Al.FormatMono16;

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

        internal Sound(int format, byte[] soundData, int sampleRate)
        {
            Al.GenBuffer(out uint buffer);
            Buffer = buffer;
            Format = format;
            SampleRate = sampleRate;
            Al.BufferData(Buffer, format, soundData, soundData.Length, sampleRate);
        }
    }
}