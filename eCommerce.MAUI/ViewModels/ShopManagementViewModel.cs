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
    public class ShopManagementViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public List<ShopViewModel> Items
        {
            get
            {
                return ShopServiceProxy.Current?.Items?.Select(c => new ShopViewModel(c)).ToList()
                    ?? new List<ShopViewModel>();
            }
        }
        public ShopViewModel SelectedItem { get; set; }

        public ShopManagementViewModel()
        {
        }

        public void RefreshItems() 
        {
            NotifyPropertyChanged("Items");
            NotifyPropertyChanged("SubTotal");
            NotifyPropertyChanged("Tax");
            NotifyPropertyChanged("Total");

        }

        public decimal SubTotal
        {
            get
            {
                return ShopServiceProxy.Current.TotalPrice();
            }
        }

        public decimal Tax
        {
            get
            {
                return ShopServiceProxy.Current.Tax;
            }
            set
            {
               ShopServiceProxy.Current.Tax = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("Total");
            }
        }

        public decimal Total
        {

           get
            {
                return SubTotal + (SubTotal * Tax);
            }
        }

        public void RemoveItem(ShopViewModel item)
        {
            if (item == null)
            {
                return;
            }

            var itemToRemove = ShopServiceProxy.Current.Items.FirstOrDefault(c => c.Id == item?.Item?.Id);
            if (itemToRemove != null)
            {
                ShopServiceProxy.Current.RemoveFromCart(itemToRemove);
                RefreshItems();
            }
        }

        public void AddItem(InventoryViewModel item)
        {
            if (item == null)
            {
                return;
            }

            ShopServiceProxy.Current.AddToCart(item.Item);
            RefreshItems();
        }

    }
}
