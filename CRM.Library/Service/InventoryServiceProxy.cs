using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CRM.Models;

namespace CRM.Library.Service
{
    internal class InventoryServiceProxy
    {
        private static InventoryServiceProxy? instance;
        private static object instanceLock = new object();

        private List<Item> items;

        public ReadOnlyCollection<Item> Items {
            get
            {
                return items.AsReadOnly();
            }
        }

        private int NextId
        {
            get
            {
                if(!items.Any()) return 1;

                return items.Select(x => x.Id).Max() + 1;
            }
            
        }

        public Item AddOrUpdate(Item item)
        {
            bool isAdd = false;
            if(item.Id == 0)
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
        private InventoryServiceProxy()
        {
            items = new List<Item>();
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
