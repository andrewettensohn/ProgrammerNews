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
    public partial class SavedArticles : ContentPage
    {
        SavedArticlesViewModel ViewModel;
        public SavedArticles()
        {
            InitializeComponent();
            BindingContext = ViewModel = new SavedArticlesViewModel();
            Title = "Saved";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModel.TopStories.Count == 0)
                ViewModel.LoadStoriesCommand.Execute(null);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            Article article = args.SelectedItem as Article;
            if (article == null)
                return;

            await Browser.OpenAsync(article.Url, BrowserLaunchMode.SystemPreferred);
            TopStoriesListView.SelectedItem = null;
        }
    }
}