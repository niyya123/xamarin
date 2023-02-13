using Doancuoiky.Model;
using Doancuoiky.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms.PancakeView;

namespace Doancuoiky.ViewModels
{
    public class HistoryOrderDetailsViewModel : BaseViewModel
    {
        public ObservableCollection<OrderDetails> OrderDetailss { get; set; }
        private decimal _TotalCost;
        public decimal TotalCost
        {
            set
            {
                _TotalCost = value;
                OnPropertyChanged();
            }
            get
            {
                return _TotalCost;
            }
        }   
        public HistoryOrderDetailsViewModel() 
        {
            OrderDetailss= new ObservableCollection<OrderDetails>();
            TotalCost = 0;
            LoadOrderDetails();
        }

        private async void LoadOrderDetails()
        {
            var orderID = Preferences.Get("orderid", String.Empty);
            var data = await new OrderDetailsDataService().GetOrderDetailsByOrderIDAsync(orderID);
            OrderDetailss.Clear();
            foreach (var order in data)
            {
                var tests = await new ProductService().GetProductsByNameAsync(order.ProductName);
                foreach (var test in tests)
                {
                    order.Image = test.ImageUrl;
                }
                OrderDetailss.Add(order);
                TotalCost += (order.Price * order.Quantity);
            }
        }
    }
}
