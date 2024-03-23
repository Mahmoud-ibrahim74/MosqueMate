using MosqueMate.Helper;
using MosqueMate.Properties;
using MosqueMateMedia.Properties;
using MosqueMateServices.AppResources;
using MosqueMateServices.Enums;
using MosqueMateServices.Helper;
using MosqueMateServices.Interfaces;
using MosqueMateServices.Repositories;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MosqueMate.Pages
{
    public partial class AppSetting : Page

    {
        IAppData appData;
        INotificationWindows notification;
        private IAudioPlayer audioPlayer;

        public AppSetting()
        {
            InitializeComponent();
            appData = AppDataRepo.Instance;
            notification = new NotificationHelper(Settings.Default.notification);

        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            BackgroundTask.ExecuteThreadUI(new Action(() =>
            {

                using ResourceJsonRepo resourcesJson = new ResourceJsonRepo();
                methodLBL.ToolTip = resourcesJson["calculationMethodToolTip"];
                pageTitle.Content = resourcesJson["SideBar.Settings"];
                LangLBL.Content = resourcesJson["Language"];
                muezzinLBL.Content = resourcesJson["Muezzin"];
                //modeTextBox.Text = resourcesJson["Mode"];
                //SaveSetting.Content = resourcesJson["Save"];
                countryLBL.Content = resourcesJson["countryLBL"];
                methodLBL.Content = resourcesJson["calculationMethod"];
                notificationLBL.Content = resourcesJson["notificationLBL"];
                notificationDesc.Text = resourcesJson["notificationDesc"];
                SaveSetting.Content = resourcesJson["Save"];
                notificationToggle.IsChecked = Settings.Default.notification ? true : false;
                notificationDesc.TextAlignment = Settings.Default.currentLang == "en" ? TextAlignment.Right : TextAlignment.Left;
                switch (Settings.Default.currentLang)
                {
                    case "en":
                        rdEnglish.IsChecked = true;
                        break;
                    case "ar":
                        rdArabic.IsChecked = true;
                        break;
                    case "fr":
                        rdFrench.IsChecked = true;
                        break;

                    default:
                        break;
                }
                #region ComboBoxes
                sheckihSelection.ItemsSource = appData.MuzzienNames().Select(c => c.Value);
                countryBox.ItemsSource = appData.CountriesValue().Select(c => c.Value);
                citiesBox.ItemsSource = appData.CitiesValue().Select(c => c.Value);
                citiesBox.SelectedItem = Settings.Default.City;
                methodBox.ItemsSource = EnumConverter<CalculationMethods>.ConvertToEnum();
                sheckihSelection.SelectedItem = Settings.Default.MuezzinFilename;
                countryBox.SelectedItem = Settings.Default.Country;
                methodBox.SelectedIndex = (int)Settings.Default.calculationMethod;
                #endregion



            }));

        }

        private void SaveSetting_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using ResourceJsonRepo resourcesJson = new ResourceJsonRepo();

                #region RadioSet
                if (rdArabic.IsChecked == true)
                    Settings.Default["currentLang"] = "ar";

                if (rdEnglish.IsChecked == true)
                    Settings.Default["currentLang"] = "en";

                if (rdFrench.IsChecked == true)
                    Settings.Default["currentLang"] = "fr";

                #endregion

                #region SetValueSettings
                string? selectedContent = sheckihSelection.SelectedItem?.ToString();
                string? selectedCountry = countryBox.SelectedItem?.ToString();
                string? selectedCities = citiesBox.SelectedItem?.ToString();
                var cityParse = selectedCities?.Split(",");
                var method = methodBox.SelectedItem?.ToString();
                var index = Enum.TryParse<CalculationMethods>(method, out var enumValue) ? enumValue : 0;
                Settings.Default["MuezzinFilename"] = selectedContent;
                Settings.Default["Country"] = selectedCountry;
                Settings.Default["City"] = cityParse?[0];
                Settings.Default["calculationMethod"] = (int)index;
                var notifycheckedState = (bool)notificationToggle.IsChecked;
                Settings.Default["notification"] = notifycheckedState;
                #endregion

                if (MessageBox.Show(resourcesJson["RestartAppWarning"],
                    "Warning",
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Warning
                    ) == MessageBoxResult.Yes)
                {
                    Settings.Default.Save();
                    RestartApp();
                }
            }
            catch (Exception ex)
            {
                notification.ShowNotification("Error", ex.Message, System.Windows.Forms.ToolTipIcon.Error);
            }

        }
        private void SaveSettings()
        {
        }

        private void sheckihSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (ResourceHelper<AudioResources> resource = new ResourceHelper<AudioResources>("AudioResources"))
            {
                var muzzinAudio = resource.GetWavResxByName(sheckihSelection.SelectedItem.ToString());
                audioPlayer = new SoundHelper(muzzinAudio);
            }
        }

        private void notificationToggle_Checked(object sender, RoutedEventArgs e)
        {
            notificationDesc.Visibility = notificationToggle.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
        }

        private void notificationToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            notificationDesc.Visibility = notificationToggle.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;

        }

        private void playMuzzien_Click(object sender, RoutedEventArgs e)
        {
            if (sheckihSelection.SelectedValue != null)
            {
                audioPlayer.PlayAudio();
            }
        }
        private void stopMuzzien_Click(object sender, RoutedEventArgs e)
        {
            if (sheckihSelection.SelectedValue != null)
            {
                audioPlayer.StopAudio();
            }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
           
        }
        private void RestartApp()
        {
            var currentExecutablePath = Process.GetCurrentProcess().MainModule.FileName;
            Process.Start(currentExecutablePath);
            Application.Current.Shutdown();
        }

    }
}

