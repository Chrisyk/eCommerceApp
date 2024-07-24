using eCommerce.Library.Models;
using eCommerce.Library.Service;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using eCommerce.Library.DTO;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System;
using System.Threading.Tasks;

namespace eCommerce.MAUI.ViewModels
{
    public class InventoryManagementViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string? Query { get; set; }

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

        public async void RefreshItems()
        {
            await InventoryServiceProxy.Current.Search(new Query(Query));
            NotifyPropertyChanged("Items");
        }

        public async void RemoveItem(InventoryViewModel item)
        {
            if (item == null)
            {
                return;
            }

            var itemToRemove = InventoryServiceProxy.Current.Items.FirstOrDefault(c => c.Id == item?.Item?.Id);
            if (itemToRemove != null)
            {
                await InventoryServiceProxy.Current.Remove(itemToRemove?.Id ?? 0);
                RefreshItems();
            }
        }

        public async void ImportItems()
        {

            var result = await FilePicker.Default.PickAsync();
            if (result != null)
            {
                if (result.FileName.EndsWith("json", StringComparison.OrdinalIgnoreCase))
                {
                    await InventoryServiceProxy.Current.ImportItems(result.FullPath);
                }
            }
            RefreshItems();
        }

        public async void Search()
        {
            await InventoryServiceProxy.Current.Search(new Query(Query));
            RefreshItems(); 
        }


        //public void UpdateItem()
        //{
        //    if (SelectedItem?.Item == null)
        //    {
        //        return;
        //    }

        //    NotifyPropertyChanged(nameof(SelectedItem));

        //    Shell.Current.GoToAsync($"//EditItem?itemId={SelectedItem.Item.Id}");
        //}
    }
}