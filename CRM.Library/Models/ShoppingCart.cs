using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Models;

namespace CRM.Library.Models
{
    internal class ShoppingCart
    {
        int Id { get; set; }
        public List<Item>? Contents { get; set; }
    }
}
