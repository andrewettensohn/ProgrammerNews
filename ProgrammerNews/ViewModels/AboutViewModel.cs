using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProgrammerNews.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://github.com/andrewettensohn/ProgrammerNews"));
            DeleteAllSavedArticlesCommand = new Command(async () => await App.DataManager.DeleteAllArticlesAsync());
        }

        public ICommand OpenWebCommand { get; }
        public ICommand DeleteAllSavedArticlesCommand { get; }
    }
}