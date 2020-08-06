using ProgrammerNews.Models;
using ProgrammerNews.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProgrammerNews.ViewModels
{
    public class TopStoriesViewModel : BaseViewModel
    {
        public ObservableCollection<Article> TopStories { get; set; }
        //public List<Article> TopStories { get; set; }
        public Command LoadStoriesCommand { get; set; }
        public TopStoriesViewModel()
        {
            TopStories = new ObservableCollection<Article>();
            //TopStories = new List<Article>();
            LoadStoriesCommand = new Command(async () => await ExecuteLoadStoriesCommand());
        }

        //async Task ExecuteLoadStoriesCommand()
        //{
        //    TopStories = await App.DataManager.GetTopStories();
        //}
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
