using System;
using System.Drawing;
using System.IO;
using System.Resources;

namespace MosqueMateServices.Helper
{
    public class ResourceHelper<T> : IDisposable
    {
        public static string RESX_PATH = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames()[0];  // get path of resource file 
        private readonly ResourceManager resourceManager;
        public ResourceHelper(string resourceFileName = "Media")
        {
            resourceManager = new ResourceManager($"MosqueMateMedia.Properties.{resourceFileName}", typeof(T).Assembly);
        }



        public Stream GetWavResxByName(string value)
        {
            if (resourceManager != null)
            {
                return resourceManager.GetStream(value);
            }
            return null;
        }
        public Bitmap GetImageResxByName(string value)
        {
            if (resourceManager != null)
            {
                return (Bitmap)resourceManager.GetObject(value);
            }
            return null;
        }
        public Bitmap GetQuranImageResxByPageNumber(int pageNumber)
        {
            string file_name = string.Empty;

            if (pageNumber >= 1 && pageNumber < 10)
                file_name = $"quran_hafs_m_Page_00{pageNumber}";
            else if (pageNumber >=10 && pageNumber < 100)
                file_name = $"quran_hafs_m_Page_0{pageNumber}";
            else
                file_name = $"quran_hafs_m_Page_{pageNumber}";



            if (resourceManager != null)
            {
                return (Bitmap)resourceManager.GetObject(file_name);
            }
            return null;
        }

        /// <summary>  
        ///   return content of json file by name   
        /// </summary>  
        /// <param name="time">value</param>  
        /// <returns> byte[]</returns>  
        public byte[] GetResourceJsonResxByName(string value)
        {
            if (resourceManager != null)
            {
                return (byte[])resourceManager.GetObject(value);
            }
            return null;
        }
        public MemoryStream GetFontFile(string fontName)
        {
            if (resourceManager != null)
            {
                return (MemoryStream)resourceManager.GetObject(fontName);
            }
            return null;
        }
        public void Dispose()
        {
            resourceManager.ReleaseAllResources();
        }
    }
}
