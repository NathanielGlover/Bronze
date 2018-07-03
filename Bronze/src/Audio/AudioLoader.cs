using System;
using System.IO;
using OpenTK.Audio.OpenAL;

namespace Bronze.Audio
{
    public static class AudioLoader
    {
        private static string resourceDirectory;
        
        public static string ResourceDirectory
        {
            get => resourceDirectory;
            set
            {
                resourceDirectory = value;
                if(!(resourceDirectory.EndsWith("/") || resourceDirectory.EndsWith("\\")))
                {
                    if(Environment.OSVersion.Platform == PlatformID.Unix)
                    {
                        resourceDirectory += "/";
                    }
                    else
                    {
                        resourceDirectory += "\\";
                    }
                }

                if(!Directory.Exists(resourceDirectory))
                {
                    throw new ArgumentException($"Directory \"{resourceDirectory}\" does not exist.");
                }
            }
        }

        static AudioLoader()
        {
            AudioContextManager.EnsureContext();
        }
        
        private static ALFormat GetSoundFormat(int channels, int bits)
        {
            switch(channels)
            {
                case 1: return bits == 8 ? ALFormat.Mono8 : ALFormat.Mono16;
                case 2: return bits == 8 ? ALFormat.Stereo8 : ALFormat.Stereo16;
                default: throw new NotSupportedException("The specified sound format is not supported.");
            }
        }
        
        private static byte[] MonoToStereo(byte[] input)
        {
            var output = new byte[input.Length * 2];
            int outputIndex = 0;
            for (int n = 0; n < input.Length; n+=2)
            {
                output[outputIndex++] = input[n];
                output[outputIndex++] = input[n+1];
                output[outputIndex++] = input[n];
                output[outputIndex++] = input[n+1];        
            }
            
            return output;
        }

        private static byte[] MixStereoToMono(byte[] input)
        {
            var output = new byte[input.Length / 2];
            int outputIndex = 0;
            for(int n = 0; n < input.Length; n += 4)
            {
                int leftChannel = BitConverter.ToInt16(input, n);
                int rightChannel = BitConverter.ToInt16(input, n + 2);
                int mixed = (leftChannel + rightChannel) / 2;
                var outSample = BitConverter.GetBytes((short) mixed);

                output[outputIndex++] = outSample[0];
                output[outputIndex++] = outSample[1];
            }

            return output;
        }

        //TODO: Add support for mp3, ogg, oga, aac, aiff, flac, and m4a
        public static Sound LoadSound(string path, bool positional = true)
        {
            string fileEnding = path.Split(new []{'.'}, 2)[1];
            string filePath = ResourceDirectory + path;
            Sound sound;
            
            switch(fileEnding)
            {
                case "wav": sound = LoadSoundFromWav(filePath, positional);
                    break;
                default: throw new NotSupportedException($"Audio format \"{fileEnding}\" is not supported.");
            }

            return sound;
        }

        private static Sound LoadSoundFromWav(string wavFile, bool mono)
        {
            var stream = File.Open(wavFile, FileMode.Open);

            using(var reader = new BinaryReader(stream))
            {
                string signature = new string(reader.ReadChars(4));
                if(signature != "RIFF")
                    throw new NotSupportedException($"{wavFile} is not a wave file.");

                int riffChunckSize = reader.ReadInt32();

                string format = new string(reader.ReadChars(4));
                if(format != "WAVE") throw new NotSupportedException($"{wavFile} is not a wave file.");

                string formatSignature = new string(reader.ReadChars(4));
                if(formatSignature != "fmt ") throw new NotSupportedException($"Wave file \"{wavFile}\" is not supported.");

                int formatChunkSize = reader.ReadInt32();
                int audioFormat = reader.ReadInt16();
                int numChannels = reader.ReadInt16();
                int sampleRate = reader.ReadInt32();
                int byteRate = reader.ReadInt32();
                int blockAlign = reader.ReadInt16();
                int bitsPerSample = reader.ReadInt16();

                string dataSignature = new string(reader.ReadChars(4));
                if(dataSignature != "data") throw new NotSupportedException($"Wave file \"{wavFile}\" is not supported.");

                int dataChunkSize = reader.ReadInt32();

                var soundData = reader.ReadBytes((int) reader.BaseStream.Length);

                var alFormat = GetSoundFormat(numChannels, bitsPerSample);
                if(mono)
                {
                    if(alFormat == ALFormat.Stereo8)
                    {
                        soundData = MixStereoToMono(soundData);
                        alFormat = ALFormat.Mono8;
                    }
                    else if(alFormat == ALFormat.Stereo16)
                    {
                        soundData = MixStereoToMono(soundData);
                        alFormat = ALFormat.Mono16;
                    }
                }
                else
                {
                    if(alFormat == ALFormat.Mono8)
                    {
                        soundData = MonoToStereo(soundData);
                        alFormat = ALFormat.Stereo8;
                    }
                    else if(alFormat == ALFormat.Mono16)
                    {
                        soundData = MonoToStereo(soundData);
                        alFormat = ALFormat.Stereo16;
                    }
                }

                return new Sound(alFormat, soundData, sampleRate);
            }
        }
    }
}