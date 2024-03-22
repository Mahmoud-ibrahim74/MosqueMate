using MosqueMate.Helper;
using MosqueMate.Helper.HelperUI;
using MosqueMateServices.Interfaces;
using MosqueMateServices.Repositories;
using System.Windows;
using System.Windows.Controls;

namespace MosqueMate.Pages
{
    public partial class Hadith : Page
    {
        private int hadithIndex = 1;
        private int hadithSize = 50;
        private readonly IHadith hadith;
        public Hadith()
        {
            InitializeComponent();
            hadith = new HadithRepository();
        }


        private  void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var result = hadith.GetAllByName();
            BackgroundTask.ExecuteThreadUI(() =>
            {
                BackgroundTask.ExecuteThreadUI(() => CustomControl.GenerateMaterialCard(hadithGridContainer, result, MosqueMateServices.Enums.RepositoriesTypes.Hadith));
            });
        }

        private void searchInQuran_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void loadMore_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

