using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public Item()
        {
        }

        public override string ToString()
        {
            return $"[{Id}] {Name} (amt. {Stock}) - ${Price} [{Description}]";
        }
    }
}
