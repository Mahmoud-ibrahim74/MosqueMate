using MosqueMate.Helper;
using MosqueMate.Helper.HelperUI;
using MosqueMateServices.AppResources;
using MosqueMateServices.DTOs;
using MosqueMateServices.Interfaces;
using MosqueMateServices.Repositories;
using System.Windows;
using System.Windows.Input;

namespace MosqueMate.Pages
{
    public partial class HdithTemplate : Window

    {
        private readonly IHadith hadith;
        private int hadithNumber = 0;
        private readonly DTOHadith dTOHadith;
        public HdithTemplate(int number)
        {
            InitializeComponent();
            hadith = new HadithRepository();
            this.hadithNumber = number;
            dTOHadith = hadith.GetHadithByID(number);       
        }

        private void closeWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BackgroundTask.ExecuteThreadUI(() =>
            {         
                using ResourceJsonRepo resource = new ResourceJsonRepo();   
                CustomControl.SetAppFont(this);
                hadithTitle.Text = resource["HadithNo"] + " "+  StringHelper.NumberToWords(this.hadithNumber);
                hadithText.Text = dTOHadith.hadith;
            });
        }
    
        private void zeker_container_KeyDown(object sender,KeyEventArgs e)
        {
           
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }
    }

}

