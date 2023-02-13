using Doancuoiky.Model;
using Doancuoiky.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Doancuoiky.ViewModels
{
    public class HistoryOrderViewModel : BaseViewModel
    {
        public ObservableCollection<Order> Orders { get; set; }

        public User user { get; set; }
        public HistoryOrderViewModel() 
        {
            var uname = Preferences.Get("Username", String.Empty);
            Orders = new ObservableCollection<Order>();
            user = new User();
            LoadOders(uname);
        }

        private async void LoadOders(string username)
        {
            var data = await new OrderDataService().GetOrdersByUsernameAsync(username);
            Orders.Clear();
            foreach (var order in data) 
            {
                Orders.Add(order);
            }
        }
    }
}
