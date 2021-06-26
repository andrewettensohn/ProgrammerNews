using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Views;
using AndroidX.AppCompat.App;

namespace ProgrammerNews.Droid
{
    [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true, Label = "Hacker News")]
    public class SplashActivity : AppCompatActivity
    {

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState, persistentState);
        }

        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }

        // Prevent the back button from canceling the startup process
        public override void OnBackPressed() { }

    }
}