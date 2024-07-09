using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class Item
    {
        private decimal _price;
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price
        {
            get
            {
                if (_price - Markdown >= 0)
                {
                    return _price - Markdown;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (value >= 0)
                {
                    _price = value;
                }
                else
                {
                    _price = 0;
                }
            }
        }
        public int Stock { get; set; }
        public bool B1G1F { get; set; }
        public decimal Markdown { get; set; }


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
