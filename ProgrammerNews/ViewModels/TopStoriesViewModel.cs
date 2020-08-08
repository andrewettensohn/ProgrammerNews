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
        public Command LoadStoriesCommand { get; set; }
        public Command Paging { get; set; }
        public TopStoriesViewModel()
        {
            TopStories = new ObservableCollection<Article>();
            LoadStoriesCommand = new Command(async () => await ExecuteLoadStoriesCommand());
            Paging = new Command(async () => await ExecutePaging());
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
