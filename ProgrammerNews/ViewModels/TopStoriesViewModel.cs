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
using System.Windows.Input;

namespace ProgrammerNews.ViewModels
{
    public class TopStoriesViewModel : BaseViewModel
    {
        public ObservableCollection<Article> TopStories { get; set; }
        public Command LoadStoriesCommand { get; set; }
        public Command PagingCommand { get; set; }
        public ICommand SaveArticleCommand 
        { get
            {
                return new Command<int>(async (x) => await ExecuteSaveArticle(x)); 
            } 
        }
        public TopStoriesViewModel()
        {
            TopStories = new ObservableCollection<Article>();
            LoadStoriesCommand = new Command(async () => await ExecuteLoadStoriesCommand());
            PagingCommand = new Command(async () => await ExecutePaging());
        }

        async Task ExecuteSaveArticle(int articleId)
        {
            Article article = TopStories.FirstOrDefault(x => x.Id == articleId);
            await App.DataManager.SaveArticleAsync(article);
        }

        async Task ExecutePaging()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var stories = await App.DataManager.PerformFeedPaging();
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

        async Task ExecuteLoadStoriesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                TopStories.Clear();
                var stories = await App.DataManager.GetTopStories();
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
