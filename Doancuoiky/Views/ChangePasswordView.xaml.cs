using Android.Preferences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Doancuoiky.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePasswordView : ContentPage
    {
        public ChangePasswordView()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            oldpassword.Text = string.Empty;
            newpassword.Text = string.Empty;
        }

        private void oldpassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            Preferences.Set("oldpassword",oldpassword.Text);
        }

        private void newpassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            Preferences.Set("newpassword", newpassword.Text);
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}