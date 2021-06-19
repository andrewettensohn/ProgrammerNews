using ProgrammerNews.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;

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

        public ICommand ArticleLinkSelectedCmd => _articleLinkSelectedCmd;
        private RelayCommand<string> _articleLinkSelectedCmd { get; set; }

        public ICommand PageTopStoriesCmd => _pageTopStoriesCmd;
        private RelayCommand _pageTopStoriesCmd { get; set; }

        public TopStoriesViewModel()
        {
            _topStories = new ObservableCollection<Article>();
            _loadArticlesCmd = new RelayCommand(async () => await ExecuteLoadStoriesCommand());
            _pageArticlesCmd = new RelayCommand(async () => await ExecutePaging());
            _saveArticleCmd = new RelayCommand<int>(async (x) => await ExecuteSaveArticleCommand(x));
            _articleLinkSelectedCmd = new RelayCommand<string>(async (x) => await ExecuteArticleLinkSelectedCommand(x));
            _pageTopStoriesCmd = new RelayCommand(async () => await ExecutePageTopStoriesCommand());
        }

        public async Task LoadViewModelAsync()
        {
            IsBusy = true;

            TopStories = new ObservableCollection<Article>(await App.DataManager.GetTopStories());
            RaiseAllPropertiesChanged();

            IsBusy = false;
        }

        private async Task ExecutePageTopStoriesCommand()
        {
            if (IsBusy) return;

            IsBusy = true;

            try
            {

                List<Article> stories = await App.DataManager.PerformFeedPaging();
                foreach (Article story in stories)
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

        private async Task ExecuteArticleLinkSelectedCommand(string url)
        {
            await Browser.OpenAsync(url, BrowserLaunchMode.SystemPreferred);
        }

        private async Task ExecuteSaveArticleCommand(int articleId)
        {
            IsBusy = true;
            Article article = TopStories.FirstOrDefault(x => x.Id == articleId);
            await App.DataManager.SaveArticleAsync(article);
            IsBusy = false;
        }

        private async Task ExecutePaging()
        {
            if (IsBusy) return;

            IsBusy = true;

            try
            {
                List<Article> stories = await App.DataManager.PerformFeedPaging();
                foreach (Article story in stories)
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
            IsBusy = true;

            try
            {
                TopStories = new ObservableCollection<Article>(await App.DataManager.GetTopStories());
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
