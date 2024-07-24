using eCommerce.Library.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using eCommerce.Library.DTO;   
namespace eCommerce.MAUI.ViewModels
{

    public class InventoryViewModel
    {
        public ICommand? EditCommand { get; private set; }
        public ICommand? AddCommand { get; private set; }

        public ItemDTO? Item;

        private ItemDTO? editItem;

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

        public bool? B1G1F
        {
            get => editItem?.B1G1F;
            set
            {
                if (editItem != null && editItem.B1G1F != value)
                {
                    editItem.B1G1F = value ?? false;
                }
            }
        }

        public decimal? Markdown
        {
            get => editItem?.Markdown;
            set
            {
                if (editItem != null && editItem.Markdown != value)
                {
                    editItem.Markdown = value ?? 0;
                }
            }
        }

        public decimal? NewPrice

        {
            get => Item?.NewPrice;

        }

        private void ExecuteEdit(InventoryViewModel? p)
        {
            if (p == null)
            {
                return;
            }
            Shell.Current.GoToAsync($"//EditItem?itemId={p.Item.Id}");

        }

        private void ExecuteAdd() {
            Shell.Current.GoToAsync($"//EditItem");
        }

        public void SetupCommands()
        {
            EditCommand = new Command(
               (c) => ExecuteEdit(c as InventoryViewModel));
            AddCommand = new Command(
                (c) => ExecuteAdd());

        }

        public InventoryViewModel()
        {
            Item = new ItemDTO();
            editItem = new ItemDTO();
            SetupCommands();
        }

        public InventoryViewModel(int id)
        {
            Item = InventoryServiceProxy.Current?.Items?.FirstOrDefault(c => c.Id == id);
            if (Item == null)
            {
                Item = new ItemDTO();
                editItem = new ItemDTO();
            } else
            {
                editItem = new ItemDTO
                {
                    Id = Item.Id,
                    Name = Item.Name,
                    Description = Item.Description,
                    Price = Item.Price,
                    Stock = Item.Stock,
                    B1G1F = Item.B1G1F,
                    Markdown = Item.Markdown,
                };
            }
            SetupCommands();
        }

        public InventoryViewModel(ItemDTO item)
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

        public async void SaveChanges()
        {
            if (Item != null)
            {
                Item.Id = editItem.Id;
                Item.Name = editItem.Name;
                Item.Description = editItem.Description;
                Item.Price = editItem.Price;
                Item.Stock = editItem.Stock;
                Item.B1G1F = editItem.B1G1F;
                Item.Markdown = editItem.Markdown;
                Item = await InventoryServiceProxy.Current.AddOrUpdate(Item);

            }
        }

    }
}
