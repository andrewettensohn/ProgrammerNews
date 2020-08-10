using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProgrammerNews.Data;
using ProgrammerNews.Views;
using System.IO;

namespace ProgrammerNews
{
    public partial class App : Application
    {
        public static DataManager DataManager {get; private set;}
        public App()
        {
            InitializeComponent();
            DataManager = new DataManager();
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
