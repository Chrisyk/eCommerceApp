using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Library.Models;

namespace eCommerce.Library.DTO
{
    public class ItemDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public bool B1G1F { get; set; }
        public decimal Markdown { get; set; }

        public decimal NewPrice
        {
            get
            {
                if (Price - Markdown < 0)
                {
                    return 0;
                }
                else
                {
                    return Price - Markdown;
                }
            }

        }

        public ItemDTO(Item item)
        {

            Id = item.Id;
            Name = item.Name;
            Description = item.Description;
            Price = item.Price;
            Stock = item.Stock;
            B1G1F = item.B1G1F;
            Markdown = item.Markdown;
        }

        public ItemDTO()
        {
            Price = 0;
            Markdown = 0;
            B1G1F = false;

        }
    }
}
