using MosqueMateMedia.Properties;
using MosqueMateServices.DTOs;
using MosqueMateServices.Helper;
using MosqueMateServices.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
namespace MosqueMateServices.Repositories
{
    public class HadithRepository : IHadith, IDisposable
    {
        private readonly FileHelper fileHelper;
        List<DTOHadith> dTOHadiths = new List<DTOHadith>();
        public HadithRepository()
        {
            fileHelper = new FileHelper();
            var res = fileHelper.ReadResourcesFile(HadithResources.hadithNawawi);
            dTOHadiths = JsonConvert.DeserializeObject<List<DTOHadith>>(res);
        }

        public void Dispose()
        {
            dTOHadiths.Clear();
        }

        public List<DTOHadith> GetAll()
        {
            return dTOHadiths;
        }

        public List<string> GetAllByName()
        {
            var result = dTOHadiths.Select(x => x.hadith).ToList();
            var haidthName = result.Select(x => x.Split("\n")[0]).ToList();
            return haidthName;
        }

        public DTOHadith GetHadithByID(int id)
        {
            var result = dTOHadiths.Where(x => x.index == id).FirstOrDefault();
            return result;
        }

        public DTOHadith HadithPagination(int pageIndex, int pageSize)
        {
            var result = dTOHadiths.Skip((pageIndex - 1) * pageSize).Take(pageSize).FirstOrDefault();
            return result;
        }
    }
}
