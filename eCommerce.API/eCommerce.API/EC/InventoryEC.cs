using eCommerce.API.Database;
using eCommerce.Library.DTO;
using eCommerce.Library.Models;
using eCommerce.Library.Service;
namespace eCommerce.API.EC
{
    public class InventoryEC
    {
        private IEnumerable<Item> Items = new List<Item>();
        public InventoryEC()
        {
        }

        public async Task<IEnumerable<ItemDTO>> Get()
        {
            return Filebase.Current.Items.Take(100).Select(p => new ItemDTO(p));
        }

        public async Task<IEnumerable<ItemDTO>> Search(string? query)
        {
            return Filebase.Current.Items.Where(p =>
            (p?.Name != null && p.Name.ToUpper().Contains(query?.ToUpper() ?? string.Empty))
                ||
            (p?.Description != null && p.Description.ToUpper().Contains(query?.ToUpper() ?? string.Empty)))
                .Take(100).Select(p => new ItemDTO(p));
        }

        public async Task<IEnumerable<ItemDTO>> Import(IEnumerable<Item> items)
        {
            return items.Select(p => new ItemDTO(Filebase.Current.Import(p)));
        }

        public async Task<ItemDTO> AddOrUpdate(ItemDTO item)
        {
            
            return new ItemDTO (Filebase.Current.AddOrUpdate(new Item(item)));
        }

        public async Task<ItemDTO?> Delete(int id)
        {
            return new ItemDTO(Filebase.Current.Delete(id));

        }
    }
}
