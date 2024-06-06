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
    internal class ShoppingCartService
    {
        private static ShoppingCartService? instance;
        private static object instanceLock = new object();

        public ReadOnlyCollection<ShoppingCart>? carts;

        public ShoppingCart Cart
        {
            get
            {
                if (carts == null || !carts.Any())
                {
                    return new ShoppingCart();
                }
                return carts?.FirstOrDefault() ?? new ShoppingCart();
            }
        }

        private ShoppingCartService() { }

        public static ShoppingCartService Current
        {
            get
            {
                lock (instanceLock) 
                {
                    if (instance == null)
                    {
                        instance = new ShoppingCartService();

                    }
                }
                return instance;
            }
        
        }

        public void AddToCart(Item newItem)
        {
            if (Cart == null || Cart.Contents == null)
            {
                return;
            }

            var existingItem = Cart?.Contents?.FirstOrDefault(existingItems => existingItems.Id == newItem.Id);

            var inventoryItem = InventoryServiceProxy.Current.Items.FirstOrDefault(product => product.Id == newItem.Id);

            if (existingItem == null || inventoryItem == null)
            {
                return;
            }

            inventoryItem.Stock -= newItem.Stock;

            if (existingItem != null)
            {
                existingItem.Stock += newItem.Stock;
            }
            else
            {
                Cart.Contents.Add(newItem);
            }
        }
    }
}
