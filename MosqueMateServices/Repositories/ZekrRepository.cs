using MosqueMateMedia.Properties;
using MosqueMateServices.DTOs;
using MosqueMateServices.Helper;
using MosqueMateServices.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MosqueMateServices.Repositories
{
    public class ZekrRepository : IZekr,IDisposable
    {
        private readonly FileHelper fileHelper;
        private int suraNumber = 1;
        List<DTOAzkar> dTOAzkars = new List<DTOAzkar>();
        IEnumerable<IGrouping<string,DTOAzkar>> GroupedZkar;
        public ZekrRepository()
        {
            fileHelper = new FileHelper();
            var res = fileHelper.ReadResourcesFile(AzkarResources.azkar);
            dTOAzkars = JsonConvert.DeserializeObject<List<DTOAzkar>>(res);
            GroupedZkar = dTOAzkars.
                GroupBy(x => x.category);
                //.Select(g => new List<DTOAzkar>(g)).ToList();
        }

        public void Dispose()
        {
            dTOAzkars.Clear();
        }

        public List<string> GetAllCategoriesByGrouping()
        {
            var groupedAzkar = dTOAzkars.
                     GroupBy(x => x.category)
                     .Select(g => g.Key).ToList();
            return groupedAzkar;
        }
        public List<DTOAzkar> GetZekrByName(string categoryName)
        {
            var result = GroupedZkar.
                Where(x => x.Key.Contains(categoryName))
               .Select(g => new List<DTOAzkar>(g)).FirstOrDefault();
            return result;
        }

        public DTOAzkar ZekrPagination(List<DTOAzkar> zekr ,int pageIndex, int pageSize)
        {
            return zekr.
            Skip((pageIndex - 1) * pageSize).
            Take(pageSize).FirstOrDefault();
        }
    }
}
