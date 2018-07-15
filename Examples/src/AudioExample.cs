using System.Threading;
using Bronze.Audio;

namespace Examples
{
    internal class AudioExample
    {
        public static void _Main(string[] args)
        {
            AudioLoader.ResourceDirectory = "/path/to/audio/files";
            
            var sound1 = AudioLoader.LoadSound("sound1.wav"); //Uses resource directory defined above
            var sound2 = AudioLoader.LoadSound("/path/to/audio/files/sound2.wav"); //Uses absolute path
            var sound3 = AudioLoader.LoadSound("sound3.wav");
            
            var source = new AudioSource(sound1);
            
            source.Play();
            Thread.Sleep((int) (source.Sound.Duration * 1000));

            source.Sound = sound2;
            
            source.Play();
            Thread.Sleep((int) (source.Sound.Duration * 1000));

            source.Sound = sound3;
            
            source.Play();
            Thread.Sleep((int) (source.Sound.Duration * 1000));
        }
    }
}