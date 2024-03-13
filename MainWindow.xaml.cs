using MosqueMate.Helper;
using MosqueMate.Helper.HelperUI;
using MosqueMate.Properties;
using MosqueMateMedia.Properties;
using MosqueMateServices.AppResources;
using MosqueMateServices.Context;
using MosqueMateServices.Helper;
using MosqueMateServices.Interfaces;
using MosqueMateServices.Repositories;
using System;
using System.Windows;

namespace MosqueMate
{
    public partial class MainWindow : Window
    {
        INotificationWindows notification;
        IAppData appData;
        IZekr zekr;
        private int zekrId = 1;
        private int ZekrIndex = 1;
        private IAudioPlayer audioPlayer;
        public bool isPanelShow { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            //TimeInterval();
            notification = new NotificationHelper(Settings.Default.notification);
            appData = AppDataRepo.Instance;
            zekr = new ZekrRepository();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BackgroundTask.ExecuteNormalTask(() =>
            {

            });
            BackgroundTask.ExecuteThreadUI(() =>
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
                    var bitmap = BitmapHelper.ConvertBitmapToBitmapImage(Media.prayer);
                    appLogo.ImageSource = bitmap;
                    #endregion

                }
                //TimeInterval();
                PagesNavigation.Navigate(new Uri("Pages/Home.xaml", UriKind.RelativeOrAbsolute));
            });
        }

        //private void TimeInterval()
        //{
        //    BackgroundTask.RunTaskWithWhile(1000, () =>
        //    {
        //        timeCLock.Content = DateTime.Now.ToString("hh:mm:ss tt");
        //        using ResourceJsonRepo resource = new ResourceJsonRepo();
        //        georgianDate.Content = DateTimeHelper.georgianDate;
        //        hijriDate.FlowDirection = Settings.Default.currentLang == "en" ? FlowDirection.LeftToRight : FlowDirection.RightToLeft;
        //        hijriDate.Content = DateTimeHelper.hijriDate;
        //    });
        //}
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
            PagesNavigation.Navigate(new Uri("Pages/home.xaml", UriKind.RelativeOrAbsolute));

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
    }
}