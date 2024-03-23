using MaterialDesignThemes.Wpf;
using MosqueMate.Helper;
using MosqueMate.Helper.HelperUI;
using MosqueMateServices.AppResources;
using MosqueMateServices.Enums;
using MosqueMateServices.Interfaces;
using MosqueMateServices.Repositories;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MosqueMate.Pages
{
    public partial class Quran : Page
    {
        private readonly ISouraNames soura;
        public Quran()
        {
            InitializeComponent();
            soura = new SuraNamesRepository();
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var result = soura.GetAllSoura();
            BackgroundTask.ExecuteThreadUI(() => CustomControl.GenerateMaterialCard(quranGridContainer, result,MosqueMateServices.Enums.RepositoriesTypes.Quran), this);
            using ResourceJsonRepo resource = new ResourceJsonRepo();
            continueReading.ToolTip = resource["ContinueReading"];
        }

        private void searchInQuran_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (quranGridContainer.Children.Count > 2)
            {
                var result = soura.GetSouraIdByName(searchInQuran.Text);
                if (result != 0)
                {
                    var card = quranGridContainer.Children[(int)result-1];
                    var castCard = card as Card;
                    Point relativePoint = castCard.TranslatePoint(new Point(0, 0), quranGridContainer);
                    quranGridScroll.ScrollToVerticalOffset(relativePoint.Y - 300); // get position of searched card
                }
            }
        }

        private void continueReading_Click(object sender, RoutedEventArgs e)
        {
            new QuranImageTemplate(0, QuranMode.Completion).ShowDialog();
        }
    }
}

