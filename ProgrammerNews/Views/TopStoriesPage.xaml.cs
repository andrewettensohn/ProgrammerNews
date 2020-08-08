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
        public TopStoriesPage()
        {

            InitializeComponent();
            BindingContext = ViewModel = new TopStoriesViewModel();
            Title = "Hacker News Feed";
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            Article article = args.SelectedItem as Article;
            if (article == null)
                return;

            await Browser.OpenAsync(article.Url, BrowserLaunchMode.SystemPreferred);
            TopStoriesListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModel.TopStories.Count == 0)
                ViewModel.LoadStoriesCommand.Execute(null);
        }

        private double previousScrollPosition = 0;
        private void TopStoriesListView_Scrolled(object sender, ScrolledEventArgs e)
        {
            if (previousScrollPosition < e.ScrollY)
            {
                if (Convert.ToInt16(e.ScrollY) != 0)
                {
                    ViewModel.Paging.Execute(null);
                    previousScrollPosition = e.ScrollY;
                }
            }
            else
            {
                if (Convert.ToInt16(e.ScrollY) == 0)
                {
                    previousScrollPosition = 0;
                }
            }
        }
    }
}