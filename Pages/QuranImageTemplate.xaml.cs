using MosqueMate.Helper;

using MosqueMate.Helper.HelperUI;
using MosqueMate.Properties;
using MosqueMateMedia.Properties;
using MosqueMateServices.Helper;
using System;
using System.Windows;
using System.Windows.Input;

namespace MosqueMate.Pages
{
    public partial class QuranImageTemplate : Window

    {

        private int souraId;
        private double zoomX = 1.0;
        private int pageIndex = 1;
        public QuranImageTemplate(int souraId)
        {
            InitializeComponent();
            this.souraId = 5;
        }

        private void closeWindow_Click(object sender, RoutedEventArgs e)
        {
            //Settings.Default["souraId"] = souraId;
            Settings.Default["ayaId"] = pageIndex;
            Settings.Default.Save();
            this.Close();

        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //if (Settings.Default.souraId == souraId)
            //    QuranIndex = Settings.Default.ayaId-1;


            BackgroundTask.ExecuteThreadUI(() =>
            {
                CustomControl.SetAppFont(this);
                //if (Soura != null)
                //{
                //    QuranTitle.Text = Settings.Default.currentLang == "en" ? Soura.name : SouraNames.GetSouraNameById(souraId);
                //    QuranText.Text = string.Join(ayahSeperator, Quran.QuranPagination(QuranIndex, pageSize));
                //    DescLBL.Text = Settings.Default.currentLang == "en" ? $"{Soura.count} Ayat " : $"{Soura.count} آية ";
                //}
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
        private void QuranCounter_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //if (QuranIndex == 0)
            //    QuranIndex = 1;


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
                this.Close();
            }
        }

        private void quranGridContainer_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            //if (e.Delta > 0)
            //{
            //    zoomX += 0.2;
            //    pageImage.RenderTransform = new ScaleTransform(zoomX, zoomX);

            //}
            //else
            //{
            //    zoomX -= 0.2;
            //    pageImage.RenderTransform = new TranslateTransform(zoomX, zoomX);
            //}

        }
        private void LoadImage()
        {
            using ResourceHelper<QuranImagesResources> helper = new ResourceHelper<QuranImagesResources>("QuranImagesResources");
            var pageBitmap = helper.GetQuranImageResxByPageNumber(pageIndex);
            var convertedBitmp = BitmapHelper.ConvertBitmapToBitmapImage(pageBitmap);
            pageImage.Source = convertedBitmp;
        }
    }
}