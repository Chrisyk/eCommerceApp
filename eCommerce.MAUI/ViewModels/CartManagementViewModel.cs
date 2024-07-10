using CRM.Library.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CRM.Models;

namespace eCommerce.MAUI.ViewModels
{
    public class CartManagementViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public List<CartViewModel> Carts
        {
            get
            {
                return ShopServiceProxy.Current?.Carts?.Select(c => new CartViewModel(c)).ToList()
                    ?? new List<CartViewModel>();
            }
        }

        public int SelectedCartId { get; set; }

        public CartManagementViewModel()
        {
        }

        public void RefreshItems()
        {
            NotifyPropertyChanged("Carts");
            NotifyPropertyChanged("CartIds");
            NotifyPropertyChanged("SelectedCartId");
            NotifyPropertyChanged("SubTotal");
            NotifyPropertyChanged("Tax");
            NotifyPropertyChanged("Total");

        }



        public void RemoveCart(CartViewModel cart)
        {
            if (cart == null)
            {
                return;
            }

            var cartToRemove = ShopServiceProxy.Current.Carts.FirstOrDefault(c => c.Id == cart?.Cart?.Id);
            if (cartToRemove != null)
            {
                ShopServiceProxy.Current.RemoveCart(cartToRemove);
                RefreshItems();
            }
        }

        public void AddCart()
        {
            ShopServiceProxy.Current.AddCart();
            RefreshItems();
        }



    }
}
