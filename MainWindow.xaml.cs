using MosqueMate.Helper;
using MosqueMate.Helper.HelperUI;
using MosqueMateMedia.Properties;
using MosqueMateServices.AppResources;
using MosqueMateServices.Helper;
using MosqueMateServices.Helper.API;
using MosqueMateServices.Interfaces;
using MosqueMateServices.Repositories;
using Newtonsoft.Json;
using PrayIDataServices.Helper.API;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace MosqueMate
{
    public partial class MainWindow : Window
    {
        IAppData appData;
        public bool isPanelShow { get; private set; }
        public MainWindow()
        {
            InitializeComponent();
            appData = AppDataRepo.Instance;
            API.url = "https://api.aladhan.com/v1/timingsByCity/{DateTime.Now:dd-MM-yyyy}?" + $"city={appData.City}&country={appData.Country}&method={appData.method}";
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (ResourceJsonRepo resource = new ResourceJsonRepo())
            {
                #region ControlFilling
                CustomControl.SetAppFont(this);
                this.Title = resource["AppName"];
                this.appTitle.Text = resource["AppName"];
                homeLBL.Text = resource["SideBar.Home"];
                closeAppLBL.Text = resource["CloseApp"];
                minimizeText.Text = resource["minimize"];
                SettingsLBL.Text = resource["SideBar.Settings"];
                contactUsTxt.Text = resource["SideBar.contactUs"];
                AdhkarText.Text = resource["SideBar.Adhkar"];
                quranText.Text = resource["SideBar.Quran"];
                hadithTxt.Text = resource["Hadith"];
                #endregion

                #region AppLogo
                var bitmap = BitmapHelper.ConvertBitmapToBitmapImage(Media.pray.ToBitmap());
                appLogo.ImageSource = bitmap;
                #endregion

            }

            BackgroundTask.ExecuteNormalTask(async () =>
            {
                if (await SendApiRequest())
                {
                    BackgroundTask.ExecuteThreadUI(() =>
                    {
                        PagesNavigation.Navigate(new Uri("Pages/Home.xaml", UriKind.RelativeOrAbsolute));
                    });
                }
                else
                {
                    using NotificationWindows notification = new NotificationWindows(true,()=>AppHelper.RestartApp());
                    using ResourceJsonRepo resource = new ResourceJsonRepo();
                    notification.ShowNotification("Error", resource["checkConnction"], System.Windows.Forms.ToolTipIcon.Error);
                    return;
                }

            });
        }
        private void closeAppBTN_Click(object sender, RoutedEventArgs e)
        {
            using ResourceJsonRepo resource = new ResourceJsonRepo();
            if (MessageBox.Show(resource["CloseApp"], "warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();

            }
        }
        private void minimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void homeBtn_Click(object sender, RoutedEventArgs e)
        {
            BackgroundTask.ExecuteNormalTask(() =>
            {
                if (appData.API_DATA != null)
                {
                    BackgroundTask.ExecuteThreadUI(() =>
                    {
                        PagesNavigation.Navigate(new Uri("Pages/Home.xaml", UriKind.RelativeOrAbsolute));
                    });
                }
                else
                {
                    using NotificationWindows notification = new NotificationWindows(true);
                    using ResourceJsonRepo resource = new ResourceJsonRepo();
                    notification.ShowNotification("Error", resource["checkConnction"], System.Windows.Forms.ToolTipIcon.Error);
                    return;
                }

            });
        }
        private void settingsBtn_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new Uri("Pages/AppSetting.xaml", UriKind.RelativeOrAbsolute));
        }

        private void contactUsBtn_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new Uri("Pages/Contactus.xaml", UriKind.RelativeOrAbsolute));

        }

        private void AdhkarBtn_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new Uri("Pages/Adhkar.xaml", UriKind.RelativeOrAbsolute));
        }

        private void quranBtn_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new Uri("Pages/Quran.xaml", UriKind.RelativeOrAbsolute));
        }

        private void hadithBtn_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new Uri("Pages/Hadith.xaml", UriKind.RelativeOrAbsolute));

        }
        private async Task<bool> SendApiRequest()
        {
            #region API_Data
            var responce = await API.GetMethodAsync();
            if (responce != null && API.HttpException == null)
            {

                #region SendRequestAPI
                var desrialize = JsonConvert.DeserializeObject<PrayerTimesResponse>(responce);
                appData.API_DATA = desrialize;
                #endregion
                return true;
            }
            else
                return false;

            #endregion
        }
    }
}