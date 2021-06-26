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

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                SetValue(ref _isLoading, value);
            }
        }

        public ICommand LoadArticlesCmd => _loadArticlesCmd;
        private RelayCommand _loadArticlesCmd { get; set; }

        public ICommand SaveArticleCmd => _saveArticleCmd;
        private RelayCommand<int> _saveArticleCmd { get; set; }

        public ICommand ArticleLinkSelectedCmd => _articleLinkSelectedCmd;
        private RelayCommand<string> _articleLinkSelectedCmd { get; set; }

        private double previousScrollPosition = 0;

        public TopStoriesViewModel()
        {
            _topStories = new ObservableCollection<Article>();
            _loadArticlesCmd = new RelayCommand(async () => await ExecuteLoadStoriesCommand());
            _saveArticleCmd = new RelayCommand<int>(async (x) => await ExecuteSaveArticleCommand(x));
            _articleLinkSelectedCmd = new RelayCommand<string>(async (x) => await ExecuteArticleLinkSelectedCommand(x));
        }

        public async Task LoadViewModelAsync()
        {
            IsBusy = true;
            IsLoading = true;

            TopStories = new ObservableCollection<Article>(await App.DataManager.GetTopStories());
            RaiseAllPropertiesChanged();

            IsLoading = false;
            IsBusy = false;
        }

        public async Task ExecutePageTopStoriesCommand(object sender, ScrolledEventArgs e)
        {
            if (IsBusy) return;

            IsBusy = true;

            try
            {
                if (previousScrollPosition < e.ScrollY && Convert.ToInt16(e.ScrollY) != 0)
                {
                    List<Article> stories = await App.DataManager.PerformFeedPaging();
                    foreach (Article story in stories)
                    {
                        TopStories.Add(story);
                    }
                    previousScrollPosition = e.ScrollY;
                }
                else if (Convert.ToInt16(e.ScrollY) == 0)
                {
                    previousScrollPosition = 0;
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
            IsLoading = true;

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
                IsLoading = false;
            }
        }
    }
}
