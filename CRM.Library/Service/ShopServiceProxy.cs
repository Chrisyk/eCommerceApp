using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Library.Models;
using CRM.Models;

namespace CRM.Library.Service
{
    public class ShopServiceProxy
    {
        private static ShopServiceProxy? instance;
        private static object instanceLock = new object();
        private List<Item> items;
        public ReadOnlyCollection<Item> Items
        {
            get
            {
                return items.AsReadOnly();
            }
        }

        private ShopServiceProxy() {

            items = new List<Item> {
                new Item{Name="Banana", Id=1, Description="Fruity", Price=10, Stock=2, B1G1F=false}
            };
            Tax = 0.7m;
        }

        public decimal Tax { get; set; }

        public decimal TotalPrice()
        {
            decimal total = 0;
            for (int i = 0; i < items.Count; i++)
            {
                total += items[i].Price;
            }
            return total;
        }

        public static ShopServiceProxy Current
        {
            get
            {
                lock (instanceLock) 
                {
                    if (instance == null)
                    {
                        instance = new ShopServiceProxy();

                    }
                }
                return instance;
            }
        
        }

        public void RemoveFromCart(Item item)
        {
            var inventoryItem = InventoryServiceProxy.Current.Items.FirstOrDefault(product => product.Id == item.Id);

            if (inventoryItem == null)
            {
                return;
            }
            lock (instanceLock)
            {
                inventoryItem.Stock += 1;
                items.Remove(item);
            }
        }

        public void AddToCart(Item newItem)
        {
            var inventoryItem = InventoryServiceProxy.Current.Items.FirstOrDefault(product => product.Id == newItem.Id);

            if (inventoryItem == null)
            {
                return;
            }
            lock (instanceLock)
            {
                if (inventoryItem.Stock > 0)
                {
                    inventoryItem.Stock -= 1;
                    items.Add(newItem);
                    if (inventoryItem.B1G1F)
                    {
                        Item newItemFree = new Item
                        {
                            Id = newItem.Id,
                            Name = newItem.Name,
                            Description = newItem.Description,
                            Price = newItem.Price,
                            Stock = newItem.Stock,
                            B1G1F = newItem.B1G1F,
                        };
                        items.Add(newItemFree);
                    }
                }
            }
        }

    }
}
