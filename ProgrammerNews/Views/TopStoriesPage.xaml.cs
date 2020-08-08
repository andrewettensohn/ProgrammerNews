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
                //scrolled down
                previousScrollPosition = e.ScrollY;
            }
            else
            {
                //scrolled up

                if (Convert.ToInt16(e.ScrollY) == 0)
                {
                    
                }
                else
                {
                    ViewModel.Paging.Execute(null);
                    previousScrollPosition = 0;
                }
                
            }
                //MyScrollView scrollView = sender as MyScrollView;
                //ListView listView = sender as ListView;
                //double scrollingSpace = listView. - listView.Height;

                //if (scrollingSpace < e.ScrollY)
                //{
                //    ViewModel.Paging.Execute(null);
                //}
                // Touched bottom
                // Do the things you want to do
            }

            //private void TopStoriesListViewItemAppearing(object sender, ItemVisibilityEventArgs e)
            //{
            //    int index = e.ItemIndex;
            //    //(x % n) == 0
            //    if ((index % 20) == 0)
            //    {
            //        Article article = e.Item as Article;
            //        ViewModel.Paging.Execute(null);
            //    }
            //}

            //private void TopStoriesListView_Scrolled(object sender, ScrolledEventArgs e)
            //{
            //    ViewModel.Paging.Execute(null);
            //}
        }
}