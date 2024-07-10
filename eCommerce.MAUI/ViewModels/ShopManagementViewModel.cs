using CRM.Library.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CRM.Models;
using CRM.Library.Models;

namespace eCommerce.MAUI.ViewModels
{
    public class ShopManagementViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler ?PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int Id { get; set; }

        public List<ShopViewModel> Items
        {
            get
            {
                return ShopServiceProxy.Current.Carts.FirstOrDefault(c => c.Id == Id).Contents.Select(c => new ShopViewModel(c)).ToList()
                    ?? new List<ShopViewModel>();
            }
        }
        public ShopViewModel? SelectedItem { get; set; }

        public ShopManagementViewModel()
        {
        }

        public ShopManagementViewModel(int id)
        {
            Id = id;
        }

        public void RefreshItems()
        {
            NotifyPropertyChanged("Id");
            NotifyPropertyChanged("Items");
            NotifyPropertyChanged("SubTotal");
            NotifyPropertyChanged("Tax");
            NotifyPropertyChanged("Total");

        }

        public decimal SubTotal
        {
            get
            {
                return ShopServiceProxy.Current.TotalPrice(Id);
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

            var itemToRemove = ShopServiceProxy.Current.Carts[Id].Contents.FirstOrDefault(c => c.Id == item?.Id);
            if (itemToRemove != null)
            {
                ShopServiceProxy.Current.RemoveFromCart(itemToRemove, Id);
                RefreshItems();
            }
        }

        public void AddItem(InventoryViewModel item)
        {
            if (item == null)
            {
                return;
            }
            if (item.Item == null)
            {
                return;
            }

            ShopServiceProxy.Current.AddToCart(item.Item, Id);
            RefreshItems();
        }

    }
}