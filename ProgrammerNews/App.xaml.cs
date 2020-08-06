using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProgrammerNews.Services;
using ProgrammerNews.Views;

namespace ProgrammerNews
{
    public partial class App : Application
    {
        public static DataManager DataManager {get; private set;}
        public App()
        {
            InitializeComponent();
            DataManager = new DataManager();
            //DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
