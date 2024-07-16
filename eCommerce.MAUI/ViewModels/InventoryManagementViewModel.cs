using eCommerce.Library.Models;
using eCommerce.Library.Service;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace eCommerce.MAUI.ViewModels
{
    public class InventoryManagementViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<InventoryViewModel> Items
        {
            get
            {
                return InventoryServiceProxy.Current?.Items?.Select(c => new InventoryViewModel(c)).ToList()
                    ?? new List<InventoryViewModel>();
            }
        }

        public InventoryViewModel SelectedItem { get; set; }

        public InventoryManagementViewModel()
        {
        }

        public void RefreshItems()
        {
            NotifyPropertyChanged("Items");
        }

        public void RemoveItem(InventoryViewModel item)
        {
            if (item == null)
            {
                return;
            }

            var itemToRemove = InventoryServiceProxy.Current.Items.FirstOrDefault(c => c.Id == item?.Item?.Id);
            if (itemToRemove != null)
            {
                InventoryServiceProxy.Current.Remove(itemToRemove.Id);
                RefreshItems();
            }
        }


        public void UpdateItem()
        {
            if (SelectedItem?.Item == null)
            {
                return;
            }

            NotifyPropertyChanged(nameof(SelectedItem));

            Shell.Current.GoToAsync($"//EditItem?itemId={SelectedItem.Item.Id}");
            InventoryServiceProxy.Current.AddOrUpdate(SelectedItem.Item);
        }
    }
}