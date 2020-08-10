using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProgrammerNews.Views
{
    [DesignTimeVisible(false)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            CurrentPage = null;
            
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();


            //if (CurrentPage != null && CurrentPage.Title == "Saved")
            //{
            //    //SavedArticles page = CurrentPage as SavedArticles;
            //    //page.ViewModel.LoadStoriesCommand.Execute(null);
            //    CurrentPage 
            //}
        }
    }
}