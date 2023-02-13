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
using static Android.Provider.ContactsContract.CommonDataKinds;

namespace Doancuoiky.ViewModels
{
    
    public class UserDetailsViewModel : BaseViewModel
    {
        public ObservableCollection<User> Userdetails { get; set; }

        public UserDetailsViewModel()
        {
            var uname = Preferences.Get("Username", String.Empty);
            Userdetails= new ObservableCollection<User>();
            GetUserDetails(uname);
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

        public void Notify()
        {
            OnPropertyChanged(nameof(Nickname));
        }
    }
}
