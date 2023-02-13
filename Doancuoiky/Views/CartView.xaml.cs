using Doancuoiky.Model;
using Doancuoiky.Services;
using Doancuoiky.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Doancuoiky.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartView : ContentPage
    {
        public CartView()
        {
            InitializeComponent();
            VM = (CartViewModel)BindingContext;
            LabelName.Text= "Chào mừng " + Preferences.Get("Username","Guest") + ",";
        }

        public CartViewModel VM { get; }

        protected override void OnAppearing()
        {
            VM.Notify();
            base.OnAppearing();
        }

        async private void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new ProductsView());
        }

        async void SwipeItem_Invoked(object sender, EventArgs e)
        {
            
            var item1 = (SwipeItem)sender;
            var cartitem = (Model.UserCartItem)item1.CommandParameter;
            var result = await DisplayAlert("Xóa", "Bạn muốn xóa cái này chứ", "Có", "Không");
            if (result)
            {
                var cis = new CartItemService();
                cis.RemoveSelectedItemsFromCart(cartitem.CartItemId);
                var cn = DependencyService.Get<ISQLite>().GetConnection();
                var items = cn.Table<CartItem>().ToList();
                ObservableCollection<UserCartItem> CartItems = new ObservableCollection<UserCartItem>();
                decimal TotalCost = 0;
                foreach (var item in items)
                {
                    CartItems.Add(new UserCartItem()
                    {
                        CartItemId = item.CartItemId,
                        ProductId = item.ProductId,
                        ProductName = item.ProductName,
                        Price = item.Price,
                        Quantity = item.Quantity,
                        Cost = item.Price * item.Quantity,
                        Image = item.Image
                    });

                    TotalCost += (item.Quantity * item.Price);
                }
                ListViewCartItems.ItemsSource = CartItems;
                Gia.Text = "Total cost:"+string.Format("{0:N0}",
                TotalCost)+ " VNĐ";
            }
        }

        async void SwipeItem_Invoked_1(object sender, EventArgs e)
        {
            var cn = DependencyService.Get<ISQLite>().GetConnection();
            var item1 = (SwipeItem)sender;
            var cartitem = (Model.UserCartItem)item1.CommandParameter;
            var result = await DisplayPromptAsync("Đổi số lượng", "Số lượng mới: ", "Yes", "No");
            if (int.TryParse(result, out int value))
            {
                
                var item = cn.Table<CartItem>().ToList()
                    .FirstOrDefault(c => c.ProductId == cartitem.ProductId);
                if (item == null) return;
                else
                {
                    item.Quantity =value;
                    cn.Update(item);
                }
                Console.WriteLine(item.ProductName);
                Console.WriteLine(item.Quantity);
                cn.Commit();
                
            }
            var items = cn.Table<CartItem>().ToList();
            cn.Close();
            ObservableCollection<UserCartItem> CartItems = new ObservableCollection<UserCartItem>();
            decimal TotalCost = 0;
            foreach (var item in items)
            {
                CartItems.Add(new UserCartItem()
                {
                    CartItemId = item.CartItemId,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    Cost = item.Price * item.Quantity,
                    Image = item.Image
                });

                TotalCost += (item.Quantity * item.Price);
            }
            ListViewCartItems.ItemsSource = CartItems;
            Gia.Text = "Total cost:" + string.Format("{0:N0}",
            TotalCost) + " VNĐ";
        }
    }
}