using Doancuoiky.Helpers;
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
	public partial class ProductDetailsView : ContentPage
	{
		ProductDetailsViewModel pvm;
		public ProductDetailsView(Product product)
		{
			InitializeComponent();
			pvm = new ProductDetailsViewModel(product);
			this.BindingContext = pvm;
        }

        async void ImageButton_Clicked(object sender, EventArgs e)
        {
			await Navigation.PopModalAsync();
        }

    }
}