using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProgrammerNews.Views
{
    [DesignTimeVisible(false)]
    public partial class MainPage : TabbedPage
    {
        private Page myCurrentPage;
        public MainPage()
        {
            InitializeComponent();
            //myCurrentPage = CurrentPage; 
        }

        protected override void OnCurrentPageChanged()
        {
            //if (myCurrentPage.BindingContext is IShowable)
            //{
            //    (myCurrentPage.BindingContext as IShowable).OnHide();
            //}
            //base.OnCurrentPageChanged();
            //myCurrentPage = CurrentPage;

            //if (myCurrentPage.BindingContext is IShowable)
            //{
            //    (myCurrentPage.BindingContext as IShowable).OnShow();
            //}

            //if (CurrentPage != null && CurrentPage.Title == "Saved")
            //{
            //    MessagingCenter.Send(this, "Hi");
            //}
        }
    }
}