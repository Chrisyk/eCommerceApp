using eCommerce.API.EC;
using eCommerce.Library.Models;
using eCommerce.Library.DTO;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;

        public InventoryController(ILogger<InventoryController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IEnumerable<ItemDTO>> Get()
        {
            return await new InventoryEC().Get();
        }

        [HttpDelete("/{id}")]
        public async Task<ItemDTO?> Delete(int id)
        {
            return await new InventoryEC().Delete(id);
        }

        [HttpPost()]
        public async Task<ItemDTO> AddOrUpdate([FromBody] ItemDTO item)
        {
            return await new InventoryEC().AddOrUpdate(item);
        }

        [HttpPost("Search")]
        public async Task<IEnumerable<ItemDTO>> Get(Query query)
        {
            return await new InventoryEC().Search(query.QueryString);
        }

        [HttpPost("Import")]
        public async Task<IEnumerable<ItemDTO>> Import([FromBody] IEnumerable<Item> items)
        {
            return await new InventoryEC().Import(items);
        }
    }
}
