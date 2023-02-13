using Doancuoiky.Model;
using Doancuoiky.Views;
using Doancuoiky.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Doancuoiky.Services;

namespace Doancuoiky.ViewModels
{
    public class CartViewModel : BaseViewModel
    {
        public ObservableCollection<UserCartItem> CartItems { get; set; }

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

        private decimal _Quantity;
        public decimal Quantity
        {
            set
            {
                _Quantity = value;
                OnPropertyChanged();
            }
            get
            {
                return _Quantity;
            }
        }

        public Command PlaceOrdersCommand { get; set; }

        public CartViewModel() 
        {
            CartItems= new ObservableCollection<UserCartItem>();
            LoadItems();
            PlaceOrdersCommand = new Command(async () => await PlaceOrdersAsync());
        }

        private async Task PlaceOrdersAsync()
        {
            var id = await new OrderService().PlaceOrderAsync() as string;
            RemoveItemsFromCart();
            await Application.Current.MainPage.Navigation.PushModalAsync(new OrdersView(id));
        }

        public void Notify()
        {
            OnPropertyChanged(nameof(TotalCost));
            OnPropertyChanged(nameof(Quantity));
        }

        private void RemoveItemsFromCart()
        {
            var cis = new CartItemService();
            cis.RemoveItemsFromCart();
        }

        private void LoadItems()
        {
            var cn = DependencyService.Get<ISQLite>().GetConnection();
            var items = cn.Table<CartItem>().ToList();
            CartItems.Clear();
            foreach (var item in items) 
            {
                CartItems.Add(new UserCartItem()
                {
                    CartItemId = item.CartItemId,
                    ProductId= item.ProductId,
                    ProductName= item.ProductName,
                    Price= item.Price,
                    Quantity= item.Quantity,
                    Cost = item.Price * item.Quantity,
                    Image= item.Image
                });

                TotalCost += (item.Quantity * item.Price);
            }
        }
    }
}
