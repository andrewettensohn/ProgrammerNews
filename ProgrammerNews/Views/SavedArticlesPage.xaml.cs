using ProgrammerNews.Models;
using ProgrammerNews.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProgrammerNews.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SavedArticlesPage : ContentPage
    {
        private SavedArticlesViewModel _vm;
        public SavedArticlesPage()
        {
            InitializeComponent();
            BindingContext = _vm = new SavedArticlesViewModel();
            Title = "Saved";
        }

        protected override async void OnAppearing()
        {
            await _vm.LoadViewModelAsync();
            base.OnAppearing();
        }
    }
}