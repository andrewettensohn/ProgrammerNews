using ProgrammerNews.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using System.Windows.Input;

namespace ProgrammerNews.ViewModels
{
    public class TopStoriesViewModel : BaseViewModel
    {
        private ObservableCollection<Article> _topStories;
        public ObservableCollection<Article> TopStories
        {
            get => _topStories;
            set
            {
                SetValue(ref _topStories, value);
            }
        }

        public ICommand LoadArticlesCmd => _loadArticlesCmd;
        private RelayCommand _loadArticlesCmd { get; set; }

        public ICommand PageArticlesCmd => _pageArticlesCmd;
        private RelayCommand _pageArticlesCmd { get; set; }

        public ICommand SaveArticleCmd => _saveArticleCmd;
        private RelayCommand<int> _saveArticleCmd { get; set; }

        public TopStoriesViewModel()
        {
            _topStories = new ObservableCollection<Article>();
            _loadArticlesCmd = new RelayCommand(async () => await ExecuteLoadStoriesCommand());
            _pageArticlesCmd = new RelayCommand(async () => await ExecutePaging());
            _saveArticleCmd = new RelayCommand<int>(async (x) => await ExecuteSaveArticleCommand(x));
        }

        public async Task LoadViewModelAsync()
        {
            TopStories = new ObservableCollection<Article>(await App.DataManager.GetTopStories());
            RaiseAllPropertiesChanged();
        }

        private async Task ExecuteSaveArticleCommand(int articleId)
        {
            Article article = TopStories.FirstOrDefault(x => x.Id == articleId);
            await App.DataManager.SaveArticleAsync(article);
        }

        private async Task ExecutePaging()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                List<Article> stories = await App.DataManager.PerformFeedPaging();
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

        private async Task ExecuteLoadStoriesCommand()
        {
            if (IsBusy) return;

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
