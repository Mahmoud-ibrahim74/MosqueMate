using MosqueMate.Helper;
using MosqueMate.Helper.HelperUI;
using MosqueMate.Properties;
using MosqueMateMedia.Properties;
using MosqueMateServices.Enums;
using MosqueMateServices.Helper;
using System.Windows;
using System.Windows.Input;

namespace MosqueMate.Pages
{
    public partial class QuranImageTemplate : Window
    {
        private int pageIndex;

        public QuranImageTemplate(int pageIndex,QuranMode mode)
        {
            InitializeComponent();
            switch (mode)
            {
                case QuranMode.Custom:
                    this.pageIndex = pageIndex;
                    break;
                case QuranMode.Completion:
                    this.pageIndex = Settings.Default.pageIndex;    
                    break;
            }
        }

        private void closeWindow_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default["pageIndex"] = pageIndex;
            Settings.Default.Save();
            this.Close();

        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BackgroundTask.ExecuteThreadUI(() =>
            {
                CustomControl.SetAppFont(this);
                if (this.pageIndex == 0)
                    LoadImage();
                else
                    LoadImage();
            });
        }
        private void nextQuran()
        {
            pageIndex++;
            LoadImage();
        }
        private void prevQuran()
        {
            if (pageIndex == 1)
                return;

            pageIndex--;
            LoadImage();

        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                e.Handled = true;
                prevQuran();
            }
            else if (e.Key == Key.Right)
            {
                e.Handled = true;
                nextQuran();
            }
            else if (e.Key == Key.Escape)
            {
                Settings.Default["pageIndex"] = pageIndex;
                Settings.Default.Save();
                this.Close();
            }
        }
        private void LoadImage()
        {
            using ResourceHelper<QuranImagesResources> helper = new ResourceHelper<QuranImagesResources>("QuranImagesResources");
            var pageBitmap = helper.GetQuranImageResxByPageNumber(this.pageIndex);
            var convertedBitmp = BitmapHelper.ConvertBitmapToBitmapImage(pageBitmap);
            pageImage.Source = convertedBitmp;
        }
    }
}