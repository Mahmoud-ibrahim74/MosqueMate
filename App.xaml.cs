using Microsoft.Win32;
using MosqueMate.Properties;
using MosqueMateServices.Helper;
using MosqueMateServices.Interfaces;
using MosqueMateServices.Repositories;
using System.IO;
using System.Windows;

namespace MosqueMate
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IAppData appData;
        INotificationWindows NotificationWindows;
        public App()
        {

            #region Intialize_System_Data
            appData = AppDataRepo.Instance;
            appData.currLang = Settings.Default.currentLang;
            appData.City = Settings.Default.City;
            appData.Country = Settings.Default.Country;
            appData.method = (int)Settings.Default.calculationMethod;
            #endregion
            NotificationWindows = new NotificationWindows(Settings.Default.notification);
            if (Settings.Default.autoStartUp)
                AddApplicationToStartup();
            else
                RemoveApplicationFromStartup(); 
        }
        public void AddApplicationToStartup()
        {
            string appName = "MosqueMate";
            string appPath = Application.ResourceAssembly.Location.Replace(".dll", ".exe");
            if (File.Exists(appPath))
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                {
                    key.SetValue(appName, appPath);
                }
            }

        }
        public void RemoveApplicationFromStartup()
        {
            string appName = "MosqueMate";
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                if (key.GetValue(appName) != null)
                {
                    key.DeleteValue(appName);
                }
            }
        }

    }
}
