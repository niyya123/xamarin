using Doancuoiky.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Doancuoiky.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryOrderView : ContentPage
    {
        public HistoryOrderView()
        {
            InitializeComponent();
            //LabelName.Text = "Chào mừng" + Preferences.Get("Username", "Guest") + ",";
        }

        private async void ImageButton_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void ListViewCartItems_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedOrder = e.SelectedItem as Order;
            if (selectedOrder == null) { return; }
            Preferences.Set("orderid", selectedOrder.OrderId);
            await Navigation.PushModalAsync(new HistoryOrderDetailsView());
            ListViewCartItems.SelectedItem = null;
        }
    }
}