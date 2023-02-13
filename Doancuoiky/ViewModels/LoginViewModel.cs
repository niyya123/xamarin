using Doancuoiky.Helpers;
using Doancuoiky.Model;
using Doancuoiky.Services;
using Doancuoiky.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Doancuoiky.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _Username;
        public string Username
        {
            set
            {
                this._Username=value;
                OnPropertyChanged();
            }
            get
            {
                return this._Username;
            }
        }

        private string _Password;
        public string Password
        {
            set
            {
                this._Password = value;
                OnPropertyChanged();
            }
            get
            {
                return this._Password;
            }
        }

        private string _Nickname;
        public string Nickname
        {
            set
            {
                this._Nickname = value;
                OnPropertyChanged();
            }
            get
            {
                return this._Nickname;
            }
        }

        private string _Address;
        public string Address
        {
            set
            {
                this._Address = value;
                OnPropertyChanged();
            }
            get
            {
                return this._Address;
            }
        }

        private string _Sdt;
        public string Sdt
        {
            set
            {
                this._Sdt = value;
                OnPropertyChanged();
            }
            get
            {
                return this._Sdt;
            }
        }

        private bool _IsBusy;
        public bool IsBusy
        {
            set
            {
                this._IsBusy = value;
                OnPropertyChanged();
            }
            get
            {
                return this._IsBusy;
            }
        }

        private bool _Result;
        public bool Result
        {
            set
            {
                this._Result = value;
                OnPropertyChanged();
            }
            get
            {
                return this._Result;
            }
        }

        private User NicknameResult;

        public Command LoginCommand { get; set; }
        public Command RegisterCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await LoginCommandAsync());
            RegisterCommand = new Command(async () => await RegisterCommandAsync());
        }

        private async Task RegisterCommandAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                var userService = new UserService();
                Result = await userService.RegisterUser(Username, Password, Nickname, Address, Sdt);
                if (Result)
                {
                    await Application.Current.MainPage.DisplayAlert("Thành công", "Người dùng đã đăng ký thành công", "OK");
                    await userService.LoginUser(Username, Password);
                    Preferences.Set("Nickname", Nickname);
                    Preferences.Set("Username", Username);
                    await Application.Current.MainPage.Navigation.PushModalAsync(new ProductsView());
                }    
                else
                    await Application.Current.MainPage.DisplayAlert("Lỗi", "Người dùng đã đăng ký với thông tin này", "OK");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Lỗi", ex.Message, "Đồng ý");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task LoginCommandAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                var userService = new UserService();
                Result = await userService.LoginUser(Username, Password);
                NicknameResult = await userService.GetUserInfoAsync(Username);
                Preferences.Set("Nickname", NicknameResult.Nickname);
                if (Result)
                {
                    Preferences.Set("Username", Username);
                    await Application.Current.MainPage.DisplayAlert("Thành công", "Đăng nhập thành công", "Cùng xem sản phẩm nhé!");
                    await Application.Current.MainPage.Navigation.PushModalAsync(new ProductsView());
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Lỗi", "Thông tin đăng nhập sai", "Đồng ý"); 
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Lỗi", ex.Message, "Đồng ý");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
