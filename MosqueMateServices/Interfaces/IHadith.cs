using MosqueMateServices.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MosqueMateServices.Interfaces
{
    public interface IHadith
    {
        public List<string> GetAllByName();
        List<DTOHadith> GetAll();
        DTOHadith GetHadithByID(int id);
        DTOHadith HadithPagination (int pageIndex, int pageSize);
    }
}
