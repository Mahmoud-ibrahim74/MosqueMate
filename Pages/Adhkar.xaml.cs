using MaterialDesignThemes.Wpf;
using MosqueMate.Helper;
using MosqueMate.Helper.HelperUI;
using MosqueMateServices.Interfaces;
using MosqueMateServices.Repositories;
using System.Windows;
using System.Windows.Controls;

namespace MosqueMate.Pages
{
    public partial class Adhkar : Page
    {
        private readonly IZekr zekr;
        public Adhkar()
        {
            InitializeComponent();
            zekr = new ZekrRepository();
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var result = zekr.GetAllCategoriesByGrouping();
            BackgroundTask.ExecuteThreadUI(() => CustomControl.GenerateMaterialCard(adhkarGridContainer, result,MosqueMateServices.Enums.RepositoriesTypes.Azkar));
        }

        private void searchInZekr_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (adhkarGridContainer.Children.Count > 2 || !string.IsNullOrWhiteSpace(searchInZekr.Text))
            {
                var point = CustomControl.GetOffsetChildOfElement<Card>(adhkarGridContainer, searchInZekr.Text);
                adhkarGridScroll.ScrollToVerticalOffset(point.Y - 300); // get position of searched card
            }
        }
    }
}

