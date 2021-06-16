using ProgrammerNews.Models;
using ProgrammerNews.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            Article article = args.SelectedItem as Article;
            if (article == null)
                return;

            await Browser.OpenAsync(article.Url, BrowserLaunchMode.SystemPreferred);
            SavedStoriesListView.SelectedItem = null;
        }
    }
}