using eCommerce.Library.Models;

namespace eCommerce.API.Database
{
    public static class FakeDatabase
    {
        public static int NextProductId
        {
            get
            {
                if (!Items.Any())
                {
                    return 1;
                }

                return Items.Select(p => p.Id).Max() + 1;
            }
        }

        public static List<Item> Items { get; } = new List<Item> {
                new Item{Name="Banana", Id=1, Description="Fruity", Price=10, Stock=200, B1G1F=true},
                new Item{Name="Apple", Id=2, Description="Fruity", Price=5, Stock=100, B1G1F=false},
            };
    }
}
