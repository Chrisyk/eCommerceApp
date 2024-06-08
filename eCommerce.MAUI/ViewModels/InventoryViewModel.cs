using CRM.Library.Service;
using CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Item = CRM.Models.Item;

namespace eCommerce.MAUI.ViewModels
{

    public class InventoryViewModel
    {
        public ICommand? EditCommand { get; private set; }

        public Item? Item;

        private Item? editItem;

        public int? Id
        {
            get => editItem?.Id;
            set
            {
                if (editItem != null && editItem.Id != value)
                {
                    editItem.Price = value ?? 0;
                }
            }
        }

        public string? Name
        {
            get
            {
                return editItem?.Name;
            }
            set
            {
                if (editItem != null)
                {
                    editItem.Name = value;
                }
            }
        }

        public string? Description
        {
            get => editItem?.Description;
            set
            {
                if (editItem != null && editItem.Description != value)
                {
                    editItem.Description = value;
                }
            }
        }

        public decimal? Price
        {
            get => editItem?.Price;
            set
            {
                if (editItem != null && editItem.Price != value)
                {
                    editItem.Price = value ?? 0;
                }
            }
        }

        public int? Stock
        {
            get => editItem?.Stock;
            set
            {
                if (editItem != null && editItem.Stock != value)
                {
                    editItem.Stock = value ?? 0;
                }
            }
        }

        private void ExecuteEdit(InventoryViewModel? p)
        {
            if (p == null)
            {
                return;
            }
            Shell.Current.GoToAsync($"//EditItem?itemId={p.Item.Id}");

        }

        public void Add()
        {
            InventoryServiceProxy.Current.AddOrUpdate(Item);
        }

        public void SetupCommands()
        {
            EditCommand = new Command(
               (c) => ExecuteEdit(c as InventoryViewModel));
        }

        public InventoryViewModel()
        {
            Item = new Item();
            editItem = new Item();
            SetupCommands();
        }

        public InventoryViewModel(int id)
        {
            Item = InventoryServiceProxy.Current?.Items?.FirstOrDefault(c => c.Id == id);
            if (Item == null)
            {
                Item = new Item();
                editItem = new Item();
            } else
            {
                editItem = new Item
                {
                    Id = Item.Id,
                    Name = Item.Name,
                    Description = Item.Description,
                    Price = Item.Price,
                    Stock = Item.Stock,
                };
            }
            SetupCommands();
        }

        public InventoryViewModel(Item item)
        {
            Item = item;
            editItem = item;
            SetupCommands();
        }

        public string? Display
        {
            get
            {
                return ToString();
            }
        }

        public void SaveChanges()
        {
            if (Item != null)
            {
                Item.Id = editItem.Id;
                Item.Name = editItem.Name;
                Item.Description = editItem.Description;
                Item.Price = editItem.Price;
                Item.Stock = editItem.Stock;
                Add();
            }
        }

    }
}
