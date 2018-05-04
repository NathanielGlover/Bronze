using OpenTK.Audio.OpenAL;

namespace Bronze.Audio
{
    public class Sound
    {
        internal int Buffer { get; }
        internal ALFormat Format { get; }
        
        public int SampleRate { get; }

        internal Sound(ALFormat format, byte[] soundData, int sampleRate)
        {
            Buffer = AL.GenBuffer();
            Format = format;
            SampleRate = sampleRate;
            AL.BufferData(Buffer, format, soundData, soundData.Length, sampleRate);
        }
    }
}