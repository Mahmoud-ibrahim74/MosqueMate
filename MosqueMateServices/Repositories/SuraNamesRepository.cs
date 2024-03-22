using MosqueMateMedia.Properties;
using MosqueMateServices.DTOs;
using MosqueMateServices.Helper;
using MosqueMateServices.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace MosqueMateServices.Repositories
{
    public class SuraNamesRepository : ISouraNames
    {
        private readonly FileHelper fileHelper;
        List<Surah> DTOSura = new List<Surah>();
        public SuraNamesRepository()
        {
            fileHelper = new FileHelper();
            var res = fileHelper.ReadResourcesFile(QuranImagesResources.surahNames);
            DTOSura = JsonConvert.DeserializeObject<DTOSuraNames>(res).surahs;
        }

        public List<string> GetAllSoura()
        {
           return DTOSura.Select(x =>x.name).ToList();  
        }

        public int GetSouraIdByName(string name)
        {
            return DTOSura.Where(x => x.name.Contains(name)).Select(x=>x.index).FirstOrDefault();
        }

        public string GetSouraNameById(int id)
        {
            return DTOSura.Where(x => x.index == id).Select(x => x.name).FirstOrDefault();
        }
    }
}
