using CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Library.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public List<Item> Contents { get; set; }

        public ShoppingCart()
        {
            Contents = new List<Item>();
        }
    }
}
