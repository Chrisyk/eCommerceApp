using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Library.DTO;

namespace eCommerce.Library.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public List<ItemDTO> Contents { get; set; }

        public ShoppingCart()
        {
            Contents = new List<ItemDTO>();
        }
    }
}
