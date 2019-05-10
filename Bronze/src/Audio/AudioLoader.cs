using System;
using System.IO;
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
            ContextManager.EnsureContext();
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

        //TODO: Add support for mp3 format
        public static Sound LoadSound(string path, AudioType type = AudioType.DontCare)
        {
            if(!File.Exists(path))
            {
                path = ResourceDirectory + path;
            }

            if(!File.Exists(path)) throw new FileNotFoundException($"Audio file \"{path}\" could not be found");

            var file = SndFile.OpenRead(path);
            if(ReferenceEquals(file, null))
            {
                throw new NotSupportedException($"The audio format for \"{path}\" is not supported");
            }

            var data = new short[file.Frames * file.Format.Channels];
            file.ReadFrames(data, file.Frames);

            //If number of channels doesn't match requested audio type 
            int channels = file.Format.Channels;
            if(!(type == AudioType.DontCare || type == AudioType.Positional && channels == 1 || type == AudioType.Stereo && channels == 2))
            {
                switch(type)
                {
                    case AudioType.Positional:
                        data = MixStereoToMono(data);
                        channels = 1;
                        break;
                    case AudioType.Stereo:
                        data = MonoToStereo(data);
                        channels = 2;
                        break;
                    case AudioType.DontCare:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            }

            int bitsPerSample;
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
                    throw new NotSupportedException($"Audio format subtype \"{file.Format.Subtype}\" is not supported.");
            }

            return new Sound(GetSoundFormat(channels, bitsPerSample), data, file.Format.SampleRate * (file.Format.Channels / channels));
        }
    }

    public enum AudioType
    {
        Positional,
        Stereo,
        DontCare
    }
}