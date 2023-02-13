using Doancuoiky.Model;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Doancuoiky.Helpers
{
    public class AddProductData
    {
        FirebaseClient client;
        public List<Product> Products { get; set; }
        public AddProductData() 
        {
            client = new FirebaseClient("https://laptop-shop-26c01-default-rtdb.asia-southeast1.firebasedatabase.app/");
            Products = new List<Product>()
            {
                new Product
                {
                    ProductID = 1,
                    CategoryID = 1,
                    ImageUrl = "asustuf",
                    Name= "Asus Tuf A15",
                    Description = "Laptop Gaming Asus phân khúc tầm trung A15",
                    Rating = "3.9",
                    RatingDetail = "29",
                    HomeSelected = "Heart",
                    Price = 21000000
                },
                new Product
                {
                    ProductID = 2,
                    CategoryID = 1,
                    ImageUrl = "lenovolegion",
                    Name= "Lenovo Legion 5",
                    Description = "Laptop Gaming Lenovo phân khúc tầm cận cao cấp Legion 5",
                    Rating = "4.5",
                    RatingDetail = "159",
                    HomeSelected = "Heart",
                    Price = 27000000
                },
                new Product
                {
                    ProductID = 3,
                    CategoryID = 2,
                    ImageUrl = "asuszenbook",
                    Name= "Asus Zenbook 14",
                    Description = "Laptop Business Asus phân khúc tầm cận cao cấp Zenbook 14",
                    Rating = "4.3",
                    RatingDetail = "10",
                    HomeSelected = "Heart",
                    Price = 20000000
                },
                new Product
                {
                    ProductID = 4,
                    CategoryID = 2,
                    ImageUrl = "lenovoideapad",
                    Name= "Lenovo IdeaPad 3",
                    Description = "Laptop Business Lenovo phân khúc entry IdeaPad 3",
                    Rating = "4.8",
                    RatingDetail = "50",
                    HomeSelected = "Heart",
                    Price = 13000000
                },
                new Product
                {
                    ProductID = 5,
                    CategoryID = 3,
                    ImageUrl = "hppavilion",
                    Name= "HP Pavilion X360",
                    Description = "Laptop Cảm ứng HP phân khúc tầm trung X360",
                    Rating = "4.4",
                    RatingDetail = "5",
                    HomeSelected = "Heart",
                    Price = 16000000
                },
                new Product
                {
                    ProductID = 6,
                    CategoryID = 3,
                    ImageUrl = "hpspectre",
                    Name= "HP Spectre X360",
                    Description = "Laptop Cảm ứng HP phân khúc tầm cao X360",
                    Rating = "4.9",
                    RatingDetail = "2",
                    HomeSelected = "Heart",
                    Price = 50000000
                },
            };
        }

        public async Task AddProductAsync()
        {
            try
            {
                foreach (var item in Products)
                {
                    await client.Child("Products").PostAsync(new Product()
                    {
                        ProductID = item.ProductID,
                        CategoryID = item.CategoryID,
                        ImageUrl = item.ImageUrl,
                        Name = item.Name,
                        Description = item.Description,
                        Rating = item.Rating,
                        HomeSelected = item.HomeSelected,
                        Price = item.Price,
                        RatingDetail = item.RatingDetail
                    });
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Lỗi", ex.Message, "OK");
            }
        }
    }
}
