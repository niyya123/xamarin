using Android.OS;
using Android.Preferences;
using Doancuoiky.Model;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Doancuoiky.Services
{
    public class ProductService
    {
        FirebaseClient client;
        public ProductService() 
        {
            client = new FirebaseClient("https://laptop-shop-26c01-default-rtdb.asia-southeast1.firebasedatabase.app/");
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var products = (await client.Child("Products")
                .OnceAsync<Product>())
                .Select(p => new Product
                {
                    CategoryID = p.Object.CategoryID,
                    Description = p.Object.Description,
                    HomeSelected = p.Object.HomeSelected,
                    ImageUrl = p.Object.ImageUrl,
                    Name= p.Object.Name,
                    Price= p.Object.Price,
                    ProductID = p.Object.ProductID,
                    Rating= p.Object.Rating,
                    RatingDetail= p.Object.RatingDetail,
                }).ToList();
            return products;
        }

        public async Task<ObservableCollection<Product>> GetProductsByCategoryAsync(int categoryID)
        {
            var productsByCategory = new ObservableCollection<Product>();
            var items = (await GetProductsAsync()).Where(p => p.CategoryID == categoryID).ToList();
            foreach (var item in items)
            {
                productsByCategory.Add(item);
            }
            return productsByCategory;
        }

        public async Task<ObservableCollection<Product>> GetProductsByNameAsync(string productname)
        {
            var productsByName = new ObservableCollection<Product>();
            var items = (await GetProductsAsync()).Where(p => p.Name==productname).ToList();
            foreach (var item in items)
            {
                productsByName.Add(item);
            }
            return productsByName;
        }

        public async Task<ObservableCollection<Product>> GetLatestProductsAsync()
        {
            var latestProducts = new ObservableCollection<Product>();
            var items = (await GetProductsAsync()).OrderByDescending(p => p.ProductID).Take(3);
            foreach (var item in items)
            {
                latestProducts.Add(item);
            }
            return latestProducts;
        }
    }
}
