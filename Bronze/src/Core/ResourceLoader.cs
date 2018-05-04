using System;
using System.IO;
using Bronze.Audio;
using OpenTK.Audio.OpenAL;

namespace Bronze.Core
{
    public static class ResourceManager
    {
        public static string ResourceDirectory { get; set; }

        static ResourceManager()
        {
            ContextManager.EnsureDefaultContext();
            AudioContextManager.EnsureContext();
        }

        public static Sound LoadSound(string path)
        {
            var stream = File.Open(ResourceDirectory + path, FileMode.Open);

            using(var reader = new BinaryReader(stream))
            {
                string signature = new string(reader.ReadChars(4));
                if(signature != "RIFF")
                    throw new NotSupportedException("Specified stream is not a wave file.");

                int riffChunckSize = reader.ReadInt32();

                string format = new string(reader.ReadChars(4));
                if(format != "WAVE") throw new NotSupportedException("Specified stream is not a wave file.");

                string formatSignature = new string(reader.ReadChars(4));
                if(formatSignature != "fmt ") throw new NotSupportedException("Specified wave file is not supported.");

                int formatChunkSize = reader.ReadInt32();
                int audioFormat = reader.ReadInt16();
                int numChannels = reader.ReadInt16();
                int sampleRate = reader.ReadInt32();
                int byteRate = reader.ReadInt32();
                int blockAlign = reader.ReadInt16();
                int bitsPerSample = reader.ReadInt16();

                string dataSignature = new string(reader.ReadChars(4));
                if(dataSignature != "data") throw new NotSupportedException("Specified wave file is not supported.");

                int dataChunkSize = reader.ReadInt32();
                
                ALFormat GetSoundFormat(int channels, int bits)
                {
                    switch(channels)
                    {
                        case 1: return bits == 8 ? ALFormat.Mono8 : ALFormat.Mono16;
                        case 2: return bits == 8 ? ALFormat.Stereo8 : ALFormat.Stereo16;
                        default: throw new NotSupportedException("The specified sound format is not supported.");
                    }
                }

                var soundData = reader.ReadBytes((int) reader.BaseStream.Length);
                return new Sound(GetSoundFormat(numChannels, bitsPerSample), soundData, sampleRate);
            }
        }
    }
}