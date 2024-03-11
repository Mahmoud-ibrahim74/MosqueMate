using MosqueMateServices.Interfaces;
using MosqueMateServices.Repositories;
using Newtonsoft.Json;
using PrayIDataServices.Helper.API;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MosqueMateServices.Helper
{
    public class FileHelper : IFileHelper
    {
        public string resourcePath { get; set; }
        private readonly IAppData _appData;
        System.Resources.ResourceManager ResourceManager;
        public FileHelper()
        {
            this._appData = AppDataRepo.Instance;
            //ResourceManager = new System.Resources.ResourceManager("MosqueMateServices.Properties.Resources", typeof(Resources).Assembly);
            resourcePath = _appData.ResourcePath + @"LocalAPI.json";
            //if (!File.Exists(resourcePath))
            //    File.Create(resourcePath);
        }
        public async Task<bool> ReadLocalJson()
        {
            try
            {
                FileInfo info = new FileInfo(resourcePath);
                if (info.Length < 1)
                    return false;

                using (StreamReader reader = new StreamReader(resourcePath))
                {
                    var json = await reader.ReadToEndAsync();
                    var prayerTimes = JsonConvert.DeserializeObject<PrayerTimesResponse>(json);
                }
            }
            catch (Exception ex)
            {
                using NotificationHelper notification = new NotificationHelper(false);
                notification.ShowNotification("ERROR", ex.Message, System.Windows.Forms.ToolTipIcon.Error);
            }
            return true;
        }

        public void WriteLocalJson(string content,string path)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.Write(content);
                }
            }
            catch (Exception ex)
            {
                using NotificationHelper notification = new NotificationHelper(false);
                notification.ShowNotification("ERROR", ex.Message, ToolTipIcon.Error);
            }
        }

        public async Task<bool> LoadFiles()
        {
            //using (AdhanAPI adhan = new AdhanAPI($"https://api.aladhan.com/v1/timingsByCity/{DateTime.Now.ToString("dd-MM-yyyy")}?"))
            //{
            //    if (InternetHelper.InternetAvailableFromMachine() && InternetHelper.InternetAvailableFromHost())
            //    {
            //        var responce = await adhan.GetAdanResponceAsync();
            //        if (responce != string.Empty)
            //        {
            //            WriteLocalJson(responce);
            //        }
            //        else
            //        {
            //            if (!await ReadLocalJson())
            //            {
            //                MessageBox.Show(text: "Your App need internet to work", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                return false;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        if (!await ReadLocalJson())
            //        {
            //            MessageBox.Show(text: "Your App need internet to work", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            return false;
            //        }
            //    }
            //}
            return true;
        }

        public string ReadResourcesFile(byte[] content)
        {
            using (MemoryStream stream = new MemoryStream(content))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    if (stream.Length > 0)
                        return reader.ReadToEnd();
                }
            }
            return string.Empty;
        }

        #region IO_For_Resources_Files


        #endregion
    }
}
