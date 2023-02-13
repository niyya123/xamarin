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
    public partial class ChangeInfoView : ContentPage
    {
        public ChangeInfoView()
        {
            InitializeComponent();
        }

        private void newnickname_TextChanged(object sender, TextChangedEventArgs e)
        {
            Preferences.Set("newnickname", newnickname.Text);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void newaddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            Preferences.Set("newaddress", newaddress.Text);
        }

        private void newphone_TextChanged(object sender, TextChangedEventArgs e)
        {
            Preferences.Set("newphone", newphone.Text);
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            newaddress.Text = string.Empty;
            newphone.Text = string.Empty;
            newnickname.Text= string.Empty;
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            if ((newaddress.Text) == string.Empty || (newphone.Text) == string.Empty || (newnickname.Text) == string.Empty) 
            {
                await Application.Current.MainPage.DisplayAlert("Lỗi", "Vui lòng điền đầy đủ thông tin", "OK");
            }
        }
    }
}