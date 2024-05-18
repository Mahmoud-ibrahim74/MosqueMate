using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MosqueMateServices.Interfaces
{
    public interface IAPI
    {
        Task<string> GetAsync();
    }
}
