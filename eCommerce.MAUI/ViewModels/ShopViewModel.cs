using CRM.Library.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Item = CRM.Models.Item;

namespace eCommerce.MAUI.ViewModels
{
    public class ShopViewModel
    {
        public ICommand? AddCommand { get; private set; }

        public Item? Item;

        public int? Id
        {
            get => Item?.Id;

        }

        public string? Name
        {
            get
            {
                return Item?.Name;
            }

        }

        public string? Description
        {
            get => Item?.Description;

        }

        public decimal? Price
        {
            get => Item?.Price;

        }

        public int? Stock
        {
            get => Item?.Stock;
 
        }

        public void ExecuteAdd(Item item)
        {
            if (item == null)
            {
                return;
            }

            ShopServiceProxy.Current.AddToCart(item);
        }

        public void SetupCommands()
        {
            AddCommand = new Command(
                (c) => ExecuteAdd(c as Item));

        }

        public ShopViewModel()
        {
            Item = new Item();
            SetupCommands();
        }

        public ShopViewModel(int id)
        {
            Item = ShopServiceProxy.Current?.Items?.FirstOrDefault(c => c.Id == id);
            if (Item == null)
            {
                Item = new Item();
            }
            else

            SetupCommands();
        }

        public ShopViewModel(Item item)
        {
            Item = item;
            SetupCommands();
        }

    }
}
