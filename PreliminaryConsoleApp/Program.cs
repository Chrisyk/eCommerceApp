using CRM.Library.Service;
using CRM.Models;

namespace CRM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string response = "";
            var itemSvc = ItemServiceProxy.Current;
            var cart = new Dictionary<Item, int>();
            var checkedOut = false;

            Item item1 = new Item
            {
                Name = "Banana",
                Description = "Fruit",
                Price = 10,
                Stock = 20,
                Id = 1
            };
            Item item2 = new Item
            {
                Name = "Apple",
                Description = "Fruit",
                Price = 2,
                Stock = 2,
                Id = 2
            };
            itemSvc.Items.Add(item1);
            itemSvc.Items.Add(item2);

            while (!checkedOut)
            {
                Console.WriteLine("1. Inventory\n2. Shop");
                response = Console.ReadLine();
                switch (response)
                {
                    case "Inventory":
                        while (response != "Exit")
                        {
                            string selected, description;
                            int id, price, stock;
                            
                            Console.WriteLine("1. Add\n2. Update\n3. Read\n4. Delete\n5. Exit");
                            response = Console.ReadLine();
                            switch(response){
                                case "Add":
                                    Console.WriteLine("Write the name of the item you wish to add: ");
                                    selected = Console.ReadLine();
                                    if (selected == "" || selected == null)
                                    {
                                        Console.WriteLine("Name can't be empty");
                                        break;
                                    }
                                    Console.WriteLine("Write the description of the item you wish to add: ");
                                    description = Console.ReadLine();
                                    if (description == "")
                                    {
                                        Console.WriteLine("Description can't be empty");
                                        break;
                                    }
                                    Console.WriteLine("Write the price of the item: ");
                                    price = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Write the stock amount: ");
                                    stock = Convert.ToInt32(Console.ReadLine());
                                    Item newItem = new Item
                                    {
                                        Name = selected,
                                        Description = description,
                                        Price = price,
                                        Stock = stock,
                                        Id = itemSvc.LastId + 1
                                    };
                                    itemSvc.Add(newItem);
                                    break;
                                case "Update":
                                    string option;
                                    Console.WriteLine("Write the id of the item you wish to edit: ");
                                    try
                                    {
                                        id = Convert.ToInt32(Console.ReadLine());
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.ToString());
                                        break;
                                    }
                                    if (itemSvc.Items[id - 1] != null)
                                    {
                                        Console.WriteLine("Write the property do you wish to edit: ");
                                        Console.WriteLine("1. Name\n2. Description\n3. Price\n4. Stock");
                                        option = Console.ReadLine();

                                        if (option == "Name")
                                        {
                                            Console.WriteLine("Enter new name: ");
                                            option = Console.ReadLine();
                                            itemSvc.Items[id - 1].Name = option;
                                        }else if (option == "Description")
                                        {
                                            Console.WriteLine("Enter new description: ");
                                            option= Console.ReadLine();
                                            itemSvc.Items[id - 1].Description = option;
                                        }else if (option == "Price")
                                        {
                                            Console.WriteLine("Enter new price");
                                            option= Console.ReadLine();
                                            itemSvc.Items[id - 1].Price = Convert.ToInt32(option);
                                        }else if (option == "Stock")
                                        {
                                            Console.WriteLine("Enter new stock");
                                            option = Console.ReadLine();
                                            itemSvc.Items[id - 1].Stock = Convert.ToInt32(option);
                                        }
                                    } else
                                    {
                                        Console.WriteLine("Item not found");
                                    }
                                    break;
                                case "Delete":
                                    Console.Write("Enter the id of the item you wish to delete: ");
                                    try
                                    {
                                        id = Convert.ToInt32(Console.ReadLine());
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.ToString());
                                        break;
                                    }
                                    if (itemSvc.Items[id - 1] != null)
                                    {
                                        itemSvc.Remove(id);
                                        Console.WriteLine("Item Removed Successfully");
                                    }
                                    break;
                                case "Read":
                                    foreach (Item item in itemSvc.Items)
                                    {
                                        Console.WriteLine(item);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case "Shop":
                        Console.WriteLine("Shop\n==============================");
                        foreach (Item item in itemSvc.Items)
                        {
                            Console.WriteLine(item);
                        }
                        Console.WriteLine("==============================");
                        while (response != "Exit" && !checkedOut)
                        {
                            int id;
                            
                            Console.WriteLine("1. Items\n2. Buy\n3. Delete\n4. Cart\n5.Checkout\n6.Exit");
                            response = Console.ReadLine();
                            switch (response)
                            {
                                case "Items":
                                    Console.WriteLine("Shop\n==============================");
                                    foreach (Item item in itemSvc.Items)
                                    {
                                        Console.WriteLine(item);
                                    }
                                    Console.WriteLine("==============================");
                                    break;
                                case "Buy":
                                    Console.WriteLine("Write the Id of the item you want to buy");
                                    try {
                                        id = Convert.ToInt32(Console.ReadLine());
                                    } catch(Exception e)
                                    {
                                        Console.WriteLine(e.ToString());
                                        break;
                                    }
                                    var targetItem = itemSvc.Items[id - 1];
                                    if (targetItem != null)
                                    {
                                        if (targetItem.Stock > 0)
                                        {
                                        targetItem.Stock--;

                                        } else
                                        {
                                            Console.WriteLine("Item out of stock");
                                            break;
                                        }

                                        if (cart.ContainsKey(targetItem))
                                        {
                                            cart[targetItem]++;
                                        }
                                        else
                                        {
                                            cart[targetItem] = 1;
                                        }
                                        Console.WriteLine("Item added successfully");

                                    } else
                                    {
                                        Console.WriteLine("Item not found");
                                    }
                                    break;
                                case "Delete":
                                    Console.WriteLine("Write the id of the item you want to delete");
                                    try
                                    {
                                        id = Convert.ToInt32(Console.ReadLine());
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.ToString());
                                        break;
                                    }
                                    targetItem = itemSvc.Items[id - 1];
                                    if (cart.ContainsKey(targetItem)){
                                        if (cart[targetItem] > 1)
                                        {
                                            cart[targetItem]--;
                                        }
                                        else
                                        {
                                            cart.Remove(targetItem);
                                        }
                                            targetItem.Stock++;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Item not found");
                                    }

                                    break;
                                case "Cart":
                                    Console.WriteLine("Cart\n==============================");
                                    if (cart.Count == 0)
                                    {
                                        Console.WriteLine("Cart is empty");
                                    }
                                    foreach (var item in cart)
                                    {
                                        Console.WriteLine($"[{item.Key.Id}] {item.Key.Name} - Quantity: {item.Value}");
                                    }
                                    break;
                                case "Checkout":
                                    double subtotal = 0;
                                    double tax = 0;
                                    foreach (var item in cart)
                                    {
                                        subtotal += item.Key.Price * item.Value;

                                    }
                                    tax = subtotal * 0.07;
                                    Console.WriteLine("Receipt\n=====================");
                                    foreach(var item in cart)
                                    {
                                        Console.WriteLine($"{item.Key.Name} (amt. {item.Value}) - ${item.Key.Price * item.Value}");
                                    }
                                    Console.WriteLine($"Subtotal: {subtotal}\nTax: {tax}\nTotal: {subtotal + tax}");
                                    checkedOut = true;
                                    break;

                            }

                        }

                        break;
                    default:
                        break;
                }
                    
             }
          
        }
    }
}