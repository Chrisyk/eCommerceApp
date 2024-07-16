using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Library.Models;

namespace eCommerce.Library.Service
{
    public class ShopServiceProxy
    {
        private static ShopServiceProxy? instance;
        private static object instanceLock = new object();
        private List<ShoppingCart> carts;
        public ReadOnlyCollection<ShoppingCart> Carts
        {
            get
            {
                return carts.AsReadOnly();
            }
        }

        private ShopServiceProxy()
        {

            carts = new List<ShoppingCart>
            {
               new ShoppingCart
               {
                   Id = 0,
                   Contents = new List<Item>(),
               }
            };
            Tax = 0.7m;
        }

        public decimal Tax { get; set; }

        public decimal TotalPrice(int id)
        {
            decimal total = 0;
            for (int i = 0; i < Current.Carts.FirstOrDefault(c => c.Id == id)?.Contents?.Count; i++)
            {
                total += Current.Carts.FirstOrDefault(c => c.Id == id).Contents[i].NewPrice;
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

        public void RemoveFromCart(Item item, int id)
        {
            var inventoryItem = InventoryServiceProxy.Current.Items.FirstOrDefault(product => product.Id == item.Id);

            if (inventoryItem == null)
            {
                return;
            }
            lock (instanceLock)
            {
                inventoryItem.Stock += 1;
                Current.Carts.FirstOrDefault(c => c.Id == id)?.Contents?.Remove(item);
            }
        }

        public void RemoveCart(ShoppingCart cart)
        {
            lock (instanceLock)
            {
                if (carts.Contains(cart))
                {
                    carts.Remove(cart);

                }
            }
        }

        public int getLatestCartId()
        {
            lock (instanceLock)
            {
                if (carts.Count == 0)
                {
                    return 0;
                }
                int maxId = carts.Max(cart => cart.Id);
                return maxId;
            }
        }

        public void AddCart()
        {
            lock (instanceLock)
            {
                ShoppingCart newCart = new ShoppingCart();
                newCart.Id = getLatestCartId() + 1;
                carts.Add(newCart);
            }
        }

        public void AddToCart(Item newItem, int id)
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
                    Current.Carts.FirstOrDefault(c => c.Id == id)?.Contents?.Add(newItem);
                    if (inventoryItem.B1G1F)
                    {
                        inventoryItem.Stock -= 1;
                        Item newItemFree = new Item
                        {
                            Id = newItem.Id,
                            Name = newItem.Name,
                            Description = newItem.Description,
                            Price = 0,
                            Stock = newItem.Stock,
                            B1G1F = newItem.B1G1F,
                        };
                        Current.Carts.FirstOrDefault(c => c.Id == id)?.Contents?.Add(newItemFree);
                    }
                }
            }
        }

    }
}
