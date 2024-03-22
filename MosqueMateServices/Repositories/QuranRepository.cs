using MosqueMateMedia.Properties;
using MosqueMateServices.DTOs;
using MosqueMateServices.Helper;
using MosqueMateServices.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
namespace MosqueMateServices.Repositories
{
    public class QuranRepository : IQuran
    {
        private readonly FileHelper fileHelper;
        private int suraNumber = 1;
        DTOQuran dTOQuran = new DTOQuran();
        public QuranRepository()
        {
                
        }
        public QuranRepository(int suraNumber)
        {
            //this.suraNumber = suraNumber;
            //var suraName = $"surah_{suraNumber}";
            ////var resource = QuranResources.FindByName(suraName);
            //fileHelper = new FileHelper();
            //var res = fileHelper.ReadResourcesFile(resource);
            //dTOQuran = JsonConvert.DeserializeObject<DTOQuran>(res);
        }
        public DTOQuran GetSoura()
        {
            return dTOQuran;
        }

        public List<string> QuranPagination(int pageIndex, int pageSize = 20) //20 ayat as a default
        {
            var result = dTOQuran.verse.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return result;
        }
    }
}
