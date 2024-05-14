using System;
using System.Media;

namespace ElderBorn
{
    public class Sound
    {
        private SoundPlayer player;
        private string[] sounds;


        public Sound()
        {
            sounds = new string[30];
            sounds[0] = "./sounds/coin.wav";
        }


        public void set(int i)
        {
            try
            {
                player?.Dispose();

                player = new SoundPlayer(sounds[i]);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error setting sound: {e.Message}");
            }
        }


        public void play()
        {
            player?.Play();
        }


        public void loop()
        {
            player?.PlayLooping();
        }


        public void stop()
        {
            player?.Stop();
        }
    }
}