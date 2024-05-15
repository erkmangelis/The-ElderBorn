using System;
using System.Collections.Generic;
using NAudio.Wave;

namespace ElderBorn
{
    public class MusicBox 
    {
        private readonly Dictionary<string, WaveOutEvent> waveOuts;
        private readonly Dictionary<string, AudioFileReader> audioFiles;
        private readonly Dictionary<string, string> sounds;


        public MusicBox()
        {  
            waveOuts = new Dictionary<string, WaveOutEvent>();
            audioFiles = new Dictionary<string, AudioFileReader>();

            sounds = new Dictionary<string, string>
            {
                { "theme", "./sounds/theme.wav" },
                { "coin", "./sounds/coin.wav" }
            };
        }


        // Load sound sources
        public void Load(string sound)
        {
            if (!waveOuts.ContainsKey(sound))
            {
                var waveOut = new WaveOutEvent();
                var audioFile = new AudioFileReader(sounds[sound]);

                waveOut.Init(audioFile);
                waveOuts[sound] = waveOut;
                audioFiles[sound] = audioFile;
            }
        }


        // Plays music once
        public void Play(string sound)
        {
            if (!waveOuts.ContainsKey(sound))
                {
                    // Load before playing sound
                    Load(sound);

                    // Play sound
                    waveOuts[sound].Play();

                    // When sound ends, dispose 
                    waveOuts[sound].PlaybackStopped += (s, e) =>
                    {
                        Dispose(sound);
                    };
                }
        }


        // Plays music with loop
        public void Loop(string sound)
        {
            if (!waveOuts.ContainsKey(sound))
            {
                // Load before playing sound
                Load(sound);

                // When sound ends, go back to start point and play again the sound
                waveOuts[sound].PlaybackStopped += (s, e) =>
                {
                    if (waveOuts[sound].PlaybackState == PlaybackState.Stopped)
                    {
                        audioFiles[sound].Position = 0;  // Go start point
                        waveOuts[sound].Play();          // Play sound
                    }
                };

                // Play sound
                waveOuts[sound].Play();
            }
        }


        // Stops the music
        public void Stop(string sound)
        {
            if (waveOuts.ContainsKey(sound))
            {
                // Stop sound
                waveOuts[sound].Stop();

                // Dispose
                Dispose(sound);
            }
        }


        // Clean instances not needed
        public void Dispose(string sound)
        {
            waveOuts[sound].Dispose();
            audioFiles[sound].Dispose();
        }
    }
}