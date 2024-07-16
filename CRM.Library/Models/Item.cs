using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Library.Models
{
    public class Item
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

        public Item()
        {
            Price = 0;
            Markdown = 0;
            B1G1F = false;

        }

        public override string ToString()
        {
            return $"[{Id}] {Name} (amt. {Stock}) - ${Price} [{Description}]";
        }
    }
}
