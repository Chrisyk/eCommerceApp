using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Library.Utility;
using Newtonsoft.Json;
using eCommerce.Library.Models;

namespace eCommerce.Library.Service
{
    public class InventoryServiceProxy
    {
        private static InventoryServiceProxy? instance;
        private static object instanceLock = new object();

        private List<Item> items;

        public ReadOnlyCollection<Item> Items
        {
            get
            {
                return items.AsReadOnly();
            }
        }

        private InventoryServiceProxy()
        {
            items = new List<Item> {
                new Item{Name="Banana", Id=1, Description="Fruity", Price=10, Stock=200, B1G1F=true}
            };
            var response = new WebRequestHandler().Get("/Inventory").Result;
            items = JsonConvert.DeserializeObject<List<Item>>(response);
        }

        private int NextId
        {
            get
            {
                if (!items.Any()) return 1;

                return items.Select(x => x.Id).Max() + 1;
            }

        }

        public Item AddOrUpdate(Item item)
        {
            bool isAdd = false;
            if (item.Id == 0)
            {
                isAdd = true;
                item.Id = NextId;
            }
            if (isAdd)
            {
                items.Add(item);
            }
            return item;
        }

        public void Remove(int id)
        {
            if (items == null)
            {
                return;
            }
            var itemToDelete = items.FirstOrDefault(c => c.Id == id);
            if (itemToDelete != null)
            {
                while (id < items.Count)
                {
                    items[id].Id = id;
                    id++;
                }
                items.Remove(itemToDelete);
            }
        }
        public static InventoryServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new InventoryServiceProxy();
                    }
                }
                return instance;
            }
        }
    }
}
