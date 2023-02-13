using Doancuoiky.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Doancuoiky
{
    public partial class LogoView : ContentPage
    {
        public LogoView()
        {
            InitializeComponent();
            string uname = Preferences.Get("Username", String.Empty);
            if (String.IsNullOrEmpty(uname))
            {
                GoToLogin();
            }
            else
            {
                GoToProduct();
            }
        }

        private async void GoToProduct()
        {
            await Task.Delay(5000);
            await Navigation.PushModalAsync(new ProductsView());
        }

        private async void GoToLogin()
        {
            await Task.Delay(5000);
            await Navigation.PushModalAsync(new LoginView());
        }
    }
}
