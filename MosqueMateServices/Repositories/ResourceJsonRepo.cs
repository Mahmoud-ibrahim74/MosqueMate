using MosqueMateServices.Helper;
using MosqueMateServices.Interfaces;
using MosqueMateServices.Repositories;
using Newtonsoft.Json.Linq;
using System;

namespace MosqueMateServices.AppResources
{
    public class ResourceJsonRepo:IDisposable
    {
        private static JObject resourcesJson { get; set; }
        private readonly IAppData _appData;
        private readonly IFileHelper fileHelper;
        public ResourceJsonRepo()
        {
            _appData =  AppDataRepo.Instance;
            fileHelper = new FileHelper();
            var res = fileHelper.ReadResourcesFile(MosqueMateMedia.Properties.Media.AppResources);
            resourcesJson = JObject.Parse(res);
        }
        public string this[string resourceName]
        {
            get
            {
                if (resourcesJson != null)
                {
                    var result = resourcesJson
                        .SelectToken($"{resourceName}.{_appData.currLang}")
                        ?.Value<string>();

                    return result ?? string.Empty;
                }
                return string.Empty;
            }
        }
        public static string GetResourcesByName( string resourceName)
        {
            if (resourcesJson != null)
            {
                var result = resourcesJson
                    .SelectToken($"{resourceName}.{AppDataRepo.Instance.currLang}")
                    ?.Value<string>();

                return result ?? string.Empty;
            }
            return string.Empty;
        }

        public void Dispose()
        {
            resourcesJson.RemoveAll();  
        }
    }
}
