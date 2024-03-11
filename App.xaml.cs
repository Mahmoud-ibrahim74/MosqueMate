using MosqueMate.Properties;
using MosqueMateServices.Helper;
using MosqueMateServices.Interfaces;
using MosqueMateServices.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MosqueMate
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IAppData appData;
        INotificationWindows notificationHelper;
        public App()
        {

            #region Intialize_System_Data
            appData = AppDataRepo.Instance;
            appData.currLang = Settings.Default.currentLang;
            appData.City = Settings.Default.City;
            appData.Country = Settings.Default.Country;
            appData.method = (int)Settings.Default.calculationMethod;
            #endregion
            notificationHelper = new NotificationHelper(Settings.Default.notification);
        }
        
    }
}
