using eCommerce.API.Database;
using eCommerce.Library.Models;
namespace eCommerce.API.EC
{
    public class InventoryEC
    {
        private IEnumerable<Item> Items = new List<Item>();
        public InventoryEC()
        {
        }

        public async Task<IEnumerable<Item>> Get()
        {
            return FakeDatabase.Items.Take(100);
        }
    }
}
