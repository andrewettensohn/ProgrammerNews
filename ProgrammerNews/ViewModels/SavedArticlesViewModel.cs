using ProgrammerNews.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using System.Windows.Input;
using System.Collections.Generic;
using Xamarin.Essentials;

namespace ProgrammerNews.ViewModels
{
    public class SavedArticlesViewModel : BaseViewModel
    {
        private ObservableCollection<Article> _savedArticles;
        public ObservableCollection<Article> SavedArticles
        {
            get => _savedArticles;
            set
            {
                SetValue(ref _savedArticles, value);
            }
        }

        public ICommand LoadSavedArticlesCmd => _loadSavedArticlesCmd;
        private RelayCommand _loadSavedArticlesCmd { get; set; }

        public ICommand DeleteArticleCmd => _deleteArticlesCmd;
        private RelayCommand<int> _deleteArticlesCmd { get; set; }

        public ICommand ArticleLinkSelectedCmd => _articleLinkSelectedCmd;
        private RelayCommand<string> _articleLinkSelectedCmd { get; set; }

        public SavedArticlesViewModel()
        {
            _savedArticles = new ObservableCollection<Article>();
            _loadSavedArticlesCmd = new RelayCommand(async () => await ExecuteLoadStoriesCommand());
            _deleteArticlesCmd = new RelayCommand<int>(async (x) => await ExecuteDeleteArticleCommand(x));
            _articleLinkSelectedCmd = new RelayCommand<string>(async (x) => await ExecuteArticleLinkSelectedCommand(x));
        }

        public async Task LoadViewModelAsync()
        {
            IsBusy = true;
            SavedArticles = new ObservableCollection<Article>(await App.DataManager.GetSavedArticles());
            RaiseAllPropertiesChanged();
            IsBusy = false;
        }

        private async Task ExecuteArticleLinkSelectedCommand(string url)
        {
            await Browser.OpenAsync(url, BrowserLaunchMode.SystemPreferred);
        }

        private async Task ExecuteDeleteArticleCommand(int id)
        {
            Article article = SavedArticles.FirstOrDefault(x => x.Id == id);
            await App.DataManager.DeleteArticleAsync(article);
            SavedArticles.Remove(article);
        }

        private async Task ExecuteLoadStoriesCommand()
        {
            if (IsBusy) return;

            IsBusy = true;

            try
            {
                SavedArticles.Clear();
                List<Article> stories = await App.DataManager.GetSavedArticles();
                foreach (Article story in stories)
                {
                    SavedArticles.Add(story);
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
