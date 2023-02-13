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
    public class CategoryViewModel : BaseViewModel
    {
		private Category _SelectedCategory;

		public Category SelectedCategory
        {
			set 
			{ 
				_SelectedCategory = value;
				OnPropertyChanged();
			}
            get 
			{ 
				return _SelectedCategory; 
			}				
        }

        public ObservableCollection<Product> ProductsByCategory { get; set; }
		private int _TotalProducts;
		public int TotalProducts
		{
            set
            {
                _TotalProducts = value;
                OnPropertyChanged();
            }
            get
			{ 
				return _TotalProducts; 
			} 
		}

		public CategoryViewModel(Category category) 
		{
			SelectedCategory = category;
			ProductsByCategory = new ObservableCollection<Product>();
			GetProducts(category.CategoryID);
		}

        private async void GetProducts(int categoryID) 
		{
			var data = await new ProductService().GetProductsByCategoryAsync(categoryID);
			ProductsByCategory.Clear();
			foreach (var item in data)	
			{
				ProductsByCategory.Add(item);
			}
			TotalProducts= ProductsByCategory.Count;
		}
	}
}
