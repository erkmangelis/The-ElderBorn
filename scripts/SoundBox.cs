using System;
using System.Media;

namespace ElderBorn
{
    public class SoundBox
    {
        private SoundPlayer player;
        private readonly Dictionary<string, string> sounds;


        public SoundBox()
        {
            sounds = new Dictionary<string, string>
            {
                { "coin", "./sounds/coin.wav" }
            };
        }


        // Sets the sound file for to play
        public void set(string sound)
        {
            try
            {
                player?.Dispose();
                player = new SoundPlayer(sounds[sound]);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error setting sound: {e.Message}");
            }
        }


        // Plays sound once
        public void play(string sound)
        {
            set(sound);
            player?.Play();
        }


        // Plays sound with loop
        public void loop(string sound)
        {
            set(sound);
            player?.PlayLooping();
        }


        // Stops the sound currently playing
        public void stop()
        {
            player?.Stop();
        }
    }
}