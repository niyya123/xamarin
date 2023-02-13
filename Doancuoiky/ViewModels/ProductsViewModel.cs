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
    public class ProductsViewModel : BaseViewModel
    {
        private string _Nickname;
        public string Nickname
        {   
            set 
            {
                _Nickname = value;
                OnPropertyChanged();
            }
            get
            {
                return _Nickname;
            }
        }

        private int _UserCartItemsCount;
        public int UserCartItemsCount
        {
            set 
            { 
                _UserCartItemsCount = value; 
                OnPropertyChanged();
            }
            get 
            { 
                return _UserCartItemsCount; 
            }
        }

        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Product> LatestProducts { get; set; }

        public Command ViewCartCommand { get; set; }
        public Command LogoutCommand { get; set; }
        public Command ViewHistoryOrderCommand { get; set; }

        public ProductsViewModel()
        {
            var nname = Preferences.Get("Nickname", String.Empty);
            if (String.IsNullOrEmpty(nname))
                Nickname = "Vô danh";
            else
                Nickname = nname;

            UserCartItemsCount = new CartItemService().GetUserCartCount();

            Categories= new ObservableCollection<Category>();
            LatestProducts = new ObservableCollection<Product>();

            ViewCartCommand = new Command(async () => await ViewCartAsync());
            LogoutCommand = new Command(async () => await LogoutAsync());
            ViewHistoryOrderCommand = new Command(async () => await ViewHistoryOrderAsync());

            GetCategories();
            GetLatestProducts();
        }

        public void Notify()
        {
            OnPropertyChanged(nameof(UserCartItemsCount));
        }

        private async Task ViewHistoryOrderAsync()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new HistoryOrderView());
        }

        private async Task LogoutAsync()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new LogoutView());
        }

        private async Task ViewCartAsync()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new CartView());
        }

        private async void GetLatestProducts()
        {
            var data = await new ProductService().GetLatestProductsAsync();
            LatestProducts.Clear();
            foreach (var item in data)
            {
                LatestProducts.Add(item);
            }
        }

        private async void GetCategories()
        {
            var data = await new CategoryDataService().GetCategoriesAsync();
            Categories.Clear();
            foreach (var item in data)
            {
                Categories.Add(item);
            }
        }
    }
}
