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
using ProgrammerNews.Views;

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

            //MessagingCenter.Subscribe<MainPage, string>(this, "Hi", async (sender, arg) =>
            //{
            //    //await DisplayAlert("Message received", "arg=" + arg, "OK");
            //});
        }

        async Task ExecuteDeleteArticleCommand(int id)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Article article = SavedStories.FirstOrDefault(x => x.Id == id);
                await App.DataManager.DeleteArticleAsync(article);
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
