using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MosqueMateServices.Interfaces
{
    public interface IAudioPlayer : IDisposable
    {
        public void PlayAudio();
       public void StopAudio();
        public void Dispose();
    }
}
