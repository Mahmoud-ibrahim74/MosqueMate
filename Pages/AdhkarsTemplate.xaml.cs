using MosqueMate.Helper;
using MosqueMate.Helper.HelperUI;
using MosqueMate.Properties;
using MosqueMateServices.AppResources;
using MosqueMateServices.DTOs;
using MosqueMateServices.Interfaces;
using MosqueMateServices.Repositories;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace MosqueMate.Pages
{
    public partial class AdhkarsTemplate : Window

    {

        private readonly IZekr zekr;
        private string zekrCategory;
        private int zekrCategoryCount;
        private int ZekrIndex = 1;
        private int ZekrIndexCount = 1;
        private List<DTOAzkar> zekrResult;
        public AdhkarsTemplate(string zekrCategory)
        {
            InitializeComponent();
            this.zekr = new ZekrRepository();
            this.zekrCategory = zekrCategory;
            zekrResult = zekr.GetZekrByName(zekrCategory);
        }

        private void closeWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BackgroundTask.ExecuteThreadUI(() =>
            {
                CustomControl.SetAppFont(this);
                if (zekrResult != null)
                {
                    ZekrIndex = 1; // reset index when toggle panel 
                    var zekrContent = zekrResult[ZekrIndex - 1];
                    zekrTitle.Text = zekrContent.category;
                    zekrText.Text = zekrContent.zekr;
                    DescLBL.Content = $"   {zekrContent.reference}";
                    ZekrIndexCount = Convert.ToInt32(zekrContent.count == string.Empty ? "0" : zekrContent.count);
                    SetLabelsZekr();

                }
            });
        }
        private void nextZekr()
        {
            if (ZekrIndex == zekrResult.Count)
                return;
            ZekrIndex++;
            var zekr = this.zekr.ZekrPagination(zekrResult, ZekrIndex, 1);
            zekrText.Text = zekr.zekr;
            DescLBL.Content = $"   {zekr.reference}";
            ZekrIndexCount = Convert.ToInt32(zekr.count == string.Empty ? "0" : zekr.count);


        }
        private void prevZekr()
        {
            if (ZekrIndex == 1)
                return;
            ZekrIndex--;
            var zekr = this.zekr.ZekrPagination(zekrResult, ZekrIndex, 1);
            zekrText.Text = zekr.zekr;
            DescLBL.Content = $"   {zekr.reference}";
            ZekrIndexCount = Convert.ToInt32(zekr.count == string.Empty ? "0" : zekr.count);


        }
        private void SetLabelsZekr()
        {
            using (ResourceJsonRepo resource = new ResourceJsonRepo())
            {
                #region SetZekrLabels
                var timeInFileIndex = zekrCategoryCount > 3 && zekrCategoryCount < 11 ? "Times" : "Time";
                zekrCounter.Text = (Settings.Default.currentLang == "ar" ? ZekrIndexCount + " " + resource[timeInFileIndex] : resource[timeInFileIndex] + " " + ZekrIndexCount);
                numberLBL.Content = ZekrIndex + " / " + zekrResult.Count;
                numberLBL.FlowDirection = FlowDirection.RightToLeft;

                #endregion
            }
        }

        private void zekrCounter_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (ZekrIndex == 0)
                ZekrIndex = 1;

            if (zekrResult.Count > 1)
            {
                if (e.Key == Key.Left)
                {
                    e.Handled = true;
                    prevZekr();
                    SetLabelsZekr();
                }
                else if (e.Key == Key.Right)
                {
                    e.Handled = true;
                    nextZekr();
                    SetLabelsZekr();
                }
                else if (e.Key == Key.Escape)
                {
                    this.Close();
                }
            }
            if (e.Key == Key.S && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                BitmapHelper.CaptureScreenshot(this);
            }

        }

        private void Window_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (zekrResult.Count > 1)
            {
                if (e.Delta < 0)
                {
                    e.Handled = true;
                    nextZekr();
                    SetLabelsZekr();
                }
                else
                {
                    e.Handled = true;
                    prevZekr();
                    SetLabelsZekr();
                }
            }
        }
    }

}

