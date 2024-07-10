using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.MAUI.ViewModels
{
    public class CompositeViewModel
    {
        public InventoryManagementViewModel InventoryViewModel { get; set; }
        public ShopManagementViewModel ShopViewModel { get; set; }

        public CompositeViewModel(int id)
        {
            InventoryViewModel = new InventoryManagementViewModel();
            ShopViewModel = new ShopManagementViewModel(id);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
