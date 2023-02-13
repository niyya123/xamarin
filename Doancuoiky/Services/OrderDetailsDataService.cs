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
    public class OrderDetailsDataService
    {
        FirebaseClient client;
        public OrderDetailsDataService()
        {
            client = new FirebaseClient("https://laptop-shop-26c01-default-rtdb.asia-southeast1.firebasedatabase.app/");
        }

        public async Task<List<OrderDetails>> GetOrderDetailsAsync()
        {
            var orderDetails = (await client.Child("OrderDetails")
                .OnceAsync<OrderDetails>())
                .Select(od => new OrderDetails
                {
                    OrderDetailId = od.Object.OrderDetailId,
                    OrderId= od.Object.OrderId,
                    ProductId= od.Object.ProductId,
                    ProductName= od.Object.ProductName,
                    Quantity= od.Object.Quantity,
                    Price= od.Object.Price,
                }).ToList();
            return orderDetails;
        }

        public async Task<ObservableCollection<OrderDetails>> GetOrderDetailsByOrderIDAsync(string orderID)
        {
            var orderDetailsByOderID =  new ObservableCollection<OrderDetails>();
            var items = (await GetOrderDetailsAsync()).Where(od=>od.OrderId==orderID).ToList();
            foreach (var item in items) 
            {
                orderDetailsByOderID.Add(item);
            }
            return orderDetailsByOderID;
        }
    }
}
