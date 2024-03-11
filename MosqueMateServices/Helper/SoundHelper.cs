using MosqueMateServices.Interfaces;
using System;
using System.IO;
using System.Media;

namespace MosqueMateServices.Helper
{
    public class SoundHelper : IAudioPlayer
    {
        private readonly SoundPlayer playerMuzzin;
        private readonly Stream audioMuzzinStream;
        public SoundHelper(Stream audioMuzzinStream)
        {
            playerMuzzin = new SoundPlayer();
            this.audioMuzzinStream = audioMuzzinStream;
        }

        public void PlayAudio()
        {
            playerMuzzin.Stream = this.audioMuzzinStream;   
            playerMuzzin.Play();
        }
        public void StopAudio()
        {
            playerMuzzin.Stop();
        }
        public void Dispose()
        {
            playerMuzzin.Dispose();
            audioMuzzinStream.Dispose();
        }
    }
}
