using ProgrammerNews.Models;
using ProgrammerNews.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProgrammerNews.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TopStoriesPage : ContentPage
    {
        TopStoriesViewModel ViewModel;
        //public ICommand OpenWebCommand { get; set; }
        public TopStoriesPage()
        {

            InitializeComponent();
            BindingContext = ViewModel = new TopStoriesViewModel();
            Title = "Hacker News Feed";
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var article = args.SelectedItem as Article;
            if (article == null)
                return;

            //await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(article)));
            //OpenWebCommand = new Command(async () => await Browser.OpenAsync(article.Url));
            await Browser.OpenAsync(article.Url, BrowserLaunchMode.SystemPreferred);
            // Manually deselect item.
            TopStoriesListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModel.TopStories.Count == 0)
                ViewModel.LoadStoriesCommand.Execute(null);
        }
    }
}