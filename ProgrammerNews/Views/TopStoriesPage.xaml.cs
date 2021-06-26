using ProgrammerNews.Models;
using ProgrammerNews.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProgrammerNews.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TopStoriesPage : ContentPage
    {
        public TopStoriesViewModel ViewModel;
        public TopStoriesPage()
        {

            InitializeComponent();
            BindingContext = ViewModel = new TopStoriesViewModel();
            Title = "Hacker News Feed";
        }

        protected override async void OnAppearing()
        {
            await ViewModel.LoadViewModelAsync();
            base.OnAppearing();
        }

        private void TopStoriesListView_Scrolled(object sender, ScrolledEventArgs e) => Task.Run(async () => await ViewModel.ExecutePageTopStoriesCommand(sender, e));
    }
}