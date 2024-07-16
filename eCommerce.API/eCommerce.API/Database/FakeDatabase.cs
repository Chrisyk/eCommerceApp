using eCommerce.Library.Models;

namespace eCommerce.API.Database
{
    public static class FakeDatabase
    {
        public static IEnumerable<Item> Items { get; } = new List<Item> {
                new Item{Name="Banana", Id=1, Description="Fruity", Price=10, Stock=200, B1G1F=true}
            }.Take(100);
    }
}
