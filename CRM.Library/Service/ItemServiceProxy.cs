using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Models;

namespace CRM.Library.Service
{
    public class ItemServiceProxy
    {
        private ItemServiceProxy() { 
            items = new List<Item>();
        }
        public static ItemServiceProxy? instance;
        private static object instanceLock = new object();
        public static ItemServiceProxy Current
        {
            get
            {

                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ItemServiceProxy();
                    }
                }
                return instance;
            }
                
        }

        private List<Item>? items;
        public List<Item>? Items
        {
            get
            {
                return items;
            }
        }
        //======== functionality
        public int LastId
        {
            get
            {
                if (items?.Any() ?? false)
                {
                    return items?.Select(c => c.Id)?.Max() ?? 0;
                }
                return 0;
            }
        }
        public void Add(Item item)
        {
            if (items == null)
            {
                return;
            }
            items.Add(item);
            
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
    }
}
