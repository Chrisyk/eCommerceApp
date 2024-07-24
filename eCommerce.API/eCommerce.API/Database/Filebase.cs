using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Library.Models;

namespace eCommerce.API.Database
{
    public class Filebase
    {
        private string _root;

        private static Filebase _instance;


        public static Filebase Current
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new Filebase();
                }

                return _instance;
            }
        }

        private Filebase()
        {
            _root = @"C:\temp\Items";
        }

        public int NextProductId
        {
            get
            {
                if (!Items.Any())
                {
                    return 1;
                }

                return Items.Select(p => p.Id).Max() + 1;
            }
        }

        public Item AddOrUpdate(Item item)
        {
            //set up a new Id if one doesn't already exist
            if(item.Id <= 0)
            {
                item.Id = NextProductId;
            }

            //go to the right place
            string path = $"{_root}\\{item.Id}.json";

            //if the item has been previously persisted
            if(File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }

            //write the file
            File.WriteAllText(path, JsonConvert.SerializeObject(item));

            //return the item, which now has an id
            return item;
        }

        public Item Import(Item item)
        {
            //set up a new Id for the imported item so it doesn't replace anything
            item.Id = NextProductId;

            //go to the right place
            string path = $"{_root}\\{item.Id}.json";

            //if the item has been previously persisted
            if (File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }

            //write the file
            File.WriteAllText(path, JsonConvert.SerializeObject(item));

            //return the item, which now has an id
            return item;
        }


        public List<Item> Items
        {
            get
            {
                var root = new DirectoryInfo(_root);
                var items = new List<Item>();
                foreach (var appFile in root.GetFiles())
                {
                    var item = JsonConvert.DeserializeObject<Item>(File.ReadAllText(appFile.FullName));
                    if (item != null)
                    {
                        items.Add(item);
                    }
                }
                return items;
            }
        }

        public Item Delete(int id)
        {
            //TODO: refer to AddOrUpdate for an idea of how you can implement this.
            //go to the right place
            string path = $"{_root}\\{id}.json";
            Item? item = null;

            //if the item has been previously persisted
            if (File.Exists(path))
            {
                //blow it up
                item = Items.FirstOrDefault(i => i.Id == id);
                File.Delete(path);
            }

            return item;

        }
    }


    // ------------------- FAKE MODEL FILES, REPLACE THESE WITH A REFERENCE TO YOUR MODELS -------- //

}
