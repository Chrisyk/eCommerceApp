using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Models;

namespace CRM.Library.Models
{
    public class ShoppingCart
    {
        int Id { get; set; }
        public List<Item> Contents { get; set; }

        public ShoppingCart()
        {
            Contents = new List<Item>
            {
                new Item{Name="Banana", Id=1, Description="Fruity", Price=10, Stock=2}
            };
        }

    }
}
