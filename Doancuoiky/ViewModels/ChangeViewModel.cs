using Doancuoiky.Model;
using Doancuoiky.Services;
using Doancuoiky.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Doancuoiky.ViewModels
{

    public class ChangeViewModel : BaseViewModel
    {
        public ObservableCollection<User> Userdetails { get; set; }

        public Command ChangePasswordCommand { get; set; }
        public Command ChangeInfoCommand { get; set; }
        public ChangeViewModel()
        {
            var uname = Preferences.Get("Username", String.Empty);
            Userdetails = new ObservableCollection<User>();
            GetUserDetails(uname);

            ChangePasswordCommand = new Command(async () => await ChangePasswordAsync());
            ChangeInfoCommand = new Command(async () => await ChangeInfoAsync());
        }

        private async Task ChangeInfoAsync()
        {
            var uname = Preferences.Get("Username", String.Empty);
            var nnickname = Preferences.Get("newnickname", String.Empty);
            var naddress = Preferences.Get("newaddress", String.Empty);
            var nphone = Preferences.Get("newphone", String.Empty);
            var result = await new UserService().UpdateUserInfo(uname, nnickname,naddress,nphone);
            if (result == false)
            {
                await Application.Current.MainPage.DisplayAlert("Lỗi", "Đổi thông tin thất bại, thử lại sau", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Thành công", "Đổi thông tin thành công", "OK");
                Preferences.Set("Nickname", nnickname);
                await Application.Current.MainPage.Navigation.PushModalAsync(new UserView());
            }

        }

        private async Task ChangePasswordAsync()
        {
            var uname = Preferences.Get("Username", String.Empty);
            var opassword = Preferences.Get("oldpassword", String.Empty);
            var npassword = Preferences.Get("newpassword", String.Empty);
            var judge = await new UserService().isPasswordRight(opassword);
            if (judge==false) 
            {
                await Application.Current.MainPage.DisplayAlert("Không hợp lệ", "Mật khẩu cũ nhập sai, vui lòng nhập lại", "OK");
                return;
            }
            if (judge == true)
            {
                var result = await new UserService().UpdateUser(uname, npassword);
                if (result == false)
                {
                    await Application.Current.MainPage.DisplayAlert("Lỗi", "Cập nhật mật khẩu không thành công", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Thành công", "Đổi mật khẩu thành công, vui lòng đăng nhập lại", "OK");
                    Preferences.Remove("Username");
                    await Application.Current.MainPage.Navigation.PushModalAsync(new LoginView());
                }
            }

        }

        private async void GetUserDetails(string uname)
        {
            var data = await new UserService().GetUserByName(uname);
            Userdetails.Clear();
            foreach (var item in data)
            {
                Userdetails.Add(item);
            }
        }
    }
}
