using ProgrammerNews.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using System.Windows.Input;

namespace ProgrammerNews.ViewModels
{
    public class SavedArticlesViewModel : BaseViewModel
    {
        public ObservableCollection<Article> SavedStories { get; set; }
        public Command LoadStoriesCommand { get; set; }
        public ICommand DeleteArticleCommand
        {
            get
            {
                return new Command<int>(async (x) => await ExecuteDeleteArticleCommand(x));
            }
        }

        public SavedArticlesViewModel()
        {
            SavedStories = new ObservableCollection<Article>();
            LoadStoriesCommand = new Command(async () => await ExecuteLoadStoriesCommand());
        }

        async Task ExecuteDeleteArticleCommand(int id)
        {
            Article article = SavedStories.FirstOrDefault(x => x.Id == id);
            await App.DataManager.DeleteArticleAsync(article);
            await ExecuteLoadStoriesCommand();
        }

        async Task ExecuteLoadStoriesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                SavedStories.Clear();
                var stories = await App.DataManager.GetSavedArticles();
                foreach (var story in stories)
                {
                    SavedStories.Add(story);
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
