using Android.Content.Res;
using Doancuoiky.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("Lobster-Regular.ttf", Alias = "Font1")]
namespace Doancuoiky
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new LogoView();
            //MainPage = new LoginView();
            //MainPage = new NavigationPage(new SettingsPage());
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
