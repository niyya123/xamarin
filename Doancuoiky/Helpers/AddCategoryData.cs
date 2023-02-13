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
    public class AddCategoryData
    {
        public List<Category> Categories { get; set; }

        FirebaseClient client;


        public AddCategoryData() 
        {
            client = new FirebaseClient("https://laptop-shop-26c01-default-rtdb.asia-southeast1.firebasedatabase.app/");
            Categories = new List<Category>()
            {
                new Category
                {
                    CategoryID = 1,
                    CategoryName= "Laptop Gaming",
                    CategoryPoster = "LTGM",
                    ImageUrl = "gaming.png"
                },
                new Category
                {
                    CategoryID = 2,
                    CategoryName= "Laptop Business",
                    CategoryPoster = "LTBS",
                    ImageUrl = "business.png"
                },
                new Category
                {
                    CategoryID = 3,
                    CategoryName= "Laptop Cảm ứng",
                    CategoryPoster = "LTWS",
                    ImageUrl = "camung.png"
                },
            };
        }

        public async Task AddCategoriesAsync()
        {
            try
            {
                foreach(var category in Categories)
                {
                    await client.Child("Categories").PostAsync(new Category()
                    {
                        CategoryID = category.CategoryID,
                        CategoryName = category.CategoryName,
                        CategoryPoster = category.CategoryPoster,
                        ImageUrl = category.ImageUrl
                    });
                }
            }
            catch(Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Lỗi", ex.Message, "OK"); 
            }
        }
    }
}
