using Doancuoiky.Model;
using Doancuoiky.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Doancuoiky.ViewModels
{
    public class ProductDetailsViewModel : BaseViewModel
    {
		private Product _SelectedProduct;
		public Product SelectedProduct
        {
            set
            {
                _SelectedProduct = value;
                OnPropertyChanged();
            }
            get 
			{ 
				return _SelectedProduct; 
			}
		}

		private int _TotalQuantity;
		public int TotalQuantity
        {
            set
            {
                this._TotalQuantity = value;
                if (this._TotalQuantity < 0) this._TotalQuantity = 0;
                if (this._TotalQuantity > 10) this._TotalQuantity -= 1;
                OnPropertyChanged();
            }
            get 
			{ 
				return _TotalQuantity; 
			}
		}

		public Command IncrementCommand { get; set; }
		public Command DecrementCommand { get; set; }
		public Command AddToCartCommand { get; set; }
		public Command ViewCartCommand { get; set; }
        public Command HomeCommand { get; set; }

		public ProductDetailsViewModel(Product product)
		{
			SelectedProduct = product;
			TotalQuantity = 0;

			IncrementCommand = new Command(() => IncrementOrder());
            DecrementCommand = new Command(() => DecrementOrder());
            AddToCartCommand = new Command(() => AddToCart());
            ViewCartCommand = new Command(() => ViewCartAsync());
            HomeCommand = new Command(() => GotoHomeAsync());
        }

        private async void GotoHomeAsync()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new ProductsView());
        }

        private async void ViewCartAsync()
        {
			await Application.Current.MainPage.Navigation.PushModalAsync(new CartView());
        }

        private void AddToCart()
        {
            var cn = DependencyService.Get<ISQLite>().GetConnection();
			try
			{
				CartItem ci = new CartItem()
				{
					ProductId = SelectedProduct.ProductID,
					ProductName = SelectedProduct.Name,
					Price = SelectedProduct.Price,
					Quantity = TotalQuantity,
					Image=SelectedProduct.ImageUrl
				};
				var item = cn.Table<CartItem>().ToList()
					.FirstOrDefault(c=>c.ProductId== SelectedProduct.ProductID);
				if (item == null) cn.Insert(ci);
				else
				{
					item.Quantity += TotalQuantity;
					cn.Update(item);
				}
				cn.Commit();
				cn.Close();
				Application.Current.MainPage.DisplayAlert("Giỏ hàng", "Sản phẩm bạn lựa đã được thêm vào giỏ hàng",
					"OK");
			}
			catch (Exception ex)
			{
                Application.Current.MainPage.DisplayAlert("Lỗi Giỏ hàng", ex.Message,
                    "OK");
            }
			finally
			{
				cn.Close();
			}
        }

        private void DecrementOrder()
        {
            TotalQuantity--;
        }

        private void IncrementOrder()
        {
			TotalQuantity++;
        }
    }
}
