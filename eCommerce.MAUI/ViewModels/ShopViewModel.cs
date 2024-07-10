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

        public bool? B1G1F {
            get => Item?.B1G1F;
        }

        public decimal? Markdown 
        
        {
            get => Item?.Markdown;
        
        }

        public decimal? NewPrice

        {
            get => Item?.NewPrice;

        }


        public void SetupCommands()
        {

        }

        public ShopViewModel()
        {
            Item = new Item();
            SetupCommands();
        }

        public ShopViewModel(Item item)
        {
            Item = item;
            SetupCommands();
        }

    }
}