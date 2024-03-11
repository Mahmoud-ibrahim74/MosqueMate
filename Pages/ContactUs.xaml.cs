using MosqueMate.Helper;
using MosqueMate.Properties;
using MosqueMateMedia.Properties;
using MosqueMateServices.AppResources;
using MosqueMateServices.Context;
using MosqueMateServices.DTOs;
using MosqueMateServices.Helper;
using MosqueMateServices.Interfaces;
using MosqueMateServices.Repositories;
using MosqueMateServices.Services;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace MosqueMate.Pages
{
    public partial class Contactus : Page

    {
        IAppData appData;
        INotificationWindows notification;
        private IAudioPlayer audioPlayer;

        public Contactus()
        {
            InitializeComponent();
            appData = AppDataRepo.Instance;
            notification = new NotificationHelper(Settings.Default.notification);

        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            BackgroundTask.ExecuteThreadUI(() =>
            {
                var bitmapImage = BitmapHelper.ConvertBitmapToBitmapImage(Media.GetInTouch);
                imageSupport.Source = bitmapImage;
            });
        }
        private void RestartApp()
        {
            var currentExecutablePath = Process.GetCurrentProcess().MainModule.FileName;
            Process.Start(currentExecutablePath);
            Application.Current.Shutdown();
        }

        private void facebookLink_Click(object sender, RoutedEventArgs e)
        {
            WinFormHelper.OpenAppLink("https://www.facebook.com/Houda405");
        }

        private void whatsappLink_Click(object sender, RoutedEventArgs e)
        {
            WinFormHelper.OpenAppLink("https://wa.me/2001069903556");
        }

        private void linkedInLink_Click(object sender, RoutedEventArgs e)
        {
            WinFormHelper.OpenAppLink("https://www.linkedin.com/in/mahmoud-ibrahim74/");
        }
        private void githubLink_Click(object sender, RoutedEventArgs e)
        {
            WinFormHelper.OpenAppLink("https://github.com/Mahmoud-ibrahim74");

        }

    

        private void sendBtn_Click(object sender, RoutedEventArgs e)
        {
            using ResourceJsonRepo resource = new ResourceJsonRepo();

            #region SchemaValidation
            if (string.IsNullOrEmpty(fname.Text))
            {
                MessageBox.Show(resource["fnameValidation"], "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(lname.Text))
            {
                MessageBox.Show(resource["lnameValidation"], "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(email.Text))
            {
                MessageBox.Show(resource["emailValidation"], "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(body.Text))
            {
                MessageBox.Show(resource["bodyValidation"], "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!EMail.IsValidEmail(email.Text))
            {
                MessageBox.Show(resource["emailValidation2"], "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            #endregion

            DTOContactUs contactUs = new DTOContactUs()
            {
                email = email.Text,
                fname = fname.Text,
                lname = lname.Text, 
                issue = body.Text,  
            };
            BackgroundTask.ExecuteNormalTaskWithDisable(() =>
            {
                AppDbContext supportConetxt = new AppDbContext();
                supportConetxt.InsertIssue(contactUs);

            }, this);


        }
    }
}

