
using Doancuoiky.Model;
using Doancuoiky.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Doancuoiky.Views
{
    public partial class SearchView : ContentPage
    {
        public SearchView()
        {
            InitializeComponent();
            getProduct();
        }
        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Product> data = await new ProductService().GetProductsAsync();
            SearchBar searchBar = (SearchBar)sender;
            HotelView.ItemsSource = data.Where(x => x.Name.ToLower().Contains(searchBar.Text.ToLower()));
        }
        private async void getProduct()
        {
            List<Product> data = await new ProductService().GetProductsAsync();
            HotelView.ItemsSource = data;
        }

        private async void HotelView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedProduct = e.SelectedItem as Product;
            if (selectedProduct == null)
                return;
            await Navigation.PushModalAsync(new ProductDetailsView(selectedProduct));
            ((ListView)sender).SelectedItem = null;
        }
    }
}