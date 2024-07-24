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
using eCommerce.Library.DTO;
using System.Collections;

namespace eCommerce.Library.Service
{
    public class InventoryServiceProxy
    {
        private static InventoryServiceProxy? instance;
        private static object instanceLock = new object();

        private List<ItemDTO> items;

        public ReadOnlyCollection<ItemDTO> Items
        {
            get
            {
                return items.AsReadOnly();
            }
        }

        private InventoryServiceProxy()
        {
            var response = new WebRequestHandler().Get("/Inventory").Result;
            items = JsonConvert.DeserializeObject<List<ItemDTO>>(response);
        }

        public async Task<ItemDTO> AddOrUpdate(ItemDTO item)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
            var result = await new WebRequestHandler().Post("/Inventory", item);
            return JsonConvert.DeserializeObject<ItemDTO>(result);
        }

        public async Task<IEnumerable<ItemDTO>> get()
        {

            var response = await new WebRequestHandler().Get("/Inventory");
            var deserializedResult = JsonConvert.DeserializeObject<List<ItemDTO>>(response);
            items = deserializedResult?.ToList() ?? new List<ItemDTO>();
            return items;
        }

        public async Task<ItemDTO?> Remove(int id)
        {
            var response = await new WebRequestHandler().Delete($"/{id}");
            var itemToDelete = JsonConvert.DeserializeObject<ItemDTO>(response);
            return itemToDelete;
        }
        public async Task<IEnumerable<ItemDTO>> Search(Query? query)
        {
            if (query == null || string.IsNullOrEmpty(query.QueryString))
            {
                return await get();
            }

            var result = await new WebRequestHandler().Post("/Inventory/Search", query);
            items = JsonConvert.DeserializeObject<List<ItemDTO>>(result) ?? new List<ItemDTO>();
            return items;
        }

        public async Task ImportItems(string path)
        {
            try
            {
                string jsonContent = await File.ReadAllTextAsync(path);
                var items = JsonConvert.DeserializeObject<List<Item>>(jsonContent);
                if (items != null)
                {
                    var result = await new WebRequestHandler().Post("/Inventory/Import", items);
                }
            } catch(Exception)
            {
                throw new Exception("Invalid JSON file");
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
