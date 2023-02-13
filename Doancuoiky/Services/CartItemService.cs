using Doancuoiky.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Doancuoiky.Services
{
    public class CartItemService
    {

        public int GetUserCartCount()
        {
            var cn = DependencyService.Get<ISQLite>().GetConnection();
            var count = cn.Table<CartItem>().Count();
            cn.Close();
            return count;
        }

        public void RemoveItemsFromCart()
        {
            var cn = DependencyService.Get<ISQLite>().GetConnection();
            cn.DeleteAll<CartItem>();
            cn.Commit();
            cn.Close();
        }
        public void RemoveSelectedItemsFromCart(int id)
        {
            var cn = DependencyService.Get<ISQLite>().GetConnection();
            cn.Delete<CartItem>(id);
            cn.Commit();
            cn.Close();
        }
        public void UpdateSelectedItemsFromCart(CartItem a)
        {
            var cn = DependencyService.Get<ISQLite>().GetConnection();
            cn.Update(a);
            cn.Commit();
            cn.Close();
        }
    }
}
