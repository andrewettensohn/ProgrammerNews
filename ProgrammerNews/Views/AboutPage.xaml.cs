using ProgrammerNews.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProgrammerNews.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AboutPage : ContentPage
    {
        public AboutViewModel ViewModel;
        public AboutPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new AboutViewModel();
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            bool deleteConfirmed = await DisplayAlert("Confirm", "Are you sure you want to delete all saved articles?", "Delete", "Cancel");

            if (deleteConfirmed)
            {
                ViewModel.DeleteAllSavedArticlesCommand.Execute(null);
            }
        }
    }
}