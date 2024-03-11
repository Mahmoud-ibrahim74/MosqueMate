using MosqueMateServices.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MosqueMateServices.Interfaces
{
    public interface IQuran
    {
        DTOQuran GetSoura();
        public List<string> QuranPagination(int pageIndex, int pageSize = 20); //20 ayat as a default
    }
}
