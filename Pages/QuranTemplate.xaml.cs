using MosqueMate.Helper;
using MosqueMate.Helper.HelperUI;
using MosqueMate.Properties;
using MosqueMateServices.DTOs;
using MosqueMateServices.Interfaces;
using MosqueMateServices.Repositories;
using System;
using System.Windows;
using System.Windows.Input;

namespace MosqueMate.Pages
{
    public partial class QuranTemplate : Window

    {

        private readonly IQuran Quran;
        private int QuranIndex = 1;
        private int pageSize = 10;
        private DTOQuran Soura;
        private readonly ISouraNames SouraNames;
        private int souraId;
        private string ayahSeperator = "☪";
        public QuranTemplate(int souraId)
        {
            InitializeComponent();
            this.Quran = new QuranRepository(souraId);
            SouraNames = new SuraNamesRepository();
            Soura = Quran.GetSoura();
            this.souraId = souraId;
        }

        private void closeWindow_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default["souraId"] = souraId;
            Settings.Default["ayaId"] = QuranIndex;
            Settings.Default.Save();
            this.Close();

        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            if (Settings.Default.souraId == souraId)
                QuranIndex = Settings.Default.ayaId-1;


            BackgroundTask.ExecuteThreadUI(() =>
            {
                CustomControl.SetAppFont(this);
                if (Soura != null)
                {
                    QuranTitle.Text = Settings.Default.currentLang == "en" ? Soura.name : SouraNames.GetSouraNameById(souraId);
                    QuranText.Text = string.Join(ayahSeperator, Quran.QuranPagination(QuranIndex, pageSize));
                    DescLBL.Text = Settings.Default.currentLang == "en" ? $"{Soura.count} Ayat " : $"{Soura.count} آية ";
                }
            });
        }
        private void nextQuran()
        {
            var ayatList = this.Quran.QuranPagination(QuranIndex, pageSize);
            if (ayatList.Count < 1)
                return;
            QuranText.Text = string.Join(ayahSeperator, ayatList);
            QuranIndex++;
        }
        private void prevQuran()
        {
            var ayatList = this.Quran.QuranPagination(QuranIndex, pageSize);
            if (ayatList.Count < 1)
                return;
            QuranText.Text = string.Join(ayahSeperator, ayatList);
            QuranIndex--;

        }
        private void QuranCounter_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (QuranIndex == 0)
                QuranIndex = 1;


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
                Settings.Default["souraId"] = souraId;
                Settings.Default["ayaId"] = QuranIndex;
                Settings.Default.Save();
                this.Close();
            }
        }

    }
}