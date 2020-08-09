using ProgrammerNews.Models;
using ProgrammerNews.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

namespace ProgrammerNews.ViewModels
{
    public class SavedArticlesViewModel : BaseViewModel
    {
        public ObservableCollection<Article> TopStories { get; set; }
        public Command LoadStoriesCommand { get; set; }
        public Command SaveArticleCommand { get; set; }
        public SavedArticlesViewModel()
        {
            TopStories = new ObservableCollection<Article>();
            LoadStoriesCommand = new Command(async () => await ExecuteLoadStoriesCommand());
        }

        async Task ExecuteLoadStoriesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                TopStories.Clear();
                var stories = await App.DataManager.GetSavedArticles();
                foreach (var story in stories)
                {
                    TopStories.Add(story);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
