using MosqueMateServices.DTOs;
using System.Collections.Generic;

namespace MosqueMateServices.Interfaces
{
    public interface IZekr
    {
        public List<string> GetAllCategoriesByGrouping();
        List<DTOAzkar> GetZekrByName(string categoryName);
        public DTOAzkar ZekrPagination(List<DTOAzkar> zekr, int pageIndex, int pageSize);
    }
}
