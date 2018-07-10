using System;
using System.IO;
using System.Linq;
using OpenAL;
using libsndfile.NET;

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

        private static int GetSoundFormat(int channels, int bits)
        {
            switch(channels)
            {
                case 1: return bits == 8 ? Al.FormatMono8 : Al.FormatMono16;
                case 2: return bits == 8 ? Al.FormatStereo8 : Al.FormatStereo16;
                default: throw new NotSupportedException("The specified sound format is not supported.");
            }
        }

        private static short[] MonoToStereo(short[] input)
        {
            var output = new short[input.Length * 2];
            int outputIndex = 0;
            for(int n = 0; n < input.Length; n += 2)
            {
                output[outputIndex++] = input[n];
                output[outputIndex++] = input[n + 1];
                output[outputIndex++] = input[n];
                output[outputIndex++] = input[n + 1];
            }

            return output;
        }

        private static short[] MixStereoToMono(short[] input)
        {
            var output = new short[input.Length];
            int outputIndex = 0;
            for(int n = 0; n < input.Length; n += 2)
            {
                short leftChannel = input[n];
                short rightChannel = input[n + 1];
                short mixed = (short) ((leftChannel + rightChannel) / 2);

                output[outputIndex++] = mixed;
                output[outputIndex++] = mixed;
            }

            return output;
        }

        public static Sound LoadSound(string path, AudioType type = AudioType.Positional)
        {
            string filePath = ResourceDirectory + path;
            if(!File.Exists(filePath)) throw new FileNotFoundException($"Audio file \"{filePath}\" could not be found");

            var file = SndFile.OpenRead(filePath);
            if(file == null)
            {
                throw new NotSupportedException($"The audio format for \"{filePath}\" is not supported");
            } 
            
            var data = new short[file.Frames * file.Format.Channels];
            file.ReadFrames(data, file.Frames);

            int bitsPerSample;
            
            //If number of channels doesn't match requested audio type 
            if(!(type == AudioType.Positional && file.Format.Channels == 1 || type == AudioType.Stereo && file.Format.Channels == 2))
            {
                switch(type)
                {
                    case AudioType.Positional:
                        data = MixStereoToMono(data);
                        break;
                    case AudioType.Stereo:
                        data = MonoToStereo(data);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            }

            switch(file.Format.Subtype)
            {
                case SfFormatSubtype.PCM_S8:
                case SfFormatSubtype.PCM_U8:
                case SfFormatSubtype.DPCM_8:
                    bitsPerSample = 8;
                    break;
                case SfFormatSubtype.PCM_16:
                case SfFormatSubtype.DWVW_16:
                case SfFormatSubtype.DPCM_16:
                case SfFormatSubtype.VORBIS:
                    bitsPerSample = 16;
                    break;
                default:
                    throw new NotSupportedException($"Audio format \"{file.Format.Subtype}\" is not supported.");
            }
            
            return new Sound(GetSoundFormat(file.Format.Channels, bitsPerSample), data, file.Format.SampleRate);
        }
    }

    public enum AudioType
    {
        Positional,
        Stereo
    }
}