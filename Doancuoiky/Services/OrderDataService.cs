using Doancuoiky.Model;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doancuoiky.Services
{
    public class OrderDataService
    {
        FirebaseClient client;
        public OrderDataService()
        {
            client = new FirebaseClient("https://laptop-shop-26c01-default-rtdb.asia-southeast1.firebasedatabase.app/");
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            var orders = (await client.Child("Orders")
                .OnceAsync<Order>())
                .Select(o => new Order
                {
                    OrderId = o.Object.OrderId,
                    TotalCost= o.Object.TotalCost,
                    Username= o.Object.Username,
                    Address= o.Object.Address,
                    Phone= o.Object.Phone
                }).ToList();
            return orders;
        }

        public async Task<ObservableCollection<Order>> GetOrdersByUsernameAsync(string username)
        {
            var ordersByUsername = new ObservableCollection<Order>();
            var items = (await GetOrdersAsync()).Where(o => o.Username == username).ToList();
            foreach (var item in items) 
            {
                ordersByUsername.Add(item);
            }
            return ordersByUsername;
        }
    }
}
