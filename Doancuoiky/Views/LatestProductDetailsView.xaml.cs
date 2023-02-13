using Doancuoiky.Model;
using Doancuoiky.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Doancuoiky.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LatestProductDetailsView : ContentPage
    {
        ProductDetailsViewModel pvm;
        public LatestProductDetailsView(Product product)
        {
            InitializeComponent();
            pvm = new ProductDetailsViewModel(product);
            this.BindingContext = pvm;
        }

        async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new ProductsView());
        }
    }
}